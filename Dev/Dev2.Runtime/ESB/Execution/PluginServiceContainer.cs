
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
using Dev2.DataList.Contract;
using Dev2.DynamicServices.Objects;
using Dev2.Services.Execution;
using Dev2.Workspaces;
using Warewolf.Storage;

namespace Dev2.Runtime.ESB.Execution
{
    // BUG 9619 - 2013.06.05 - TWR - Refactored
    public class PluginServiceContainer : EsbExecutionContainer
    {
        readonly IServiceExecution _pluginServiceExecution;

        public PluginServiceContainer(ServiceAction sa, IDSFDataObject dataObj, IWorkspace theWorkspace, IEsbChannel esbChannel)
            : base(sa, dataObj, theWorkspace, esbChannel)
        {
            _pluginServiceExecution = new PluginServiceExecution(dataObj, true);
        }

        public PluginServiceContainer(IServiceExecution pluginServiceExecution)
        {
            _pluginServiceExecution = pluginServiceExecution;
        }

        public override Guid Execute(out ErrorResultTO errors, int update)
        {
            _pluginServiceExecution.InstanceInputDefinitions = InstanceInputDefinition;
            _pluginServiceExecution.InstanceOutputDefintions = InstanceOutputDefinition;

            var result = _pluginServiceExecution.Execute(out errors, update);
            return result;
        }

        public override IExecutionEnvironment Execute(IDSFDataObject inputs, IDev2Activity activity)
        {
            return null;
        }
    }
}
