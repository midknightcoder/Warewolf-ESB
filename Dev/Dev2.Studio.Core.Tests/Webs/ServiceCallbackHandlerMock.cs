
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
using Caliburn.Micro;
using Dev2.Studio.Core.Interfaces;
using Dev2.Studio.Core.Workspaces;
using Dev2.Webs.Callbacks;
using Newtonsoft.Json.Linq;

namespace Dev2.Core.Tests.Webs
{
    public class ServiceCallbackHandlerMock : ServiceCallbackHandler
    {
        public ServiceCallbackHandlerMock(IEventAggregator eventPublisher, IEnvironmentRepository environmentRepository, IShowDependencyProvider provider)
            : base(eventPublisher, environmentRepository, provider)
        {
        }

        public void TestSave(IEnvironmentModel environmentModel, JObject jsonObj)
        {
            base.Save(environmentModel, jsonObj);
        }

        public void TestCheckForServerMessages(IEnvironmentModel environmentModel, Guid id, IWorkspaceItemRepository workspace)
        {
            CheckForServerMessages(environmentModel, id, workspace);
        }
    }
}
