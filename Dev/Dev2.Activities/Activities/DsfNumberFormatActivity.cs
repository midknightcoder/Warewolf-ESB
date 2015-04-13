
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2014 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Dev2;
using Dev2.Activities;
using Dev2.Activities.Debug;
using Dev2.Common;
using Dev2.Common.ExtMethods;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Data.Operations;
using Dev2.Data.TO;
using Dev2.DataList.Contract;
using Dev2.Diagnostics;
using Dev2.Util;
using Dev2.Validation;
using Unlimited.Applications.BusinessDesignStudio.Activities.Utilities;
using Warewolf.Storage;

// ReSharper disable CheckNamespace
namespace Unlimited.Applications.BusinessDesignStudio.Activities
// ReSharper restore CheckNamespace
{

    public class DsfNumberFormatActivity : DsfActivityAbstract<string>
    {
        #region Class Members

        // ReSharper disable InconsistentNaming
        private static readonly IDev2NumberFormatter _numberFormatter; //  REVIEW : Should this not be an instance variable....
        // ReSharper restore InconsistentNaming

        #endregion Class Members

        #region Constructors

        public DsfNumberFormatActivity()
            : base("Format Number")
        {
            RoundingType = "None";
        }

        static DsfNumberFormatActivity()
        {
            _numberFormatter = new Dev2NumberFormatter(); // REVIEW : Please use a factory method to create

        }
        #endregion Constructors

        #region Properties

        [Inputs("Expression")]
        [FindMissing]
        public string Expression { get; set; }

        [Inputs("RoundingType")]
        public string RoundingType { get; set; }

        [Inputs("RoundingDecimalPlaces")]
        [FindMissing]
        public string RoundingDecimalPlaces { get; set; }

        [Inputs("DecimalPlacesToShow")]
        [FindMissing]
        public string DecimalPlacesToShow { get; set; }

        [Outputs("Result")]
        [FindMissing]
        public new string Result { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        #endregion Properties

        #region Override Methods

        protected override void OnExecute(NativeActivityContext context)
        {
            _debugInputs = new List<DebugItem>();
            _debugOutputs = new List<DebugItem>();
            var dataObject = context.GetExtension<IDSFDataObject>();

            var allErrors = new ErrorResultTO();

            InitializeDebug(dataObject);
            try
            {
                var expression = Expression ?? string.Empty;
                var roundingDecimalPlaces = RoundingDecimalPlaces ?? string.Empty;
                var decimalPlacesToShow = DecimalPlacesToShow ?? string.Empty;
               
                if(dataObject.IsDebugMode())
                {
                    AddDebugInputItem(expression, "Number",  dataObject.Environment);
                    if(!String.IsNullOrEmpty(RoundingType))
                    {
                        AddDebugInputItem(new DebugItemStaticDataParams(RoundingType, "Rounding"));
                    }
                    AddDebugInputItem(roundingDecimalPlaces, "Rounding Value", dataObject.Environment);
                    AddDebugInputItem(decimalPlacesToShow, "Decimals to show", dataObject.Environment);
                }
                var colItr = new WarewolfListIterator();
                var expressionIterator = CreateDataListEvaluateIterator(expression, dataObject.Environment);
                var roundingDecimalPlacesIterator = CreateDataListEvaluateIterator(roundingDecimalPlaces, dataObject.Environment);
                var decimalPlacesToShowIterator = CreateDataListEvaluateIterator(decimalPlacesToShow, dataObject.Environment);
                colItr.AddVariableToIterateOn(expressionIterator);
                colItr.AddVariableToIterateOn(roundingDecimalPlacesIterator);
                colItr.AddVariableToIterateOn(decimalPlacesToShowIterator);
                // Loop data ;)
                var rule = new IsSingleValueRule(() => Result);
                var single = rule.Check();

                while(colItr.HasMoreData())
                {
                    int decimalPlacesToShowValue;
                    var tmpDecimalPlacesToShow = colItr.FetchNextValue(decimalPlacesToShowIterator);
                    var adjustDecimalPlaces = tmpDecimalPlacesToShow.IsRealNumber(out decimalPlacesToShowValue);
                    if(!string.IsNullOrEmpty(tmpDecimalPlacesToShow) && !adjustDecimalPlaces)
                    {
                        throw new Exception("Decimals to show is not valid");
                    }


                    var tmpDecimalPlaces = colItr.FetchNextValue(roundingDecimalPlacesIterator);
                    var roundingDecimalPlacesValue = 0;
                    if(!string.IsNullOrEmpty(tmpDecimalPlaces) && !tmpDecimalPlaces.IsRealNumber(out roundingDecimalPlacesValue))
                    {
                        throw new Exception("Rounding decimal places is not valid");
                    }

                    var binaryDataListItem = colItr.FetchNextValue(expressionIterator);
                    var val = binaryDataListItem;
                    FormatNumberTO formatNumberTo = new FormatNumberTO(val, RoundingType, roundingDecimalPlacesValue, adjustDecimalPlaces, decimalPlacesToShowValue);
                    var result = _numberFormatter.Format(formatNumberTo);

                    if(single != null)
                    {
                        allErrors.AddError(single.Message);
                    }
                    else{
                        UpdateResultRegions(dataObject.Environment, result);
                        if(dataObject.IsDebugMode())
                {
                    AddDebugOutputItem(new DebugItemStaticDataParams(result,Result,"","="));
                }
                    }
                }
                
            }
            catch(Exception e)
            {
                Dev2Logger.Log.Error("DSFNumberFormatActivity", e);
                allErrors.AddError(e.Message);
            }
            finally
            {
                if(allErrors.HasErrors())
                {
                    if(dataObject.IsDebugMode())
                    {
                        AddDebugOutputItem(new DebugItemStaticDataParams("", Result, ""));
                    }
                    DisplayAndWriteError("DsfNumberFormatActivity", allErrors);
                    var errorString = allErrors.MakeDisplayReady();
                    dataObject.Environment.AddError(errorString);
                }

                if(dataObject.IsDebugMode())
                {
                    DispatchDebugState(context, StateType.Before);
                    DispatchDebugState(context, StateType.After);
                }
            }
        }

        void UpdateResultRegions(IExecutionEnvironment environment, string result)
        {
            environment.Assign(Result, result);
        }

        #endregion

        #region Private Methods

        private void AddDebugInputItem(string expression, string labelText, IExecutionEnvironment environment)
        {
            DebugItem itemToAdd = new DebugItem();
            if (environment != null)
            {
                AddDebugItem(new DebugEvalResult(expression, labelText, environment), itemToAdd);
            }
            else
            {
                AddDebugItem(new DebugItemStaticDataParams("", labelText, expression), itemToAdd);
            }

            _debugInputs.Add(itemToAdd);
        }

        #endregion

        #region Get Debug Inputs/Outputs

        public override List<DebugItem> GetDebugInputs(IExecutionEnvironment dataList)
        {
            foreach(IDebugItem debugInput in _debugInputs)
            {
                debugInput.FlushStringBuilder();
            }
            return _debugInputs;
        }

        public override List<DebugItem> GetDebugOutputs(IExecutionEnvironment dataList)
        {
            foreach(IDebugItem debugOutput in _debugOutputs)
            {
                debugOutput.FlushStringBuilder();
            }
            return _debugOutputs;
        }

        #endregion

        #region Update ForEach Inputs/Outputs

        public override void UpdateForEachInputs(IList<Tuple<string, string>> updates, NativeActivityContext context)
        {
            if(updates != null)
            {
                foreach(Tuple<string, string> t in updates)
                {

                    if(t.Item1 == Expression)
                    {
                        Expression = t.Item2;
                    }

                    if(t.Item1 == RoundingType)
                    {
                        RoundingType = t.Item2;
                    }

                    if(t.Item1 == RoundingDecimalPlaces)
                    {
                        RoundingDecimalPlaces = t.Item2;
                    }

                    if(t.Item1 == DecimalPlacesToShow)
                    {
                        DecimalPlacesToShow = t.Item2;
                    }
                }
            }
        }

        public override void UpdateForEachOutputs(IList<Tuple<string, string>> updates, NativeActivityContext context)
        {
            if(updates != null)
            {
                var itemUpdate = updates.FirstOrDefault(tuple => tuple.Item1 == Result);
                if(itemUpdate != null)
                {
                    Result = itemUpdate.Item2;
                }
            }
        }

        #endregion

        #region GetForEachInputs/Outputs

        public override IList<DsfForEachItem> GetForEachInputs()
        {
            return GetForEachItems(Expression, RoundingType, RoundingDecimalPlaces, DecimalPlacesToShow);
        }

        public override IList<DsfForEachItem> GetForEachOutputs()
        {
            return GetForEachItems(Result);
        }

        #endregion

    }
}
