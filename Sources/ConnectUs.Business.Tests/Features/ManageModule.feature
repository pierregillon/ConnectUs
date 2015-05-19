Feature: ManageModule
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Add a module sends module added event
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
	Then I get a module added event with the name "ConnectUs.FileExplorer" and the version "1.0.0.0"

Scenario: Add an invalid module throw exception
	Given A module manager
	When I add the module "Modules/ConnectUs.Common.dll" in the module manager
	Then I get a module exception with the message "The assembly 'ConnectUs.Common' is not a valid module. No 'Module' class has been found."

Scenario: Remove a unknown module throw exception
	Given A module manager
	When I remove the module "Modules/FileExplorer.dll" in the module manager
	Then I get a module exception with the message "Unable to remove the module 'Modules/FileExplorer.dll' : it was not found."

Scenario: Remove an added module sends module removed event
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I remove the module "Modules/FileExplorer.dll" in the module manager
	Then I get a module removed event with the name "ConnectUs.FileExplorer" and the version "1.0.0.0"

Scenario: Get modules returns correct values
	Given A module manager
	When I add the module "Modules/FileExplorer.dll" in the module manager
		And I get the modules of the module manager
	Then I get 1 module
		And The 1 module has the name "ConnectUs.FileExplorer" and the version "1.0.0.0"
