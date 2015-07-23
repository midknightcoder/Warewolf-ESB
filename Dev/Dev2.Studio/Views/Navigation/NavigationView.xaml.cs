
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
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Dev2.Common.Interfaces.Explorer;
using Dev2.Models;
using Dev2.ViewModels.Deploy;

// ReSharper disable once CheckNamespace
namespace Dev2.Studio.Views.Navigation
{
    public partial class NavigationView
    {
        #region Constructor

        public NavigationView()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Dependency Properties

        #region ItemContainerStyle

        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set
            {
               SetValue(ItemContainerStyleProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ItemStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(NavigationView), new PropertyMetadata(null));

        #endregion ItemContainerStyle

        #region ItemTemplate

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(NavigationView), new PropertyMetadata(null));

        #endregion ItemTemplate

        #endregion Dependency Properties

        public void SelectItem(string resource)
        {
            var vm = DataContext as DeployNavigationViewModel;
            var explorerItems = vm.ExplorerItemModels.First();
            var children = explorerItems.AllChildren().Where(a => a.ResourcePath == resource);
            foreach(var a in children)
            {
               a.SetIsChecked(true, false, false, true); 
            }
            
           
        }
    }

    public static class ExplorerItemChildren
    {
        public static IEnumerable<IExplorerItemModel> AllChildren(this IExplorerItemModel item)
        {
            if( item.Children==null || !item.Children.Any())
            {
                return new List<IExplorerItemModel>(){ item};
            }
            else
            {
                var currList = new List<IExplorerItemModel>() { item };
                currList.AddRange( item.Children.SelectMany(AllChildren));
                return currList;
            }
        }
    }
}
