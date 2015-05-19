using System;
using System.Collections.Generic;
using ConnectUs.ClientSide.Modules;
using NFluent;
using TechTalk.SpecFlow;
using System.Linq;

namespace ConnectUs.Business.Tests.Steps
{
    [Binding]
    public class ModuleManagerSteps
    {
        public ModuleManager ModuleManager
        {
            get { return ScenarioContext.Current.Get<ModuleManager>("ModuleManager"); }
            set { ScenarioContext.Current.Set(value, "ModuleManager"); }
        }
        public ModuleAddedEventArgs ModuleAddedEventArgs
        {
            get { return ScenarioContext.Current.Get<ModuleAddedEventArgs>("ModuleAddedEventArgs"); }
            set { ScenarioContext.Current.Set(value, "ModuleAddedEventArgs"); }
        }
        public ModuleRemovedEventArgs ModuleRemovedEventArgs
        {
            get { return ScenarioContext.Current.Get<ModuleRemovedEventArgs>("ModuleRemovedEventArgs"); }
            set { ScenarioContext.Current.Set(value, "ModuleRemovedEventArgs"); }
        }
        public ModuleException ModuleException
        {
            get { return ScenarioContext.Current.Get<ModuleException>("ModuleException"); }
            set { ScenarioContext.Current.Set(value, "ModuleException"); }
        }
        public IEnumerable<Module> Modules
        {
            get { return ScenarioContext.Current.Get<IEnumerable<Module>>("Modules"); }
            set { ScenarioContext.Current.Set(value, "Modules"); }
        }

        [Given(@"A module manager")]
        public void GivenAModuleManager()
        {
            ModuleManager = new ModuleManager();
            ModuleManager.ModuleAdded += (sender, args) => { ModuleAddedEventArgs = args; };
            ModuleManager.ModuleRemoved += (sender, args) => { ModuleRemovedEventArgs = args; };
        }

        [When(@"I add the module ""(.*)"" in the module manager")]
        public void WhenIAddTheModuleInTheModuleManager(string modulePath)
        {
            try {
                ModuleManager.AddModule(modulePath);
            }
            catch (ModuleException ex) {
                ModuleException = ex;
            }
        }

        [When(@"I get the modules of the module manager")]
        public void WhenIGetTheModulesOfTheModuleManager()
        {
            Modules = ModuleManager.GetModules();
        }

        [When(@"I remove the module ""(.*)"" in the module manager")]
        public void WhenIRemoveTheModuleInTheModuleManager(string modulePath)
        {
            try {
                ModuleManager.RemoveModule(modulePath);
            }
            catch (ModuleException ex) {
                ModuleException = ex;
            }
        }

        [Then(@"I get a module added event with the name ""(.*)"" and the version ""(.*)""")]
        public void ThenIGetAModuleAddedEventWithTheNameAndTheVersion(string moduleName, string version)
        {
            Check.That(ModuleAddedEventArgs).IsNotNull();
            Check.That(ModuleAddedEventArgs.Module).IsNotNull();
            Check.That(ModuleAddedEventArgs.Module.Name).IsEqualTo(moduleName);
            Check.That(ModuleAddedEventArgs.Module.Version).IsEqualTo(new Version(version));
        }

        [Then(@"I get a module removed event with the name ""(.*)"" and the version ""(.*)""")]
        public void ThenIGetAModuleRemovedEventWithTheNameAndTheVersion(string moduleName, string version)
        {
            Check.That(ModuleRemovedEventArgs).IsNotNull();
            Check.That(ModuleRemovedEventArgs.Module).IsNotNull();
            Check.That(ModuleRemovedEventArgs.Module.Name).IsEqualTo(moduleName);
            Check.That(ModuleRemovedEventArgs.Module.Version).IsEqualTo(new Version(version));
        }

        [Then(@"I get a module exception with the message ""(.*)""")]
        public void ThenIGetAModuleExceptionWithTheMessage(string message)
        {
            Check.That(ModuleException).IsNotNull();
            Check.That(ModuleException.Message).IsEqualTo(message);
        }

        [Then(@"I get (.*) module")]
        public void ThenIGetModule(int moduleCount)
        {
            Check.That(Modules).IsNotNull();
            Check.That(Modules.Count()).IsEqualTo(moduleCount);
        }

        [Then(@"The (.*) module has the name ""(.*)"" and the version ""(.*)""")]
        public void ThenTheModuleHasTheNameAndTheVersion(int index, string moduleName, string version)
        {
            var module = Modules.ElementAt(index - 1);
            Check.That(module.Name).IsEqualTo(moduleName);
            Check.That(module.Version).IsEqualTo(new Version(version));
        }
    }
}