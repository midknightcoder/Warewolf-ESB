
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.UI;

namespace Dev2.Core.Tests.Custom_Dev2_Controls.Intellisense
{
    class MockIntellisenseTextbox : IntellisenseTextBox
    {
        public int TextChangedCounter { get; set; }

        
        public void InitTestClass()
        {
            TextChangedCounter = 0;           
        }

        protected override void TheTextHasChanged()
        {            
            TextChangedCounter++;            
        }
    }
}
