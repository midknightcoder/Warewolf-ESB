
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
using Dev2.Activities;
using Dev2.Activities.Debug;
using Dev2.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.DataList.Contract;
using Dev2.Diagnostics;
using Dev2.DynamicServices.Objects;
using Dev2.Runtime.ESB.WF;
using Dev2.Runtime.Execution;
using Dev2.Runtime.Hosting;
using Dev2.Runtime.Security;
using Dev2.Workspaces;
using Warewolf.Storage;

namespace Dev2.Runtime.ESB.Execution
{
    public class WfExecutionContainer : EsbExecutionContainer
    {
          

        // BUG 9304 - 2013.05.08 - TWR - Added IWorkflowHelper parameter to facilitate testing
        public WfExecutionContainer(ServiceAction sa, IDSFDataObject dataObj, IWorkspace theWorkspace, IEsbChannel esbChannel)
            : base(sa, dataObj, theWorkspace, esbChannel)
        {
        }

        /// <summary>
        /// Executes the specified errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="update"></param>
        /// <returns></returns>
        public override Guid Execute(out ErrorResultTO errors, int update)
        {
            errors = new ErrorResultTO();
           // WorkflowApplicationFactory wfFactor = new WorkflowApplicationFactory();
            Guid result = GlobalConstants.NullDataListID;


            Dev2Logger.Log.Debug("Entered Wf Container");

            // Set Service Name
            DataObject.ServiceName = ServiceAction.ServiceName;
           
            // Set server ID, only if not set yet - original server;
            if(DataObject.ServerID == Guid.Empty)
                DataObject.ServerID = HostSecurityProvider.Instance.ServerID;

            // Set resource ID, only if not set yet - original resource;
            if(DataObject.ResourceID == Guid.Empty && ServiceAction != null && ServiceAction.Service != null)
                DataObject.ResourceID = ServiceAction.Service.ID;

            // Travis : Now set Data List
            DataObject.DataList = ServiceAction.DataListSpecification;
            // Set original instance ID, only if not set yet - original resource;
            if(DataObject.OriginalInstanceID == Guid.Empty)
                DataObject.OriginalInstanceID = DataObject.DataListID;
            Dev2Logger.Log.Info(String.Format("Started Execution for Service Name:{0} Resource Id:{1} Mode:{2}",DataObject.ServiceName,DataObject.ResourceID,DataObject.IsDebug?"Debug":"Execute"));
            //Set execution origin
            if(!string.IsNullOrWhiteSpace(DataObject.ParentServiceName))
            {
                DataObject.ExecutionOrigin = ExecutionOrigin.Workflow;
                DataObject.ExecutionOriginDescription = DataObject.ParentServiceName;
            }
            else if(DataObject.IsDebug)
            {
                DataObject.ExecutionOrigin = ExecutionOrigin.Debug;
            }
            else
            {
                DataObject.ExecutionOrigin = ExecutionOrigin.External;
            }

            try
            {
                // BUG 9304 - 2013.05.08 - TWR - Added CompileExpressions
                //_workflowHelper.CompileExpressions(theActivity,DataObject.ResourceID);

                //IDSFDataObject exeResult = wfFactor.InvokeWorkflow(activity.Value, DataObject,
                //                                                   new List<object> { EsbChannel, }, instanceId,
                //                                                   TheWorkspace, bookmark, out errors);
                var wfappUtils = new WfApplicationUtils();
                IExecutionToken exeToken = new ExecutionToken { IsUserCanceled = false };
                DataObject.ExecutionToken = exeToken;
                ErrorResultTO invokeErrors;
                if (DataObject.IsDebugMode())
                {
                    wfappUtils.DispatchDebugState(DataObject, StateType.Start, DataObject.Environment.HasErrors(), DataObject.Environment.FetchErrors(), out invokeErrors, DateTime.Now, true,false,false);
                }
                Eval(DataObject.ResourceID, DataObject);
                if (DataObject.IsDebugMode())
                {
                    wfappUtils.DispatchDebugState(DataObject, StateType.End, DataObject.Environment.HasErrors(), DataObject.Environment.FetchErrors(), out invokeErrors, DataObject.StartTime, false, true);
                }
                result = DataObject.DataListID;
            }
            catch(InvalidWorkflowException iwe)
            {
                Dev2Logger.Log.Error(iwe);
                var msg = iwe.Message;

                int start = msg.IndexOf("Flowchart ", StringComparison.Ordinal);

                // trap the no start node error so we can replace it with something nicer ;)
                errors.AddError(start > 0 ? GlobalConstants.NoStartNodeError : iwe.Message);
            }
            catch(Exception ex)
            {
                Dev2Logger.Log.Error(ex);
                errors.AddError(ex.Message);
            }
            Dev2Logger.Log.Info(String.Format("Completed Execution for Service Name:{0} Resource Id: {1} Mode:{2}",DataObject.ServiceName,DataObject.ResourceID,DataObject.IsDebug?"Debug":"Execute"));
            return result;
        }

        public void Eval(Guid resourceID, IDSFDataObject dataObject)
        {
            Dev2Logger.Log.Debug("Getting Resource to Execute");
            IDev2Activity resource = ResourceCatalog.Instance.Parse(TheWorkspace.ID, resourceID);
            Dev2Logger.Log.Debug("Got Resource to Execute");
            EvalInner(dataObject, resource, dataObject.ForEachUpdateValue);

        }
        

        public override IExecutionEnvironment Execute(IDSFDataObject inputs, IDev2Activity activity)
        {
            return null;
        }

        public List<DebugItem> GetDebugInputs(IList<IDev2Definition> inputs,  ErrorResultTO errors)
        {
            if(errors == null)
            {
                throw new ArgumentNullException("errors");
            }

            var results = new List<DebugItem>();
            foreach(IDev2Definition dev2Definition in inputs)
            {
                var variableName = GetVariableName(dev2Definition);
                DebugItem itemToAdd = new DebugItem();
                AddDebugItem(new DebugEvalResult(variableName, "", DataObject.Environment, 0), itemToAdd); //todo:confirm
                results.Add(itemToAdd);
            }

            foreach(IDebugItem debugInput in results)
            {
                debugInput.FlushStringBuilder();
            }

            return results;
        }
        string GetVariableName(IDev2Definition value)
        {
            return String.IsNullOrEmpty(value.RecordSetName)
                  ? String.Format("[[{0}]]", value.Name)
                  : String.Format("[[{0}]]", value.RecordSetName);
        }
        void AddDebugItem(DebugOutputBase parameters, IDebugItem debugItem)
        {
            var debugItemResults = parameters.GetDebugItemResult();
            debugItem.AddRange(debugItemResults);
        }

        public void Eval(DynamicActivity flowchartProcess, IDSFDataObject dsfDataObject,int update)
        {
            IDev2Activity resource = new ActivityParser().Parse(flowchartProcess);

            EvalInner(dsfDataObject, resource, update);
        }

        static void EvalInner(IDSFDataObject dsfDataObject, IDev2Activity resource,int update)
        {
            if(resource == null)
            {
                return;
            }
            var next = resource.Execute(dsfDataObject, update);
            while(next != null)
            {
                next = next.Execute(dsfDataObject, update);
            }
        }
    }
}
