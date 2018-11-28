# Please view this in a "markdown" reader e.g.  https://dillinger.io/



# GSA Readme - Written in MarkDown 
This is a C# .NET Core 2 solution to the GSA Developer test written by Kashif Khan in approx 4 hours.

This solution was developed using Visual Studio 17. And can be executed by opening the solution .sln file and running IIS. Viewable typically on localhost:8000.

My assumption was to handle only the "happy path" of processes and not consider security and error handling. I typically follow microservice architecture and split models up as much as I can, having as little relationship as possible, thereby promoting a decoupled system. For example, moving one model into a different microservice is much easier with the methodoligies I have used.


### I have included:
- Swagger support available on localhost:8000/swagger which displays a helpful ui for the api
- Additional controllers to help debugging for each model
- Out of the box docker support
- An in-memory database by default a SQL server.
- Database seeding on run time
- CSV conversion using libraries (which can be improved)
- Solutions to all questions (partial answer to last requirement)
- An application DB context.
- DTOs
- Libraries such as ChoETL which is an easy to use CSV parser
    
### I have not included:
- No mechanism for null data / null arguments etc. Although these are demonstrated in the scaffoled controllers e.g. PNLsController.cs 
- A complete solution for the final requirement, although this appears to be only a simple int/decimal conversion problem - the logic seems correct.

### Further work
- Docker support is partially enabled, there would have to be a mysql server container configured in docker compose for this to be ideal.
- Correction of "Compound daily returns"
- Optimisations: Use of async and linq queries
- Ideally utilising microservice architecture where models are seperated into their own services
-- Transition into NOSQL
- Better CSV parsing on seeding
- Logging e.g. Seq

Any further questions contact info@kashifkhan.me
