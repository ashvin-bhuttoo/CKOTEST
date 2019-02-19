# CKOTEST 2019

This Repository Contains the Solution Folders:

1. API
	a. ClientLibrary
		This project is a Class Library that implements Part 2 of the CKO test using .NET Framework 4.5.2
	b. WebAPI
		This project implements Part 1 of the CKO test using ASPNET MVC WEBAPI on .NET 4.5.2 and a RESTFUL design.

2. API_Test
	a. ClientLibrary_TEST
		This project is a Unit Test project for API/ClientLibrary
	b. WebAPI_TEST
		This project is a Unit Test project for API/WebAPI

Steps to use this Repository's Code:

1. Open API/API.sln, BUILD SOLUTION (packages will be restored and may take some time)
2. Open API_Test/API_Test.sln, BUILD SOLUTION (packages will be restored and may take some time)
3. Run Project WebAPI in solution API.sln, Wait for the Aspnet development server to start on port 1947
4. Run all tests found in solution API_Test.sln in Test Explorer