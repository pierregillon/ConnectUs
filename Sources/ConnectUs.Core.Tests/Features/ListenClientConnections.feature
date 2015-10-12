Feature: Listen client connection
	In order to see them and to execute requests
	As connectUs server
	I want to be notified when client are connected or disconnected

Scenario: Start listening client
	Given A connection listener
		And A server configuration with a port set to 9000
		And A client listener linked with the connection listener and the server parameters
	When I start the client listener
	Then The connection listener is started on port 9000

Scenario: Connection established raise client connected event
	Given A connection listener
		And A server configuration with a port set to 9000
		And A client listener linked with the connection listener and the server parameters
	When I start the client listener
		And a connection is established
	Then A new ClientConnected event is raised

Scenario: Connection lost raise client disconnected event
	Given A connection listener
		And A server configuration with a port set to 9000
		And A client listener linked with the connection listener and the server parameters
	When I start the client listener
		And a connection is established
		And the connection is lost
	Then A client disconnected event is raised
		And the client of the connected event is the same of the disconnected event
