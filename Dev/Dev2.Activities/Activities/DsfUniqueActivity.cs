
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dev2.Activities.Debug;
using Dev2.Common;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.DataList.Contract;
using Dev2.Diagnostics;
using Dev2.Util;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Warewolf.Storage;

namespace Dev2.Activities
{
    public class DsfUniqueActivity : DsfActivityAbstract<string>
    {

        /// <summary>
        /// The property that holds all the convertions
        /// </summary>

        [FindMissing]
        public string InFields { get; set; }

        [FindMissing]
        public string ResultFields { get; set; }

        /// <summary>
        /// The property that holds the result string the user enters into the "Result" box
        /// </summary>
        [FindMissing]
        public new string Result { get; set; }


        #region Ctor

        public DsfUniqueActivity()
            : base("Unique Records")
        {
            InFields = string.Empty;
            ResultFields = string.Empty;
        }

        #endregion

        #region Overrides of DsfNativeActivity<string>

        // ReSharper disable RedundantOverridenMember
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
        }


        /// <summary>
        /// When overridden runs the activity's execution logic
        /// </summary>
        /// <param name="context">The context to be used.</param>
        protected override void OnExecute(NativeActivityContext context)
        {
            IDSFDataObject dataObject = context.GetExtension<IDSFDataObject>();

            ExecuteTool(dataObject, 0);
        }

        protected override void ExecuteTool(IDSFDataObject dataObject, int update)
        {

            var allErrors = new ErrorResultTO();
            InitializeDebug(dataObject);
            var toresultfields = Result.Split(',');
            var fromFields = InFields.Split(',');
            var fromResultFieldresultfields = ResultFields.Split(',');

            try
            {
                PreExecution(dataObject, fromFields, update);
                if(String.IsNullOrEmpty(InFields))
                {
                    throw new Exception("Invalid In fields");
                }
                if(String.IsNullOrEmpty(ResultFields))
                {
                    throw new Exception("Invalid from fields");
                }
                if(toresultfields.Any(a => !ExecutionEnvironment.IsValidRecordSetIndex(a)))
                {
                    throw new Exception("invalid result");
                }
                if(fromFields.Any(a => !ExecutionEnvironment.IsValidRecordSetIndex(a)))
                {
                    throw new Exception("invalid from");
                }
                if(fromResultFieldresultfields.Any(a => !ExecutionEnvironment.IsValidRecordSetIndex(a)))
                {
                    throw new Exception("invalid selected fields");
                }

                dataObject.Environment.AssignUnique(fromFields, fromResultFieldresultfields, toresultfields, update);
            }
            catch(Exception e)
            {
                Dev2Logger.Log.Error("DSFUnique", e);
                allErrors.AddError(e.Message);
            }
            finally
            {
                PostExecute(dataObject, toresultfields, allErrors.HasErrors(),update);
                // Handle Errors
                var hasErrors = allErrors.HasErrors();
                if(hasErrors)
                {
                    DisplayAndWriteError("DsfUniqueActivity", allErrors);
                    foreach(var error in allErrors.FetchErrors())
                    {
                        dataObject.Environment.AddError(error);
                    }
                }

                if(dataObject.IsDebugMode())
                {
                    DispatchDebugState(dataObject, StateType.Before, update);
                    DispatchDebugState(dataObject, StateType.After, update);
                }
            }
        }

        void PostExecute(IDSFDataObject dataObject, IEnumerable<string> toresultfields, bool hasErrors, int update)
        {
            if(dataObject.IsDebugMode())
            {
                int i = 1;
                foreach(var field in toresultfields)
                {
                    
                    if(!string.IsNullOrEmpty(field))
                    {
                        try
                        {
                            var res = new DebugEvalResult(dataObject.Environment.ToStar(field), "", dataObject.Environment, update);

                            if (!hasErrors)
                            AddDebugOutputItem(new DebugItemStaticDataParams("","",i.ToString(CultureInfo.InvariantCulture)));
                            AddDebugOutputItem(res);
                        }
                        catch(Exception)
                        {
                            AddDebugOutputItem(new DebugItemStaticDataParams("", field, ""));
                            throw;
                        }
                    }
                    i++;
                }
            }
        }

        void PreExecution(IDSFDataObject dataObject, IEnumerable<string> fromFields, int update)
        {
            if(dataObject.IsDebugMode())
            {
                AddDebugInputItem(new DebugItemStaticDataParams("", "In Field(s)"));
                foreach(var field in fromFields)
                {
                    // TODO : if EvaluateforDebug
                    if(!string.IsNullOrEmpty(field))
                    {
                        try
                        {
                            AddDebugInputItem(new DebugEvalResult( field, "", dataObject.Environment, update));
                        }
                        catch(Exception)
                        {
                            AddDebugInputItem(new DebugItemStaticDataParams("", field, ""));
                        }
                    }
                }
                AddDebugInputItem(new DebugItemStaticDataParams("",ResultFields, "Return Fields"));
            }

        }

        //static void UpdateStarNotationColumns(DebugItem itemToAdd)
        //{
        //    var groups = itemToAdd.ResultsList.Where(a => DataListUtil.IsValueRecordset(a.Variable) && String.IsNullOrEmpty(a.GroupName)).GroupBy(a => DataListUtil.ExtractRecordsetNameFromValue(a.Variable) + DataListUtil.ExtractFieldNameFromValue(a.Variable));

        //    var maxId = itemToAdd.ResultsList.Count > 0 ? itemToAdd.ResultsList.Max(a => a.GroupIndex) : 0;
        //    foreach(var g in groups)
        //    {

        //        foreach(var res in g)
        //        {
        //            maxId++;
        //            res.GroupIndex = maxId;
        //            res.GroupName = "[[" + DataListUtil.ExtractRecordsetNameFromValue(res.Variable) + "()." + DataListUtil.ExtractFieldNameFromValue(res.Variable) + "]]";
        //        }
        //    }
        //}



        public override enFindMissingType GetFindMissingType()
        {
            return enFindMissingType.StaticActivity;
        }

        public override void UpdateForEachInputs(IList<Tuple<string, string>> updates)
        {
            if(updates != null)
            {
                foreach(Tuple<string, string> t in updates)
                {

                    if(t.Item1 == InFields)
                    {
                        InFields = t.Item2;
                    }
                    if(t.Item1 == ResultFields)
                    {
                        ResultFields = t.Item2;
                    }
                }
            }
        }

        public override void UpdateForEachOutputs(IList<Tuple<string, string>> updates)
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

        #region Overrides of DsfNativeActivity<string>
        public override List<DebugItem> GetDebugInputs(IExecutionEnvironment dataList, int update)
        {
            foreach(IDebugItem debugInput in _debugInputs)
            {
                debugInput.FlushStringBuilder();
            }
            return _debugInputs;
        }

        public override List<DebugItem> GetDebugOutputs(IExecutionEnvironment dataList, int update)
        {
            foreach(IDebugItem debugOutput in _debugOutputs)
            {
                debugOutput.FlushStringBuilder();
            }
            return _debugOutputs;
        }

        #endregion

        #region Private Methods

        #endregion

        #endregion

        #region GetForEachInputs/Outputs

        public override IList<DsfForEachItem> GetForEachInputs()
        {
            return GetForEachItems(InFields, ResultFields);
        }

        public override IList<DsfForEachItem> GetForEachOutputs()
        {
            return GetForEachItems(Result);
        }

        #endregion


    }
}
