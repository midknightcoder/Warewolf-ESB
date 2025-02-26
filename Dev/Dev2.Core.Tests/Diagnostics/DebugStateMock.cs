
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
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Diagnostics.Debug;

namespace Dev2.Tests.Diagnostics
{
    public class DebugStateMock : DebugState
    {
        public int SaveFileHitCount { get; set; }
        public string SaveFileContents { get; private set; }

        //9142 TODO
        public string SaveFile(string contents)
        {
            SaveFileHitCount++;
            SaveFileContents = contents;
            return null;
        }

        //9142 TODO
        public void TryCache(IList<IDebugItem> items)
        {
            
        }
    }
}
