Feature: ProcessRequestsOnClientSide
	In order to get result
	As a client
	I want to execute the right command from request and data

Scenario: Execute known request on the client request processor returns correct data.
	Given A mocked module service
		And A client request processor
	When I process the request "echoRequest" with the data "myData"
	Then I get the response "myData"

Scenario: Execute unknown request on the client request processor throws exception.
	Given A mocked module service
		And A client request processor
	When I process the request "unknownRequest" with the data "myData"
	Then I get a process exception "The request 'unknownRequest' is unknown."
