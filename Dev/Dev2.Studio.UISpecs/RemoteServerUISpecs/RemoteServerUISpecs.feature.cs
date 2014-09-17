﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18063
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dev2.Studio.UI.Specs.RemoteServerUISpecs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UITesting.CodedUITestAttribute()]
    public partial class RemoteServerUISpecsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "RemoteServerUISpecs.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "RemoteServerUISpecs", "In order to avoid silly mistakes\r\nAs a math idiot\r\nI want to be told the sum of t" +
                    "wo numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "RemoteServerUISpecs")))
            {
                Dev2.Studio.UI.Specs.RemoteServerUISpecs.RemoteServerUISpecsFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line 8
    testRunner.Given("I click \"EXPLORER,UI_localhost_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
    testRunner.Given("I click \"RIBBONSETTINGS\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
    testRunner.And("I clear table \"ACTIVETAB,UI_SettingsView_AutoID,SecurityViewContent,ServerPermiss" +
                    "ionsDataGrid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
    testRunner.And("I clear table \"ACTIVETAB,UI_SettingsView_AutoID,SecurityViewContent,ResourcePermi" +
                    "ssionsDataGrid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
    testRunner.And("\"SECURITYPUBLICADMINISTRATOR\" is unchecked", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
    testRunner.And("I click \"SECURITYPUBLICADMINISTRATOR\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
       testRunner.And("I click \"SECURITYSAVE\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
    testRunner.Given("all tabs are closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Testing Remote Server Connection Creating Remote Workflow and Executing")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "RemoteServerUISpecs")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("RemtoeServer")]
        public virtual void TestingRemoteServerConnectionCreatingRemoteWorkflowAndExecuting()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Testing Remote Server Connection Creating Remote Workflow and Executing", new string[] {
                        "RemtoeServer"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 20
 testRunner.Given("I have Warewolf running", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.And("all tabs are closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.Given("I click \"EXPLORER,UI_localhost_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Address",
                        "AuthType",
                        "UserName",
                        "Password"});
            table1.AddRow(new string[] {
                        "http://localhost:3142",
                        "Public",
                        "",
                        ""});
#line 23
    testRunner.Given("I create a new remote connection \"Test\" as", ((string)(null)), table1, "Given ");
#line 27
    testRunner.Given("I click \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
 testRunner.Given("I click \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID,UI_BARNEY_AutoID,UI_Dec" +
                    "ision Testing_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
    testRunner.Then("\"EXPLORER,UI_Test (http://localhost:3142/)_AutoID,UI_BARNEY_AutoID,UI_Decision Te" +
                    "sting_AutoID,UI_CanEdit_AutoID\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
    testRunner.Then("\"EXPLORER,UI_Test (http://localhost:3142/)_AutoID,UI_BARNEY_AutoID,UI_Decision Te" +
                    "sting_AutoID,UI_CanExecute_AutoID\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
 testRunner.Given("I double click \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID,UI_BARNEY_AutoID" +
                    ",UI_Decision Testing_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
 testRunner.Given("\"WORKFLOWDESIGNER,Decision Testing(FlowchartDesigner)\" is visible within \"5\" seco" +
                    "nds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
 testRunner.Given("I click \"EXPLORER,UI_localhost_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 36
 testRunner.Given("I click \"EXPLORER,UI_localhost_AutoID,UI_BARNEY_AutoID,UI_Decision Testing_AutoID" +
                    "\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 37
 testRunner.Given("I double click \"EXPLORER,UI_localhost_AutoID,UI_BARNEY_AutoID,UI_Decision Testing" +
                    "_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.Given("\"WORKFLOWDESIGNER,Decision Testing(FlowchartDesigner)\" is visible within \"5\" seco" +
                    "nds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 39
 testRunner.And("I send \"{F6}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.Given("\"DEBUGOUTPUT,Assign\" is visible within \"25\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 42
 testRunner.Given("I click \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
 testRunner.And("I click \"RIBBONNEWENDPOINT\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.Given("I send \"Assign\" to \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 46
 testRunner.And("I drag \"TOOLASSIGN\" onto \"WORKSURFACE,StartSymbol\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.Given("I send \"rec().a\" to \"WORKSURFACE,Assign (1)(MultiAssignDesigner),SmallViewContent" +
                    ",SmallDataGrid,UI_ActivityGridRow_0_AutoID,UI_TextBox_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 49
 testRunner.And("I send \"{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("I send \"Warewolf\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("I send \"{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("I send \"rec().a{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I send \"TestRemote{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.When("I double click point \"5,5\" on \"WORKSURFACE,Assign (2)(MultiAssignDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.When("I click \"WORKSURFACE,Assign (2)(MultiAssignDesigner),DoneButton\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 56
 testRunner.Then("\"WORKSURFACE,Assign (2)(MultiAssignDesigner),SmallViewContent\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 58
    testRunner.Given("I double click \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 59
    testRunner.Given("I send \"{Delete}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 60
 testRunner.Given("I send \"Data Merge\" to \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 61
    testRunner.Given("I drag \"TOOLDATAMERGE\" onto \"WORKSURFACE,Assign (2)(MultiAssignDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 63
 testRunner.Given("I send \"[[rec(1).a]]\" to \"TOOLDATAMERGESMALLVIEWGRID,UI__Row1_InputVariable_AutoI" +
                    "D\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
 testRunner.And("I send \"{TAB}{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("I send \"8\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("I send \"{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.And("I send \"[[rec(2).a]]\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.And("I send \"{TAB}{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.And("I send \"10\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And("I send \"[[result]]\" to \"WORKSURFACE,Data Merge (2)(DataMergeDesigner),SmallViewCo" +
                    "ntent,UI__Resulttxt_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("I send \"{F6}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.Given("\"DEBUGOUTPUT,Assign\" is visible within \"10\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 75
 testRunner.Given("I click \"RIBBONSAVE\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 76
 testRunner.And("I send \"{TAB}{TAB}{TAB}{TAB}{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.And("I send \"Remote1\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And("I send \"{TAB}{TAB}{TAB}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.And("I send \"{Enter}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
    testRunner.Given("\"WORKFLOWDESIGNER,Remote1(FlowchartDesigner)\" is visible within \"12\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 81
    testRunner.And("all tabs are closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.Given("I click \"EXPLORER,UI_localhost_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 84
 testRunner.Given("I click \"EXPLORER,UI_localhost_AutoID,UI_Examples_AutoID,UI_Utility - Email_AutoI" +
                    "D\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 85
 testRunner.Given("I double click \"EXPLORER,UI_localhost_AutoID,UI_Examples_AutoID,UI_Utility - Emai" +
                    "l_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 86
 testRunner.Given("\"WORKFLOWDESIGNER,Utility - Email(FlowchartDesigner)\" is visible within \"5\" secon" +
                    "ds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 87
 testRunner.Given("I click \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 88
 testRunner.Given("I click \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID,UI_Remote1_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 89
 testRunner.Given("I drag \"EXPLORER,UI_Test (http://localhost:3142/)_AutoID,UI_Remote1_AutoID\" onto " +
                    "\"WORKFLOWDESIGNER,Utility - Email(FlowchartDesigner),Email(EmailDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 90
 testRunner.Given("\"WORKFLOWDESIGNER,Utility - Email(FlowchartDesigner),Remote1(ServiceDesigner)\" is" +
                    " visible within \"5\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 91
 testRunner.And("I send \"{F6}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
