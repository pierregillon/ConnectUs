Feature: EncodeData
	In order to send bytes and received object from connections
	As server or client
	I want to encode and decode data

Scenario: Encode data with encoder
	Given A data to encode "hello world"
		And An encoder
	When I encode the data with the encoder
	Then I get an encoded data

Scenario: Decode and encoded data with encoder
	Given A data to encode "hello world"
		And An encoder
	When I encode the data with the encoder
		And I decode the encoded data with the encoder
	Then I get the decoded data "hello world"