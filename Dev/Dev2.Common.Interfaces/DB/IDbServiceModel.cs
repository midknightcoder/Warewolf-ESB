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
using System.Data;
using Dev2.Common.Interfaces.ServerProxyLayer;

namespace Dev2.Common.Interfaces.DB
{
    public interface IDbServiceModel
    {
        ICollection<IDbSource> RetrieveSources();
        ICollection<IDbAction> GetActions(IDbSource source);
        void CreateNewSource();
        void EditSource(IDbSource selectedSource);
        DataTable TestService(IDatabaseService inputValues);
        IEnumerable<IServiceOutputMapping> GetDbOutputMappings(IDbAction action);
        void SaveService(IDatabaseService toModel);
    }
}
