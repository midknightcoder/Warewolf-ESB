
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
using System.Collections.Specialized;
using System.Security.Principal;
using Dev2.Common;
using Dev2.Runtime.WebServer;
using Dev2.Runtime.WebServer.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dev2.Tests.Runtime.WebServer
{
    [TestClass]
    public class WebGetRequestHandlerTest
    {

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("WebGetRequestHandler_ProcessRequest")]
        public void WebGetRequestHandler_ProcessRequest_WhenValidUserContext_ExpectExecution()
        {
            //------------Setup for test--------------------------
            Mock<IPrincipal> principle = new Mock<IPrincipal>();
            principle.Setup(p => p.Identity.Name).Returns("FakeUser");
            principle.Setup(p => p.Identity.Name).Verifiable();

            Mock<ICommunicationContext> ctx = new Mock<ICommunicationContext>();
            NameValueCollection boundVariables = new NameValueCollection { { "servicename", "ping" }, { "instanceid", "" }, { "bookmark", "" } };
            NameValueCollection queryString = new NameValueCollection { { GlobalConstants.DLID, Guid.Empty.ToString() }, { "wid", Guid.Empty.ToString() } };
            ctx.Setup(c => c.Request.BoundVariables).Returns(boundVariables);
            ctx.Setup(c => c.Request.QueryString).Returns(queryString);
            ctx.Setup(c => c.Request.Uri).Returns(new Uri("http://localhost"));
            ctx.Setup(c => c.Request.User).Returns(principle.Object);

            var webGetRequestHandler = new WebGetRequestHandler();

            //------------Execute Test---------------------------
            webGetRequestHandler.ProcessRequest(ctx.Object);

            //------------Assert Results-------------------------
            principle.Verify(p => p.Identity.Name, Times.AtLeast(1));
        }


        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("WebGetRequestHandler_ProcessRequest")]
        public void WebGetRequestHandler_ProcessRequest_WhenValidUserContextWhenNullDataListID_ExpectExecution()
        {
            //------------Setup for test--------------------------
            Mock<IPrincipal> principle = new Mock<IPrincipal>();
            principle.Setup(p => p.Identity.Name).Returns("FakeUser");
            principle.Setup(p => p.Identity.Name).Verifiable();

            Mock<ICommunicationContext> ctx = new Mock<ICommunicationContext>();
            NameValueCollection boundVariables = new NameValueCollection { { "servicename", "ping" }, { "instanceid", "" }, { "bookmark", "" } };
            NameValueCollection queryString = new NameValueCollection { { GlobalConstants.DLID, null }, { "wid", Guid.Empty.ToString() } };
            ctx.Setup(c => c.Request.BoundVariables).Returns(boundVariables);
            ctx.Setup(c => c.Request.QueryString).Returns(queryString);
            ctx.Setup(c => c.Request.Uri).Returns(new Uri("http://localhost"));
            ctx.Setup(c => c.Request.User).Returns(principle.Object);

            var webGetRequestHandler = new WebGetRequestHandler();

            //------------Execute Test---------------------------
            webGetRequestHandler.ProcessRequest(ctx.Object);

            //------------Assert Results-------------------------
            principle.Verify(p => p.Identity.Name, Times.AtLeast(1));
        }
    }
}
