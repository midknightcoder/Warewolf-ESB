﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Dev2.Activities.Debug;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Data.Decisions.Operations;
using Dev2.Data.SystemTemplates.Models;
using Dev2.Data.Util;
using Dev2.DataList.Contract;
using Dev2.Diagnostics;
using Newtonsoft.Json;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Warewolf.Storage;

namespace Dev2.Activities
{
    public class DsfDecision:DsfActivityAbstract<string>
    {


        // ReSharper disable MemberCanBePrivate.Global
       public IEnumerable<IDev2Activity> TrueArm { get; set; }
  
       public IEnumerable<IDev2Activity> FalseArm { get; set; }
       public Dev2DecisionStack Conditions { get; set; }
       // ReSharper restore MemberCanBePrivate.Global
        readonly DsfFlowDecisionActivity _inner;    
        #region Overrides of DsfNativeActivity<string>
        public DsfDecision(DsfFlowDecisionActivity inner)
        {
            _inner = inner;
        }
 
        public DsfDecision() { }
        /// <summary>
        /// When overridden runs the activity's execution logic 
        /// </summary>
        /// <param name="context">The context to be used.</param>
        protected override void OnExecute(NativeActivityContext context)
        {
        }

        public override void UpdateForEachInputs(IList<Tuple<string, string>> updates)
        {
        }

        public override void UpdateForEachOutputs(IList<Tuple<string, string>> updates)
        {
        }

        public override IList<DsfForEachItem> GetForEachInputs()
        {
            return null;
        }

        public override IList<DsfForEachItem> GetForEachOutputs()
        {
            return null;
        }

        private  Dev2Decision ParseDecision(IExecutionEnvironment env , Dev2Decision decision)
        {
            var col1 =env.EvalAsList(decision.Col1, 0);
            var col2 = env.EvalAsList(decision.Col2, 0);
            var col3 = env.EvalAsList(decision.Col3, 0);
            return new Dev2Decision { Cols1 = col1, Cols2 = col2, Cols3 = col3, EvaluationFn = decision.EvaluationFn };
        }

        #region Overrides of DsfNativeActivity<string>

        public override IDev2Activity Execute(IDSFDataObject dataObject, int update)
        {
            ErrorResultTO allErrors = new ErrorResultTO();
            try
            {
                InitializeDebug(dataObject);
                    

                if (dataObject.IsDebugMode())
                {
                    _debugInputs = CreateDebugInputs(dataObject.Environment);
                    DispatchDebugState(dataObject, StateType.Before,0);
                }

                var stack = Conditions.TheStack.Select(a => ParseDecision(dataObject.Environment, a));


                var factory = Dev2DecisionFactory.Instance();
                var res = stack.SelectMany(a =>
                {
                    if (a.EvaluationFn == enDecisionType.IsError)
                    {
                        return new[] { dataObject.Environment.AllErrors.Count > 0 };
                    }
                    if (a.EvaluationFn == enDecisionType.IsNotError)
                    {
                        return new[] { dataObject.Environment.AllErrors.Count == 0 };
                    }
                    IList<bool> ret = new List<bool>();
                    var iter = new WarewolfListIterator();
                    var c1 = new WarewolfAtomIterator(a.Cols1);
                    var c2 = new WarewolfAtomIterator(a.Cols2);
                    var c3 = new WarewolfAtomIterator(a.Cols3);
                    iter.AddVariableToIterateOn(c1);
                    iter.AddVariableToIterateOn(c2);
                    iter.AddVariableToIterateOn(c3);
                    while (iter.HasMoreData())
                    {
                        ret.Add(factory.FetchDecisionFunction(a.EvaluationFn).Invoke(new[] { iter.FetchNextValue(c1), iter.FetchNextValue(c2), iter.FetchNextValue(c3) }));
                    }
                    return ret;

                });
                var resultval = And ? res.Aggregate(true, (a, b) => a && b) : res.Any(a => a);
                if (dataObject.IsDebugMode())
                    _debugOutputs = GetDebugOutputs(resultval.ToString());

                if (resultval)
                {
                    if (TrueArm != null)
                    {
                        var activity = TrueArm.FirstOrDefault();
                        return activity;
                        }
                    }
                else
                {
                    if (FalseArm != null)
                    {
                        var activity = FalseArm.FirstOrDefault();
                        return activity;
                    }
                }
            }
            catch (Exception e)
            {
                allErrors.AddError(e.Message);
            }
            finally
            {
                // Handle Errors
                var hasErrors = allErrors.HasErrors();
                if (hasErrors)
                {
                    DisplayAndWriteError("DsfDeleteRecordsActivity", allErrors);
                    var errorString = allErrors.MakeDisplayReady();
                    dataObject.Environment.AddError(errorString);

                }


                }
            if (dataObject.IsDebugMode())
            {
                _debugOutputs = new List<DebugItem>();
                _debugOutputs = new List<DebugItem>();
                DispatchDebugState(dataObject, StateType.Duration, update);
            }
            return null;
            }

        #endregion

        protected override void ExecuteTool(IDSFDataObject dataObject, int update)
        {
           
        }

        #region Overrides of DsfNativeActivity<string>

        public override List<DebugItem> GetDebugInputs(IExecutionEnvironment env, int update)
        {
            return _debugInputs;
        }

        #endregion

        List<DebugItem> CreateDebugInputs(IExecutionEnvironment env)
        {
            List<IDebugItem> result = new List<IDebugItem>();

            var allErrors = new ErrorResultTO();

           

            try
            {
                Dev2DecisionStack dds =Conditions;
                ErrorResultTO error;
                string userModel = dds.GenerateUserFriendlyModel(env, dds.Mode, out error);
                allErrors.MergeErrors(error);

                foreach (Dev2Decision dev2Decision in dds.TheStack)
                {
                    AddInputDebugItemResultsAfterEvaluate(result, ref userModel, env, dev2Decision.Col1, out  error);
                    allErrors.MergeErrors(error);
                    AddInputDebugItemResultsAfterEvaluate(result, ref userModel, env, dev2Decision.Col2, out error);
                    allErrors.MergeErrors(error);
                    AddInputDebugItemResultsAfterEvaluate(result, ref userModel, env, dev2Decision.Col3, out error);
                    allErrors.MergeErrors(error);
                }

                var itemToAdd = new DebugItem();

                userModel = userModel.Replace("OR", " OR\r\n")
                                     .Replace("AND", " AND\r\n")
                                     .Replace("\r\n ", "\r\n")
                                     .Replace("\r\n\r\n", "\r\n")
                                     .Replace("  ", " ");

                AddDebugItem(new DebugItemStaticDataParams(userModel, "Statement"), itemToAdd);
                result.Add(itemToAdd);

                itemToAdd = new DebugItem();
                AddDebugItem(new DebugItemStaticDataParams(dds.Mode == Dev2DecisionMode.AND ? "YES" : "NO", "Require All decisions to be True"), itemToAdd);
                result.Add(itemToAdd);
            }
            catch (JsonSerializationException)
            {

            }
            catch (Exception e)
            {
                allErrors.AddError(e.Message);
            }
            finally
            {
                if (allErrors.HasErrors())
                {
                    var serviceName = GetType().Name;
                    DisplayAndWriteError(serviceName, allErrors);
                }
            }

            var val =  result.Select(a => a as DebugItem).ToList();
            _inner.SetDebugInputs(val);
            return val;
        }

        #region Overrides of DsfNativeActivity<string>

        public override List<DebugItem> GetDebugOutputs(IExecutionEnvironment env, int update)
        {
            return _debugOutputs;
        }

        #endregion

        // Travis.Frisinger - 28.01.2013 : Amended for Debug
        List<DebugItem> GetDebugOutputs(string theResult)
        {
            var result = new List<DebugItem>();
            string resultString = theResult;
            DebugItem itemToAdd = new DebugItem();
            var dds = Conditions;

            try
            {


                if (theResult == "True")
                {
                    resultString = dds.TrueArmText;
                }
                else if (theResult == "False")
                {
                    resultString = dds.FalseArmText;
                }

                itemToAdd.AddRange(new DebugItemStaticDataParams(resultString, "").GetDebugItemResult());
                result.Add(itemToAdd);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {

                itemToAdd.AddRange(new DebugItemStaticDataParams(resultString, "").GetDebugItemResult());
                result.Add(itemToAdd);

            }

            _inner.SetDebugOutputs(result);
            return result;
        }


        void AddInputDebugItemResultsAfterEvaluate(List<IDebugItem> result, ref string userModel, IExecutionEnvironment env, string expression, out ErrorResultTO error, DebugItem parent = null)
        {
            error = new ErrorResultTO();
            if (expression != null && DataListUtil.IsEvaluated(expression))
            {
                DebugOutputBase debugResult;
                if (error.HasErrors())
                {
                    debugResult = new DebugItemStaticDataParams("", expression, "");
                }
                else
                {
                    var expressiomToStringValue = ExecutionEnvironment.WarewolfEvalResultToString(env.Eval(expression, 0));// EvaluateExpressiomToStringValue(expression, decisionMode, dataList);
                    userModel = userModel.Replace(expression, expressiomToStringValue);
                    debugResult = new DebugItemWarewolfAtomResult(expressiomToStringValue, expression, "");
                }

                var itemResults = debugResult.GetDebugItemResult();

                var allReadyAdded = new List<IDebugItemResult>();

                itemResults.ForEach(a =>
                {
                    var found = result.SelectMany(r => r.FetchResultsList())
                                      .SingleOrDefault(r => r.Variable.Equals(a.Variable));
                    if (found != null)
                    {
                        allReadyAdded.Add(a);
                    }
                });

                allReadyAdded.ForEach(i => itemResults.Remove(i));

                if (parent == null)
                {
                    result.Add(new DebugItem(itemResults));
                }
                else
                {
                    parent.AddRange(itemResults);
                }
            }
        }
        #endregion

        public bool And { private get; set; }
    }
}
