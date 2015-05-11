Feature: CommunicationWithTcpClientConnections
	In order to communicate with other part
	As server or client
	I want to send request and receive response from connection

Scenario: Established connection in 2 ways
	Given A connection is established between server and client on port 9000
	Then A client connection is instancied
		And A server connection is instancied

Scenario: Send data from server connection to client connection
	Given A connection is established between server and client on port 9000
	When I send the data "hello world" through the server connection
	Then The client connection receives the data "hello world"

Scenario: Send data from client connection to server connection
	Given A connection is established between server and client on port 9000
	When I send the data "hello world" through the client connection
	Then The server connection receives the data "hello world"

Scenario: Client connection closed throw connection exception
	Given A connection is established between server and client on port 9000
	When I close the client connection
		And I send the data "hello world" through the server connection
	Then I get a connection exception
