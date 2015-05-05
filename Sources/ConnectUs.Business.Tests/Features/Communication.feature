Feature: Communication
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Connection of a client to a server
	Given A server
		And A client
	When The client connects the server
	Then The client list of the server has 1 element

Scenario: Server requests client information
	Given A server
		And A client with an ip to '192.168.1.25' and a machine name to 'FsTivit3'
	When The client connects the server
		And The server requests to the client 1 its information
	Then The received information contains an ip to "192.168.1.25"
		And The received information contains a machine name to "FsTivit3"
