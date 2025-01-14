
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
using System.Text;
using Dev2.Common;
using Dev2.Communication;
using Dev2.Data;
using Dev2.Data.Util;
using Dev2.DataList.Contract;
using Dev2.DynamicServices.Objects;
using Dev2.Runtime.ESB.Management;
using Dev2.Workspaces;
using Warewolf.Storage;

namespace Dev2.Runtime.ESB.Execution
{
    /// <summary>
    /// Execute an internal or management service
    /// </summary>
    public class InternalServiceContainer : EsbExecutionContainer
    {

        public InternalServiceContainer(ServiceAction sa, IDSFDataObject dataObj, IWorkspace theWorkspace, IEsbChannel esbChannel, EsbExecuteRequest request)
            : base(sa, dataObj, theWorkspace, esbChannel, request)
        {
            var dataListTO = new DataListTO(sa.DataListSpecification.ToString());
            if(request.Args == null)
            {
                request.Args = new Dictionary<string, StringBuilder>();
                foreach(var input in dataListTO.Inputs)
                {
                    var warewolfEvalResult = dataObj.Environment.Eval(DataListUtil.AddBracketsToValueIfNotExist(input),0);
                    if(warewolfEvalResult.IsWarewolfAtomResult)
                    {
                        var scalarResult = warewolfEvalResult as WarewolfDataEvaluationCommon.WarewolfEvalResult.WarewolfAtomResult;
                        if(scalarResult != null && !scalarResult.Item.IsNothing)
                        {
                            request.Args.Add(input, new StringBuilder(scalarResult.Item.ToString()));
                        }
                    }
                }
            }
        }

        public override Guid Execute(out ErrorResultTO errors, int update)
        {
            errors = new ErrorResultTO();
            var invokeErrors = new ErrorResultTO();
            Guid result = GlobalConstants.NullDataListID;

            try
            {
                EsbManagementServiceLocator emsl = new EsbManagementServiceLocator();
                IEsbManagementEndpoint eme = emsl.LocateManagementService(ServiceAction.Name);

                if(eme != null)
                {
                    // Web request for internal service ;)
                    if(Request.Args == null)
                    {
                        GenerateRequestDictionaryFromDataObject(out invokeErrors);
                        errors.MergeErrors(invokeErrors);
                    }

                    var res = eme.Execute(Request.Args, TheWorkspace);
                    Request.ExecuteResult = res;
                    errors.MergeErrors(invokeErrors);
                    result = DataObject.DataListID;
                    Request.WasInternalService = true;
                }
                else
                {
                    errors.AddError("Could not locate management service [ " + ServiceAction.ServiceName + " ]");
                }
            }
            catch(Exception ex)
            {
                errors.AddError(ex.Message);
            }

            return result;
        }

        public override IExecutionEnvironment Execute(IDSFDataObject inputs, IDev2Activity activity)
        {
            return null;
        }

        private void GenerateRequestDictionaryFromDataObject(out ErrorResultTO errors)
        {
            errors = null;
            Request.Args = new Dictionary<string, StringBuilder>();            
        }
    }
}
