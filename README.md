# Ships
Just an application to show CRUD operation in .Net 7

 <p align="center"><img width=70% src="https://user-images.githubusercontent.com/9815699/235737626-f4b50838-97a6-49b3-9f50-34ece1678c68.png"></p>
 
What is used:
- Clean Architecture
- .Net 7
- React 18 / Redux toolkits
- JWT Authentication
- CQRS + MdeiatR
- MediatR Pipelines
- Exception Handling
- AutoMapper
- Fluent Validation
- NUnit / FluentAssertions / Moq / Respawn


To Do:
- [x] Fix frontend error handling :(
- [ ] Rate limiting for frontend
- [ ] Api versioning
- [ ] Adding Serilog
- [ ] Caching


How to run:

```
git clone https://github.com/moslem-hadi/Ships.git
```
Then go to the folder:
```
cd Ships
```
And run docker compose:
```
docker-compose up -d
```
Then open `http://localhost:3000`


**Make sure that ports 3000 and 7195 are free**

  
---
*Another approach is to:*
- open the backend solution and run the Ships.WebApi project on port 7195
- open frontend project in cmd and run
```
npm i
npm start
```

## Here's what I did:
I used .Net 7 and I have  5 projects:

This structure is based on a clean architecture solution. the Infrastructure layer is responsible for implementing the Application layer.  
There is a validation for Ship code in the front end using Yup. and another validation in the back end using FluentValidation.

I used MediatR to make my controllers cleaner and follow the CQRS pattern. Also, I used MediatR Notification to handle some events like ShipCreated or ShipDeleted.  
I used MediatR pipeline behaviours to   
* Check if the user is authenticated  
* Log long-running requests.  
* And perform validation on commands and queries.  
  
I used AuthoMapper to map Entities to DTOs and reverse.   
  
For testing, I used NUnit, FluentAssertions, and Moq
  
For the front end, I used React and redux toolkits.
  
Finally, I created a docker-compose file so the project can run with a command. And I used SQL server image.
