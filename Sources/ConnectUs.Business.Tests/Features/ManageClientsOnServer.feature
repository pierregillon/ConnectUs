Feature: Communication
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Connection of a client to a server
	Given A server
		And A client
	When The client connects the server
	Then The server has 1 client

Scenario: Disconnection of a client to a server
	Given A server
		And A client
	When The client connects the server
	And The client disconnects from the server
	Then The server has 0 client
