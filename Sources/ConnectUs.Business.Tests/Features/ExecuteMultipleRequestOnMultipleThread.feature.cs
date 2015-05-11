﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34014
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ConnectUs.Business.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class ExecuteMultipleRequestOnMultipleThreadFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ExecuteMultipleRequestOnMultipleThread.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ExecuteMultipleRequestOnMultipleThread", "In order to send pings\nAs server\nI want to execute multiple request on multiple t" +
                    "hread on a server request processor", ProgrammingLanguage.CSharp, new string[] {
                        "ConcurrentRequestExecution"});
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
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "ExecuteMultipleRequestOnMultipleThread")))
            {
                ConnectUs.Business.Tests.Features.ExecuteMultipleRequestOnMultipleThreadFeature.FeatureSetup(null);
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send multiple requests on multiple thread get correct response")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExecuteMultipleRequestOnMultipleThread")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ConcurrentRequestExecution")]
        public virtual void SendMultipleRequestsOnMultipleThreadGetCorrectResponse()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send multiple requests on multiple thread get correct response", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("A connection is established between server and client on port 9000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
  testRunner.And("A client request processor is initialized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
  testRunner.And("A server request processor is initialized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.When("I start the client continuous request processor", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 12
  testRunner.And("I send the request \"Request1\" through the server request processor on the thread " +
                    "1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
  testRunner.And("I send the request \"Request2\" through the server request processor on the thread " +
                    "2", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
  testRunner.And("I send the request \"Request3\" through the server request processor on main thread" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
  testRunner.And("I send the request \"Request4\" through the server request processor on the thread " +
                    "3", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
  testRunner.And("I send the request \"Request5\" through the server request processor on the thread " +
                    "4", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
  testRunner.And("I send the request \"Request6\" through the server request processor on main thread" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.Then("I get a response with the result \"Response1\" on thread 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 19
  testRunner.And("I get a response with the result \"Response2\" on thread 2", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
  testRunner.And("I get a response with the result \"Response3\" on main thread index 0", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
  testRunner.And("I get a response with the result \"Response4\" on thread 3", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
  testRunner.And("I get a response with the result \"Response5\" on thread 4", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
  testRunner.And("I get a response with the result \"Response6\" on main thread index 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Send multiple requests on main thread get correct response")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ExecuteMultipleRequestOnMultipleThread")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ConcurrentRequestExecution")]
        public virtual void SendMultipleRequestsOnMainThreadGetCorrectResponse()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Send multiple requests on main thread get correct response", ((string[])(null)));
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given("A connection is established between server and client on port 9000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
  testRunner.And("A client request processor is initialized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
  testRunner.And("A server request processor is initialized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.When("I start the client continuous request processor", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
  testRunner.And("I send the request \"Request1\" through the server request processor on main thread" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
  testRunner.And("I send the request \"Request2\" through the server request processor on main thread" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.Then("I get a response with the result \"Response1\" on main thread index 0", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 33
  testRunner.And("I get a response with the result \"Response2\" on main thread index 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
