
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
using System.Collections.Generic;

namespace Dev2.DataList.Contract.Binary_Objects.Structs
{
    [Serializable]
    // ReSharper disable InconsistentNaming
    public struct SBinaryDataList
    {
        public Guid UID { get; set; }
        public Guid ParentUID { get; set; }

        // Template dictionary
        public IDictionary<string, IBinaryDataListEntry> _templateDict;
        // Intellisesne parts to return 
        public IList<IDev2DataLanguageIntellisensePart> _intellisenseParts;
        // Catalog for intellisense namespaces
        public IList<string> _intellisensedNamespace;
    }
}
