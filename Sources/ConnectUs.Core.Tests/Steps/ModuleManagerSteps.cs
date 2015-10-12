using System;
using System.Collections.Generic;
using System.Linq;
using ConnectUs.Core.ModuleManagement;
using NFluent;
using TechTalk.SpecFlow;

namespace ConnectUs.Core.Tests.Steps
{
    [Binding]
    public class ModuleManagerSteps
    {
        public ModuleManager ModuleManager
        {
            get { return ScenarioContext.Current.Get<ModuleManager>("ModuleManager"); }
            set { ScenarioContext.Current.Set(value, "ModuleManager"); }
        }
        public ModuleLoadedEventArgs ModuleLoadedEventArgs
        {
            get { return ScenarioContext.Current.Get<ModuleLoadedEventArgs>("ModuleLoadedEventArgs"); }
            set { ScenarioContext.Current.Set(value, "ModuleLoadedEventArgs"); }
        }
        public ModuleUnloadedEventArgs ModuleUnloadedEventArgs
        {
            get { return ScenarioContext.Current.Get<ModuleUnloadedEventArgs>("ModuleUnloadedEventArgs"); }
            set { ScenarioContext.Current.Set(value, "ModuleUnloadedEventArgs"); }
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
        public ModuleName ModuleName
        {
            get { return ScenarioContext.Current.Get<ModuleName>("ModuleName"); }
            set { ScenarioContext.Current.Set(value, "ModuleName"); }
        }

        [Given(@"A module manager")]
        public void GivenAModuleManager()
        {
            ModuleManager = new ModuleManager();
            ModuleManager.ModuleLoaded += (sender, args) => { ModuleLoadedEventArgs = args; };
            ModuleManager.ModuleUnloaded += (sender, args) => { ModuleUnloadedEventArgs = args; };
        }

        [When(@"I add the module ""(.*)"" in the module manager")]
        public void WhenIAddTheModuleInTheModuleManager(string modulePath)
        {
            try {
                ModuleName = ModuleManager.AddModule(modulePath);
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
        public void WhenIRemoveTheModuleInTheModuleManager(string name)
        {
            try {
                ModuleManager.RemoveModule(new ModuleName(name));
            }
            catch (ModuleException ex) {
                ModuleException = ex;
            }
        }

        [When(@"I load the module ""(.*)"" in the module manager")]
        public void WhenILoadTheModuleInTheModuleManager(string name)
        {
            try {
                ModuleManager.LoadModule(new ModuleName(name));
            }
            catch (ModuleException ex) {
                ModuleException = ex;
            }
        }

        [When(@"I unload the module ""(.*)"" in the module manager")]
        public void WhenIUnloadTheModuleInTheModuleManager(string name)
        {
            try
            {
                ModuleManager.UnloadModule(new ModuleName(name));
            }
            catch (ModuleException ex)
            {
                ModuleException = ex;
            }
        }

        // ----- Then

        [Then(@"I get a module name ""(.*)""")]
        public void ThenIGetAModuleName(string name)
        {
            Check.That(ModuleName).IsEqualTo(new ModuleName(name));
        }

        [Then(@"I get a module loaded event with the name ""(.*)"" and the version ""(.*)""")]
        public void ThenIGetAModuleLoadedEventWithTheNameAndTheVersion(string moduleName, string version)
        {
            Check.That(ModuleLoadedEventArgs).IsNotNull();
            Check.That(ModuleLoadedEventArgs.Module).IsNotNull();
            Check.That(ModuleLoadedEventArgs.Module.Name).IsEqualTo(new ModuleName(moduleName));
            Check.That(ModuleLoadedEventArgs.Module.Version).IsEqualTo(new Version(version));
        }

        [Then(@"I get a module unloaded event with the name ""(.*)"" and the version ""(.*)""")]
        public void ThenIGetAModuleUnloadedEventWithTheNameAndTheVersion(string moduleName, string version)
        {
            Check.That(ModuleUnloadedEventArgs).IsNotNull();
            Check.That(ModuleUnloadedEventArgs.Module).IsNotNull();
            Check.That(ModuleUnloadedEventArgs.Module.Name).IsEqualTo(new ModuleName(moduleName));
            Check.That(ModuleUnloadedEventArgs.Module.Version).IsEqualTo(new Version(version));
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
            Check.That(module.Name).IsEqualTo(new ModuleName(moduleName));
            Check.That(module.Version).IsEqualTo(new Version(version));
        }
    }
}