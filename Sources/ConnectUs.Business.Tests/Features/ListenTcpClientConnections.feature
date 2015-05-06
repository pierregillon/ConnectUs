@ConnectionListener
Feature: ListenTcpClientConnections
	In order to communication with tcp client
	As server
	I want to listen tcp client connections

Scenario: Connection of tcp client to connection listener raises a connection established event
	Given A socket connection listener
		And A tcp client
	When The connection listener starts listening on the port 9000
		And The tcp client connect to the host 'localhost' and the port 9000
	Then The connection listener raises a connection established event
		And The connection established raised contains a connection

Scenario: Connection of tcp client provide a connection to communicate with it
	Given A socket connection listener
		And A tcp client
	When The connection listener starts listening on the port 9000
		And The tcp client connect to the host 'localhost' and the port 9000
		And I send the request 'ping' through the connection of the connection established event
	Then the tcp client received the request
