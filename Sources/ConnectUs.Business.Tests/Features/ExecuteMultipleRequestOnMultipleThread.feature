Feature: ExecuteMultipleRequestOnMultipleThread
	In order to send pings
	As server
	I want to execute multiple request on multiple thread on a server request processor

Scenario: Send multiple requests on multiple thread get correct response
	Given A connection is established between server and client on port 9000
		And A client request processor is initialized
		And A server request processor is initialized
	When I start the client continuous request processor
		And I send the request "Request1" through the server request processor on the thread 1
		And I send the request "Request2" through the server request processor on the thread 2
		And I send the request "Request3" through the server request processor on main thread
		And I send the request "Request4" through the server request processor on the thread 3
		And I send the request "Request5" through the server request processor on the thread 4
		And I send the request "Request6" through the server request processor on main thread
	Then I get a response with the result "Response1" on thread 1
		And I get a response with the result "Response2" on thread 2
		And I get a response with the result "Response3" on main thread index 0
		And I get a response with the result "Response4" on thread 3
		And I get a response with the result "Response5" on thread 4
		And I get a response with the result "Response6" on main thread index 1

Scenario: Send multiple requests on main thread get correct response
	Given A connection is established between server and client on port 9000
		And A client request processor is initialized
		And A server request processor is initialized
	When I start the client continuous request processor
		And I send the request "Request1" through the server request processor on main thread
		And I send the request "Request2" through the server request processor on main thread
	Then I get a response with the result "Response1" on main thread index 0
		And I get a response with the result "Response2" on main thread index 1
