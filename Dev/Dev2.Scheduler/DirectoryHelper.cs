
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Dev2.Common.Interfaces.Scheduler.Interfaces;

namespace Dev2.Scheduler
{
    [ExcludeFromCodeCoverage] // not required for code coverage this is simply a pass through required for unit testing
    public class DirectoryHelper : IDirectoryHelper
    {
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string CreateIfNotExists(string debugOutputPath)
        {
            if(!Directory.Exists(debugOutputPath))
            {
                return Directory.CreateDirectory(debugOutputPath).Name;
            }

            return debugOutputPath;
        }
    }
}
