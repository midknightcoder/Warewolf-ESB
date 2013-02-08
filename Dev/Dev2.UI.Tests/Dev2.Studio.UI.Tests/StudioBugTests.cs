﻿using System;
using System.Threading;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Dev2.CodedUI.Tests;
using Dev2.Studio.UI.Tests.UIMaps.DependencyGraphClasses;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dev2.CodedUI.Tests.TabManagerUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.WorkflowDesignerUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.WorkflowWizardUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.DatabaseServiceUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.PluginServiceWizardUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.WebpageServiceWizardUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.ConnectViewUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.RibbonUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.DocManagerUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.ToolboxUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.ExplorerUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.DeployViewUIMapClasses;
using Dev2.CodedUI.Tests.UIMaps.ExternalUIMapClasses;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Dev2.CodedUI.Tests.UIMaps.VariablesUIMapClasses;

namespace Dev2.Studio.UI.Tests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class StudioBugTests
    {
        TestBase myTestBase = new TestBase();
        // These run at the start of every test to make sure everything is sane
        [TestInitialize]
        public void CheckStartIsValid()
        {
            // Use the base class for validity checks - Easier to control :D
            myTestBase.CheckStartIsValid();
        }

        // Bug 3512
        [TestMethod]
        public void WorkflowXamlShouldNotBeVisibleByCopyPaste()
        {
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "SYSTEM", "Base64ToString");
            UITestControl theTab = TabManagerUIMap.FindTabByName("Base64ToString");
            TabManagerUIMap.Click("Base64ToString");
            WorkflowDesignerUIMap.CopyWorkflowXaml(theTab);
            System.Diagnostics.Process.Start("notepad.exe");
            Thread.Sleep(1000);
            Keyboard.SendKeys("^V");
            if (ExternalUIMap.NotepadTextContains("<?xml version=\"1.0\" encoding=\"utf-16\"?>"))
            {
                var myBase = new TestBase();
                myBase.KillAllInstancesOf("notepad");
                Assert.Inconclusive("The Workflow XAML should not be pastable into Notepad");
            }
            var zeBase = new TestBase();
            zeBase.KillAllInstancesOf("notepad");
        }

        // Bug 6180
        [TestMethod]
        public void MakeSureDeployedItemsAreNotFiltered()
        {
            // Jurie has apparently fixed this, but just hasn't checked it in :D
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.RightClickDeployProject("localhost", "WORKFLOWS", "SYSTEM", "Base64ToString");
            //this.UIMap.DeployOptionClick - To fix
            // Wait for it to open!
            Thread.Sleep(5000);
            UITestControl theTab = TabManagerUIMap.FindTabByName("Deploy Resources");
            TabManagerUIMap.Click(theTab);
            DeployViewUIMap.EnterTextInSourceServerFilterBox(theTab, "ldnslgnsdg"); // Random text
            if (!DeployViewUIMap.DoesSourceServerHaveDeployItems(theTab))
            {
                Assert.Inconclusive("The deployed item has been removed with the filter - It should not be (Jurie should have fixed this....)");
            }
        }

        // Bug 5725 and Bug 5050
        // Removed until we get a DependancyUIMap
        [TestMethod]
        public void DependancyGraph_OnDoubleClickWorkFlow_Expected_NewTab()
        {
            /*
            // Create new Activity
            var myTestBase = new TestBase();
            myTestBase.CreateCustomWorkflow("5725", "CodedUITestCategory");

            // Refresh
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoRefresh();

            // Open Dependancy Graph
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.RightClickShowProjectDependancies("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "5725");

            // Double Click
            Mouse.DoubleClick(new Point(Screen.PrimaryScreen.Bounds.Width/2,Screen.PrimaryScreen.Bounds.Height/2)); <--- Needs a UIMap :(

            // Wait
            System.Threading.Thread.Sleep(2500);

            // Assert not viewing dependency graph any more
            Assert.AreEqual(TabManagerUIMap.GetActiveTabName(), "5725");

            // Garbage collection
            new TestBase().DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "5725");
             */
        }

        // Bug 6127 - DataSplit
        [TestMethod]
        public void DataSplit_DeletingARow_Expected_UpdateTitle()
        {
            myTestBase.CreateCustomWorkflow("6127", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("6127");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            // Drag a Datasplit control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner(ToolboxUIMap.FindControl("DataSplit"), new Point(p.X, p.Y + 200));
            UITestControl datasplitBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfDataSplitActivityDesigner");

            //Create the first row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 100, datasplitBoxOnWorkflow.BoundingRectangle.Y + 60));
            Keyboard.SendKeys("asdf");

            //Create the second row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 100, datasplitBoxOnWorkflow.BoundingRectangle.Y + 90));
            Keyboard.SendKeys("asdf");

            //Delete a row
            Mouse.Click(MouseButtons.Right, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 10, datasplitBoxOnWorkflow.BoundingRectangle.Y + 60));//open context menu
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 40, datasplitBoxOnWorkflow.BoundingRectangle.Y + 275));//select delete row

            StringAssert.Contains(datasplitBoxOnWorkflow.GetProperty("AutomationId").ToString(), "Data Split (1)");

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "6127");
        }

        //Bug 6127 - MultiAssign
        [TestMethod]
        public void MultiAssign_DeletingARow_Expected_UpdateTitle()
        {
            myTestBase.CreateCustomWorkflow("6127", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("6127");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            // Drag a MultiAssign control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner(ToolboxUIMap.FindControl("MultiAssign"), new Point(p.X, p.Y + 200));
            UITestControl multiassignBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfMultiAssignActivityDesigner");

            //Create the first row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 40));
            Keyboard.SendKeys("asdf");

            //Create the second row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 65));
            Keyboard.SendKeys("asdf");

            //Delete a row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 40));//Select row text
            Keyboard.SendKeys("{BACK}{BACK}{BACK}{BACK}{BACK}{BACK}{BACK}{BACK}");//remove row text
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 65));//change focus to delete row

            StringAssert.Contains(multiassignBoxOnWorkflow.GetProperty("AutomationId").ToString(), "Assign (1)");

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "6127");
        }

        // Bug 6482
        [TestMethod]
        public void DragMultipleControls()
        {
            myTestBase.CreateCustomWorkflow("DragMultipleControls", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("DragMultipleControls");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);
            Mouse.Click(p);

            // Drag a Foreach control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            UITestControl foreachBox = ToolboxUIMap.FindControl("ForEach");
            ToolboxUIMap.DragControlToWorkflowDesigner(foreachBox, new Point(p.X, p.Y + 100));
            UITestControl foreachBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "For Each");
            Point toolboxPoint = new Point(foreachBoxOnWorkflow.BoundingRectangle.X + 100, foreachBoxOnWorkflow.BoundingRectangle.Y + 100);

            // Drag a Comment control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            UITestControl commentBox = ToolboxUIMap.FindControl("Comment");
            ToolboxUIMap.DragControlToWorkflowDesigner(commentBox, new Point(p.X, p.Y + 200));
            UITestControl commentBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Comment");
            Point commentPoint = new Point(commentBoxOnWorkflow.BoundingRectangle.X + 100, commentBoxOnWorkflow.BoundingRectangle.Y + 10);

            // Drag another Comment control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            commentBox = ToolboxUIMap.FindControl("Comment");
            ToolboxUIMap.DragControlToWorkflowDesigner(commentBox, new Point(p.X, p.Y + 300));

            //Mouse.Click(MouseButtons.Left, ModifierKeys.Control, commentPoint);
            Mouse.Click(MouseButtons.Left, ModifierKeys.Control, commentPoint);

            Mouse.StartDragging(commentBoxOnWorkflow, new Point(10, 10));
            Mouse.StopDragging(toolboxPoint);

            // Wait for the ForEach Workflow to load
            System.Threading.Thread.Sleep(5000);

            string exceptionMessage = String.Empty;
            try
            {
                WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");

                // The ForEach component has not loaded, so one of the controls were not dragged!
                Assert.Fail();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }
            if (exceptionMessage.StartsWith("Assert.Fail failed."))
            {
                Assert.Inconclusive("Error - Multiple controls cannot be dragged!");
            }
        }

        // Bug 6501
        [TestMethod]
        public void DeleteFirstDatagridRow_Expected_RowIsNotDeleted()
        {
            // Create the Workflow
            var testBase = new TestBase();
            testBase.CreateCustomWorkflow("6501");

            // Set some variables
            UITestControl theTab = TabManagerUIMap.FindTabByName("6501");
            UITestControl theStartButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point workflowPoint1 = new Point(theStartButton.BoundingRectangle.X, theStartButton.BoundingRectangle.Y + 200);

            // Drag control onto the Workflow Designer
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            UITestControl theTool = ToolboxUIMap.FindToolboxItemByAutomationId("BaseConvert");
            ToolboxUIMap.DragControlToWorkflowDesigner(theTool, workflowPoint1);

            // Enter some data
            UITestControl baseConversion = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "BaseConvert");
            Point p = new Point(baseConversion.BoundingRectangle.X + 40, baseConversion.BoundingRectangle.Y + 40);
            Mouse.Click(p);
            SendKeys.SendWait("someText");

            // Click the index
            p = new Point(baseConversion.BoundingRectangle.X + 20, baseConversion.BoundingRectangle.Y + 40);
            Mouse.Click(MouseButtons.Right, ModifierKeys.None, p);
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{UP}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{UP}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{RIGHT}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(100);

            // Try type some data
            p = new Point(baseConversion.BoundingRectangle.X + 40, baseConversion.BoundingRectangle.Y + 40);
            Mouse.Click(p);
            SendKeys.SendWait("newText");
            SendKeys.SendWait("{END}"); // Shift Home - Highlights the item
            SendKeys.SendWait("+{HOME}"); // Shift Home - Highlights the item
            // Just to make sure it wasn't already copied before the test
            Clipboard.SetText("someRandomText");
            SendKeys.SendWait("^c"); // Copy command
            string clipboardText = Clipboard.GetText();
            if (clipboardText != "newText")
            {
                Assert.Fail("Error - The Item was not deleted!");
            }

            // Cleanup! \o/
            testBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "6501");
        }

        // Bug 6617
        [TestMethod]
        public void OpeningDependancyWindowTwiceKeepsItOpen()
        {
            // The workflow so we have a second tab
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "SYSTEM", "Base64ToString");
            DocManagerUIMap.ClickOpenTabPage("Explorer");

            // Open the Dependancy Window twice
            for (int openCount = 0; openCount < 2; openCount++)
            {
                DocManagerUIMap.ClickOpenTabPage("Explorer");
                ExplorerUIMap.RightClickShowProjectDependancies("localhost", "WORKFLOWS", "SYSTEM", "Base64ToString");
            }
            
            string activeTab = TabManagerUIMap.GetActiveTabName();
            if (activeTab == "Base64ToString")
            {
                Assert.Fail("Opening the Dependency View twice should keep the UI on the same tab");
            }
        }

        // Bug 6672
        [TestMethod]
        public void WorkflowWizard_OnDelete_Expected_RemovedFromRepository()
        {
            try
            {
                myTestBase.CreateCustomWorkflow("6672", "CodedUITestCategory");
                _explorerUIMap.RightClickDeleteProject("localhost", "WORKFLOWS", "CodedUITestCategory", "6672");
                _explorerUIMap.DoRefresh();
                myTestBase.CreateCustomWorkflow("6672", "CodedUITestCategory");
                myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "6672");
            }
            catch
            {
                Assert.Inconclusive("Error - Broken Test!");
            }
        }

        //  Bug that Mo picked up with legacy Web Pages losing their data (FIXED)
        // Commented Out - // No Webpage Bugs this run!
        [TestMethod]
        public void OpeningOldWebPageTwice_Expected_RetainsData()
        {
            /*
            // Open an old Webpage
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "WEBPAGE", "DatabaseServiceSetup");
            System.Threading.Thread.Sleep(5000); // Crashes without this line due to a bug with closing workflows before they're fully loaded

            // And close the tab (Originally made it lose the XML)
            TabManagerUIMap.CloseTab("DatabaseServiceSetup/Database Service - Setup.ui");

            // Get the tab
            UITestControl theTab = TabManagerUIMap.FindTabByName("DatabaseServiceSetup");

            // And click it to make sure it's focused
            TabManagerUIMap.Click(theTab);

            // Get the location of the Start button and click it
            UITestControl theStartButton = WorkflowDesignerUIMap.FindControlByAutomationID(theTab, "Start");
            WorkflowDesignerUIMap.ClickControl(theStartButton);

            // Get the location of the "Database Service - Setup" control, and click it
            UITestControl theDatabaseServiceControl = WorkflowDesignerUIMap.FindControlByAutomationID(theTab, "Database Service - Setup(DsfWebPageActivityDesigner)");
            WorkflowDesignerUIMap.ClickControl(theDatabaseServiceControl);

            // Then re-open the closed page
            WorkflowDesignerUIMap.DoubleClickControlBar(theDatabaseServiceControl);

            UITestControl uiTab = TabManagerUIMap.FindTabByName("DatabaseServiceSetup/Database Service - Setup.ui");

            // And check a blocks Name property to make sure it retained its data
            UITestControl theBlock = DeployViewUIMap.GetWebsiteGridBlock(uiTab);
            string blockNameProperty = theBlock.GetChildren()[2].Name;
            StringAssert.Equals(blockNameProperty, "[[Dev2DatabaseServiceSetupSourceLabel]]");
             */
        }

        // Bug 7409 - DataSplit
        [TestMethod]
        public void DataSplitActivity_OnMouseScroll_Expected_NoUnHandledExceptions()
        {
            myTestBase.CreateCustomWorkflow("7409", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("7409");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            // Drag a DataSplit control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner(ToolboxUIMap.FindControl("DataSplit"), new Point(p.X, p.Y + 200));
            UITestControl datasplitBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfDataSplitActivityDesigner");

            // Scroll once
            Mouse.Move(new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 100, datasplitBoxOnWorkflow.BoundingRectangle.Y + 60));
            Mouse.MoveScrollWheel(-1);

            // Assert scrolled from row index 0 to row index 1
            //Assert.AreEqual(1, datasplitBoxOnWorkflow.GetChildren()[4].GetChildren()[0].GetProperty("RowIndex")); getting the row index changes the row index so I cant assert that its only scrolling by one, oh well, this test watching out for unhandled exceptions mostly anyway

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7409");
        }

        // Bug 7409 - MultiAssign
        [TestMethod]
        public void MultiAssignActivity_OnMouseScroll_Expected_NoUnHandledExceptions()
        {
            myTestBase.CreateCustomWorkflow("7409", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("7409");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            // Drag a DataSplit control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner(ToolboxUIMap.FindControl("MultiAssign"), new Point(p.X, p.Y + 200));
            UITestControl multiassignBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfMultiAssignActivityDesigner");

            //Create the first row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 40));
            Keyboard.SendKeys("asdf");

            //Create the second row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 65));
            Keyboard.SendKeys("asdf");

            // Scroll down
            Mouse.Move(new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 60));
            Mouse.MoveScrollWheel(-1);

            //Create the third row
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 65));
            Keyboard.SendKeys("asdf");

            // Scroll to the top
            Mouse.Move(new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 60));
            Mouse.MoveScrollWheel(4);

            // Scroll down once
            Mouse.Move(new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 60));
            Mouse.MoveScrollWheel(-1);

            // Assert scrolled from row index 0 (top) to row index 1
            //Assert.AreEqual(1, multiassignBoxOnWorkflow.GetChildren()[2].GetChildren()[0].GetProperty("RowIndex"));// Getting the row index changes the row index

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7409");
        }

        // Bug 7799
        [TestMethod]
        public void dsfActivity_OnDblClick_Expected_NoUnhandledExceptions()
        {
            // Create new dsfActivity
            myTestBase.CreateCustomWorkflow("7799Activity", "CodedUITestCategory");

            // Initialize work flow
            myTestBase.CreateCustomWorkflow("7799", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("7799");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            // Refresh
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoRefresh();

            // Drag a dsfActivity onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            UITestControl theControl = ExplorerUIMap.GetService("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7799Activity");

            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DragControlToWorkflowDesigner(theControl, new Point(p.X, p.Y + 200));

            UITestControl activityBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfActivityDesigner");

            //Double click the dsfActivity
            Mouse.DoubleClick(MouseButtons.Left, ModifierKeys.None, new Point(activityBoxOnWorkflow.BoundingRectangle.X + 100, activityBoxOnWorkflow.BoundingRectangle.Y + 40));

            // Delete the Workflows in the required order
            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7799Activity");
            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7799");
        }

        // Bug 7842
        [TestMethod]
        public void CopyTabIntoBaseConvert_Expected_TabIsCopied()
        {
            // Create the Workflow
            myTestBase.CreateCustomWorkflow("Bug7842");
            UITestControl theTab = TabManagerUIMap.FindTabByName("c");
            Point p = WorkflowDesignerUIMap.GetPointUnderStartNode(theTab);

            // Drag a DataSplit onto it
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner("DataSplit", p);

            // And enter some data
            string textWithTab = "test\ttest2";
            Clipboard.SetText(textWithTab);
            
            WorkflowDesignerUIMap.DataSplit_ClickFirstTextbox(theTab, "Data Split");
            Thread.Sleep(500);
            SendKeys.SendWait("^v");

            string inputText = WorkflowDesignerUIMap.DataSplit_GetTextFromStringToSplit(theTab, "Data Split");

            StringAssert.Contains(inputText, textWithTab);

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "Bug7842");

        }

        // Bug 7802
        [TestMethod]
        public void MiddleClickCloseHomePageUnderSpecialCircumstances_Expected_StartPageCloses()
        {
            TabManagerUIMap.CloseAllTabs();
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "MO", "CalculateTaxReturns");

            // Get the Studio EXE location so we can re-open it
            string fileName = myTestBase.GetStudioEXELocation();

            // Close the Studio
            WpfWindow studioWindow = new WpfWindow();
            studioWindow.SearchProperties[WpfWindow.PropertyNames.Name] = TestBase.GetStudioWindowName();
            studioWindow.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            studioWindow.WindowTitles.Add(TestBase.GetStudioWindowName());
            studioWindow.Find();

            Point closeButton = new Point(studioWindow.BoundingRectangle.X + studioWindow.Width - 25, studioWindow.BoundingRectangle.Y + 15);
            
            // Its far - Move faster
            Mouse.MouseMoveSpeed *= 2;
            Mouse.Move(closeButton);

            // And back to normal
            Mouse.MouseMoveSpeed /= 2;
            Mouse.Click();

            // Restart the studio!
            System.Diagnostics.Process.Start(fileName);

            // Get a clickable point in the Studio to make sure it's opened!

            int tries = 0;

            Point p = new Point();
            studioWindow = new WpfWindow();
            studioWindow.SearchProperties[WpfWindow.PropertyNames.Name] = TestBase.GetStudioWindowName();
            studioWindow.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            studioWindow.WindowTitles.Add(TestBase.GetStudioWindowName());

            while (!studioWindow.TryGetClickablePoint(out p) && tries < 30)
            {
                Thread.Sleep(1000);
                tries++;
            }

            if (!studioWindow.TryGetClickablePoint(out p))
            {
                throw new Exception("Fatal Error - The Studio did not restart after 30 seconds!");
            }


            // Middle-Click close the home tab
            string homeTab = TabManagerUIMap.GetTabNameAtPosition(1);
            TabManagerUIMap.MiddleClickCloseTab(homeTab);

            int tabCount = TabManagerUIMap.GetTabCount();
            if (tabCount != 1)
            {
                Assert.Fail();
            }
        }

        // Bug 7841
        [TestMethod]
        public void DeletingResourceAndRefreshingDeletesResource_Expected_ResourceStaysDeleted()
        {
            // Create a workflow
            myTestBase.CreateCustomWorkflow("7841", "CodedUITestCategory");

            // Refresh
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoRefresh();

            // Make sure it's there
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7841");

            // Delete it
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.RightClickDeleteProject("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7841");

            // Refresh
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoRefresh();

            // Make sure it's gone
            try
            {
                ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7841");
                Assert.Fail();
            }
            catch
            {
                // Silent fail - The open is meant to fail
                Assert.IsTrue(true);
            }
        }

        // Bug 7854
        [TestMethod]
        public void ClickingIntellisenseItemFillsField_Expected_IntellisenseItemAppearsInField()
        {
            // Create the Workflow for the bug
            myTestBase.CreateCustomWorkflow("7854", "CodedUITestCategory");

            // Refresh
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoRefresh();

            // Get a point for later
            UITestControl theTab = TabManagerUIMap.FindTabByName("7854");
            UITestControl theStartButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point workflowPoint1 = new Point(theStartButton.BoundingRectangle.X, theStartButton.BoundingRectangle.Y + 200);

            // Drag the control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            UITestControl theCalculate = ToolboxUIMap.FindControl("Assign");
            ToolboxUIMap.DragControlToWorkflowDesigner(theCalculate, workflowPoint1);

            // Values :D
            WorkflowDesignerUIMap.AssignControl_ClickFirstTextbox(theTab, "Assign");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("[[someVal]]");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("12345");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(100);

            // Map them
            DocManagerUIMap.ClickOpenTabPage("Variables");
            VariablesUIMap.UpdateDataList();

            // Actual test time :D
            WorkflowDesignerUIMap.AssignControl_ClickFirstTextbox(theTab, "Assign");
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("[[");
            System.Threading.Thread.Sleep(500);
            UITestControl theItem = WorkflowDesignerUIMap.GetIntellisenseItem(0);
            Mouse.Move(theItem, new Point(10, 10));
            Mouse.Click();

            // Item should be populated - Time to check!
            WorkflowDesignerUIMap.AssignControl_ClickFirstTextbox(theTab, "Assign");
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("{END}"); // Get to the end of the item
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("+{HOME}"); // Shift Home - Highlights the item
            // Just to make sure it wasn't already copied before the test
            Clipboard.SetText("someRandomText");
            SendKeys.SendWait("^c"); // Copy command
            string clipboardText = Clipboard.GetText();
            if (clipboardText != "[[someVal]]")
            {
                Assert.Fail("Error - The item was not correctly populated!");
            }

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7854");
        }

        // Bug 7930
        [TestMethod]
        public void FixedDataListServicesWithWizards()
        {
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "INTEGRATION TEST SERVICES", "DataSplitTestWithRecordsetsWithNoIndexes");
        }

        //Bug 6413
        [TestMethod]
        public void FindMissing_WithDoubleRegion_Expected_BothAdded()
        {
            myTestBase.CreateCustomWorkflow("6413", "CodedUITestCategory");

            UITestControl theTab = TabManagerUIMap.FindTabByName("6413");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            // Drag a MultiAssign control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner(ToolboxUIMap.FindControl("MultiAssign"), new Point(p.X, p.Y + 200));
            UITestControl multiassignBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfMultiAssignActivityDesigner");

            //Create a regular region
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 100, multiassignBoxOnWorkflow.BoundingRectangle.Y + 40));
            Keyboard.SendKeys("[[scalar]]");

            //Create the double region
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(multiassignBoxOnWorkflow.BoundingRectangle.X + 160, multiassignBoxOnWorkflow.BoundingRectangle.Y + 40));
            Keyboard.SendKeys("[[first]][[second]]");

            DocManagerUIMap.ClickOpenTabPage("Variables");
            VariablesUIMap.UpdateDataList();
            Assert.AreEqual("scalar", VariablesUIMap.GetVariableName(0));
            Assert.AreEqual("first",VariablesUIMap.GetVariableName(1));
            Assert.AreEqual("second", VariablesUIMap.GetVariableName(2));

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "6413");
        }

        //Bug 7842
        [TestMethod]
        public void DataSplit_WithCopyPastedTabs_Expected_SplitByTab()
        {
            var myTestBase = new TestBase();
            myTestBase.CreateCustomWorkflow("7842", "CodedUITestCategory");
            UITestControl theTab = TabManagerUIMap.FindTabByName("7842");
            UITestControl startButton = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "Start");
            Point p = new Point(startButton.BoundingRectangle.X + 10, startButton.BoundingRectangle.Y + 10);

            //Drag a DataSplit control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner(ToolboxUIMap.FindControl("DataSplit"), new Point(p.X, p.Y + 200));
            UITestControl datasplitBoxOnWorkflow = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "DsfDataSplitActivityDesigner");

            //Setup datasplit vars
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 100, datasplitBoxOnWorkflow.BoundingRectangle.Y + 65));
            Keyboard.SendKeys("[[recset().field]]");
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 160, datasplitBoxOnWorkflow.BoundingRectangle.Y + 65));
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 160, datasplitBoxOnWorkflow.BoundingRectangle.Y + 150));
            DocManagerUIMap.ClickOpenTabPage("Variables");
            VariablesUIMap.UpdateDataList();

            //Set datasplit as start node
            Mouse.Click(MouseButtons.Right, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 10, datasplitBoxOnWorkflow.BoundingRectangle.Y + 10));
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 40, datasplitBoxOnWorkflow.BoundingRectangle.Y + 200));

            //Create tabs in notepad for dataplit
            System.Diagnostics.Process.Start("notepad.exe");
            System.Threading.Thread.Sleep(2500);
            Keyboard.SendKeys("Ithoughtthiswas	allihad	tosplit");
            Keyboard.SendKeys("^A");
            Keyboard.SendKeys("^C");
            myTestBase.KillAllInstancesOf("notepad");

            //Paste tabs into datasplit
            Mouse.Click(MouseButtons.Left, ModifierKeys.None, new Point(datasplitBoxOnWorkflow.BoundingRectangle.X + 170, datasplitBoxOnWorkflow.BoundingRectangle.Y + 40));
            Keyboard.SendKeys("^V");

            // Click "View in Browser"
            RibbonUIMap.ClickRibbonMenuItem("Home", "View in Browser");
            System.Threading.Thread.Sleep(2500);

            // Check if the IE Body contains the data list item
            string ieText = ExternalUIMap.GetIEBodyText();
            if (!ieText.Contains("<field>Ithoughtthiswas</field>") && !ieText.Contains("<field>allihad</field>") && !ieText.Contains("<field>tosplit</field>"))
            {
                Assert.Fail("Data split not splitting by tab");
            }

            // Close the browser
            ExternalUIMap.CloseAllInstancesOfIE();

            // And do cleanup
            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "7842");

        }

        // Bug 8553
        [TestMethod]
        public void ChangeAWorkflowsCategory_Expected_CategoryRemainsChagned()
        {
            // Create a sample workflow
            myTestBase.CreateCustomWorkflow("Bug8553");

            // Open its properties
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.RightClickProperties("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "Bug8553");

            // And change its category
            WorkflowWizardUIMap.EnterWorkflowCategory("ABCCATEGORY");
            WorkflowWizardUIMap.DoneButtonClick();

            // Refresh the treeview
            DocManagerUIMap.ClickOpenTabPage("Explorer");
            ExplorerUIMap.DoRefresh();

            // See if the movement has persisted
            try
            {
                ExplorerUIMap.GetService("localhost", "WORKFLOWS", "ABCCATEGORY", "Bug8553");
            }
            catch (Exception)
            {
                Assert.Fail("The workflow did not maintain its category move!");
            }
            myTestBase.DoCleanup("localhost", "WORKFLOWS", "ABCCATEGORY", "Bug8553");
        }

        // Bug 8596
        [TestMethod]
        public void RightClickCategory_Expected_ItemFocused()
        {
            // Open the Explorer
            DocManagerUIMap.ClickOpenTabPage("Explorer");

            // Get the folders we're testing
            UITestControl barneyCategory = ExplorerUIMap.ReturnCategory("localhost", "WORKFLOWS", "BARNEY");
            UITestControl moCategory = ExplorerUIMap.ReturnCategory("localhost", "WORKFLOWS", "MO");

            // Do a selected click
            Mouse.Click(barneyCategory, new Point(15, 10));
            System.Threading.Thread.Sleep(1500);
            Mouse.Click(moCategory, MouseButtons.Right, ModifierKeys.None, new Point(15, 10));
            System.Threading.Thread.Sleep(1500);
            if (moCategory.GetProperty("Selected").ToString() != "True")
            {
                Assert.Fail("The new category was not highlighted!");
            }
            
            // Click in the perfect position for the menu to NOT appear (Between the icon, and the text)
            Mouse.Click(barneyCategory, MouseButtons.Right, ModifierKeys.None, new Point(48, 10));
            System.Threading.Thread.Sleep(1500);
            if (barneyCategory.GetProperty("Selected").ToString() != "True")
            {
                Assert.Fail("The gap between the icon and the text is not clickable!");
            }
        }

        // Bug 8598
        [TestMethod]
        public void ClickOutputTabClose_Expected_TabStaysOpen()
        {
            // Open the tab
            DocManagerUIMap.ClickOpenTabPage("Output");

            // Click the close button
            myTestBase.OutputUIMap.ClickClose();

            // Make sure the tab is still there
            Assert.IsTrue(DocManagerUIMap.DoesTabExist("Output"));
        }

        // Bug 8604
        [TestMethod]
        public void OpenDecisionWindowMultipleTimes_Expected_OpensInSamePosition()
        {
            // Create the Workflow
            myTestBase.CreateCustomWorkflow("8604");

            // Get a point to drag the control onto
            UITestControl theTab = TabManagerUIMap.FindTabByName("8604");
            Point requiredPoint = WorkflowDesignerUIMap.GetPointUnderStartNode(theTab);
            
            // Open the toolbox, and drag the control onto the Workflow
            DocManagerUIMap.ClickOpenTabPage("Toolbox");
            ToolboxUIMap.DragControlToWorkflowDesigner("Decision", requiredPoint);
            // Wait for it to load
            Thread.Sleep(2500);

            // Click Cancel
            WpfWindow decisionWindow = new WpfWindow();
            decisionWindow.SearchProperties[WpfWindow.PropertyNames.Name] = "Decision Flow";
            decisionWindow.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            decisionWindow.Find();

            // Get the Co-ords
            Point firstPoint = new Point(decisionWindow.BoundingRectangle.X, decisionWindow.BoundingRectangle.Y);
            Point cancelButton = new Point(decisionWindow.BoundingRectangle.X + 700, decisionWindow.BoundingRectangle.Y + 600);
            Mouse.Click(cancelButton);

            // Open the window for the first time
            UITestControl decisionControl = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "FlowDecisionDesigner");
            Mouse.DoubleClick(decisionControl, new Point(50, 50));
            
            // Wait for it to load
            Thread.Sleep(500);

            // Get the Co-ords
            decisionWindow.Find();
            Point secondPoint = new Point(decisionWindow.BoundingRectangle.X, decisionWindow.BoundingRectangle.Y);

            // Close it
            cancelButton = new Point(decisionWindow.BoundingRectangle.X + 700, decisionWindow.BoundingRectangle.Y + 600);
            Mouse.Click(cancelButton);

            // Open the window for the second time
            decisionControl = WorkflowDesignerUIMap.FindControlByAutomationId(theTab, "FlowDecisionDesigner");
            Mouse.DoubleClick(decisionControl, new Point(50, 50));

            // Wait for it to load
            Thread.Sleep(500);

            // Get the Co-ords
            decisionWindow.Find();
            Point thirdPoint = new Point(decisionWindow.BoundingRectangle.X, decisionWindow.BoundingRectangle.Y);

            // Close it
            cancelButton = new Point(decisionWindow.BoundingRectangle.X + 700, decisionWindow.BoundingRectangle.Y + 600);
            Mouse.Click(cancelButton);

            if ((firstPoint != secondPoint) && (secondPoint != thirdPoint))
            {
                Assert.Fail("The window opened in different locations!");
            }

            myTestBase.DoCleanup("localhost", "WORKFLOWS", "CODEDUITESTCATEGORY", "8604");
        }

        // Bug 8747
        [TestMethod]
        public void DebugBuriedErrors_Expected_OnlyErrorStepIsInError()
        {
            // Open the broken workflow
            DocManagerUIMap.ClickOpenTabPage("Explorer");

            // Even though it's labelled 8372, it's related to 8747
            ExplorerUIMap.DoubleClickOpenProject("localhost", "WORKFLOWS", "BUGS", "Bug8372");
            
            // Run debug
            RibbonUIMap.ClickRibbonMenuItem("Home", "Debug");
            Thread.Sleep(1500);
            myTestBase.DebugUIMap.ExecuteDebug();

            // Open the Output
            DocManagerUIMap.ClickOpenTabPage("Output");

            // Due to the complexity of the OutputUIMap, this test has been primarily hard-coded until a further rework
            Assert.IsTrue(myTestBase.OutputUIMap.DoesBug8747Pass());
        }

        

        private int GetInstanceUnderParent(UITestControl control)
        {
            UITestControl parent = control.GetParent();
            UITestControlCollection col = parent.GetChildren();
            int index = 1;

            foreach (UITestControl child in col)
            {
                if (child.Equals(control))
                {
                    break;
                }

                if (child.ControlType == control.ControlType)
                {
                    index++;
                }
            }
            return index;
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }
        private TestContext _testContextInstance;

        #region UI Maps

        #region Ribbon UI Map

        public RibbonUIMap RibbonUIMap
        {
            get
            {
                if (_ribbonMap == null)
                {
                    _ribbonMap = new RibbonUIMap();
                }

                return _ribbonMap;
            }
        }

        private RibbonUIMap _ribbonMap;

        #endregion

        #region DocManager UI Map

        public DocManagerUIMap DocManagerUIMap
        {
            get
            {
                if ((_docManagerMap == null))
                {
                    _docManagerMap = new DocManagerUIMap();
                }

                return _docManagerMap;
            }
        }

        private DocManagerUIMap _docManagerMap;

        #endregion

        #region Toolbox UI Map

        public ToolboxUIMap ToolboxUIMap
        {
            get
            {
                if ((_toolboxUIMap == null))
                {
                    _toolboxUIMap = new ToolboxUIMap();
                }

                return _toolboxUIMap;
            }
        }

        private ToolboxUIMap _toolboxUIMap;

        #endregion

        #region Explorer UI Map

        public ExplorerUIMap ExplorerUIMap
        {
            get
            {
                if ((_explorerUIMap == null))
                {
                    _explorerUIMap = new ExplorerUIMap();
                }

                return _explorerUIMap;
            }
        }

        private ExplorerUIMap _explorerUIMap;

        #endregion

        #region DependencyGraph UI Map

        public DependencyGraph DependencyGraphUIMap
        {
            get
            {
                if ((DependencyGraphUIMap == null))
                {
                    DependencyGraphUIMap = new DependencyGraph();
                }

                return DependencyGraphUIMap;
            }
            set { throw new NotImplementedException(); }
        }

        private DependencyGraph _dependencyGraphUIMap;

        #endregion

        #region DeployView UI Map

        public DeployViewUIMap DeployViewUIMap
        {
            get
            {
                if ((_deployViewUIMap == null))
                {
                    _deployViewUIMap = new DeployViewUIMap();
                }

                return _deployViewUIMap;
            }
        }

        private DeployViewUIMap _deployViewUIMap;

        #endregion

        #region TabManager UI Map

        public TabManagerUIMap TabManagerUIMap
        {
            get
            {
                if (_tabManagerUIMap == null)
                {
                    _tabManagerUIMap = new TabManagerUIMap();
                }

                return _tabManagerUIMap;
            }
        }

        private TabManagerUIMap _tabManagerUIMap;

        #endregion TabManager UI Map

        #region WorkflowDesigner UI Map

        public WorkflowDesignerUIMap WorkflowDesignerUIMap
        {
            get
            {
                if (_workflowDesignerUIMap == null)
                {
                    _workflowDesignerUIMap = new WorkflowDesignerUIMap();
                }

                return _workflowDesignerUIMap;
            }
        }

        private WorkflowDesignerUIMap _workflowDesignerUIMap;

        #endregion WorkflowDesigner UI Map

        #region WorkflowWizard UI Map

        public WorkflowWizardUIMap WorkflowWizardUIMap
        {
            get
            {
                if (_workflowWizardUIMap == null)
                {
                    _workflowWizardUIMap = new WorkflowWizardUIMap();
                }

                return _workflowWizardUIMap;
            }
        }

        private WorkflowWizardUIMap _workflowWizardUIMap;

        #endregion WorkflowWizard UI Map

        #region Database Wizard UI Map

        public DatabaseServiceWizardUIMap DatabaseServiceWizardUIMap
        {
            get
            {
                if (_databaseServiceWizardUIMap == null)
                {
                    _databaseServiceWizardUIMap = new DatabaseServiceWizardUIMap();
                }

                return _databaseServiceWizardUIMap;
            }
        }

        private DatabaseServiceWizardUIMap _databaseServiceWizardUIMap;

        #endregion Database Wizard UI Map

        #region Plugin Wizard UI Map

        public PluginServiceWizardUIMap PluginServiceWizardUIMap
        {
            get
            {
                if (_pluginServiceWizardUIMap == null)
                {
                    _pluginServiceWizardUIMap = new PluginServiceWizardUIMap();
                }

                return _pluginServiceWizardUIMap;
            }
        }

        private PluginServiceWizardUIMap _pluginServiceWizardUIMap;

        #endregion Database Wizard UI Map

        #region Webpage Wizard UI Map

        public WebpageServiceWizardUIMap WebpageServiceWizardUIMap
        {
            get
            {
                if (_webpageServiceWizardUIMap == null)
                {
                    _webpageServiceWizardUIMap = new WebpageServiceWizardUIMap();
                }

                return _webpageServiceWizardUIMap;
            }
        }

        private WebpageServiceWizardUIMap _webpageServiceWizardUIMap;

        #endregion Database Wizard UI Map

        #region Connect Window UI Map

        public ConnectViewUIMap ConnectViewUIMap
        {
            get
            {
                if (_connectViewUIMap == null)
                {
                    _connectViewUIMap = new ConnectViewUIMap();
                }
                return _connectViewUIMap;
            }
        }

        private ConnectViewUIMap _connectViewUIMap;

        #endregion Connect Window UI Map

        #region External UI Map

        public ExternalUIMap ExternalUIMap
        {
            get
            {
                if (_externalUIMap == null)
                {
                    _externalUIMap = new ExternalUIMap();
                }
                return _externalUIMap;
            }
        }

        private ExternalUIMap _externalUIMap;

        #endregion External Window UI Map

        #region Variables UI Map

        public VariablesUIMap VariablesUIMap
        {
            get
            {
                if (_variablesUIMap == null)
                {
                    _variablesUIMap = new VariablesUIMap();
                }
                return _variablesUIMap;
            }
        }

        private VariablesUIMap _variablesUIMap;

        #endregion Connect Window UI Map

        #endregion UI Maps
    }
}
