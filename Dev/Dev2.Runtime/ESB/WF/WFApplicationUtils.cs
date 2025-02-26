
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
using System.Linq;
using System.Threading;
using Dev2.Activities;
using Dev2.Activities.Debug;
using Dev2.Common;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Data.Binary_Objects;
using Dev2.Data.Util;
using Dev2.DataList.Contract;
using Dev2.Diagnostics;
using Dev2.Diagnostics.Debug;
using Dev2.Runtime.Hosting;
// ReSharper disable ReturnTypeCanBeEnumerable.Local
// ReSharper disable ParameterTypeCanBeEnumerable.Local

namespace Dev2.Runtime.ESB.WF
{
    public sealed class WfApplicationUtils
    {
        readonly Action<DebugOutputBase, DebugItem> _add;

        public WfApplicationUtils()
        {
            _add = AddDebugItem;
        }

        public void DispatchDebugState(IDSFDataObject dataObject, StateType stateType, bool hasErrors, string existingErrors, out ErrorResultTO errors, DateTime? workflowStartTime = null, bool interrogateInputs = false, bool interrogateOutputs = false, bool durationVisible=true)
        {
            errors = new ErrorResultTO();
            if(dataObject != null)
            {
                Guid parentInstanceId;
                Guid.TryParse(dataObject.ParentInstanceID, out parentInstanceId);
                bool hasError = dataObject.Environment.HasErrors();
                var errorMessage = String.Empty;
                if(hasError)
                {
                    errorMessage = dataObject.Environment.FetchErrors();
                }
                if(String.IsNullOrEmpty(existingErrors))
                {
                    existingErrors = errorMessage;
                }
                else if(!existingErrors.Contains(errorMessage))
                {
                    existingErrors += Environment.NewLine + errorMessage;
                }
                string name = "localhost";
                Guid remoteID;
                bool hasRemote = Guid.TryParse(dataObject.RemoteInvokerID,out remoteID) ;
                if (hasRemote)
                {
                    var res = ResourceCatalog.Instance.GetResource(GlobalConstants.ServerWorkspaceID, remoteID);
                    if(res!=null)
                        name = remoteID != Guid.Empty ? ResourceCatalog.Instance.GetResource(GlobalConstants.ServerWorkspaceID, remoteID).ResourceName : "localhost";
                }
                var debugState = new DebugState
                {
                    ID = dataObject.OriginalInstanceID,
                    ParentID = parentInstanceId,
                    WorkspaceID = dataObject.WorkspaceID,
                    StateType = stateType,
                    StartTime = workflowStartTime ?? DateTime.Now,
                    EndTime = DateTime.Now,
                    ActivityType = ActivityType.Workflow,
                    DisplayName = dataObject.ServiceName,
                    IsSimulation = dataObject.IsOnDemandSimulation,
                    ServerID = dataObject.ServerID,
                    OriginatingResourceID = dataObject.ResourceID,
                    OriginalInstanceID = dataObject.OriginalInstanceID,
                    Server = name,
                    Version = string.Empty,
                    SessionID = dataObject.DebugSessionID,
                    EnvironmentID = dataObject.DebugEnvironmentId,
                    ClientID = dataObject.ClientID,
                    Name = stateType.ToString(),
                    HasError = hasErrors || hasError,
                    ErrorMessage = existingErrors,
                    IsDurationVisible = durationVisible
                };

                if(interrogateInputs)
                {
                    ErrorResultTO invokeErrors;
                    var defs = DataListUtil.GenerateDefsFromDataListForDebug(FindServiceShape(dataObject.WorkspaceID, dataObject.ResourceID), enDev2ColumnArgumentDirection.Input);
                    var inputs = GetDebugValues(defs, dataObject, out invokeErrors);
                    errors.MergeErrors(invokeErrors);
                    debugState.Inputs.AddRange(inputs);
                }
                if(interrogateOutputs)
                {
                    ErrorResultTO invokeErrors;

                    var defs = DataListUtil.GenerateDefsFromDataListForDebug(FindServiceShape(dataObject.WorkspaceID, dataObject.ResourceID), enDev2ColumnArgumentDirection.Output);
                    var inputs = GetDebugValues(defs, dataObject, out invokeErrors);
                    errors.MergeErrors(invokeErrors);
                    debugState.Outputs.AddRange(inputs);
                }
                if(stateType == StateType.End)
                {
                    debugState.NumberOfSteps = dataObject.NumberOfSteps;
                }

                if(stateType == StateType.Start)
                {
                    debugState.ExecutionOrigin = dataObject.ExecutionOrigin;
                    debugState.ExecutionOriginDescription = dataObject.ExecutionOriginDescription;
                }

                if(dataObject.IsDebugMode() || (dataObject.RunWorkflowAsync && !dataObject.IsFromWebServer))
                {
                    var debugDispatcher = _getDebugDispatcher();
                    if(debugState.StateType == StateType.End)
                    {
                        while(!debugDispatcher.IsQueueEmpty)
                        {
                            Thread.Sleep(100);
                        }
                        debugDispatcher.Write(debugState, dataObject.RemoteInvoke, dataObject.RemoteInvokerID, dataObject.ParentInstanceID, dataObject.RemoteDebugItems);
                    }
                    else
                    {
                        debugDispatcher.Write(debugState);
                    }
                }
            }
        }

        readonly Func<IDebugDispatcher> _getDebugDispatcher = () => DebugDispatcher.Instance;

        List<DebugItem> GetDebugValues(IList<IDev2Definition> values, IDSFDataObject dataObject, out ErrorResultTO errors)
        {
            errors = new ErrorResultTO();
            var results = new List<DebugItem>();
            var added = new List<string>();
            foreach(IDev2Definition dev2Definition in values)
            {


                var defn = GetVariableName(dev2Definition);
                if(added.Any(a => a == defn))
                    continue;

                added.Add(defn);
                DebugItem itemToAdd = new DebugItem();
                _add(new DebugEvalResult(DataListUtil.ReplaceRecordBlankWithStar(defn), "", dataObject.Environment, 0), itemToAdd); //todo:confirm 0
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
                  : String.Format("[[{0}(){1}]]", value.RecordSetName, String.IsNullOrEmpty(value.Name) ? String.Empty : "." + value.Name);
        }

        void AddDebugItem(DebugOutputBase parameters, IDebugItem debugItem)
        {
            var debugItemResults = parameters.GetDebugItemResult();
            debugItem.AddRange(debugItemResults);
        }

        /// <summary>
        /// Finds the service shape.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="resourceId">The ID of the resource</param>
        /// <returns></returns>
        string FindServiceShape(Guid workspaceId, Guid resourceId)
        {
            const string EmptyDataList = "<DataList></DataList>";
            var resource = ResourceCatalog.Instance.GetResource(workspaceId, resourceId);

            if(resource == null)
            {
                return EmptyDataList;
            }

            var serviceShape = resource.DataList.Replace(GlobalConstants.SerializableResourceQuote,"\"").ToString();
            serviceShape = serviceShape.Replace(GlobalConstants.SerializableResourceSingleQuote,"\'");
            return string.IsNullOrEmpty(serviceShape) ? EmptyDataList : serviceShape;
        }
    }
}
