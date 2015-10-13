Feature: ProcessRequestsOnClientSide
	In order to get result
	As a client
	I want to execute the right command from request and data

Scenario: Execute known request on the client request processor returns correct data.
	Given A mocked command locator
		And A client request processor
	When I process the request "{"Name":"EchoRequest", "Value":"test"}"
	Then I get the response "{"Result":"test","Name":"EchoResponse"}"

Scenario: Execute unknown request on the client request processor throws exception.
	Given A mocked command locator
		And A client request processor
	When I process the request "{"Name":"unknownRequest"}"
	Then I get a process exception "{"Error":"The request 'unknownRequest' is unknown.","Name":"ErrorResponse"}"
