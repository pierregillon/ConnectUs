Feature: CommunicationWithTcpClientConnections
	In order to communicate with other part
	As server or client
	I want to send request and receive response from connection

Scenario: Established connection in 2 ways
	Given A connection is established between server and client on port 9000
	Then A client connection is instancied
		And A server connection is instancied

Scenario: Request sending by the server is received by the client
	Given A connection is established between server and client on port 9000
		And A request with the name "hello world"
	When I send the request through the server connection
	Then The client connection receives the request
		And The request received has the name "hello world"

Scenario: Request with 1 parameter sending by the server is received by the client
	Given A connection is established between server and client on port 9000
		And A request with the name "dir"
		And The request has a parameter "directory" with the value "c:/MyDirectory"
	When I send the request through the server connection
	Then The client connection receives the request
		And The request received contains 1 parameters
		And The request received has the name "dir"
		And The request received contains a parameter "directory" with the value "c:/MyDirectory"

Scenario: Request with 2 parameters sending by the server is received by the client
	Given A connection is established between server and client on port 9000
		And A request with the name "dir"
		And The request has a parameter "directory" with the value "c:/MyDirectory"
		And The request has a parameter "mode" with the value "readonly"
	When I send the request through the server connection
	Then The client connection receives the request
		And The request received has the name "dir"
		And The request received contains 2 parameters
		And The request received contains a parameter "directory" with the value "c:/MyDirectory"
		And The request received contains a parameter "mode" with the value "readonly"

Scenario: Response sending by the client is received by the server
	Given A connection is established between server and client on port 9000
		And A response with the content "OK"
	When I send the response through the client connection
	Then The server connection receives the response
		And The response received contains contains the value "OK"

Scenario: Client connection closed throw connection exception
	Given A connection is established between server and client on port 9000
		And A request with the name "dir"
	When I close the client connection
		And I send the request through the server connection
	Then I get a client connection exception
