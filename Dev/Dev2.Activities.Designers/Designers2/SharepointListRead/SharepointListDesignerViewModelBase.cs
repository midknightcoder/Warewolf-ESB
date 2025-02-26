using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml;
using Caliburn.Micro;
using Dev2.Activities.Designers2.Core;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core.DynamicServices;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Common.Interfaces.Infrastructure.SharedModels;
using Dev2.Data.ServiceModel;
using Dev2.Data.Util;
using Dev2.Runtime.Configuration.ViewModels.Base;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Studio.Core.Interfaces;
using Dev2.Studio.Core.Messages;
using Dev2.Threading;
using Dev2.TO;

namespace Dev2.Activities.Designers2.SharepointListRead
{
    public abstract class SharepointListDesignerViewModelBase : ActivityCollectionDesignerViewModel<SharepointSearchTo>        
    {
        readonly IEventAggregator _eventPublisher;
        readonly bool _loadOnlyEditableFields;
        static readonly SharepointSource NewSharepointSource = new SharepointSource
        {
            ResourceID = Guid.NewGuid(),
            ResourceName = "New Sharepoint Server Source..."
        };
        static readonly SharepointSource SelectSharepointSource = new SharepointSource
        {
            ResourceID = Guid.NewGuid(),
            ResourceName = "Select a Sharepoint Server Source..."
        };
        static readonly SharepointListTo SelectSharepointList = new SharepointListTo
        {
            FullName = "Select a List..."
        };
       
        bool _isInitializing;
        readonly IEnvironmentModel _environmentModel;
        readonly IAsyncWorker _asyncWorker;

        protected SharepointListDesignerViewModelBase(ModelItem modelItem, IAsyncWorker asyncWorker, IEnvironmentModel environmentModel, IEventAggregator eventPublisher, bool loadOnlyEditableFields)
            :base(modelItem)
        {
            AddTitleBarLargeToggle();
            AddTitleBarHelpToggle();
            VerifyArgument.IsNotNull("asyncWorker", asyncWorker);
            _asyncWorker = asyncWorker;
            VerifyArgument.IsNotNull("environmentModel", environmentModel);
            _environmentModel = environmentModel;
            VerifyArgument.IsNotNull("eventPublisher", eventPublisher);
            _eventPublisher = eventPublisher;
            ShowExampleWorkflowLink = Visibility.Collapsed;

            _loadOnlyEditableFields = loadOnlyEditableFields;

            SharepointServers = new ObservableCollection<SharepointSource>();
            Lists = new ObservableCollection<SharepointListTo>();

            EditSharepointServerCommand = new RelayCommand(o => EditSharepointSource(), o => IsSharepointServerSelected);
            RefreshListsCommand = new RelayCommand(o =>
            {
                RefreshSharepointSources();
                RefreshLists();
            });
            
            RefreshSharepointSources(true);
            
        }
        public static readonly DependencyProperty IsSelectedSharepointServerFocusedProperty =
           DependencyProperty.Register("IsSelectedSharepointServerFocused", typeof(bool), typeof(SharepointListDesignerViewModelBase), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSelectedListFocusedProperty =
            DependencyProperty.Register("IsSelectedListFocused", typeof(bool), typeof(SharepointListDesignerViewModelBase), new PropertyMetadata(false));
        public static readonly DependencyProperty SelectedSharepointServerProperty =
            DependencyProperty.Register("SelectedSharepointServer", typeof(SharepointSource), typeof(SharepointListDesignerViewModelBase), new PropertyMetadata(null, OnSelectedSharepointServerChanged));
        public static readonly DependencyProperty IsRefreshingProperty =
            DependencyProperty.Register("IsRefreshing", typeof(bool), typeof(SharepointListDesignerViewModelBase), new PropertyMetadata(false));
        public static readonly DependencyProperty SelectedListProperty =
            DependencyProperty.Register("SelectedList", typeof(SharepointListTo), typeof(SharepointListDesignerViewModelBase), new PropertyMetadata(null, OnSelectedListChanged));
        public static readonly DependencyProperty ListItemsProperty =
            DependencyProperty.Register("ListItems", typeof(List<SharepointReadListTo>), typeof(SharepointListDesignerViewModelBase), new PropertyMetadata(new List<SharepointReadListTo>()));
        public bool IsSelectedSharepointServerFocused { get { return (bool)GetValue(IsSelectedSharepointServerFocusedProperty); } set { SetValue(IsSelectedSharepointServerFocusedProperty, value); } }
        public bool IsSelectedListFocused { get { return (bool)GetValue(IsSelectedListFocusedProperty); } set { SetValue(IsSelectedListFocusedProperty, value); } }
        public SharepointSource SelectedSharepointServer
        {
            get
            {
                return (SharepointSource)GetValue(SelectedSharepointServerProperty);
            }
            set
            {
                SetValue(SelectedSharepointServerProperty, value);

                EditSharepointServerCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsRefreshing { get { return (bool)GetValue(IsRefreshingProperty); } set { SetValue(IsRefreshingProperty, value); } }
        public Guid SharepointServerResourceId
        {
            get
            {
                return GetProperty<Guid>();  
            }
            set
            {
                if (!_isInitializing)
                {
                    SetProperty(value);

                } 
            }
        }
        public SharepointListTo SelectedList
        {
            get
            {
                return (SharepointListTo)GetValue(SelectedListProperty);
            }
            set
            {
                SetValue(SelectedListProperty, value);
                RefreshListsCommand.RaiseCanExecuteChanged();
            }
        }
        List<SharepointReadListTo> ReadListItems
        {
            get
            {
                return GetProperty<List<SharepointReadListTo>>();
            }
            set
            {
                if (!_isInitializing)
                {
                    SetProperty(value);                    
                }
                ListItems = value;
            }
        }
        public List<SharepointReadListTo> ListItems
        {
            get
            {
                return (List<SharepointReadListTo>)GetValue(ListItemsProperty);
            }
            set
            {

                SetValue(ListItemsProperty, value);

            }
        }
        string SharepointList
        {
            get { return GetProperty<string>(); }
            set
            {
                if (!_isInitializing)
                {
                    SetProperty(value);
                }
            }
        }
        public bool IsSharepointServerSelected { get { return SelectedSharepointServer != SelectSharepointSource; } }
        public bool IsListSelected { get { return SelectedList != SelectSharepointList; } }
        public RelayCommand RefreshListsCommand { get; set; }
        public RelayCommand EditSharepointServerCommand { get; set; }
        public ObservableCollection<SharepointListTo> Lists { get; set; }
        public ObservableCollection<SharepointSource> SharepointServers { get; set; }

        protected void RefreshSharepointSources(bool isInitializing = false)
        {
            IsRefreshing = true;
            if (isInitializing)
            {
                _isInitializing = true;
            }

            LoadSharepointServerSources(() =>
            {
                SetSelectedSharepointServer(SelectedSharepointServer);
                LoadLists(() =>
                {
                    SetSelectedList(SharepointList);
                    LoadListFields(false,() =>
                    {
                        IsRefreshing = false;
                        if (isInitializing)
                        {
                            _isInitializing = false;
                        }
                    });
                });
            });
        }

        void SetSelectedList(string listName)
        {
            var selectedTable = Lists.FirstOrDefault(t => t.FullName == listName);
            if (selectedTable == null)
            {
                if (Lists.FirstOrDefault(t => t.Equals(SelectSharepointList)) == null)
                {
                    Lists.Insert(0, SelectSharepointList);
                }
                selectedTable = SelectSharepointList;
            }
            SelectedList = selectedTable;
        }

        List<SharepointListTo> GetSharepointLists(SharepointSource dbSource)
        {
            var tables = _environmentModel.ResourceRepository.GetSharepointLists(dbSource);
            return tables ?? new List<SharepointListTo>();
        }

        void LoadLists(System.Action continueWith = null)
        {
            Lists.Clear();

            if (!IsSharepointServerSelected)
            {
                if (continueWith != null)
                {
                    continueWith();
                }
                return;
            }

            // Get Selected values on UI thread BEFORE starting asyncWorker
            var selectedDatabase = SelectedSharepointServer;
            _asyncWorker.Start(() => GetSharepointLists(selectedDatabase), tableList =>
            {
                if(tableList != null)
                {
                    foreach (var listTo in tableList.OrderBy(t => t.FullName))
                    {
                        Lists.Add(listTo);
                    }
                }
                if (continueWith != null)
                {
                    continueWith();
                }
            });
        }

        void SetSelectedSharepointServer(SharepointSource sharepointServerSource)
        {
            var selectSharepointSource = sharepointServerSource ?? SharepointServers.FirstOrDefault(d => d.ResourceID == SharepointServerResourceId);
            if (selectSharepointSource == null)
            {
                if (SharepointServers.FirstOrDefault(d => d.Equals(SelectSharepointSource)) == null)
                {
                    SharepointServers.Insert(0, SelectSharepointSource);
                }
                selectSharepointSource = SelectSharepointSource;
            }
            SelectedSharepointServer = selectSharepointSource;
        }

        IEnumerable<SharepointSource> GetSharepointServers()
        {
            var sources = _environmentModel.ResourceRepository.FindSourcesByType<SharepointSource>(_environmentModel, enSourceType.SharepointServerSource) ?? new List<SharepointSource>();
            return sources;
        }

        void LoadSharepointServerSources(System.Action continueWith = null)
        {
            SharepointServers.Clear();
            SharepointServers.Add(NewSharepointSource);

            _asyncWorker.Start(() => GetSharepointServers().OrderBy(r => r.ResourceName), sharepointSources =>
            {
                if(sharepointSources != null)
                {
                    foreach (var sharepointSource in sharepointSources)
                    {
                        SharepointServers.Add(sharepointSource);
                    }
                }
                if (continueWith != null)
                {
                    continueWith();
                }
            });
        }

        static void OnSelectedSharepointServerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (SharepointListDesignerViewModelBase)d;
            if (viewModel.IsRefreshing)
            {
                return;
            }
            viewModel.OnSharepointServerChanged();
            viewModel.EditSharepointServerCommand.RaiseCanExecuteChanged();
        }

        void CreateSharepointServerSource()
        {
            IsRefreshing = true;
            _eventPublisher.Publish(new ShowNewResourceWizard("SharepointServerSource"));
            RefreshSharepointSources();
            IsRefreshing = false;
        }

        static string GetListName(SharepointListTo table)
        {
            return table == null ? null : table.FullName;
        }

        protected virtual void OnSharepointServerChanged()
        {
            if (SelectedSharepointServer == NewSharepointSource)
            {
                CreateSharepointServerSource();
                return;
            }

            IsRefreshing = true;
            // Save selection
            var listName = GetListName(SelectedList);

            SharepointServers.Remove(SelectSharepointSource);
            SharepointServerResourceId = SelectedSharepointServer.ResourceID;

            Lists.Clear();
            LoadLists(() =>
            {
                // Restore Selection
                SetSelectedList(listName);
                LoadListFields(true,() => { IsRefreshing = false; });
            });
        }

        static void OnSelectedListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (SharepointListDesignerViewModelBase)d;
            if (viewModel.IsRefreshing)
            {
                return;
            }
            viewModel.OnSelectedListChanged();
            viewModel.RefreshListsCommand.RaiseCanExecuteChanged();
        }

        protected virtual void OnSelectedListChanged()
        {
            if (SelectedList != null)
            {
                IsRefreshing = true;
                Lists.Remove(SelectSharepointList);
                SharepointList = SelectedList.FullName;
                LoadListFields(true,() => { IsRefreshing = false; });
            }
        }

        void LoadListFields(bool isFromListChange=false,System.Action continueWith=null)
        {
            if (!IsListSelected)
            {
                if (continueWith != null)
                {
                    continueWith();
                }
                return;
            }

            var selectedSharepointServer = SelectedSharepointServer;
            var selectedList = SelectedList;
            // ReSharper disable ImplicitlyCapturedClosure
            _asyncWorker.Start(() => GetListFields(selectedSharepointServer, selectedList), columnList =>
                // ReSharper restore ImplicitlyCapturedClosure
            {
                if(columnList != null)
                {
                    var fieldMappings = columnList.Select(mapping =>
                    {
                        var recordsetDisplayValue = DataListUtil.CreateRecordsetDisplayValue(selectedList.FullName.Replace(" ","").Replace(".",""),GetValidVariableName(mapping),"*");
                        var sharepointReadListTo = new SharepointReadListTo(DataListUtil.AddBracketsToValueIfNotExist(recordsetDisplayValue), mapping.Name, mapping.InternalName, mapping.Type.ToString()) { IsRequired = mapping.IsRequired };
                        return sharepointReadListTo;
                    }).ToList();
                    if (ReadListItems == null || ReadListItems.Count == 0 || isFromListChange)
                    {
                        ReadListItems = fieldMappings;
                    }
                    else
                    {
                        foreach(var sharepointReadListTo in fieldMappings)
                        {
                            var readListTo = ReadListItems.FirstOrDefault(to => to.FieldName == sharepointReadListTo.FieldName);
                            if(readListTo == null)
                            {
                                ReadListItems.Add(sharepointReadListTo);
                            }
                        }
                    }
                    ListItems = ReadListItems;
                }
                if (continueWith != null)
                {
                    continueWith();
                }
            });
        }

        static string GetValidVariableName(ISharepointFieldTo mapping)
        {
            var fixedName = mapping.Name.Replace(" ","").Replace(".","").Replace(":","").Replace(",","");
            fixedName = XmlConvert.EncodeName(fixedName);
            var startIndexOfEncoding = fixedName.IndexOf("_", StringComparison.OrdinalIgnoreCase);
            var endIndexOfEncoding = fixedName.LastIndexOf("_", StringComparison.OrdinalIgnoreCase);
            if(startIndexOfEncoding > 0 && endIndexOfEncoding > 0)
            {
                fixedName = fixedName.Remove(startIndexOfEncoding-1, endIndexOfEncoding - startIndexOfEncoding);
            }
            if (fixedName[0] == 'f' || fixedName[0] == '_' || Char.IsNumber(fixedName[0]))
            {
                fixedName = fixedName.Remove(0, 1);
            }
            return fixedName;
        }

        List<ISharepointFieldTo> GetListFields(ISharepointSource source, SharepointListTo list)
        {
            var columns = _environmentModel.ResourceRepository.GetSharepointListFields(source, list, _loadOnlyEditableFields);
            return columns ?? new List<ISharepointFieldTo>();
        }

        protected void RefreshLists()
        {
            IsRefreshing = true;

            LoadLists(() =>
            {
                SetSelectedList(SharepointList);

                LoadListFields(false,() =>
                {
                    IsRefreshing = false;
                });
            });
        }

        void EditSharepointSource()
        {
            var resourceModel = _environmentModel.ResourceRepository.FindSingle(c => c.ID == SelectedSharepointServer.ResourceID);
            if (resourceModel != null)
            {
                _eventPublisher.Publish(new ShowEditResourceWizardMessage(resourceModel));
                RefreshSharepointSources();
            }
        }


        protected override IEnumerable<IActionableErrorInfo> ValidateThis()
        {
            yield break;
        }

        protected override IEnumerable<IActionableErrorInfo> ValidateCollectionItem(ModelItem mi)
        {
            var dto = mi.GetCurrentValue() as SharepointSearchTo;
            if (dto == null)
            {
                yield break;
            }
        }
    }
}