
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dev2.Common;
using Infragistics.Calculations.CalcManager;
using Infragistics.Calculations.Engine;

// ReSharper disable CheckNamespace
namespace Dev2.MathOperations
// ReSharper restore CheckNamespace
{
    public class FunctionEvaluator : IFunctionEvaluator
    {

        #region Private Members
        private readonly IDev2CalculationManager _manager;

        #endregion Private Members

        #region Ctor

        public FunctionEvaluator()
        {
            _manager = new Dev2CalculationManager();
        }

        #endregion Ctor

        #region Public Methods
        

        
        /// <summary>
        /// Evaluate the expression according to the operation specified and pass this to the CalculationManager
        //  If something goes wrong during the execution, the error field will be populated and the method will
        //  return false.
        /// </summary>
        /// <param name="expressionTO">The expression automatic.</param>
        /// <param name="evaluation">The evaluation.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public bool TryEvaluateFunction(IEvaluationFunction expressionTO, out string evaluation, out string error)
        {
            bool isSuccessfulEvaluation;
            error = string.Empty;
            evaluation = string.Empty;

            if(!(string.IsNullOrEmpty(expressionTO.Function)))
            {
                try
                {
                    CalculationValue calcVal = _manager.CalculateFormula(expressionTO.Function);
                    evaluation = calcVal.GetResolvedValue().ToString();
                    isSuccessfulEvaluation = true;
                }
                catch(Exception ex)
                {
                    Dev2Logger.Log.Error("Function evaluation Error", ex);
                    error = ex.Message;
                    isSuccessfulEvaluation = false;
                }
            }
            else
            {
                error = "Unable to evaluate empty function";
                isSuccessfulEvaluation = false;
            }

            return isSuccessfulEvaluation;
        }


        /// <summary>
        /// Evaluate the expression according to the operation specified and pass this to the CalculationManager
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="evaluation">The evaluation.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public bool TryEvaluateFunction(string expression, out string evaluation, out string error)
        {
            bool evaluationState = false;
            error = String.Empty;
            evaluation = String.Empty;
            if(!(String.IsNullOrEmpty(expression)))
            {

                try
                {
                    CalculationValue value = _manager.CalculateFormula(expression);
                    if(value.IsError)
                    {
                        error = value.ToErrorValue().Message;
                    }
                    else
                    {
                        if(value.IsDateTime)
                        {
                            DateTime dateTime = value.ToDateTime();
                            string shortPattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                            string longPattern = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                            string finalPattern = shortPattern + " " + longPattern;
                            if(finalPattern.Contains("ss"))
                            {
                                finalPattern = finalPattern.Insert(finalPattern.IndexOf("ss", StringComparison.Ordinal) + 2, ".fff");
                            }
                            evaluation = dateTime.ToString(finalPattern);
                        }
                        else
                        {
                            evaluation = value.GetResolvedValue().ToString();
                        }
                        evaluationState = true;
                    }
                }
                catch(Exception ex)
                {
                    Dev2Logger.Log.Error("Function evaluation Error",ex);
                    error = ex.Message;
                    evaluationState = false;
                }
            }
            else
            {
                error = "Nothing to Evaluate";
            }

            return evaluationState;
        }

        // It expects a List of Type To (either strings or any type of object that is IComparable).
        // And evaluates the whole list against the expression.
        /// <summary>
        /// This is to cater for range operations 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <param name="evaluation"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool TryEvaluateFunction<T>(List<T> value, string expression, out string evaluation, out string error) where T : IConvertible
        {
            bool evaluationState;
            string evalString = string.Concat(expression, "(");
            evaluation = string.Empty;
            error = string.Empty;
            if(value == null || value.Count == 0)
            {
                evaluationState = false;
                error = "Cannot evaluate empty value list";
            }
            else if(!string.IsNullOrEmpty(expression))
            {
                evalString = value.Select(val => val.ToString(CultureInfo.InvariantCulture)).Where(eval => !string.IsNullOrEmpty(eval)).Aggregate(evalString, (current, eval) => current + string.Concat(eval, ","));
                evalString = evalString.Remove(evalString.LastIndexOf(",", StringComparison.Ordinal), 1);
                evalString = string.Concat(evalString, ")");
                try
                {
                    CalculationValue calcValue = _manager.CalculateFormula(evalString);
                    evaluation = calcValue.GetResolvedValue().ToString();
                    evaluationState = true;
                }
                catch(Exception ex)
                {
                    Dev2Logger.Log.Error("Function evaluation Error", ex);
                    error = ex.Message;
                    evaluationState = false;
                }
            }
            else
            {
                error = "Nothing to Evaluate";
                evaluationState = false;
            }
            return evaluationState;
        }

        #endregion Public Methods

        #region Statics

        public bool TryEvaluateAtomicFunction(string expression, out string evaluation, out string error)
        {
            bool evaluationState;
            error = String.Empty;
            evaluation = String.Empty;
            if(!(String.IsNullOrEmpty(expression)))
            {

                try
                {
                    CalculationValue value = _manager.CalculateFormula(expression);
                    evaluation = value.GetResolvedValue().ToString();
                    evaluationState = true;
                }
                catch(Exception ex)
                {
                    Dev2Logger.Log.Error("Function evaluation Error", ex);
                    error = ex.Message;
                    evaluationState = false;
                }
            }
            else
            {
                error = "Nothing to Evaluate";
                evaluationState = false;
            }

            return evaluationState;
        }

        #endregion Statics

        #region Private Methods

        #endregion Private Methods

    }
}
