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
using System.Linq;
using Dev2.Common.Interfaces.DB;
// ReSharper disable CheckNamespace
namespace Dev2.Common.DB
{
    /*
     * Provides new data sanitizing functionality for DB sources
     */

    public class DataSanitizerFactory
    {
        private static readonly Dictionary<enSupportedDBTypes, Type> Options
            = new Dictionary<enSupportedDBTypes, Type>();


        static DataSanitizerFactory()
        {
            Type type = typeof (IDataProviderSanitizer);

            List<Type> types = typeof (IDataProviderSanitizer).Assembly.GetTypes()
                .Where(t => (type.IsAssignableFrom(t))).ToList();

            foreach (Type t in types)
            {
                if (!t.IsAbstract && !t.IsInterface)
                {
                    var item = Activator.CreateInstance(t, true) as IDataProviderSanitizer;
                    if (item != null)
                    {
                        Options.Add(item.HandlesType(), t);
                    }
                }
            }
        }

        /// <summary>
        ///     Fetch the type to be handled, then generate a concerte instance
        /// </summary>
        /// <param name="typeOf"></param>
        /// <returns></returns>
        public static IDataProviderSanitizer GenerateNewSanitizer(enSupportedDBTypes typeOf)
        {
            IDataProviderSanitizer result;
            Type outType;

            if (!Options.TryGetValue(typeOf, out outType))
            {
                result = null;
            }
            else
            {
                result = Activator.CreateInstance(outType, true) as IDataProviderSanitizer;
            }

            return result;
        }
    }
}