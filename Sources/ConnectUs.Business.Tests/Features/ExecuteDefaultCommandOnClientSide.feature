Feature: ExecuteDefaultCommandOnClientSide
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given A connection is established between server and client on port 9000
		And A server request communicator
		And A server request processor is initialized
		
		# client
		And A command locator
		And A client request processor
		And A client request handler
		And A continuous client request processor

Scenario: Sending request from server get correct json in client side
	Given A "GetClientInformation" request
	When I start the continous client request process to process incoming request
		And I send the request by the server request communicator
		And I process the request from the client request handler
	Then I get the request name "GetClientInformationRequest" and the data "{"Name":"GetClientInformationRequest"}" on the mocked client request processor

