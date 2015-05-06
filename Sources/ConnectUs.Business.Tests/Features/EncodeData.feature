Feature: EncodeData
	In order to send bytes and received object from connections
	As server or client
	I want to encode and decode data

Scenario: Encode a request provide encoded data
	Given A request with the name "test"
		And The request has a parameter "test" with the value "test"
		And An encoder
	When I encode the request with the encoder
	Then I got an encoded data

Scenario: Encode and decode a request provide the same request
	Given A request with the name "test"
		And The request has a parameter "test" with the value "test"
		And An encoder
	When I encode the request with the encoder
		And I decode the request with the encoder
	Then I got a decoded request
		And The decoded request has the name "test"
		And the decoded request has 1 parameter
		And the decoded request has a parameter "test" with the value "test"

