Feature: ManageModule
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Add invalid module path throws exception
	Given A module manager
	When I add the module "toto/FileExplorer.dll" in the module manager
	Then I get a module exception with the message "Unable to add the module 'toto/FileExplorer.dll' : file was not found."

Scenario: Add a module gets a new module name
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
	Then I get a module name "ConnectUs.FileExplorer"

Scenario: Load an unknown module throw exception
	Given A module manager
	When I load the module "ConnectUs.Common" in the module manager
	Then I get a module exception with the message "The module 'ConnectUs.Common' was not found."

Scenario: Load an invalid module throw exception
	Given A module manager
	When I add the module "Modules/ConnectUs.Common.dll" in the module manager
		And I load the module "ConnectUs.Common" in the module manager
	Then I get a module exception with the message "The assembly 'ConnectUs.Common' is not a valid module. No 'Module' class has been found."

Scenario: Load a valid module sends module loaded event
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I load the module "ConnectUs.FileExplorer" in the module manager
	Then I get a module loaded event with the name "ConnectUs.FileExplorer" and the version "1.0.0.0"

Scenario: Unload an unknown module throw exception
	Given A module manager
	When I unload the module "ConnectUs.Common" in the module manager
	Then I get a module exception with the message "The module 'ConnectUs.Common' was not found."

Scenario: Unload a valid module sends module loaded event
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I load the module "ConnectUs.FileExplorer" in the module manager
		And I unload the module "ConnectUs.FileExplorer" in the module manager
	Then I get a module unloaded event with the name "ConnectUs.FileExplorer" and the version "1.0.0.0"

Scenario: Remove an unknown module throw exception
	Given A module manager
	When I remove the module "Modules/FileExplorer.dll" in the module manager
	Then I get a module exception with the message "The module 'Modules/FileExplorer.dll' was not found."

Scenario: Remove an added module
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I remove the module "ConnectUs.FileExplorer" in the module manager
		And I get the modules of the module manager
	Then I get 0 module

Scenario: Remove a loaded module unload it before removing
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I load the module "ConnectUs.FileExplorer" in the module manager
		And I remove the module "ConnectUs.FileExplorer" in the module manager
	Then I get a module unloaded event with the name "ConnectUs.FileExplorer" and the version "1.0.0.0"

Scenario: Get modules returns correct values
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I get the modules of the module manager
	Then I get 1 module
		And The 1 module has the name "ConnectUs.FileExplorer" and the version "1.0.0.0"
