Feature: DownloadUploadFile
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Exchange a file between client and server
	Given A connection is established between server and client on port 9000
		And A downloader with the client connection
		And An uploader with the server connection
	When I start the download of the file "Resources/downloadedFile.txt"
		And I start the upload to the file "Resources/file.txt"
		And I wait the end of the file exchange
	Then The "Resources/downloadedFile.txt" file and the "Resources/file.txt" file are equals

Scenario: Exchange a file between client and server with limit buffer size
	Given A connection is established between server and client on port 9000
		And A downloader with the client connection
		And An uploader with the server connection
	When I start the download of the file "Resources/downloadedFile.txt"
		And I start the upload to the file "Resources/file_1024_bytes.txt"
		And I wait the end of the file exchange
	Then The "Resources/downloadedFile.txt" file and the "Resources/file_1024_bytes.txt" file are equals