# Ships
Just an application to show CRUD operation in .Net 6

 <p align="center"><img width=70% src="https://user-images.githubusercontent.com/9815699/235737626-f4b50838-97a6-49b3-9f50-34ece1678c68.png"></p>
 
What is used:
- .Net 6
- React 18
- JWT Authentication
- CQRS + MdeiatR
- MediatR Pipelines
- Exception Handling
- AutoMapper
- NUnit / FluentAssertions / Moq / Respawn


To Do:
- [ ] Fix frontend error handling :(
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

**Make sure that ports 3000 and 7195 are free**

  
---
*Another approach is to:*
- open the backend solution and run the Ships.WebApi project on port 7195
- open frontend project in cmd and run
```
npm i
npm start
```
