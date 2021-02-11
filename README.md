# TaskManager
Simple Task Manager Using SQL Server EF Core

## Description
Task Manager Console Application which uses Entity Framework and Microsoft SQL Management Studio to store users, tasks and map assigned tasks.
The database has 3 Tables: ```USER```, ```TASK```, ```MAP```.
```MAP``` Table assign ```USERS``` to ```TASKS```.

## Installation
```git clone https://github.com/bardia-ardakanian/TaskManager.git```

## Usage
use ```db``` command to call its functions

#### available functions:
##### functions on ```USER TABLE```
    db add -u <username>    //add new user
    db rm -u <username>     //remove given user
    db display -u           //display USER TABLE
    db drop -u             //drop USER TABLE

##### functions on ```TASK TABLE```
    db add -t <title>       //add new user
    db rm -t <title>        //remove given user
    db display -t           //display USER TABLE
    db drop -t              //drop USER TABLE

##### functions on ```MAP TABLE```
    db assign <username> <title>        //assign task to given user
    db expel <username> <title>         //expel given task
    db display -m                       //display MAP TABLE
    db drop -m                         //drop MAP TABLE

##### help
    db --help