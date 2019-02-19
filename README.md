# CKOTEST 2019

This Repository Contains the Solution Folders:

1. API<br/>
	a. ClientLibrary<br/>
		&nbsp;&nbsp;This project is a Class Library that implements Part 2 of the CKO test using .NET Framework 4.5.2<br/>
	b. WebAPI<br/>
		&nbsp;&nbsp;This project implements Part 1 of the CKO test using ASPNET MVC WEBAPI on .NET 4.5.2 and a RESTFUL design.<br/>

2. API_Test<br/>
	a. ClientLibrary_TEST<br/>
		&nbsp;&nbsp;This project is a Unit Test project for API/ClientLibrary<br/>
	b. WebAPI_TEST<br/>
		&nbsp;&nbsp;This project is a Unit Test project for API/WebAPI<br/><br/>

Steps to use this Repository's Code:<br/>

1. Open API/API.sln in one Visual Studio 2015 instance (INST001), BUILD SOLUTION (packages will be restored and may take some time)
2. Open API_Test/API_Test.sln in another Visual Studio 2015 instance (INST002), BUILD SOLUTION (packages will be restored and may take some time)
3. Run Project WebAPI in solution API.sln on INST001, Wait for IISEXPRESS development server to start listening on port 1947
4. Run all tests found in solution API_Test.sln using Test Explorer on INST002

Note: <br/>
The API Documentation is the default page that loads on http://localhost:1947<br/>
The Unit test codes are incomplete.
