Feature: DownloadUploadFile
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Exchange a file between client and server
	Given A connection is established between server and client on port 9000
		And A downloader with the client connection
		And An uploader with the server connection
	When I start the downloader to save the file "Resources/save.txt"
		And I start the uploader to send the file "Resources/source.txt"
		And I wait the end of the file exchange
	Then The "Resources/save.txt" file and the "Resources/source.txt" file are equals