
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Collections.Generic;
using System.Text;
using Dev2.Common.Interfaces;
using Dev2.MathOperations;

namespace Dev2.Data.MathOperations {
    public static class MathOpsFactory {

        private static string _mathFnDataList = string.Empty;
        private static readonly IList<string> RawMathFnList = new List<string>();

        public static string FetchMathFnDataList() {
            // we need to init it ;)
            if (_mathFnDataList == string.Empty) {
                InitMathFnRawData();
            }
            return _mathFnDataList;
        }

        public static IList<string> FetchMathFnStringList() {
            // we need to init it ;)
            if (RawMathFnList.Count == 0) {
                InitMathFnRawData();
            }

            return RawMathFnList;
        }

        public static IFunctionEvaluator CreateFunctionEvaluator() {
            return new FunctionEvaluator();
        }

        public static IEvaluationFunction CreateEvaluationExpressionTO(string expression) {
            return new EvaluationFunctionTO(expression);
        }

        public static IFunction CreateFunction(string functionName, IList<string> arguments, IList<string> argumentDescriptions, string description) {
            return new Function(functionName, arguments, argumentDescriptions, description);
        }

        public static IFunction CreateFunction() {
            return new Function();
        }

        public static IFrameworkRepository<IFunction> FunctionRepository() {
            return new FunctionRepository();
        }


        private static void InitMathFnRawData(){
            IFrameworkRepository<IFunction> repo = FunctionRepository();
            repo.Load();
            ICollection<IFunction> fns = repo.All();
            StringBuilder tmp = new StringBuilder("<DL>");

            // build list
            foreach (IFunction f in fns) {
                RawMathFnList.Add(f.FunctionName);
                tmp.Append("<");
                tmp.Append(f.FunctionName);
                tmp.Append("/>");
            }
            tmp.Append("</DL>");

            _mathFnDataList = tmp.ToString();
        }
    }
}
