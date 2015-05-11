@ConcurrentRequestExecution
Feature: ExecuteMultipleRequestOnMultipleThread
	In order to send pings
	As server
	I want to execute multiple request on multiple thread on a server request processor

Scenario: Send multiple requests on multiple thread get correct response
	Given A connection is established between server and client on port 9000
		And A client request processor is initialized to echo requests
		And A server request processor is initialized
	When I start the echo process on client
		And I send an echo request with value "1" through the server request processor on the thread 1
		And I send an echo request with value "2" through the server request processor on the thread 2
		And I send an echo request with value "3" through the server request processor on main thread
		And I send an echo request with value "4" through the server request processor on the thread 3
		And I send an echo request with value "5" through the server request processor on the thread 4
		And I send an echo request with value "6" through the server request processor on main thread
	Then I get an echo response with the result "1" on thread 1
		And I get an echo response with the result "2" on thread 2
		And I get an echo response with the result "3" on main thread index 0
		And I get an echo response with the result "4" on thread 3
		And I get an echo response with the result "5" on thread 4
		And I get an echo response with the result "6" on main thread index 1

Scenario: Send multiple requests on main thread get correct response
	Given A connection is established between server and client on port 9000
		And A client request processor is initialized to echo requests
		And A server request processor is initialized
	When I start the echo process on client
		And I send an echo request with value "1" through the server request processor on main thread
		And I send an echo request with value "2" through the server request processor on main thread
	Then I get an echo response with the result "1" on main thread index 0
		And I get an echo response with the result "2" on main thread index 1
