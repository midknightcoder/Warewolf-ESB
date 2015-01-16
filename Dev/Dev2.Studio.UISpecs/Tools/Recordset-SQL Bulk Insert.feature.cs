﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34209
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dev2.Studio.UI.Specs.Tools
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UITesting.CodedUITestAttribute()]
    public partial class Recordset_SQLBulkInsertFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Recordset-SQL Bulk Insert.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Recordset-SQL Bulk Insert", "In order to avoid silly mistakes\r\nAs a math idiot\r\nI want to be told the sum of t" +
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
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Recordset-SQL Bulk Insert")))
            {
                Dev2.Studio.UI.Specs.Tools.Recordset_SQLBulkInsertFeature.FeatureSetup(null);
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
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Correcting errors on sql bulk insert clicking Done shows small view (using ids)")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Recordset-SQL Bulk Insert")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SQLBulkInsert")]
        public virtual void CorrectingErrorsOnSqlBulkInsertClickingDoneShowsSmallViewUsingIds()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Correcting errors on sql bulk insert clicking Done shows small view (using ids)", new string[] {
                        "SQLBulkInsert"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have Warewolf running", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("all tabs are closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.Given("I click \"EXPLORERFILTERCLEARBUTTON\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 11
 testRunner.Given("I click \"EXPLORERCONNECTCONTROL\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 12
 testRunner.Given("I click \"U_UI_ExplorerServerCbx_AutoID_localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
 testRunner.And("I click \"RIBBONNEWENDPOINT\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.Given("I double click \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
    testRunner.Given("I send \"{Delete}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 17
 testRunner.When("I send \"Sql\" to \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.Then("I drag \"TOOLBOX,PART_Tools,Recordset,Dev2.Activities.DsfSqlBulkInsertActivity\" on" +
                    "to \"WORKSURFACE,StartSymbol\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 20
 testRunner.Given("I double click \"WORKFLOWDESIGNER,Unsaved 1(FlowchartDesigner),SQL Bulk Insert(Sql" +
                    "BulkInsertDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
 testRunner.And("I click \"WORKFLOWDESIGNER,Unsaved 1(FlowchartDesigner),SQL Bulk Insert(SqlBulkIns" +
                    "ertDesigner),DoneButton\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.Given("\"WORKSURFACE,UI_Error0_AutoID\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.Given("\"WORKSURFACE,UI_Error1_AutoID\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 25
 testRunner.Given("\"WORKSURFACE,UI_Error2_AutoID\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.Given("I click point \"151,42\" on \"WORKFLOWDESIGNER,Unsaved 1(FlowchartDesigner),SQL Bulk" +
                    " Insert(SqlBulkInsertDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
 testRunner.And("I send \"testingDBSrc{ENTER}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.Given("I click point \"148,75\" on \"WORKFLOWDESIGNER,Unsaved 1(FlowchartDesigner),SQL Bulk" +
                    " Insert(SqlBulkInsertDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
 testRunner.And("I send \"dbo.City{ENTER}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("I wait for \"10\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("I click \"WORKFLOWDESIGNER,Unsaved 1(FlowchartDesigner),SQL Bulk Insert(SqlBulkIns" +
                    "ertDesigner),DoneButton\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.Given("\"WORKFLOWDESIGNER,Unsaved 1(FlowchartDesigner),SQL Bulk Insert(SqlBulkInsertDesig" +
                    "ner),SmallViewContent,UI__Database_AutoID\" is visible within \"5\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
