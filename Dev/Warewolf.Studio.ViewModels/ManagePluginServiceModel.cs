﻿
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
using Dev2;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.DB;
using Warewolf.Core;

namespace Warewolf.Studio.ViewModels
{
    public class ManagePluginServiceModel: IPluginServiceModel
    {
        readonly IStudioUpdateManager _updateRepository;
        readonly IQueryManager _queryProxy;
        //readonly IShellViewModel _shell;
        readonly string _serverName;

        #region Implementation of IDbServiceModel

        public ManagePluginServiceModel(IStudioUpdateManager updateRepository, IQueryManager queryProxy, string serverName)
        {
            VerifyArgument.AreNotNull(new Dictionary<string, object>
            {
                { "updateRepository", updateRepository }, 
                { "queryProxy", queryProxy }, 
                //{ "shell", shell } ,
                {"serverName",serverName}
            });
            _updateRepository = updateRepository;
            _queryProxy = queryProxy;
            //_shell = shell;
            _serverName = serverName;

        }

        public ICollection<IPluginSource> RetrieveSources()
        {

            return _queryProxy.FetchPluginSources();

        }

        public ICollection<IPluginAction> GetActions(IPluginSource source, INamespaceItem ns)
        {
            return _queryProxy.PluginActions(source,ns);
        }

        public ICollection<INamespaceItem> GetNameSpaces(IPluginSource source)
        {
            return _queryProxy.FetchNamespaces(source);
        }

        public void CreateNewSource()
        {
            //_shell.NewResource(ResourceType.PluginSource, Guid.NewGuid());
        }

        public void EditSource(IPluginSource selectedSource)
        {
            //_shell.EditResource(selectedSource);

        }

        public  string TestService(IPluginService inputValues)
        {
           return _updateRepository.TestPluginService(inputValues);          
        }

        public IEnumerable<IServiceOutputMapping> GetPluginOutputMappings(IPluginAction action)
        {
            return new List<IServiceOutputMapping> { new ServiceOutputMapping("bob", "The"), new ServiceOutputMapping("dora", "The"), new ServiceOutputMapping("Tree", "The") }; 
        }

        public void SaveService(IPluginService toModel)
        {
      
            _updateRepository.Save(toModel);
        }

        #endregion
    }
}
