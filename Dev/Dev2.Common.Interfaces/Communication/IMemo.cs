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

namespace Dev2.Common.Interfaces.Communication
{
    public interface IMemo : IEquatable<IMemo>
    {
        Guid InstanceID { get; set; }

        Guid ServerID { get; set; }

        Guid WorkspaceID { get; set; }

        DateTime Date { get; set; }

        string DateString { get; }

        string ToString(ISerializer serializer);
    }
}