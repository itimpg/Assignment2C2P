# Software Architect Technical Assignment
This is the web application that allow user to upload the transaction file in CSV or XML format.
User can search the transactions using the followings criteria
- by Currency 
- by date range 
- by status 

## Technologies
* .NET Core 3
* ASP.NET Core 3
* Entity Framework Core 3
* Blazor

## Architecture
This application is using 3-Tier architecture

### Presentation Layer
This layer handle how to show data on the screen and also handle user input

### Data Layer
This layer handle how to connect to the database including data entity model

### Business Layer
This layer handle data from presentation layer and return the response back
Business layer can ask data layer when need to get or update the data
