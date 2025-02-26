
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
using System.Collections.Generic;
using System.Windows;
using Dev2.Activities.Designers2.Core;
using Dev2.Common;
using Dev2.Common.DateAndTime;

namespace Dev2.Activities.Designers2.DateTime
{
    public class DateTimeDesignerViewModel : ActivityDesignerViewModel
    {
        public DateTimeDesignerViewModel(ModelItem modelItem)
            : base(modelItem)
        {
            AddTitleBarHelpToggle();
            TimeModifierTypes = new List<string>(DateTimeFormatter.TimeModifierTypes);
            SelectedTimeModifierType = string.IsNullOrEmpty(TimeModifierType) ? TimeModifierTypes[0] : TimeModifierType;
        }

        public List<string> TimeModifierTypes { get; private set; }

        public string Dev2DefaultDateTime { get { return GlobalConstants.Dev2CustomDefaultDateTimeFormat; } }

        public string SelectedTimeModifierType
        {
            get { return (string)GetValue(SelectedTimeModifierTypeProperty); }
            set { SetValue(SelectedTimeModifierTypeProperty, value); }
        }

        public static readonly DependencyProperty SelectedTimeModifierTypeProperty =
            DependencyProperty.Register("SelectedTimeModifierType", typeof(string), typeof(DateTimeDesignerViewModel), new PropertyMetadata(null, OnSelectedTimeModifierTypeChanged));

        static void OnSelectedTimeModifierTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (DateTimeDesignerViewModel)d;
            var value = e.NewValue as string;

            if(string.IsNullOrWhiteSpace(value))
            {
                viewModel.TimeModifierAmountDisplay = value;
            }
            viewModel.TimeModifierType = value;
        }

        // DO NOT bind to these properties - these are here for convenience only!!!
        string TimeModifierType { set { SetProperty(value);} get {return  GetProperty<string>();} }
        string TimeModifierAmountDisplay { set { SetProperty(value); } }

        public override void Validate()
        {
        }
    }
}
