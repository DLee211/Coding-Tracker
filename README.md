# Coding Tracker

This is a CRUD console application to track time spent coding. It is developed using C# and SQLite. 

The IDE used was JetBrain Rider.

## Requirements
- When the application starts, it should create a sqlite database, if one isn’t present.
- It should also create a table in the database, where the hours will be logged.
- You need to be able to insert, delete, update and view your logged hours.
- You should handle all possible errors so that the application never crashes
- The application should only be terminated when the user inserts 0.
- You can only interact with the database using raw SQL. You can’t use mappers such as Entity Framework
- Reporting Capabilities

## Features
- Console based UI
- CRUD DB functions
  -From the main menu users can Create, Read, Update or Delete entries for whichever date they want, entered in mm/dd/yyyy format.
  - Duplicate days will not be inputted.
  - Time and Dates inputted are checked to make sure they are in the correct format.
- Reporting uses ConsoleTableExt library 
  - https://github.com/minhhungit/ConsoleTableExt
