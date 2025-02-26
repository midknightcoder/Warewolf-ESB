
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

namespace Unlimited.UnitTest.Framework.ConverterTests.GraphTests
{
    internal class PocoTestData
    {
        public string Name { get; set; }
        public int Age { get; set; }

        private string InternalData { get; set; }

        public PocoTestData NestedData { get; set; }

        public IList<PocoTestData> EnumerableData { get; set; }
        public IList<PocoTestData> EnumerableData1 { get; set; }
    }
}
