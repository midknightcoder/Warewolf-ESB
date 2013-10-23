﻿using Dev2;
using Dev2.Common;
using Dev2.Data.Binary_Objects;
using Dev2.Data.Decision;
using Dev2.DataList.Contract;
using Dev2.DataList.Contract.Binary_Objects;
using Dev2.Diagnostics;
using Dev2.DynamicServices;
using Microsoft.VisualBasic.Activities;
using Microsoft.VisualStudio.TestTools.UnitTesting;using System.Diagnostics.CodeAnalysis;
using Moq;
using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Unlimited.Framework;
using Dev2.Runtime.ESB.Execution;

// ReSharper disable CheckNamespace
namespace ActivityUnitTests
// ReSharper restore CheckNamespace
{
    [TestClass][ExcludeFromCodeCoverage]
    public class BaseActivityUnitTest
    {

        public IEsbWorkspaceChannel DsfChannel;
        public IFrameworkSecurityContext SecurityContext;
        public Uri DsfAdddress = new Uri("http://localhost:77/dsf");
        public Mock<IEsbWorkspaceChannel> MockChannel;
        public IDataListCompiler Compiler;

        

        public BaseActivityUnitTest()
        {
            CallBackData = "Default Data";
            TestStartNode = new FlowStep
            {
                Action = new DsfCommentActivity()
            };
        }

        public Guid ExecutionID { get; set; }

        public dynamic TestData { get; set; }

        public dynamic CurrentDl { get; set; }

        public FlowStep TestStartNode { get; set; }

        public string CallBackData { get; set; }

        public DynamicActivity FlowchartProcess
        {
            get
            {
                var activity = new DynamicActivity { Implementation = () => FlowchartActivityBuilder.Implementation };
                foreach (DynamicActivityProperty prop in FlowchartActivityBuilder.Properties)
                {
                    activity.Properties.Add(prop);
                }

                return activity;
            }
        }

        public ActivityBuilder FlowchartActivityBuilder
        {
            get
            {
                var builder = new ActivityBuilder
                {
                    //Name = _workflowModel.ResourceName,
                    //Name = _workflowModel.ResourceName, //string.Format("{0}.{1}.{2}.{3}", _workflowModel.Module, _workflowModel.SubModule, _workflowModel.Action, _workflowModel.ResourceName),
                    Properties = {
                    new DynamicActivityProperty{Name = "AmbientDataList",Type = typeof(InOutArgument<List<string>>)}
                    ,new DynamicActivityProperty{ Name = "ParentWorkflowInstanceId", Type = typeof(InOutArgument<Guid>)}
                    ,new DynamicActivityProperty{ Name = "ParentServiceName", Type = typeof(InOutArgument<string>)}
                },
                    Implementation = new Flowchart
                    {
                        //DisplayName = _workflowModel.ResourceName,//string.Format("{0}.{1}.{2}.{3}",_workflowModel.Module, _workflowModel.SubModule, _workflowModel.Action, _workflowModel.ResourceName),
                        Variables = {
                         new Variable<List<string>>{Name = "InstructionList"},
                         new Variable<string>{Name = "LastResult"},
                         new Variable<bool>{Name = "HasError"},
                         new Variable<string>{Name = "ExplicitDataList"},
                         new Variable<bool>{Name = "IsValid"},
                         new Variable<UnlimitedObject>{Name = "d"},
                         new Variable<Util>{ Name = "t"},
                         new Variable<Dev2DataListDecisionHandler>{Name = "Dev2DecisionHandler"}

                        }
                        ,
                        StartNode = TestStartNode
                    }
                };


                var vbs = new VisualBasicSettings
                {
                    ImportReferences =     {
                        new VisualBasicImportReference {
                            Assembly = "Unlimited.Framework",
                            Import = "Unlimited.Framework"
                        },
                        new VisualBasicImportReference{
                             Assembly = "Unlimited.Applications.BusinessDesignStudio.Activities",
                             Import = "Unlimited.Applications.BusinessDesignStudio.Activities"

                        }
                    }
                };


                VisualBasic.SetSettings(builder, vbs);

                return builder;
            }

        }

        public dynamic ExecuteProcess(DsfDataObject dataObject = null,bool isDebug = false,IEsbChannel channel=null,bool isRemoteInvoke = false)
        {

            var svc = new ServiceAction { Name = "TestAction", ServiceName = "UnitTestService" };
            svc.SetActivity(FlowchartProcess);
            Mock<IEsbChannel> mockChannel = new Mock<IEsbChannel>();

            if (CurrentDl == null)
            {
                CurrentDl = TestData;
            }

            var errors = new ErrorResultTO();
            if (ExecutionID == Guid.Empty)
            {
                Compiler = DataListFactory.CreateDataListCompiler();

                ExecutionID = Compiler.ConvertTo(DataListFormat.CreateFormat(GlobalConstants._XML), TestData, CurrentDl, out errors);
                if(dataObject != null)
                {
                    dataObject.DataListID = ExecutionID;                    
                }
                
            }

            if (errors.HasErrors())
            {
                string errorString = errors.FetchErrors().Aggregate(string.Empty, (current, item) => current + item);

                throw new Exception(errorString);
            }

            if (dataObject == null)
            {

                dataObject = new DsfDataObject(CurrentDl, ExecutionID)
                {
                    // NOTE: WorkflowApplicationFactory.InvokeWorkflowImpl() will use HostSecurityProvider.Instance.ServerID 
                    //       if this is NOT provided which will cause the tests to fail!
                    ServerID = Guid.NewGuid()
                };

            }
            dataObject.IsDebug = isDebug;

            // we now need to set a thread ID ;)
            dataObject.ParentThreadID = 1; 

            if (isRemoteInvoke)
            {
                dataObject.RemoteInvoke = true;
                dataObject.RemoteInvokerID = Guid.NewGuid().ToString();
            }

            var esbChannel = mockChannel.Object;
            if(channel != null)
            {
                esbChannel = channel;
            }
            WfExecutionContainer wfec = new WfExecutionContainer(svc, dataObject, Dev2.Workspaces.WorkspaceRepository.Instance.ServerWorkspace, esbChannel);

            errors.ClearErrors();
            dataObject.DataListID = wfec.Execute(out errors);
            

            return dataObject;
        }


        #region ForEach Execution

        /// <summary>
        /// The ForEach Activity requires the data returned from an activity
        /// We will mock the DSF channel to return something that we expect is shaped.
        /// </summary>
        /// <returns></returns>
        public Mock<IEsbChannel> ExecuteForEachProcess(out IDSFDataObject dataObject)
        {
            var svc = new ServiceAction { Name = "ForEachTestAction", ServiceName = "UnitTestService" };
            var mockChannel = new Mock<IEsbChannel>();
            svc.SetActivity(FlowchartProcess);

            if (CurrentDl == null)
            {
                CurrentDl = TestData;
            }

            Compiler = DataListFactory.CreateDataListCompiler();
            ErrorResultTO errors;
            Guid exID = Compiler.ConvertTo(DataListFormat.CreateFormat(GlobalConstants._XML), TestData, CurrentDl, out errors);


            if (errors.HasErrors())
            {
                string errorString = errors.FetchErrors().Aggregate(string.Empty, (current, item) => current + item);

                throw new Exception(errorString);
            }

            dataObject = new DsfDataObject(CurrentDl, exID)
            {
                // NOTE: WorkflowApplicationFactory.InvokeWorkflowImpl() will use HostSecurityProvider.Instance.ServerID 
                //       if this is NOT provided which will cause the tests to fail!
                ServerID = Guid.NewGuid()
            };


            // we need to set this now ;)
            dataObject.ParentThreadID = 1;

            mockChannel.Setup(c=>c.ExecuteSubRequest(It.IsAny<IDSFDataObject>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), out errors)).Verifiable();

            WfExecutionContainer wfec = new WfExecutionContainer(svc, dataObject, Dev2.Workspaces.WorkspaceRepository.Instance.ServerWorkspace, mockChannel.Object);

            errors.ClearErrors();
            dataObject.DataListID = wfec.Execute(out errors);

            return mockChannel;
        }

        #endregion ForEach Execution

        #region Activity Debug Input/Output Test Methods

        public dynamic CheckActivityDebugInputOutput<T>(DsfNativeActivity<T> activity, string dataListShape,string dataListWithData, out List<DebugItem> inputResults,out List<DebugItem> outputResults, bool isRemoteInvoke = false)
        {

                ErrorResultTO errors;
                TestStartNode = new FlowStep
                {
                    Action = activity
                };

                TestData = dataListWithData;
                CurrentDl = dataListShape;

                Compiler = DataListFactory.CreateDataListCompiler();
                ExecutionID = Compiler.ConvertTo(DataListFormat.CreateFormat(GlobalConstants._XML), TestData, CurrentDl, out errors);
                IBinaryDataList dl = Compiler.FetchBinaryDataList(ExecutionID, out errors);

                var result = ExecuteProcess(null, true, null, isRemoteInvoke);
                inputResults = activity.GetDebugInputs(dl);
                outputResults = activity.GetDebugOutputs(dl);

                return result;
            }

        public dynamic CheckPathOperationActivityDebugInputOutput<T>(DsfNativeActivity<T> activity, string dataListShape,
                                                  string dataListWithData, out List<DebugItem> inputResults, out List<DebugItem> outputResults)
        {
                ErrorResultTO errors;
                TestStartNode = new FlowStep
                {
                    Action = activity
                };

                TestData = dataListWithData;
                CurrentDl = dataListShape;

                Compiler = DataListFactory.CreateDataListCompiler();
                ExecutionID = Compiler.ConvertTo(DataListFormat.CreateFormat(GlobalConstants._XML), TestData, CurrentDl, out errors);                
                IBinaryDataList dl = Compiler.FetchBinaryDataList(ExecutionID, out errors);
                var result = ExecuteProcess(null, true);
                inputResults = activity.GetDebugInputs(dl);
                outputResults = activity.GetDebugOutputs(dl);

                return result;
            }

        public bool CreateDataListWithRecsetAndCreateShape(List<string> recsetData,string recsetName,string fieldName,out string dataListShape,out string dataListWithData)
        {
            bool result = false;

            dataListShape = "<ADL>";
            dataListWithData = "<ADL>";
            #region Create DataList With Data
            
            foreach (string rowData in recsetData)
            {
                dataListWithData = string.Concat(dataListWithData, "<", recsetName, ">", "<", fieldName, ">", rowData, "</", fieldName, ">", "</", recsetName, ">");
            }
            dataListWithData = string.Concat(dataListWithData, "<res></res>", "</ADL>");
            
            #endregion

            #region Create Shape

            dataListShape = string.Concat(dataListShape, "<", recsetName, ">", "<", fieldName, ">", "</", fieldName, ">", "</", recsetName, ">", "<res></res></ADL>");

            #endregion

            return result;
        }

        public bool CreateDataListWithMultipleRecsetAndCreateShape(List<List<string>> recsetData, List<string> recsetName, List<string> fieldName, out string dataListShape, out string dataListWithData)
        {
            bool result = false;

            dataListShape = "<ADL>";
            dataListWithData = "<ADL>";
            #region Create DataList With Data

            for(int i = 0; i < recsetData.Count; i++)
            {
                foreach (string rowData in recsetData[i])
                {
                    dataListWithData = string.Concat(dataListWithData, "<", recsetName[i], ">", "<", fieldName[i], ">", rowData, "</", fieldName[i], ">", "</", recsetName[i], ">");
                }
                dataListShape = string.Concat(dataListShape, "<", recsetName[i], ">", "<", fieldName[i], ">", "</", fieldName[i], ">", "</", recsetName[i], ">");
            }
            
            dataListWithData = string.Concat(dataListWithData, "<res></res>", "</ADL>");

            #endregion

            #region Create Shape

            dataListShape = string.Concat(dataListShape, "<res></res></ADL>");

            #endregion

            return result;
        }
        #endregion

        #region Retrieve DataList Values

        public bool GetScalarValueFromDataList(Guid dataListId, string fieldToRetrieve, out string result, out string error)
        {
            ErrorResultTO errorResult;
            IBinaryDataListEntry entry;
            bool fine = Compiler.FetchBinaryDataList(dataListId, out errorResult).TryGetEntry(fieldToRetrieve, out entry, out error);
            if (entry != null && fine)
            {
                IBinaryDataListItem item = entry.FetchScalar();
                result = item.TheValue;
            }
            else
            {
                result = string.Empty;
            }

            return true;
        }

        public bool GetRecordSetFieldValueFromDataList(Guid dataListId, string recordSet, string fieldNameToRetrieve, out IList<IBinaryDataListItem> result, out string error)
        {
            IList<IBinaryDataListItem> dLItems = new List<IBinaryDataListItem>();
            ErrorResultTO errorResult;
            IBinaryDataListEntry entry;
            bool isCool = true;
            IBinaryDataList bdl = Compiler.FetchBinaryDataList(dataListId, out errorResult);
            bdl.TryGetEntry(recordSet, out entry, out error);

            IIndexIterator idxItr = entry.FetchRecordsetIndexes();
            while (idxItr.HasMore())
            {
                var fetchNextIndex = idxItr.FetchNextIndex();
                dLItems.Add(entry.TryFetchRecordsetColumnAtIndex(fieldNameToRetrieve, fetchNextIndex, out error).Clone());

            }

            result = dLItems;

            if (!string.IsNullOrEmpty(error))
            {
                isCool = false;
            }

            return isCool;
        }

        protected List<string> RetrieveAllRecordSetFieldValues(Guid dataListId, string recordSetName, string fieldToRetrieve, out string error)
        {
            var retVals = new List<string>();
            IList<IBinaryDataListItem> dataListItems;
            if (GetRecordSetFieldValueFromDataList(dataListId, recordSetName, fieldToRetrieve, out dataListItems, out error))
            {
                retVals.AddRange(dataListItems.Select(item => item.TheValue));
            }
            return retVals;
        }

        IDataListCompiler dlc = DataListFactory.CreateDataListCompiler();
        protected void DataListRemoval(Guid dlID)
        {
            dlc.ForceDeleteDataListByID(dlID);
        }

        #endregion Retrieve DataList Values
    }
}
