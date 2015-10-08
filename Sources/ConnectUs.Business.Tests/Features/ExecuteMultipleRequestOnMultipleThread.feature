@ConcurrentRequestExecution
Feature: ExecuteMultipleRequestOnMultipleThread
	In order to send pings
	As server
	I want to execute multiple request on multiple thread on a server request processor

Background: 
	Given A connection is established between server and client on port 9000
		# client side
		And A module manager
		And A command locator
		And A client request processor
		And A client request handler
		And A continuous client request processor
		# server side
		And A server request communicator
		And A remote client on the request processor

Scenario: Send multiple requests on multiple thread get correct response
	Given A mocked client request processor that returns echo
		And A client request handler
		And A continuous client request processor
	When I start the continous client request process to process incoming request
		And I send an echo request with value "1" through the remote client on the thread on the thread 1
		And I send an echo request with value "2" through the remote client on the thread on the thread 2
		And I send an echo request with value "3" through the remote client on main thread
		And I send an echo request with value "4" through the remote client on the thread on the thread 3
		And I send an echo request with value "5" through the remote client on the thread on the thread 4
		And I send an echo request with value "6" through the remote client on main thread
	Then I get an echo response with the result "1" on thread 1
		And I get an echo response with the result "2" on thread 2
		And I get an echo response with the result "3" on main thread index 0
		And I get an echo response with the result "4" on thread 3
		And I get an echo response with the result "5" on thread 4
		And I get an echo response with the result "6" on main thread index 1

Scenario: Send multiple requests on main thread get correct response
	Given A mocked client request processor that returns echo
		And A client request handler
		And A continuous client request processor
	When I start the continous client request process to process incoming request
		And I send an echo request with value "1" through the remote client on main thread
		And I send an echo request with value "2" through the remote client on main thread
	Then I get an echo response with the result "1" on main thread index 0
		And I get an echo response with the result "2" on main thread index 1

Scenario: Upload file through the remote client
	When I start the continous client request process to process incoming request
		And I upload file 'Resources/file.txt' to 'Resources/Uploaded/' through the remote client
	Then I get the file path result 'Resources/Uploaded/file.txt'
		And The "Resources/file.txt" file and the "Resources/Uploaded/file.txt" file are equals

Scenario: Upload file through the remote client with no directory name set new file in the current directory
	When I start the continous client request process to process incoming request
		And I upload file 'Resources/file.txt' to '' through the remote client
	Then I get the file path result 'file.txt'
		And The "Resources/file.txt" file and the "file.txt" file are equals