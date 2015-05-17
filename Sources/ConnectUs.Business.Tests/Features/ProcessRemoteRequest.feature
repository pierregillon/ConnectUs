﻿@RequestProcessor
Feature: ProcessRemoteRequest
	In order to get result from the client side
	As a server
	I want to execute a request on the client side

Scenario: Sending request from server get correct json in client side
	Given A connection is established between server and client on port 9000
		And A server request communicator
		And A mocked client request processor
		And A client request handler
		And A "GetClientInformation" request
	When I send the request by the server request communicator
		And I process the request from the client request handler
	Then I get the request name "GetClientInformationRequest" and the data "{"Name":"GetClientInformationRequest"}" on the mocked client request processor

Scenario: Process request in client side get the correct response in server side.
	Given A connection is established between server and client on port 9000
		And A server request communicator
		And A mocked client request processor
		And A client request handler
		And A "GetClientInformation" request
	When I send the request by the server request communicator
		And I process the request from the client request handler
		And I read the response from the server request communicator
	Then The response is a "GetClientInformation" response
		And The ip of the GetClientInformation response is "127.0.0.1"
		And The machine name of the GetClientInformation response is "my machine"

Scenario: Throw exception when processing request in client side throw exception on server side.
	Given A connection is established between server and client on port 9000
		And A server request communicator
		And A mocked client request processor that returns error "Error occured on client side"
		And A client request handler
		And A "GetClientInformation" request
	When I send the request by the server request communicator
		And I process the request from the client request handler
		And I read the response from the server request communicator
	Then An exception is thrown with the message "Error occured on client side"
