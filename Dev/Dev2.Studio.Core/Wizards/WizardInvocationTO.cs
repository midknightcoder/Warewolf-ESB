
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
using Dev2.Studio.Core.Wizards.Interfaces;

// ReSharper disable once CheckNamespace
namespace Dev2.Studio.Core.Wizards
{
    public class WizardInvocationTO
    {
        public Uri Endpoint { get; set; }
        public Guid TransferDatalistID { get; set; }
        public Guid ExecutionStatusCallbackID { get; set; }
        public IWizardCallbackHandler CallbackHandler { get; set; }
        public string WizardTitle { get; set; }
    }
}
