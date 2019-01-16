# SerkoExpense
This repo contains the expense management service.

All requirements as outlined in test scenario provided have been addressed the way I would approach a problem in the real world. 
TDD was utilized with continuous refactoring as the design emerged. 

The Solution has been developed using Dotnet Core 2.1.

* The Dotnet cli can be used to restore, build and run tests.
* To run the api  execute "dotnet run --project SerkoExpense.Api/SerkoExpense.Api.csproj" 
* Navigate to https://localhost:5001/api/healthcheck to confirm the api is up and running.
* Manual Testing can be acheived with a client of your choice by executing a post request to the /expense endpoint with the relevant  [InputData](InputData.json)  in the request body. 
 


# Assumptions
* Documentation for the API is not required as a part of the assesment. 
* CICD pipeline demonstration is not required.
* The provided information in email contains an invalid date (Tuesday 27 April 2017), wrote my solution around a correct date (Thursday 27 April 2017) and accommodated 
for an incorrect date. 
* A better solution may have been created if this was attempted in a collaborative manner ie. Pair Programmed or Mob Programmed.
