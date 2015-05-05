Feature: Communication
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Connection of a client to a server
	Given A server
		And A client
	When The server start at the port 9000
		And The client connects the server at the port 9000
	Then The client list of the server has 1 element

Scenario: Client send information to server on connection
	Given A server
		And A client with ip "192.168.1.25"
	When The server start at the port 9000
		And The client connects the server at the port 9000
		And I wait 2 seconds
	Then The 1 client has the ip "192.168.1.25"
