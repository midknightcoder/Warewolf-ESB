
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Dev2.Common;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.StringTokenizer.Interfaces;
using Dev2.DataList.Contract;
using Dev2.DataList.Contract.Binary_Objects;
using Warewolf.Storage;
using WarewolfParserInterop;

namespace Dev2.Data.Util
{
    /// <summary>
    /// General DataList utility methods
    /// </summary>
    public static class DataListUtil
    {
        #region Class Members

        public const string OpeningSquareBrackets = "[[";
        public const string ClosingSquareBrackets = "]]";
        public const string RecordsetIndexOpeningBracket = "(";
        public const string RecordsetIndexClosingBracket = ")";

        private static readonly HashSet<string> SysTags = new HashSet<string>();
        const string AdlRoot = "ADL";
      
        private static readonly string[] StripTags = { "<XmlData>", "</XmlData>", "<Dev2ServiceInput>", "</Dev2ServiceInput>", "<sr>", "</sr>", "<ADL />" };
        private static readonly string[] NaughtyTags = { "<Dev2ResumeData>", "</Dev2ResumeData>", 
                                                         "<Dev2XMLResult>", "</Dev2XMLResult>", 
                                                         "<WebXMLConfiguration>", "</WebXMLConfiguration>", 
                                                         "<ActivityInput>", "</ActivityInput>", 
                                                         "<ADL>","</ADL>",
                                                         "<DL>","</DL>"
                                                       };

        private static readonly XmlReaderSettings IsXmlReaderSettings = new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Auto, DtdProcessing = DtdProcessing.Ignore };

        #endregion Class Members

        #region Constructor

        static DataListUtil()
        {
            // build system tags
            foreach(Enum e in (Enum.GetValues(typeof(enSystemTag))))
            {
                SysTags.Add(e.ToString());
            }
        }

        #endregion Constructor

        /// <summary>
        /// Replaces the index of the star with fixed.
        /// </summary>
        /// <param name="exp">The exp.</param>
        /// <param name="idx">The idx.</param>
        /// <returns></returns>
        public static string ReplaceStarWithFixedIndex(string exp, int idx)
        {
            return idx > 0 ? exp.Replace("(*)", RecordsetIndexOpeningBracket + idx + RecordsetIndexClosingBracket) : exp;
        }

        /// <summary>
        /// Replaces the index of a recordset with a blank index.
        /// </summary>
        /// <param name="expression">The expession.</param>
        /// <returns></returns>
        public static string ReplaceRecordsetIndexWithBlank(string expression)
        {
            var index = ExtractIndexRegionFromRecordset(expression);

            if (string.IsNullOrEmpty(index))
            {
                return expression;
            }

            string extractIndexRegionFromRecordset = string.Format("({0})", index);
            return string.IsNullOrEmpty(extractIndexRegionFromRecordset) ? expression :
                                        expression.Replace(extractIndexRegionFromRecordset, "()");
        }

        
        /// <summary>
        /// Replaces the index of a recordset with a blank index.
        /// </summary>
        /// <param name="expression">The expession.</param>
        /// <returns></returns>
        public static string ReplaceRecordsetIndexWithStar(string expression)
        {
            var index = ExtractIndexRegionFromRecordset(expression);

            if (string.IsNullOrEmpty(index))
            {
                return expression;
            }

            string extractIndexRegionFromRecordset = string.Format("({0})", index);
            return string.IsNullOrEmpty(extractIndexRegionFromRecordset) ? expression :
                                        expression.Replace(extractIndexRegionFromRecordset, "(*)");
        }

        /// <summary>
        /// Determines whether [is calc evaluation] [the specified expression].
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="newExpression">The new expression.</param>
        /// <returns>
        ///   <c>true</c> if [is calc evaluation] [the specified expression]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCalcEvaluation(string expression, out string newExpression)
        {
            bool result = false;

            newExpression = string.Empty;

            if(expression.StartsWith(GlobalConstants.CalculateTextConvertPrefix))
            {
                if(expression.EndsWith(GlobalConstants.CalculateTextConvertSuffix))
                {
                    newExpression = expression.Substring(GlobalConstants.CalculateTextConvertPrefix.Length, expression.Length - (GlobalConstants.CalculateTextConvertSuffix.Length + GlobalConstants.CalculateTextConvertPrefix.Length));
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Composes the into user visible recordset.
        /// </summary>
        /// <param name="rs">The rs.</param>
        /// <param name="idx">The idx.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public static string ComposeIntoUserVisibleRecordset(string rs, string idx, string field)
        {
            return string.Format("{0}({1}).{2}", rs, idx, field);
        }

        /// <summary>
        /// Composes the into user visible recordset.
        /// </summary>
        /// <param name="rs">The rs.</param>
        /// <param name="idx">The idx.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public static string ComposeIntoUserVisibleRecordset(string rs, int idx, string field)
        {
            return string.Format("{0}({1}).{2}", rs, idx, field);
        }

        /// <summary>
        /// Adds the missing from right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="errors">The errors.</param>
        /// <exception cref="System.ArgumentNullException">right</exception>
        public static void MergeDataList(IBinaryDataList left, IBinaryDataList right, out ErrorResultTO errors)
        {

            if(right == null)
            {
                throw new ArgumentNullException("right");
            }

            if(left == null)
            {
                throw new ArgumentException("left");
            }

            errors = new ErrorResultTO();
            ErrorResultTO invokeErrors;
            MergeOp(left, right, out invokeErrors);
            errors.MergeErrors(invokeErrors);
            MergeOp(right, left, out invokeErrors);
            errors.MergeErrors(invokeErrors);

        }

        private static void MergeOp(IBinaryDataList left, IBinaryDataList right, out ErrorResultTO errors)
        {
            IList<string> itemKeys = right.FetchAllUserKeys();
            errors = new ErrorResultTO();

            foreach(string key in itemKeys)
            {
                IBinaryDataListEntry entry;

                string error;
                if(!left.TryGetEntry(key, out entry, out error))
                {
                    // NOTE : DO NOT ADD ERROR, IT IS A MISS AND WE ACCOUNT FOR THIS BELOW

                    // Left does not contain key, get it from the right and add ;)
                    if(right.TryGetEntry(key, out entry, out error))
                    {
                        errors.AddError(error);
                        // we found it add it to the left ;)
                        if(entry.IsRecordset)
                        {
                            left.TryCreateRecordsetTemplate(entry.Namespace, entry.Description, entry.Columns, false,
                                                            true, out error);
                            errors.AddError(error);
                        }
                        else
                        {
                            left.TryCreateScalarTemplate(string.Empty, entry.Namespace, entry.Description, false,
                                                         out error);
                            errors.AddError(error);
                        }
                    }
                    else
                    {
                        errors.AddError(error);
                    }
                }
            }
        }


        /// <summary>
        /// Remove XMLData and other nesting junk from the ADL
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        public static string StripCrap(string payload)
        {
            string result = payload;
            string[] veryNaughtyTags = NaughtyTags;

            if(!string.IsNullOrEmpty(payload))
            {

                if(StripTags != null)
                {
                    StripTags
                        .ToList()
                        .ForEach(tag =>
                        {
                            result = result.Replace(tag, "");
                        });
                }

                if(veryNaughtyTags != null)
                {
                    result = CleanupNaughtyTags(veryNaughtyTags, result);
                }

                // we now need to remove non-valid chars from the stream

                int start = result.IndexOf("<", StringComparison.Ordinal);
                if(start >= 0)
                {
                    result = result.Substring((start));
                }

                if(result.Contains("<") && result.Contains(">"))
                {
                    if(!IsXml(result))
                    {
                        // We need to replace DataList if present ;)
                        result = result.Replace("<DataList>", "").Replace("</DataList>", "");
                        result = result.Replace(string.Concat("<", AdlRoot, ">"), string.Empty).Replace(string.Concat("</", AdlRoot, ">"), "");
                        result = string.Concat("<", AdlRoot, ">", result, "</", AdlRoot, ">");
                    }
                }


            }

            return result;
        }

        /// <summary>
        /// Builds the system tag for data list.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="addBrackets">if set to <c>true</c> [add brackets].</param>
        /// <returns></returns>
        public static string BuildSystemTagForDataList(enSystemTag tag, bool addBrackets)
        {

            string result = (GlobalConstants.SystemTagNamespace + "." + tag);

            if(addBrackets)
            {
                result = OpeningSquareBrackets + result + ClosingSquareBrackets;
            }

            return result;
        }

        /// <summary>
        /// Builds the system tag for data list.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="addBrackets">if set to <c>true</c> [add brackets].</param>
        /// <returns></returns>
        public static string BuildSystemTagForDataList(string tag, bool addBrackets)
        {

            string result = (GlobalConstants.SystemTagNamespace + "." + tag);

            if(addBrackets)
            {
                result = OpeningSquareBrackets + result + ClosingSquareBrackets;
            }

            return result;
        }

        /// <summary>
        /// Extracts the attribute.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static string ExtractAttribute(string payload, string tagName, string attribute)
        {
            string result = string.Empty;

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(payload);
            XmlNodeList nl = xDoc.GetElementsByTagName(tagName);
            if(nl.Count > 0)
            {
                var xmlAttributeCollection = nl[0].Attributes;
                if(xmlAttributeCollection != null)
                {
                    result = xmlAttributeCollection[attribute].Value;
                }
            }

            return result;
        }

/*
        public static string ExtractAttributeFromTagAndMakeRecordset(string payload, string tagName, string attribute)
        {
            return ExtractAttributeFromTagAndMakeRecordset(payload, tagName, new[] { attribute }, null);
        }
*/

        /// <summary>
        /// Used to detect the #text and #cdate-section 'nodes' returned by MS XML parser
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMsXmlBugNode(string value)
        {
            bool result = value == "#text" || value == "#cdata-section";

            return result;
        }

        /// <summary>
        /// Removes the brackets.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string RemoveLanguageBrackets(string val)
        {
            return val.Replace("[", string.Empty).Replace("]", string.Empty);
        }

        /// <summary>
        /// Used to determine if a tag is a system tag or not
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static bool IsSystemTag(string tag)
        {

            // Nasty junk that has been carried!
            string[] nastyJunk = { "WebServerUrl", "Dev2WebServer", "PostData", "Service" };

            // Transfer System Tags
            bool result = SysTags.Contains(tag) || nastyJunk.Contains(tag);

            if(!result && tag.StartsWith(GlobalConstants.SystemTagNamespaceSearch))
            {
                tag = tag.Replace(GlobalConstants.SystemTagNamespaceSearch, "");
                result = SysTags.Contains(tag) || nastyJunk.Contains(tag);
            }

            return result;
        }

        /// <summary>
        /// Generates the data list from defs.
        /// </summary>
        /// <param name="defs">The defs.</param>
        /// <param name="withData">if set to <c>true</c> [with data].</param>
        /// <returns></returns>
        public static StringBuilder GenerateDataListFromDefs(IEnumerable<IDev2Definition> defs, bool withData = false)
        {
            StringBuilder result = new StringBuilder("<" + AdlRoot + ">");

            Dictionary<string, string> rsMap = new Dictionary<string, string>();

            defs
                .ToList()
                .ForEach(d =>
                {

                    if(d.IsRecordSet)
                    {
                        string tmp = string.Empty;

                        if(rsMap.Keys.Contains(d.RecordSetName))
                        {
                            tmp = rsMap[d.RecordSetName];
                        }
                        string name = d.Name.Contains(".") ? d.Name.Split('.')[1] : d.Name;
                        tmp = withData ? string.Concat(tmp, Environment.NewLine, "<", name, ">", d.RawValue, "</", name, ">") : string.Concat(tmp, Environment.NewLine, "<", name, "/>");


                        rsMap[d.RecordSetName] = tmp;
                    }
                    else
                    {
                        result.Append(withData ? string.Concat("<", d.Name, ">", d.RawValue, "</", d.Name, ">") : string.Concat("<", d.Name, "/>"));
                    }

                });

            rsMap
                .ToList()
                .ForEach(rs =>
                {
                    result.Append(Environment.NewLine);
                    result.Append(string.Concat("<", rs.Key, ">"));
                    result.Append(Environment.NewLine);
                    result.Append(rs.Value);
                    result.Append(Environment.NewLine);
                    result.Append(string.Concat("</", rs.Key, ">"));

                });

            result.Append(Environment.NewLine);
            result.Append("</" + AdlRoot + ">");

            return result;
        }

        /// <summary>
        /// Shapes the definitions to data list.
        /// </summary>
        /// <param name="defs">The defs.</param>
        /// <param name="typeOf">The type of.</param>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public static StringBuilder ShapeDefinitionsToDataList(IList<IDev2Definition> defs, enDev2ArgumentType typeOf, out ErrorResultTO errors)
        {
            StringBuilder result = new StringBuilder();

            bool isInput = false;
            errors = new ErrorResultTO();

            if(typeOf == enDev2ArgumentType.Input)
            {
                isInput = true;
            }

            if(defs == null || defs.Count == 0)
            {
                errors.AddError(string.Concat("could not locate any data of type [ ", typeOf, " ]"));
            }
            else
            {

                IRecordSetCollection recCol = DataListFactory.CreateRecordSetCollection(defs, (isInput));
                IList<IDev2Definition> scalarList = DataListFactory.CreateScalarList(defs, (isInput));

                // open datashape
                result.Append(string.Concat("<", AdlRoot, ">"));
                result.Append(Environment.NewLine);

                // append scalar shape
                result.Append(BuildDev2ScalarShape(scalarList, isInput));
                // append record set shape
                result.Append(BuildDev2RecordSetShape(recCol, isInput));

                // close datashape
                result.Append(Environment.NewLine);
                result.Append(string.Concat("</", AdlRoot, ">"));
            }

            return result;
        }

        /// <summary>
        /// Shapes the definitions to data list.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="typeOf">The type of.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="flipGeneration">if set to <c>true</c> [flip generation].</param>
        /// <param name="isDbService"></param>
        /// <returns></returns>
        public static StringBuilder ShapeDefinitionsToDataList(string arguments, enDev2ArgumentType typeOf, out ErrorResultTO errors, bool flipGeneration = false, bool isDbService = false)
        {
            StringBuilder result = new StringBuilder();
            IList<IDev2Definition> defs = null;
            bool isInput = false;
            errors = new ErrorResultTO();

            if(typeOf == enDev2ArgumentType.Output)
            {
                defs = DataListFactory.CreateOutputParser().Parse(arguments);
            }
            else if(typeOf == enDev2ArgumentType.Input)
            {
                defs = DataListFactory.CreateInputParser().Parse(arguments);
                isInput = true;
            }

            if(defs == null)
            {
                errors.AddError(string.Concat("could not locate any data of type [ ", typeOf, " ]"));
            }
            else
            {
                IRecordSetCollection recCol = isDbService ?
                    DataListFactory.CreateRecordSetCollectionForDbService(defs, !(isInput)) :
                    DataListFactory.CreateRecordSetCollection(defs, !(isInput));

                IList<IDev2Definition> scalarList = DataListFactory.CreateScalarList(defs, !(isInput));

                // open datashape
                result.Append(string.Concat("<", AdlRoot, ">"));
                result.Append(Environment.NewLine);

                // do we want to do funky things ?!
                if(flipGeneration)
                {
                    isInput = true;
                }

                // append scalar shape
                result.Append(BuildDev2ScalarShape(scalarList, isInput));
                // append record set shape
                result.Append(BuildDev2RecordSetShape(recCol, isInput));

                // close datashape
                result.Append(Environment.NewLine);
                result.Append(string.Concat("</", AdlRoot, ">"));
            }

            return result;
        }
        
        /// <summary>
        /// Shapes the definitions to data list.
        /// </summary>
        /// <returns></returns>
        public static IExecutionEnvironment InputsToEnvironment(IExecutionEnvironment outerEnvironment,string inputDefs)
        {
            var env = new ExecutionEnvironment();

            try
            {

        
            var inputs = DataListFactory.CreateInputParser().Parse(inputDefs);

            IRecordSetCollection inputRecSets = DataListFactory.CreateRecordSetCollection(inputs, false);

            IList<IDev2Definition> inputScalarList = DataListFactory.CreateScalarList(inputs, false);

            CreateRecordSetsInputs(outerEnvironment, inputRecSets, inputs, env);
            CreateScalarInputs(outerEnvironment, inputScalarList, env);
            }
            finally
            {
                
                env.CommitAssign();
            }
            return env;
        }

        static void CreateRecordSetsInputs(IExecutionEnvironment outerEnvironment, IRecordSetCollection inputRecSets, IList<IDev2Definition> inputs, ExecutionEnvironment env)
        {
            foreach(var recordSetDefinition in inputRecSets.RecordSets)
            {
                var outPutRecSet = inputs.FirstOrDefault(definition => definition.IsRecordSet && ExtractRecordsetNameFromValue(definition.MapsTo) == recordSetDefinition.SetName);
                if (outPutRecSet != null)
                {
                    var emptyList = new List<string>();
                    foreach (var dev2ColumnDefinition in recordSetDefinition.Columns)
                    {
                        if (dev2ColumnDefinition.IsRecordSet)
                        {
                            var defn = "[[" + dev2ColumnDefinition.RecordSetName + "()." + dev2ColumnDefinition.Name + "]]";


                            if (string.IsNullOrEmpty(dev2ColumnDefinition.RawValue))
                            {
                                if (!emptyList.Contains(defn))
                                {
                                    emptyList.Add(defn);
                                    continue;
                                }
                            }
                            var warewolfEvalResult = outerEnvironment.Eval(dev2ColumnDefinition.RawValue);

                            if (warewolfEvalResult.IsWarewolfAtomListresult)
                            {
                                AtomListInputs(warewolfEvalResult, dev2ColumnDefinition, env);
                            }
                            if (warewolfEvalResult.IsWarewolfAtomResult)
                            {
                                AtomInputs(warewolfEvalResult, dev2ColumnDefinition, env);
                            }
                        }
                    }
                    foreach (var defn in emptyList)
                    {
                        env.AssignDataShape(defn);
                    }
                }
            }
        }

        static void CreateScalarInputs(IExecutionEnvironment outerEnvironment, IEnumerable<IDev2Definition> inputScalarList, ExecutionEnvironment env)
        {
            foreach(var dev2Definition in inputScalarList)
            {
                env.AssignDataShape("[[" + dev2Definition.Name + "]]");
                if(!dev2Definition.IsRecordSet)
                {
                    var warewolfEvalResult = outerEnvironment.Eval(dev2Definition.RawValue);
                    if(warewolfEvalResult.IsWarewolfAtomListresult)
                    {
                        ScalarAtomList(warewolfEvalResult, env, dev2Definition);
                    }
                    else
                    {
                        ScalarAtom(warewolfEvalResult, env, dev2Definition);
                    }
                }
            }
        }

        static void ScalarAtom(WarewolfDataEvaluationCommon.WarewolfEvalResult warewolfEvalResult, ExecutionEnvironment env, IDev2Definition dev2Definition)
        {
            var data = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;
            if(data != null)
            {
                env.AssignWithFrame(new AssignValue("[[" + dev2Definition.Name + "]]", ExecutionEnvironment.WarewolfAtomToString(data.Item)));
            }
        }

        static void ScalarAtomList(WarewolfDataEvaluationCommon.WarewolfEvalResult warewolfEvalResult, ExecutionEnvironment env, IDev2Definition dev2Definition)
        {
            var data = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;
            if(data != null && data.Item.Any())
            {
                env.AssignWithFrame(new AssignValue("[[" + dev2Definition.Name + "]]", ExecutionEnvironment.WarewolfAtomToString(data.Item.Last())));
            }
        }

        static void AtomInputs(WarewolfDataEvaluationCommon.WarewolfEvalResult warewolfEvalResult, IDev2Definition dev2ColumnDefinition, ExecutionEnvironment env)
        {
            var recsetResult = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;

            if(dev2ColumnDefinition.IsRecordSet)
            {


                if(recsetResult != null)
                {
                    var correctRecSet = "[[" + dev2ColumnDefinition.RecordSetName + "(*)." + dev2ColumnDefinition.Name + "]]";

                    env.AssignWithFrame(new AssignValue(correctRecSet, PublicFunctions.AtomtoString(recsetResult.Item)));
                }
            }
        }

        static void AtomListInputs(WarewolfDataEvaluationCommon.WarewolfEvalResult warewolfEvalResult, IDev2Definition dev2ColumnDefinition, ExecutionEnvironment env)
        {
            var recsetResult = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;

            GetRecordsetIndexType(dev2ColumnDefinition.Value);

            if(recsetResult != null)
            {
                var correctRecSet = "[[" + dev2ColumnDefinition.RecordSetName + "(*)." + dev2ColumnDefinition.Name + "]]";

                env.EvalAssignFromNestedStar(correctRecSet, recsetResult);
            }
        }

        /// <summary>
        /// Determines whether the value is a recordset.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [value is recordset] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValueRecordset(string value)
        {
            bool result = false;

            if(!string.IsNullOrEmpty(value))
            {
                if(value.Contains(RecordsetIndexOpeningBracket) && value.Contains(RecordsetIndexClosingBracket))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether is a recordset with fields
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValueRecordsetWithFields(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Contains(").");
        }

        /// <summary>
        /// Evaluates if an expression is a root level evaluated variable ie [[x]]
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsRootVariable(string expression)
        {
            bool result = true;

            if(expression == null)
            {
                return false;
            }

            string[] openParts = Regex.Split(expression, @"\[\[");
            string[] closeParts = Regex.Split(expression, @"\]\]");

            //2013.05.31: Ashley lewis QA feedback on bug 9379 - count the number of opening and closing braces, they must both be more than one
            if(expression.Contains(OpeningSquareBrackets) && expression.Contains(ClosingSquareBrackets) && openParts.Count() == closeParts.Count() && openParts.Count() > 2 && closeParts.Count() > 2)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Used to extract a recordset name from a string as per the Dev2 data language spec
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ExtractRecordsetNameFromValue(string value)
        {
            if(value == null)
            {
                return string.Empty;
            }

            value = StripBracketsFromValue(value);
            string result = string.Empty;

            int openBracket = value.IndexOf(RecordsetIndexOpeningBracket, StringComparison.Ordinal);
            if(openBracket > 0)
            {
                result = value.Substring(0, openBracket);
            }

            return result;
        }

        /// <summary>
        /// Used to extract a field name from our recordset notation
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ExtractFieldNameFromValue(string value)
        {
            string result = string.Empty;
            value = StripBracketsFromValue(value);
            int dotIdx = value.LastIndexOf(".", StringComparison.Ordinal);
            if(dotIdx > 0)
            {
                result = value.Substring((dotIdx + 1));
            }

            return result;
        }
        
        /// <summary>
        /// Used to extract a field name from our recordset notation
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ExtractFieldNameOnlyFromValue(string value)
        {
            string result = string.Empty;
            int dotIdx = value.LastIndexOf(".", StringComparison.Ordinal);
            int closeIdx = value.LastIndexOf("]]", StringComparison.Ordinal);
            if(dotIdx > 0)
            {
                result = value.Substring((dotIdx + 1),closeIdx-dotIdx-1);
            }

            return result;
        }

        /// <summary>
        /// Remove [[ ]] from a value if present
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string StripBracketsFromValue(string value)
        {
            string result = string.Empty;
            if(value != null)
            {
                result = value.Replace(OpeningSquareBrackets, "").Replace(ClosingSquareBrackets, "");
            }

            return result;
        }

        /// <summary>
        /// Strips the leading and trailing brackets from value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string StripLeadingAndTrailingBracketsFromValue(string value)
        {
            string result = value;

            if(result.StartsWith(OpeningSquareBrackets))
            {
                result = result.Substring(2, (result.Length - 2));
            }

            if(result.EndsWith(ClosingSquareBrackets))
            {
                result = result.Substring(0, (result.Length - 2));
            }

            return result;
        }

        /// <summary>
        /// Checks if a region is closed
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EndsWithClosingTags(string value)
        {
            return !string.IsNullOrEmpty(value) && value.EndsWith(ClosingSquareBrackets);
        }

        /// <summary>
        /// Get the index of the closing tags in a variable
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IndexOfClosingTags(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return -1;
            }

            return value.LastIndexOf(ClosingSquareBrackets, StringComparison.Ordinal);
        }

        /// <summary>
        /// Adds [[ ]] to a variable if they are not present already
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string AddBracketsToValueIfNotExist(string value)
        {
            string result;

            if(!value.Contains(ClosingSquareBrackets))
            {
                // missing both
                result = !value.Contains(OpeningSquareBrackets) ? string.Concat(OpeningSquareBrackets, value, ClosingSquareBrackets) : string.Concat(value, ClosingSquareBrackets);
            }
            else
            {
                result = value;
            }

            return result;
        }

        /// <summary>
        /// Adds () to the end of the value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="starNotation">if set to <c>true</c> [star notation].</param>
        /// <returns></returns>
        public static string MakeValueIntoHighLevelRecordset(string value, bool starNotation = false)
        {
            var inject = "()";

            if(starNotation)
            {
                inject = "(*)";
            }

            string result = StripBracketsFromValue(value);

            if(result.EndsWith(RecordsetIndexOpeningBracket))
            {
                result = string.Concat(result, RecordsetIndexClosingBracket);
            }
            else if(result.EndsWith(RecordsetIndexClosingBracket))
            {
                return result.Replace(RecordsetIndexClosingBracket, inject);
            }
            else if(!result.EndsWith("()"))
            {
                result = string.Concat(result, inject);
            }
            return result;
        }

        /// <summary>
        /// Used to extract an index in the recordset notation
        /// </summary>
        /// <param name="rs">The rs.</param>
        /// <returns></returns>
        public static string ExtractIndexRegionFromRecordset(string rs)
        {
            string result = string.Empty;

            int start = rs.IndexOf(RecordsetIndexOpeningBracket, StringComparison.Ordinal);
            if(start > 0)
            {
                int end = rs.LastIndexOf(RecordsetIndexClosingBracket, StringComparison.Ordinal);
                if(end < 0)
                {
                    end = rs.Length;
                }

                start += 1;
                result = rs.Substring(start, (end - start));
            }

            return result;
        }


        /// <summary>
        /// Determines if recordset has a star index
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static bool IsStarIndex(string rs)
        {
            if(string.IsNullOrEmpty(rs))
            {
                return false;
            }

            return ExtractIndexRegionFromRecordset(rs) == "*";
        }

        /// <summary>
        /// An opening brace for a recordset
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsRecordsetOpeningBrace(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return false;
            }

            return value.StartsWith(RecordsetIndexOpeningBracket);
        }

        /// <summary>
        /// Is the expression evaluated
        /// </summary>  
        /// <param name="payload">The payload.</param>
        /// <returns>
        ///   <c>true</c> if the specified payload is evaluated; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFullyEvaluated(string payload)
        {
            bool result = payload != null && payload.IndexOf(OpeningSquareBrackets, StringComparison.Ordinal) >= 0 && payload.IndexOf(ClosingSquareBrackets, StringComparison.Ordinal) >= 0;

            return result;
        }      
        
        /// <summary>
        /// Is the expression evaluated
        /// </summary>  
        /// <param name="payload">The payload.</param>
        /// <returns>
        ///   <c>true</c> if the specified payload is evaluated; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEvaluated(string payload)
        {
            bool result = payload != null && payload.IndexOf(OpeningSquareBrackets, StringComparison.Ordinal) >= 0;

            return result;
        }

        /// <summary>
        /// Gets the type of the recordset index.
        /// </summary>
        /// <param name="idx">The idx.</param>
        /// <returns></returns>
        public static enRecordsetIndexType GetRecordsetIndexTypeRaw(string idx)
        {
            enRecordsetIndexType result = enRecordsetIndexType.Error;

            if(idx == "*")
            {
                result = enRecordsetIndexType.Star;
            }
            else if(string.IsNullOrEmpty(idx))
            {
                result = enRecordsetIndexType.Blank;
            }
            else
            {
                try
                {
                    // ReSharper disable ReturnValueOfPureMethodIsNotUsed

                    Convert.ToInt32(idx);
                    // ReSharper restore ReturnValueOfPureMethodIsNotUsed
                    result = enRecordsetIndexType.Numeric;
                }
                catch(Exception ex)
                {
                    Dev2Logger.Log.Error("DataListUtil", ex);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the index type of a recorset
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static enRecordsetIndexType GetRecordsetIndexType(string expression)
        {
            enRecordsetIndexType result = enRecordsetIndexType.Error;

            string idx = ExtractIndexRegionFromRecordset(expression);
            if(idx == "*")
            {
                result = enRecordsetIndexType.Star;
            }
            else if(string.IsNullOrEmpty(idx))
            {
                result = enRecordsetIndexType.Blank;
            }
            else
            {
                int convertIntTest;
                if(Int32.TryParse(idx, out convertIntTest))
                {
                    result = enRecordsetIndexType.Numeric;
                }
            }

            return result;
        }

        //used in the replace node method

        /// <summary>
        /// Checks if the info contained in data is well formed XML
        /// </summary>
        public static bool IsXml(string data)
        {
            bool isFragment;
            bool isHtml;
            var isXml = IsXml(data, out isFragment, out isHtml);
            return isXml && !isFragment && !isHtml;
        }

        /// <summary>
        /// Checks if the info contained in data is well formed XML
        /// </summary>
        public static bool IsXml(string data, out bool isFragment)
        {
            bool isHtml;
            return IsXml(data, out isFragment, out isHtml) && !isFragment && !isHtml;
        }

        /// <summary>
        /// Checks if the info contained in data is well formed XML
        /// </summary>
        static bool IsXml(string data, out bool isFragment, out bool isHtml)
        {
            string trimedData = data.Trim();
            bool result = (trimedData.StartsWith("<") && !trimedData.StartsWith("<![CDATA["));

            isFragment = false;
            isHtml = false;

            if(result)
            {
                using(TextReader tr = new StringReader(trimedData))
                {
                    using(XmlReader reader = XmlReader.Create(tr, IsXmlReaderSettings))
                    {

                        try
                        {
                            long nodeCount = 0;
                            while(reader.Read() && !isHtml && !isFragment && reader.NodeType != XmlNodeType.Document)
                            {
                                nodeCount++;

                                if(reader.NodeType != XmlNodeType.CDATA)
                                {
                                    if(reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "html" && reader.Depth == 0)
                                    {
                                        isHtml = true;
                                        result = false;
                                    }

                                    if(reader.NodeType == XmlNodeType.Element && nodeCount > 1 && reader.Depth == 0)
                                    {
                                        isFragment = true;
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Dev2Logger.Log.Error("DataListUtil", ex);
                            tr.Close();
                            reader.Close();
                            isFragment = false;
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public static bool IsJson(string data)
        {
            var tmp = data.Trim();
            if(tmp.StartsWith("{") && tmp.EndsWith("}"))
            {
                return true;
            }

            return false;
        }

        public static IList<string> GetAllPossibleExpressionsForFunctionOperations(string expression, IExecutionEnvironment env, out ErrorResultTO errors)
        {
            IList<string> result = new List<string>();
            errors = new ErrorResultTO();
            try
            {
                result = env.EvalAsListOfStrings(expression);
                
            }
            catch(Exception err)
            {
                errors.AddError(err.Message);
               
            }
            

            return result;
        }

        /// <summary>
        /// Adjusts for encoding issues.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        public static string AdjustForEncodingIssues(string payload)
        {
            string trimedData = payload.Trim();
            var isXml = (trimedData.StartsWith("<") && !trimedData.StartsWith("<![CDATA["));

            if(!isXml)
            {
                // we need to adjust. there might be a silly encoding issue with first char!
                if(trimedData.Length > 1 && trimedData[1] == '<' && trimedData[2] == '?')
                {
                    trimedData = trimedData.Substring(1);
                }
                else if(trimedData.Length > 2 && trimedData[2] == '<' && trimedData[3] == '?')
                {
                    trimedData = trimedData.Substring(2);
                }
                else if(trimedData.Length > 3 && trimedData[3] == '<' && trimedData[4] == '?')
                {
                    trimedData = trimedData.Substring(3);
                }
            }

            return trimedData;
        }

        /// <summary>
        /// Build a scalar shape
        /// </summary>
        /// <param name="scalarList">The scalar list.</param>
        /// <param name="isInput">if set to <c>true</c> [is input].</param>
        /// <returns></returns>
        private static string BuildDev2ScalarShape(IEnumerable<IDev2Definition> scalarList, bool isInput)
        {
            StringBuilder result = new StringBuilder();

            foreach(IDev2Definition def in scalarList)
            {
                if(!isInput)
                {
                    if(IsEvaluated(def.RawValue))
                    {
                        result.Append(string.Concat("<", def.Value, "></", def.Value, ">"));
                        result.Append(Environment.NewLine);
                    }

                    if(!string.IsNullOrEmpty(def.Name))
                    {
                        result.Append(string.Concat("<", def.Name, "></", def.Name, ">"));
                        result.Append(Environment.NewLine);
                    }
                }
                else
                {
                    if(!string.IsNullOrEmpty(def.Name))
                    {
                        result.Append(string.Concat("<", def.Name, "></", def.Name, ">"));
                        result.Append(Environment.NewLine);
                    }

                    // we need to process the RawValue field incase it is not in the recordsets ;)
                    var rsName = ExtractRecordsetNameFromValue(def.Value);
                    if(string.IsNullOrEmpty(rsName) && IsEvaluated(def.Value))
                    {
                        var tmpValue = RemoveLanguageBrackets(def.Value);
                        result.Append(string.Concat("<", tmpValue, "></", tmpValue, ">"));
                        result.Append(Environment.NewLine);
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Cleanups the naughty tags.
        /// </summary>
        /// <param name="toRemove">To remove.</param>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private static string CleanupNaughtyTags(string[] toRemove, string payload)
        {
            bool foundOpen = false;
            string result = payload;

            for(int i = 0; i < toRemove.Length; i++)
            {
                string myTag = toRemove[i];
                if(myTag.IndexOf("<", StringComparison.Ordinal) >= 0 && myTag.IndexOf("</", StringComparison.Ordinal) < 0)
                {
                    foundOpen = true;
                }
                else if(myTag.IndexOf("</", StringComparison.Ordinal) >= 0)
                {
                    // close tag
                    if(foundOpen)
                    {
                        // remove data between
                        int loc = i - 1;
                        if(loc >= 0)
                        {
                            int start = result.IndexOf(toRemove[loc], StringComparison.Ordinal);
                            int end = result.IndexOf(myTag, StringComparison.Ordinal);
                            if(start < end && start >= 0)
                            {
                                string canidate = result.Substring(start, ((end - start) + myTag.Length));
                                string tmpResult = canidate.Replace(myTag, "").Replace(toRemove[loc], "");
                                if(tmpResult.IndexOf("</", StringComparison.Ordinal) >= 0 || tmpResult.IndexOf("/>", StringComparison.Ordinal) >= 0)
                                {
                                    // replace just the tags
                                    result = result.Replace(myTag, "").Replace(toRemove[loc], "");
                                }
                                else
                                {
                                    // replace any tag and it's contents as long as it is not XML in side
                                    result = result.Replace(canidate, "");
                                }
                            }
                        }
                    }
                    else
                    {
                        result = result.Replace(myTag, "");
                    }

                    foundOpen = false;
                }
            }

            return result.Trim();

        }

        /// <summary>
        /// Build a recordset shape
        /// </summary>
        /// <param name="recCol"></param>
        /// <param name="isInput"></param>
        /// <returns></returns>
        private static string BuildDev2RecordSetShape(IRecordSetCollection recCol, bool isInput)
        {
            StringBuilder result = new StringBuilder();

            IList<IRecordSetDefinition> defs = recCol.RecordSets;
            HashSet<string> processedSetNames = new HashSet<string>();
            foreach(IRecordSetDefinition tmp in defs)
            {
                IList<string> postProcessDefs = new List<string>();
                // get DL recordset Name
                if(tmp.Columns.Count > 0)
                {
                    string setName = tmp.SetName;
                    result.Append(string.Concat("<", setName, ">"));
                    result.Append(Environment.NewLine);

                    processedSetNames.Add(setName);

                    IList<IDev2Definition> cols = tmp.Columns;
                    foreach(IDev2Definition tmpDef in cols)
                    {
                        if(isInput)
                        {
                            var col = ExtractFieldNameFromValue(tmpDef.MapsTo);

                            if(!string.IsNullOrEmpty(col))
                            {
                                var toAppend = ("\t<" + col + "></" + col + ">");
                                result.Append(toAppend);
                            }

                        }
                        else
                        {
                            //Name
                            string tag = ExtractFieldNameFromValue(tmpDef.Name);

                            if(string.IsNullOrEmpty(tag))
                            {
                                //Name
                                tag = tmpDef.Name;
                            }

                            result.Append(string.Concat("\t<", tag, "></", tag, ">"));

                        }
                        result.Append(Environment.NewLine);
                    }
                    result.Append(string.Concat("</", setName, ">"));
                    result.Append(Environment.NewLine);

                    //  Process post append data ;)
                    foreach(var col in postProcessDefs)
                    {
                        result.Append(col);
                        result.Append(Environment.NewLine);
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Removes the recordset brackets from a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveRecordsetBracketsFromValue(string value)
        {
            return value.Replace("()", "");
        }

        /// <summary>
        /// Extracts the input definitions from a service definition.
        /// </summary>
        /// <param name="serviceDefintion">The service definition.</param>
        /// <returns></returns>
        public static string ExtractInputDefinitionsFromServiceDefinition(string serviceDefintion)
        {
            string result = string.Empty;
            if(!string.IsNullOrEmpty(serviceDefintion))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(serviceDefintion);
                var selectSingleNode = xDoc.SelectSingleNode("//Inputs");
                if(selectSingleNode != null)
                {
                    result = selectSingleNode.OuterXml;
                }
            }
            return result;
        }


        /// <summary>
        /// Extracts the output definitions from a service definition.
        /// </summary>
        /// <param name="serviceDefintion">The service definition.</param>
        /// <returns></returns>
        public static string ExtractOutputDefinitionsFromServiceDefinition(string serviceDefintion)
        {
            string result = string.Empty;
            if(!string.IsNullOrEmpty(serviceDefintion))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(serviceDefintion);
                var selectSingleNode = xDoc.SelectSingleNode("//Outputs");
                if(selectSingleNode != null)
                {
                    result = selectSingleNode.OuterXml;
                }
            }
            return result;
        }

        /// <summary>
        /// Creates a recordset display value.
        /// </summary>
        /// <param name="recsetName">Name of the recordset.</param>
        /// <param name="colName">Name of the column.</param>
        /// <param name="indexNum">The index number.</param>
        /// <returns></returns>
        public static string CreateRecordsetDisplayValue(string recsetName, string colName, string indexNum)
        {
            return string.Concat(recsetName, RecordsetIndexOpeningBracket, indexNum, ").", colName);
        }

        /// <summary>
        /// Ecodes the region brackets in Html.
        /// </summary>
        /// <param name="stringToEncode">The string to encode.</param>
        /// <returns></returns>
        public static string HtmlEncodeRegionBrackets(string stringToEncode)
        {
            return stringToEncode.Replace("[", "&#91;").Replace("]", "&#93;");
        }

        /// <summary>
        /// Upserts the tokens.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="tokenPrefix">The token prefix.</param>
        /// <param name="tokenSuffix">The token suffix.</param>
        /// <param name="removeEmptyEntries">if set to <c>true</c> [remove empty entries].</param>
        /// <exception cref="System.ArgumentNullException">target</exception>
        public static void UpsertTokens(Collection<ObservablePair<string, string>> target, IDev2Tokenizer tokenizer, string tokenPrefix = null, string tokenSuffix = null, bool removeEmptyEntries = true)
        {
            if(target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.Clear();

            if(tokenizer == null)
            {
                return;
            }

            var newTokens = new List<string>();
            while(tokenizer.HasMoreOps())
            {
                var token = tokenizer.NextToken();
                if(string.IsNullOrEmpty(token))
                {
                    if(!removeEmptyEntries)
                    {
                        target.Add(new ObservablePair<string, string>(string.Empty, string.Empty));
                    }
                }
                else
                {
                    token = AddBracketsToValueIfNotExist(string.Format("{0}{1}{2}", tokenPrefix, StripLeadingAndTrailingBracketsFromValue(token), tokenSuffix));
                    newTokens.Add(token);
                    target.Add(new ObservablePair<string, string>(token, string.Empty));
                }
            }

            foreach(var observablePair in target)
            {
                observablePair.Key = observablePair.Key.Replace(" ", "");
            }
        }

        public static void OutputsToEnvironment(IExecutionEnvironment innerEnvironment, IExecutionEnvironment environment, string outputDefs)
        {
            try
            {


                var outputs = DataListFactory.CreateOutputParser().Parse(outputDefs);

                IRecordSetCollection outputRecSets = DataListFactory.CreateRecordSetCollection(outputs, true);

                IList<IDev2Definition> outputScalarList = DataListFactory.CreateScalarList(outputs, true);

                foreach (var recordSetDefinition in outputRecSets.RecordSets)
                {
                    var outPutRecSet = outputs.FirstOrDefault(definition => definition.IsRecordSet && definition.RecordSetName == recordSetDefinition.SetName);
                    if (outPutRecSet != null)
                    {
                        var startIndex = 0;
                        var recordSetName = recordSetDefinition.SetName;
                        if(environment.HasRecordSet(recordSetName))
                        {
                            startIndex = environment.GetLength(recordSetName);
                        }
                        foreach (var outputColumnDefinitions in recordSetDefinition.Columns)
                        {

                            var correctRecSet = "[[" + outputColumnDefinitions.RecordSetName + "(*)." + outputColumnDefinitions.Name + "]]";
                            var warewolfEvalResult = innerEnvironment.Eval(correctRecSet);
                            if (warewolfEvalResult.IsWarewolfAtomListresult)
                            {
                                var recsetResult = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;
                                if (outPutRecSet.IsRecordSet)
                                {
                                    var enRecordsetIndexType = GetRecordsetIndexType(outputColumnDefinitions.RawValue);
                                    if (enRecordsetIndexType == enRecordsetIndexType.Star)
                                    {
                                        if (recsetResult != null)
                                        {

                                            environment.EvalAssignFromNestedStar(outputColumnDefinitions.RawValue, recsetResult);
                                        }
                                    }
                                    if (enRecordsetIndexType == enRecordsetIndexType.Blank)
                                    {
                                        if (recsetResult != null)
                                        {
                                            
                                            environment.EvalAssignFromNestedLast(outputColumnDefinitions.RawValue, recsetResult, startIndex);
                                        }
                                    }
                                    if (enRecordsetIndexType == enRecordsetIndexType.Numeric)
                                    {
                                        if (recsetResult != null)
                                        {

                                            environment.EvalAssignFromNestedNumeric(outputColumnDefinitions.RawValue, recsetResult);
                                        }
                                    }

                                }
                            }

                        }
                    }
                }

                foreach (var dev2Definition in outputScalarList)
                {
                    if (!dev2Definition.IsRecordSet)
                    {
                        var warewolfEvalResult = innerEnvironment.Eval(DataListUtils.AddBracketsToValueIfNotExist(dev2Definition.Name));
                        if (warewolfEvalResult.IsWarewolfAtomListresult)
                        {
                            var data = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomListresult;
                            if (data != null && data.Item.Any())
                            {
                                environment.Assign("[[" + dev2Definition.Value + "]]", ExecutionEnvironment.WarewolfAtomToString(data.Item.Last()));
                            }
                        }
                        else
                        {
                            var data = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;
                            if (data != null)
                            {
                                environment.Assign(DataListUtils.AddBracketsToValueIfNotExist(dev2Definition.Value), ExecutionEnvironment.WarewolfAtomToString(data.Item));
                            }
                        }
                    }
                }
            }
            finally
            {
                environment.CommitAssign();
            }

        }

        public static string ReplaceRecordsetBlankWithIndex(string fullRecSetName, int length)
        {
            var blankIndex = fullRecSetName.IndexOf("().", StringComparison.Ordinal);
            if (blankIndex != -1)
            {
                return fullRecSetName.Replace("().", string.Format("({0}).", length));
            }
            return fullRecSetName;
        }
        
        public static string ReplaceRecordsetBlankWithStar(string fullRecSetName)
        {
            var blankIndex = fullRecSetName.IndexOf("().", StringComparison.Ordinal);
            if (blankIndex != -1)
            {
                return fullRecSetName.Replace("().", string.Format("({0}).", "*"));
            }
            return fullRecSetName;
        } 
        
        public static string ReplaceRecordBlankWithStar(string fullRecSetName)
        {
            var blankIndex = fullRecSetName.IndexOf("()", StringComparison.Ordinal);
            if (blankIndex != -1)
            {
                return fullRecSetName.Replace("()", string.Format("({0})", "*"));
            }
            return fullRecSetName;
        }
    }
}
