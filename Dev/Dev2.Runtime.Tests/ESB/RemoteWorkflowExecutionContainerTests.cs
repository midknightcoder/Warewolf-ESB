
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
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using Dev2.Common;
using Dev2.Data.ServiceModel;
using Dev2.DataList.Contract;
using Dev2.DynamicServices.Objects;
using Dev2.Runtime.Hosting;
using Dev2.Tests.Runtime.XML;
using Dev2.Workspaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Warewolf.Storage;

// ReSharper disable InconsistentNaming
namespace Dev2.Tests.Runtime.ESB
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RemoteWorkflowExecutionContainerTests
    {
        static XElement _connectionXml;
        static Connection _connection;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _connectionXml = XmlResource.Fetch("ServerConnection2");
            _connection = new Connection(_connectionXml);
        }

        #region CTOR

        [TestMethod]
        [TestCategory("RemoteWorkflowExecutionContainer_Constructor")]
        [Description("RemoteWorkflowExecutionContainer cannot be constructed without a resource catalog.")]
        [Owner("Trevor Williams-Ros")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoteWorkflowExecutionContainer_UnitTest_ConstructorWithNullResourceCatalog_ArgumentNullException()
        {
            var sa = new ServiceAction();
            var dataObj = new Mock<IDSFDataObject>();
            var workspace = new Mock<IWorkspace>();
            var esbChannel = new Mock<IEsbChannel>();
            new RemoteWorkflowExecutionContainerMock(sa, dataObj.Object, workspace.Object, esbChannel.Object, null);
        }

        #endregion

        #region Execute

        [TestMethod]
        [TestCategory("RemoteWorkflowExecutionContainer_Execute")]
        [Description("RemoteWorkflowExecutionContainer execute must fetch the connection uri from the resource catalog.")]
        [Owner("Trevor Williams-Ros")]
        public void RemoteWorkflowExecutionContainer_UnitTest_ExecuteWithConnectionInResourceCatalog_ConnectionRetrieved()
        {
            var resourceCatalog = new Mock<IResourceCatalog>();
            resourceCatalog.Setup(c => c.GetResourceContents(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new StringBuilder(_connectionXml.ToString()));

            var container = CreateExecutionContainer(resourceCatalog.Object);

            ErrorResultTO errors;
            container.Execute(out errors, 0);

            Assert.AreEqual(_connection.WebAddress, container.GetRequestUri, "ExecuteGetRequest did not fetch web address from resource catalog connection.");
            Assert.AreEqual(_connection.WebAddress, container.FetchRemoteDebugItemsUri, "FetchRemoteDebugItems did not fetch web address from resource catalog connection.");
        }

        [TestMethod]
        [TestCategory("RemoteWorkflowExecutionContainer_Execute")]
        [Description("RemoteWorkflowExecutionContainer execute must return an error when the connection cannot be retrieved from the resource catalog.")]
        [Owner("Trevor Williams-Ros")]
        public void RemoteWorkflowExecutionContainer_UnitTest_ExecuteWithoutConnectionInCatalog_ConnectionNotRetrieved()
        {
            var resourceCatalog = new Mock<IResourceCatalog>();
            resourceCatalog.Setup(c => c.GetResourceContents(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new StringBuilder());

            var container = CreateExecutionContainer(resourceCatalog.Object);

            ErrorResultTO errors;
            container.Execute(out errors, 0);

            Assert.AreEqual("Server source not found.", errors.MakeDisplayReady(), "Execute did not return an error for a non-existent resource catalog connection.");
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("RemoteWorkflowExecutionContainer_PerformLogExecution")]
        public void RemoteWorkflowExecutionContainer_PerformLogExecution_WhenNoDataListFragments_HasProvidedUriToExecute()
        {
            //------------Setup for test--------------------------
            const string LogUri = "http://localhost:3142/Services?Error=Error";
            var resourceCatalog = new Mock<IResourceCatalog>();
            resourceCatalog.Setup(c => c.GetResourceContents(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new StringBuilder(_connectionXml.ToString()));
            var container = CreateExecutionContainer(resourceCatalog.Object);
            //------------Execute Test---------------------------
            container.PerformLogExecution(LogUri, 0);
            //------------Assert Results-------------------------
            Assert.AreEqual(LogUri, container.LogExecutionUrl);
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("RemoteWorkflowExecutionContainer_PerformLogExecution")]
        public void RemoteWorkflowExecutionContainer_PerformLogExecution_WhenScalarDataListFragments_HasEvaluatedUriToExecute()
        {
            //------------Setup for test--------------------------
            const string LogUri = "http://localhost:1234/Services?Error=[[Err]]";
            const string ExpectedLogUri = "http://localhost:1234/Services?Error=Error Message";
            var resourceCatalog = new Mock<IResourceCatalog>();
            resourceCatalog.Setup(c => c.GetResourceContents(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new StringBuilder(_connectionXml.ToString()));
            var container = CreateExecutionContainer(resourceCatalog.Object, "<DataList><Err/></DataList>", "<root><ADL><Err>Error Message</Err></ADL></root>");
            //------------Execute Test---------------------------
            container.PerformLogExecution(LogUri, 0);
            //------------Assert Results-------------------------
            Assert.AreEqual(ExpectedLogUri, container.LogExecutionUrl);

        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("RemoteWorkflowExecutionContainer_PerformLogExecution")]
        public void RemoteWorkflowExecutionContainer_PerformLogExecution_WhenRecordsetDataListFragments_HasEvaluatedUriToExecute()
        {
            //------------Setup for test--------------------------
            EnvironmentVariables.WebServerUri = "http://localhost:3142";
            const string LogUri = "http://localhost:1234/Services?Error=[[Errors().Err]]";
            const string ExpectedLogUri = "http://localhost:1234/Services?Error=Error Message";
            var resourceCatalog = new Mock<IResourceCatalog>();
            resourceCatalog.Setup(c => c.GetResourceContents(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new StringBuilder(_connectionXml.ToString()));
            var container = CreateExecutionContainer(resourceCatalog.Object, "<DataList><Errors><Err></Err></Errors></DataList>", "<root><ADL><Errors><Err>Error Message</Err></Errors></ADL></root>");
            //------------Execute Test---------------------------
            container.PerformLogExecution(LogUri, 0);
            //------------Assert Results-------------------------
            Assert.AreEqual(ExpectedLogUri, container.LogExecutionUrl);
        }
        #endregion

        #region CreateExecutionContainer

        static RemoteWorkflowExecutionContainerMock CreateExecutionContainer(IResourceCatalog resourceCatalog, string dataListShape = "<DataList></DataList>", string dataListData = "")
        {

            var dataObj = new Mock<IDSFDataObject>();
            dataObj.Setup(d => d.EnvironmentID).Returns(_connection.ResourceID);
            dataObj.Setup(d => d.ServiceName).Returns("Test");
            dataObj.Setup(d => d.RemoteInvokeResultShape).Returns(new StringBuilder("<ADL><NumericGUID></NumericGUID></ADL>"));
            dataObj.Setup(d => d.Environment).Returns(new ExecutionEnvironment());
            ExecutionEnvironmentUtils.UpdateEnvironmentFromXmlPayload(dataObj.Object,new StringBuilder(dataListData),dataListShape, 0);
            var sa = new ServiceAction();
            var workspace = new Mock<IWorkspace>();
            var esbChannel = new Mock<IEsbChannel>();

            var container = new RemoteWorkflowExecutionContainerMock(sa, dataObj.Object, workspace.Object, esbChannel.Object, resourceCatalog)
            {
                GetRequestRespsonse = "<DataList><NumericGUID>74272317-2264-4564-3988-700350008298</NumericGUID></DataList>"
            };
            return container;
        }

        #endregion
    }
}
