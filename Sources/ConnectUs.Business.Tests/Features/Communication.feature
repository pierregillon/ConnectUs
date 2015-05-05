Feature: Communication
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Connection of a client to a server
	Given A server
		And A client
	When The server start at the port 9000
		And The client connects the server at the port 9000
	Then It appears in the client list of the server
