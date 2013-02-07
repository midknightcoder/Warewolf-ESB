﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 11.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace Dev2.CodedUI.Tests.UIMaps.ExplorerUIMapClasses
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Windows.Forms;
    using System.Threading;


    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public partial class ExplorerUIMap
    {
        public void DoRefresh()
        {
            // Find the explorer main window
            UITestControl anItem = this.UIBusinessDesignStudioWindow.UIExplorerCustom;
            anItem.Find();
            UITestControlCollection subItems = anItem.GetChildren();

            // Find the explorer sub window
            UITestControl explorerMenu = new UITestControl(anItem);
            explorerMenu.SearchProperties["AutomationId"] = "Explorer";
            explorerMenu.Find();

            // Find the refresh button
            UITestControl refreshButton = new UITestControl(explorerMenu);
            refreshButton.SearchProperties["AutomationId"] = "UI_btnRefresh_AutoID";
            refreshButton.Find();

            // And click it
            Mouse.Click(refreshButton, new Point(5, 5));
        }

        public UITestControl GetServerDDL()
        {
            // Find the explorer main window
            UITestControl anItem = this.UIBusinessDesignStudioWindow.UIExplorerCustom;
            anItem.Find();
            UITestControlCollection subItems = anItem.GetChildren();

            // Find the explorer sub window
            UITestControl explorerMenu = new UITestControl(anItem);
            explorerMenu.SearchProperties["AutomationId"] = "Explorer";
            explorerMenu.Find();

            UITestControl serverDDL = new UITestControl(explorerMenu);
            serverDDL.SearchProperties["AutomationId"] = "UI_ExplorerServerCbx_AutoID";
            serverDDL.Find();
            return serverDDL;
        }

        public void GetMenuItem(string menuItemName)
        {
            UITestControl theStudio = this.UIBusinessDesignStudioWindow;
            UITestControl theTabMenu = new UITestControl(theStudio);
            //theTabMenu.SearchProperties["AutomationID"] = "UI_ExplorerContextMenu_AutoID";

            foreach (UITestControl theControl in theStudio.GetChildren())
            {
                string theType = theControl.ClassName;
                if (theType == "Uia.Popup")
                {
                    UITestControl contextMenu = theControl.GetChildren()[0];
                    foreach (UITestControl subControl in contextMenu.GetChildren())
                    {
                        try
                        {
                            string friendlyName = subControl.FriendlyName;
                            int j = 10;
                        }
                        catch
                        {
                            // Do Nothing - Invalid control
                        }
                    }
                }
            }

        }

        private WpfTree GetExplorerTree()
        {
            return this.UIBusinessDesignStudioWindow.UIExplorerCustom.UINavigationViewUserCoCustom.UITvExplorerTree;
        }
        /// <summary>
        /// ClickExplorer
        /// </summary>
        private UITestControl GetServiceItem(string serverName, string serviceType, string folderName, string projectName)
        {
            Point p;
            UITestControl returnControl = null;

            Thread.Sleep(500);
            SendKeys.SendWait("{HOME}");
            Thread.Sleep(500);

            WpfTree uITvExplorerTree = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UINavigationViewUserCoCustom.UITvExplorerTree;
            // Uncomment these 3 lines if things start going slowly again (They help to locate the problem)
            
            //UITestControl theStudioWindow = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UINavigationViewUserCoCustom;
            //theStudioWindow.Find();
            //uITvExplorerTree.Find();

            UITestControl serverListItem = new UITestControl(uITvExplorerTree);
            serverListItem.SearchProperties.Add("AutomationId", serverName, PropertyExpressionOperator.Contains);
            serverListItem.SearchProperties.Add("ControlType", "TreeItem");
            
            serverListItem.Find();

            Thread.Sleep(500);
            SendKeys.SendWait("{HOME}");
            Thread.Sleep(500);

            // Can we see the type list? (AKA: Is the server list maximized?)
            UITestControl serviceTypeListItem = new UITestControl(serverListItem);
            serviceTypeListItem.SearchProperties.Add("AutomationId", "UI_" + serviceType + "_AutoID");
            serviceTypeListItem.Find();

            if (!serviceTypeListItem.TryGetClickablePoint(out p))
            {
                Mouse.DoubleClick(new Point(serverListItem.BoundingRectangle.X + 50, serverListItem.BoundingRectangle.Y + 5));
            }
            else
            {
                Mouse.Click(new Point(serverListItem.BoundingRectangle.X + 50, serverListItem.BoundingRectangle.Y + 5));
            }

            // Can we see the folder? (AKA: Is the type list maximised?)
            UITestControl folderNameListItem = new UITestControl(serviceTypeListItem);
            folderNameListItem.SearchProperties.Add("AutomationId", "UI_" + folderName + "_AutoID");
            folderNameListItem.Find();
            if (!folderNameListItem.TryGetClickablePoint(out p))
            {
                Mouse.DoubleClick(new Point(serviceTypeListItem.BoundingRectangle.X + 50, serviceTypeListItem.BoundingRectangle.Y + 5));
            }
            else
            {
                Mouse.Click(new Point(serviceTypeListItem.BoundingRectangle.X + 50, serviceTypeListItem.BoundingRectangle.Y + 5));
            }

            // Can we see the file? (AKA: Is the folder maximised?)
            UITestControl projectNameListItem = new UITestControl(folderNameListItem);
            projectNameListItem.SearchProperties.Add("AutomationId", "UI_" + projectName + "_AutoID");
            projectNameListItem.Find();
            if (!projectNameListItem.TryGetClickablePoint(out p))
            {
                Mouse.DoubleClick(new Point(folderNameListItem.BoundingRectangle.X + 50, folderNameListItem.BoundingRectangle.Y + 5));
            }
            else
            {
                Mouse.Click(new Point(folderNameListItem.BoundingRectangle.X + 50, folderNameListItem.BoundingRectangle.Y + 5));
            }

            return projectNameListItem;

        }

        private UITestControl GetCategory(string serverName, string serviceType, string categoryName)
        {
            Point p;
            UITestControl returnControl = null;

            Thread.Sleep(500);
            SendKeys.SendWait("{HOME}");
            SendKeys.SendWait("^{LEFT}");
            SendKeys.SendWait("^{LEFT}");
            SendKeys.SendWait("^{LEFT}");
            SendKeys.SendWait("^{LEFT}");
            SendKeys.SendWait("^{LEFT}");
            Thread.Sleep(500);

            WpfTree uITvExplorerTree = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UINavigationViewUserCoCustom.UITvExplorerTree;
            // Uncomment these 3 lines if things start going slowly again (They help to locate the problem)

            //UITestControl theStudioWindow = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UINavigationViewUserCoCustom;
            //theStudioWindow.Find();
            //uITvExplorerTree.Find();

            UITestControl serverListItem = new UITestControl(uITvExplorerTree);
            serverListItem.SearchProperties.Add("AutomationId", serverName, PropertyExpressionOperator.Contains);
            serverListItem.SearchProperties.Add("ControlType", "TreeItem");

            serverListItem.Find();

            Point clickablePoint = new Point();
            if (!serverListItem.TryGetClickablePoint(out clickablePoint))
            {
                // Click in it, and fix the alignment
                Mouse.Click(serverListItem, new Point(100, 100));

                // Re-allign the Explorer
                SendKeys.SendWait("{PAGEUP}");
                SendKeys.SendWait("{PAGEUP}");
                SendKeys.SendWait("{PAGEUP}");

                SendKeys.SendWait("^{LEFT}");
                SendKeys.SendWait("^{LEFT}");
                SendKeys.SendWait("^{LEFT}");
                SendKeys.SendWait("^{LEFT}");
                SendKeys.SendWait("^{LEFT}");
            }

            Thread.Sleep(500);
            SendKeys.SendWait("{HOME}");
            Thread.Sleep(500);

            // Can we see the type list? (AKA: Is the server list maximized?)
            UITestControl serviceTypeListItem = new UITestControl(serverListItem);
            serviceTypeListItem.SearchProperties.Add("AutomationId", "UI_" + serviceType + "_AutoID");
            serviceTypeListItem.Find();

            if (!serviceTypeListItem.TryGetClickablePoint(out p))
            {
                Mouse.DoubleClick(new Point(serverListItem.BoundingRectangle.X + 50, serverListItem.BoundingRectangle.Y + 5));
            }
            else
            {
                Mouse.Click(new Point(serverListItem.BoundingRectangle.X + 50, serverListItem.BoundingRectangle.Y + 5));
            }

            // Can we see the folder? (AKA: Is the type list maximised?)
            UITestControl folderNameListItem = new UITestControl(serviceTypeListItem);
            folderNameListItem.SearchProperties.Add("AutomationId", "UI_" + categoryName + "_AutoID");
            folderNameListItem.Find();
            if (!folderNameListItem.TryGetClickablePoint(out p))
            {
                Mouse.DoubleClick(new Point(serviceTypeListItem.BoundingRectangle.X + 50, serviceTypeListItem.BoundingRectangle.Y + 5));
            }
            else
            {
                Mouse.Click(new Point(serviceTypeListItem.BoundingRectangle.X + 50, serviceTypeListItem.BoundingRectangle.Y + 5));
            }
            return folderNameListItem;
        }


        public void EnterExplorerSearchText(string textToSearchWith)
        {
            WpfEdit uIUI_txtSearch_AutoIDEdit = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UIUI_txtSearch_AutoIDEdit;

            Mouse.Click(uIUI_txtSearch_AutoIDEdit, new Point(5, 5));
            SendKeys.SendWait(textToSearchWith);
        }

        public void ClearExplorerSearchText()
        {
            WpfEdit uIUI_txtSearch_AutoIDEdit = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UIUI_txtSearch_AutoIDEdit;

            Mouse.Click(uIUI_txtSearch_AutoIDEdit, new Point(5, 5));
            SendKeys.SendWait("{HOME}");
            SendKeys.SendWait("+{END}");
            SendKeys.SendWait("{DELETE}");
        }

        public UITestControlCollection GetCategoryItems()
        {
            UITestControlCollection categories = GetNavigationItemCategories();

            UITestControlCollection services = new UITestControlCollection();
            foreach (UITestControl cat in categories)
            {
                UITestControlCollection categoryChildren = cat.GetChildren();
                foreach (UITestControl catChild in categoryChildren)
                {
                    if (catChild.ControlType.ToString() == "TreeItem")
                    {
                        Point p = new Point();
                        if (catChild.TryGetClickablePoint(out p))
                        {
                            services.Add(cat);
                        }
                    }
                }
  
            }
            return services; 
        }

        public UITestControlCollection GetNavigationItemCategories()
        {
            WpfTree tree = GetExplorerTree();

            UITestControl serverNode = tree.GetChildren()[0];

            UITestControlCollection categories = serverNode.GetChildren();

            UITestControlCollection categoryCollection = new UITestControlCollection();

            foreach (UITestControl category in categories)
            {
                if (category.ControlType.ToString() == "TreeItem")
                {
                    categoryCollection.Add(category);
                }
            }

            return categoryCollection;
        }

        private UITestControl GetConnectedServer(string serverName)
        {
            UITestControl returnControl = new UITestControl();
            WpfTree uITvExplorerTree = this.UIBusinessDesignStudioWindow.UINavigationViewUserCoCustom.UITvExplorerTree;
            UITestControl server = null;
            foreach (UITestControl serverListItem in uITvExplorerTree.GetChildren()) // 0 for the first server in the list
            {
                if (serverListItem.GetProperty(WpfTree.PropertyNames.AutomationId).ToString().Contains(serverName))
                {
                    server = serverListItem;
                    break;
                }
                else
                {
                    server = null;
                }
            }
            return server;
        }

        private UITestControl GetServiceTypeControl(string serverName, string serviceType)
        {
            Point p;
            UITestControl server = GetConnectedServer(serverName);
            UITestControl serviceTypeReturn = null;
            foreach (UITestControl serviceTypeListItem in server.GetChildren())
            {
                if (serviceTypeListItem.FriendlyName.Contains(serviceType))
                {
                    // If the service type is not visible, expand the server list
                    if (!serviceTypeListItem.TryGetClickablePoint(out p))
                    {
                        Mouse.DoubleClick(new Point(server.BoundingRectangle.X + 50, server.BoundingRectangle.Y + 5));
                    }
                    else
                    {
                        Mouse.Click(new Point(server.BoundingRectangle.X + 50, server.BoundingRectangle.Y + 5));
                    }
                    serviceTypeReturn = serviceTypeListItem;
                    break;
                }
                else
                {
                    serviceTypeReturn = null;
                }
            }
            return serviceTypeReturn;
        }


        /// ConnectBtnClick
        /// </summary>
        /*
        public void ConnectBtnClick()
        {
            #region Variable Declarations
            //WpfButton uIConnectButton = this.UIBusinessDesignStudioWindow.UIExplorerCustom.UIConnectButton;
            #endregion

            // Click 'Connect' button
            //Mouse.Click(uIConnectButton, new Point(35, 11));
        }
         */

        /// <summary>
        /// Environment_Explorer_AnotherServer_Exists
        /// </summary>
        //public void CheckForPartnerServer_Exists() {
        //    #region Variable Declarations
        //    WpfTreeItem uIUI_SashensServerhttpTreeItem = this.UIBusinessDesignStudioWindow.UINavigationViewUserCoCustom.UITvExplorerTree.UIUI_SashensServerhttpTreeItem;
        //    #endregion

        //    // Verify that the 'AutomationId' property of 'UI_Sashens Server (http://rsaklfsashennai:77/dsf)_...' tree item is not equal to 'null'
        //    Assert.IsNotNull(uIUI_SashensServerhttpTreeItem.AutomationId);
        //}

        #region Properties
        public UIBusinessDesignStudioWindow UIBusinessDesignStudioWindow
        {
            get
            {
                if ((this.mUIBusinessDesignStudioWindow == null))
                {
                    this.mUIBusinessDesignStudioWindow = new UIBusinessDesignStudioWindow();
                }
                return this.mUIBusinessDesignStudioWindow;
            }
        }
        #endregion


        #region Fields
        private UIBusinessDesignStudioWindow mUIBusinessDesignStudioWindow;

        #endregion
    }

    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UIBusinessDesignStudioWindow : WpfWindow
    {

        public UIBusinessDesignStudioWindow()
        {
            #region Search Criteria
            this.SearchProperties[WpfWindow.PropertyNames.Name] = TestBase.GetStudioWindowName();
            this.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }

        #region Navigation Item

        public UINavigationViewUserCoCustom UINavigationViewUserCoCustom
        {
            get
            {
                if ((this.mUINavigationViewUserCoCustom == null))
                {
                    this.mUINavigationViewUserCoCustom = new UINavigationViewUserCoCustom(this);
                }
                return this.mUINavigationViewUserCoCustom;
            }
        }

        private UINavigationViewUserCoCustom mUINavigationViewUserCoCustom;

        #endregion Navigation Item

        #region Explorer Item

        public UIExplorerCustom UIExplorerCustom
        {
            get
            {
                if ((this.mUIExplorerCustom == null))
                {
                    this.mUIExplorerCustom = new UIExplorerCustom(this);
                }
                return this.mUIExplorerCustom;
            }
        }

        private UIExplorerCustom mUIExplorerCustom;

        #endregion Explorer Item

    }

    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UIExplorerCustom : WpfCustom
    {

        public UIExplorerCustom(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "Uia.ContentPane";
            this.SearchProperties["AutomationId"] = "UI_ExplorerPane_AutoID";
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }

        #region Properties


        public WpfEdit UIUI_txtSearch_AutoIDEdit
        {
            get
            {
                if ((this.mUIUI_txtSearch_AutoIDEdit == null))
                {
                    this.mUIUI_txtSearch_AutoIDEdit = new WpfEdit(this);
                    #region Search Criteria
                    this.mUIUI_txtSearch_AutoIDEdit.SearchProperties[WpfEdit.PropertyNames.AutomationId] = "UI_txtSearch_AutoID";
                    this.mUIUI_txtSearch_AutoIDEdit.WindowTitles.Add(TestBase.GetStudioWindowName());
                    #endregion
                }
                return this.mUIUI_txtSearch_AutoIDEdit;
            }
        }


        public UINavigationViewUserCoCustom UINavigationViewUserCoCustom
        {
            get
            {
                if ((this.mUINavigationViewUserCoCustom == null))
                {
                    this.mUINavigationViewUserCoCustom = new UINavigationViewUserCoCustom(this);
                }
                return this.mUINavigationViewUserCoCustom;
            }
        }


        #endregion

        #region Fields
        private UINavigationViewUserCoCustom mUINavigationViewUserCoCustom;
        private WpfEdit mUIUI_txtSearch_AutoIDEdit;
        #endregion
    }

    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class OtherServer : WpfTreeItem
    {

        public OtherServer(UITestControl searchLimitContainer, string serverAutomationId) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfTreeItem.PropertyNames.AutomationId] = /*"UI_Sashens Server (http://rsaklfsashennai:77/dsf)_AutoID"*/ serverAutomationId;
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }

        #region Properties
        public WpfTreeItem UIUI_WORKFLOWSERVICES_TreeItem
        {
            get
            {
                if ((this.mUIUI_WORKFLOWSERVICES_TreeItem == null))
                {
                    this.mUIUI_WORKFLOWSERVICES_TreeItem = new WpfTreeItem(this);
                    #region Search Criteria
                    this.mUIUI_WORKFLOWSERVICES_TreeItem.SearchProperties[WpfTreeItem.PropertyNames.AutomationId] = "UI_WORKFLOW SERVICES_AutoID";
                    this.mUIUI_WORKFLOWSERVICES_TreeItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                    this.mUIUI_WORKFLOWSERVICES_TreeItem.WindowTitles.Add(TestBase.GetStudioWindowName());
                    #endregion
                }
                return this.mUIUI_WORKFLOWSERVICES_TreeItem;
            }
        }
        #endregion

        #region Fields
        private WpfTreeItem mUIUI_WORKFLOWSERVICES_TreeItem;
        #endregion
    }


    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UINavigationViewUserCoCustom : WpfCustom
    {

        public UINavigationViewUserCoCustom(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "Uia.NavigationView";
            this.SearchProperties["AutomationId"] = "TheNavigationView";
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }

        #region Properties
        public UITvExplorerTree UITvExplorerTree
        {
            get
            {
                if ((this.mUITvExplorerTree == null))
                {
                    this.mUITvExplorerTree = new UITvExplorerTree(this);
                }
                return this.mUITvExplorerTree;
            }
        }
        #endregion

        #region Fields
        private UITvExplorerTree mUITvExplorerTree;
        #endregion
    }

    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UITvExplorerTree : WpfTree
    {

        public UITvExplorerTree(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfTree.PropertyNames.AutomationId] = "tvExplorer";
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }

        #region Properties
        public UIUI_localhosthttp1270TreeItem UIUI_localhosthttp1270TreeItem
        {
            get
            {
                if ((this.mUIUI_localhosthttp1270TreeItem == null))
                {
                    this.mUIUI_localhosthttp1270TreeItem = new UIUI_localhosthttp1270TreeItem(this);
                }
                return this.mUIUI_localhosthttp1270TreeItem;
            }
        }
        #endregion

        #region Fields
        private UIUI_localhosthttp1270TreeItem mUIUI_localhosthttp1270TreeItem;
        #endregion
    }

    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UIUI_localhosthttp1270TreeItem : WpfTreeItem
    {

        public UIUI_localhosthttp1270TreeItem(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfTreeItem.PropertyNames.AutomationId] = "UI_localhost (http://127.0.0.1:77/dsf)_AutoID";
            this.WindowTitles.Add(TestBase.GetStudioWindowName());
            #endregion
        }

        #region Properties
        public WpfTreeItem UIUI_WORKFLOWSERVICES_TreeItem
        {
            get
            {
                if ((this.mUIUI_WORKFLOWSERVICES_TreeItem == null))
                {
                    this.mUIUI_WORKFLOWSERVICES_TreeItem = new WpfTreeItem(this);
                    #region Search Criteria
                    this.mUIUI_WORKFLOWSERVICES_TreeItem.SearchProperties[WpfTreeItem.PropertyNames.AutomationId] = "UI_WORKFLOW SERVICES_AutoID";
                    this.mUIUI_WORKFLOWSERVICES_TreeItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                    this.mUIUI_WORKFLOWSERVICES_TreeItem.WindowTitles.Add(TestBase.GetStudioWindowName());
                    #endregion
                }
                return this.mUIUI_WORKFLOWSERVICES_TreeItem;
            }
        }
        #endregion

        #region Fields
        private WpfTreeItem mUIUI_WORKFLOWSERVICES_TreeItem;
        #endregion
    }
}
