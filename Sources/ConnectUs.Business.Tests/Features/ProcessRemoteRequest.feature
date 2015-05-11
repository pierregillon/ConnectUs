@RequestProcessor
Feature: ProcessRemoteRequest
	In order to get result from the client side
	As a server
	I want to execute a request on the client side

Scenario: Execute remotely a unknown request throw exception
	Given A connection is established between server and client on port 9000
		And A client request processor is initialized
		And A server request processor is initialized
		And A request with the name "unknownRequest"
	When I start the client continuous request processor
		And I process the request in the server request processor
	Then I get a response with the error "The request 'unknownRequest' is unknown on the client."

Scenario: Execute remotely a known request throw exception
	Given A connection is established between server and client on port 9000
		And A client request processor is initialized
		And A server request processor is initialized
		And A request with the name "GetClientInformation"
	When I start the client continuous request processor
		And I process the request in the server request processor
	Then I get a response with the result "{"Ip" : "192.168.1.1", "MachineName" : "mycomputer"}"
