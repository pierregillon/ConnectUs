Feature: ProcessRequestsOnClientSide
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given A client request processor
		And A request with the name "GetClientInformation"
	When I process the request with the client request processor
	Then The response is a "GetClientInformation" response
