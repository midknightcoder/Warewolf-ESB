
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Activities.Presentation.Model;
using System.Windows;
using Dev2.Activities.Designers2.Core;

namespace Dev2.Activities.Designers2.Comment
{
    public class CommentDesignerViewModel : ActivityDesignerViewModel
    {
        public CommentDesignerViewModel(ModelItem modelItem)
            : base(modelItem)
        {
            AddTitleBarHelpToggle();
            ShowLarge = true;
            ThumbVisibility = Visibility.Visible;
        }

        #region Overrides of ActivityDesignerViewModel


        #endregion

        public override void Validate()
        {
        }
    }
}
