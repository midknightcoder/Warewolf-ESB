
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
using System.IO;
using Dev2.DataList;
using Warewolf.Storage;

namespace Dev2.BussinessLogic
{
    public class RsOpNotBetween : AbstractRecsetSearchValidation
    {
        public override Func<DataASTMutable.WarewolfAtom, bool> CreateFunc(IEnumerable<DataASTMutable.WarewolfAtom> values, IEnumerable<DataASTMutable.WarewolfAtom> warewolfAtoms, IEnumerable<DataASTMutable.WarewolfAtom> tovals, bool all)
        {

            return a => !RunBetween(warewolfAtoms, tovals, a);

        }


        static bool RunBetween(IEnumerable<DataASTMutable.WarewolfAtom> warewolfAtoms, IEnumerable<DataASTMutable.WarewolfAtom> tovals, DataASTMutable.WarewolfAtom a)
        {
            WarewolfListIterator iterator = new WarewolfListIterator();
            var from = new WarewolfAtomIterator(warewolfAtoms);
            var to = new WarewolfAtomIterator(tovals);
            iterator.AddVariableToIterateOn(@from);
            iterator.AddVariableToIterateOn(to);
            while (iterator.HasMoreData())
            {
                var fromval = iterator.FetchNextValue(@from);
                var toVal = iterator.FetchNextValue(to);

                DateTime fromDt;
                if (DateTime.TryParse(fromval, out fromDt))
                {
                    DateTime toDt;
                    if (!DateTime.TryParse(toVal, out toDt))
                    {
                        throw new InvalidDataException("IsBetween Numeric and DateTime mis-match");
                    }
                    DateTime recDateTime;
                    if (DateTime.TryParse(a.ToString(), out recDateTime))
                    {
                        if (recDateTime > fromDt && recDateTime < toDt)
                        {
                            return true;
                        }
                    }
                }
                double fromNum;
                if (double.TryParse(fromval, out fromNum))
                {
                    double toNum;
                    if (!double.TryParse(toVal, out toNum))
                    {
                        return false;
                    }
                    double recNum;
                    if (!double.TryParse(a.ToString(), out recNum))
                    {
                        continue;
                    }
                    if (recNum > fromNum && recNum < toNum)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override string HandlesType()
        {
            return "Not Between";
        }
    }
}

