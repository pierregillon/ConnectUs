Feature: ExecuteClientCommandOnServerSide
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: The client information query converts the response in client information
	Given A response with the name "GetClientInformation"
		And The GetClientInformation response has the ip "192.168.1.1"
		And The GetClientInformation response has the machine name "mycomputer"
		And A mocked remote client that returns the response
	When I ask the client information
	Then I get a client information
		And the client information has the ip to "192.168.1.1"
		And the client information has the machine name to "mycomputer"