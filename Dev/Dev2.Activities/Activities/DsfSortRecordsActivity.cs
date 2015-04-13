
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
using Dev2;
using Dev2.Activities;
using Dev2.Activities.Debug;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.DataList.Contract;
using Dev2.Diagnostics;
using Dev2.Util;
using Unlimited.Applications.BusinessDesignStudio.Activities.Utilities;
using Warewolf.Storage;

// ReSharper disable CheckNamespace
namespace Unlimited.Applications.BusinessDesignStudio.Activities
// ReSharper restore CheckNamespace
{
    public class DsfSortRecordsActivity : DsfActivityAbstract<string>
    {

        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        [Inputs("SortField")]
        [FindMissing]
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets the selected sort.
        /// </summary>
        [Inputs("SelectedSort")]
        // ReSharper disable MemberCanBePrivate.Global
        public string SelectedSort { get; set; }
        // ReSharper restore MemberCanBePrivate.Global

        public DsfSortRecordsActivity()
            : base("Sort Records")
        {
            SortField = string.Empty;
            SelectedSort = "Forward";
            DisplayName = "Sort Records";
        }

        // ReSharper disable RedundantOverridenMember
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
        }
        // ReSharper restore RedundantOverridenMember


        protected override void OnExecute(NativeActivityContext context)
        {
            _debugInputs = new List<DebugItem>();
            _debugOutputs = new List<DebugItem>();

            IDSFDataObject dataObject = context.GetExtension<IDSFDataObject>();

            ErrorResultTO allErrors = new ErrorResultTO();

            InitializeDebug(dataObject);

            try
            {
                bool descOrder = String.IsNullOrEmpty(SelectedSort) || SelectedSort.Equals("Backwards");
                if (dataObject.IsDebugMode())
                {
                    AddDebugInputItem(SortField, "Sort Field", dataObject.Environment);
                }
                if (!string.IsNullOrEmpty(SortField))
                {
                    dataObject.Environment.SortRecordSet(SortField, descOrder);
                }
                else
                {
                    allErrors.AddError("No recordset given");
                }
            }
            finally
            {

                if (allErrors.HasErrors())
                {
                    DisplayAndWriteError("DsfSortRecordsActivity", allErrors);
                    foreach (var error in allErrors.FetchErrors())
                    {
                        dataObject.Environment.AddError(error);
                    }
                }
                if (dataObject.IsDebugMode())
                {
                    DebugOutputs(dataObject);

                    DispatchDebugState(context, StateType.Before);
                    DispatchDebugState(context, StateType.After);
                }
            }
        }

        void DebugOutputs(IDSFDataObject dataObject)
        {
            if(dataObject.IsDebugMode())
            {
                var data = dataObject.Environment.Eval(dataObject.Environment.ToStar(SortField));
                if(data.IsWarewolfAtomListresult)
                {
                    var lst = data as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;
                    AddDebugOutputItem(new DebugItemWarewolfAtomListResult(lst, "", "", SortField, "", "", "="));
                }
                else if (data.IsWarewolfAtomResult)
                {
                    var atomData = data as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;
                    if (atomData != null && atomData.Item.IsNothing)
                    {
                        AddDebugOutputItem(new DebugItemStaticDataParams("", SortField, "", "="));
                    }
                }
            }
        }

        #region Private Methods

        private void AddDebugInputItem(string expression, string labelText, IExecutionEnvironment env)
        {
            var data =  env.Eval(env.ToStar( expression));
            if (data.IsWarewolfAtomListresult)
            {
                var lst = data as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;
                AddDebugInputItem(new DebugItemWarewolfAtomListResult(lst,"","",expression, labelText,"","="));
                AddDebugInputItem(new DebugItemStaticDataParams(SelectedSort, "Sort Order"));
            }
            else if (data.IsWarewolfAtomResult)
            {
                var atomData = data as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;
                if (atomData != null && atomData.Item.IsNothing)
                {
                    AddDebugInputItem(new DebugItemStaticDataParams("", expression, labelText, "="));
                    AddDebugInputItem(new DebugItemStaticDataParams(SelectedSort, "Sort Order"));
                }
            }
        }

        #endregion Private Methods

        public override void UpdateForEachInputs(IList<Tuple<string, string>> updates, NativeActivityContext context)
        {
            if(updates != null)
            {
                foreach(Tuple<string, string> t in updates)
                {

                    if(t.Item1 == SortField)
                    {
                        SortField = t.Item2;
                    }
                }
            }
        }

        public override void UpdateForEachOutputs(IList<Tuple<string, string>> updates, NativeActivityContext context)
        {
            if(updates != null)
            {
                foreach(Tuple<string, string> t in updates)
                {

                    if(t.Item1 == SortField)
                    {
                        SortField = t.Item2;
                    }
                }
            }
        }

        #region GetForEachInputs/Outputs

        public override IList<DsfForEachItem> GetForEachInputs()
        {
            return GetForEachItems(SortField);
        }

        public override IList<DsfForEachItem> GetForEachOutputs()
        {
            return GetForEachItems(SortField);
        }

        #endregion


        #region GetDebugInputs/Outputs

        #region GetDebugInputs

        public override List<DebugItem> GetDebugInputs(IExecutionEnvironment env)
        {
            return _debugInputs;
        }


        public override List<DebugItem> GetDebugOutputs(IExecutionEnvironment env)
        {
            return _debugOutputs;
        }

        #endregion

        #endregion

    }
}
