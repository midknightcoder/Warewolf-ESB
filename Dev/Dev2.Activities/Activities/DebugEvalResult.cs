using System;
using System.Collections.Generic;
using Dev2.Activities.Debug;
using Dev2.Common;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Data;
using Dev2.Data.Util;
using Dev2.DataList.Contract;
using Warewolf.Storage;
using WarewolfParserInterop;

namespace Dev2.Activities
{
    public class DebugEvalResult : DebugOutputBase
    {
        string _inputVariable;
        readonly string _label;
        readonly WarewolfDataEvaluationCommon.WarewolfEvalResult _evalResult;

        public DebugEvalResult(string inputVariable, string label, IExecutionEnvironment environment,int update, bool isDataMerge = false)
        {
            _inputVariable = inputVariable.Trim();
            _label = label;
            try
            {
                if (ExecutionEnvironment.IsRecordsetIdentifier(_inputVariable) && DataListUtil.IsEvaluated(_inputVariable))
                {
                    if (DataListUtil.GetRecordsetIndexType(_inputVariable) == enRecordsetIndexType.Blank)
                    {
                        var length = environment.GetLength(DataListUtil.ExtractRecordsetNameFromValue(_inputVariable));
                        _inputVariable = DataListUtil.ReplaceRecordsetBlankWithIndex(_inputVariable, length);
                    }                    
                }
                if (isDataMerge)
                {
                    var evalForDataMerge = environment.EvalForDataMerge(_inputVariable, update);
                    var innerIterator = new WarewolfListIterator();
                    var innerListOfIters = new List<WarewolfIterator>();
                    foreach (var listOfIterator in evalForDataMerge)
                    {
                        var inIterator = new WarewolfIterator(listOfIterator);
                        innerIterator.AddVariableToIterateOn(inIterator);
                        innerListOfIters.Add(inIterator);
                    }
                    var atomList = new List<DataASTMutable.WarewolfAtom>();
                    while (innerIterator.HasMoreData())
                    {
                        var stringToUse = "";
                        // ReSharper disable once LoopCanBeConvertedToQuery
                        foreach (var warewolfIterator in innerListOfIters)
                        {
                            stringToUse += warewolfIterator.GetNextValue();
                        }
                        atomList.Add(DataASTMutable.WarewolfAtom.NewDataString(stringToUse));
                    }
                    var finalString = string.Join("", atomList);
                    _evalResult = WarewolfDataEvaluationCommon.WarewolfEvalResult.NewWarewolfAtomListresult(new WarewolfAtomList<DataASTMutable.WarewolfAtom>(DataASTMutable.WarewolfAtom.Nothing, atomList));
                    if (DataListUtil.IsFullyEvaluated(finalString))
                    {
                        _inputVariable = finalString;
                        _evalResult = environment.Eval(finalString, update);
                    }
                    else
                    {
                        var evalToExpression = environment.EvalToExpression(_inputVariable, update);
                        if (DataListUtil.IsEvaluated(evalToExpression))
                        {
                            _inputVariable = evalToExpression;
                        }
                    }
                }
                else
                {
                    var evalToExpression = environment.EvalToExpression(_inputVariable, update);
                    if (DataListUtil.IsEvaluated(evalToExpression))
                    {
                        _inputVariable = evalToExpression;
                    }
                    _evalResult = environment.Eval(_inputVariable, update);
                }

            }
            catch(Exception e)
            {
                Dev2Logger.Log.Error(e.Message,e);
                _evalResult = WarewolfDataEvaluationCommon.WarewolfEvalResult.NewWarewolfAtomResult(DataASTMutable.WarewolfAtom.Nothing);
            }
            
        }

        #region Overrides of DebugOutputBase

        public override string LabelText
        {
            get
            {
                return _label;
            }
        }

        public override List<IDebugItemResult> GetDebugItemResult()
        {
            if (_evalResult.IsWarewolfAtomResult)
            {
                var scalarResult = _evalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;
                if (scalarResult != null && !scalarResult.Item.IsNothing)
                {
                    var warewolfAtomToString = ExecutionEnvironment.WarewolfAtomToString(scalarResult.Item);
                    if (warewolfAtomToString == _inputVariable && DataListUtil.IsEvaluated(_inputVariable))
                    {
                        warewolfAtomToString = null;
                    }
                    if (!DataListUtil.IsEvaluated(_inputVariable))
                    {
                        _inputVariable = null;
                    }
                    return new DebugItemWarewolfAtomResult(warewolfAtomToString, _inputVariable, LabelText).GetDebugItemResult();
                }
            }
            else if (_evalResult.IsWarewolfAtomListresult)
            {
                var listResult = _evalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;
                if (listResult != null)
                {
                    return new DebugItemWarewolfAtomListResult(listResult, "", "", _inputVariable, LabelText, "", "=").GetDebugItemResult();
                }
            }
            else if (_evalResult.IsWarewolfRecordSetResult)
            {
                var listResult = _evalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfRecordSetResult;
                if (listResult != null)
                {
                    return new DebugItemWarewolfRecordset(listResult.Item, _inputVariable, LabelText,  "=").GetDebugItemResult();
                }
            }

            return new DebugItemStaticDataParams("",_inputVariable,LabelText).GetDebugItemResult();
        }

        #endregion
    }
}