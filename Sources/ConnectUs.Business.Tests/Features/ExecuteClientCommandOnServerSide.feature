Feature: ExecuteClientCommandOnServerSide
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: The client throws client exception if response has error
	Given A request processor that returns a response with error "invalid request"
		And A client on the request processor
	When I ask the client information
	Then A client exception is thrown with the message "invalid request"

Scenario: The client information create a valid request
	Given A request processor that returns a response with result "{}"
		And A client on the request processor
	When I ask the client information
	Then The request has the name "GetClientInformation"
		And The request contains 0 parameters

Scenario: The client information query converts the response in client information
	Given A request processor that returns a response with result "{"Ip" : "192.168.1.1", "MachineName" : "mycomputer"}"
		And A client on the request processor
	When I ask the client information
	Then I get a client information
		And the client information has the ip to "192.168.1.1"
		And the client information has the machine name to "mycomputer"