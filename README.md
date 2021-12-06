# Nordax recruitment assignment 
Hi! The purpose of this assignment is to get familiar with the nordax stack and to solve a realistic use case.

### The task at hand
The task at hand consists in writing code in all parts of the stack:
1.	**Frontend:** In the LoanApplication component
    1. Implement a form for applying for a loan. Should contain a few fields such as:
        * textboxes
        * radio buttons
        * checkboxes
        * dropdowns 
        * file uploads
    2. Implement a simple loan-application overview to display current loan-applications
2.	**Backend:** Start in the LoanApplication controller
    1. Implement support in the API to accept loan application data
    2. Implement support to fetch loan applications and corresponding files
3.	**Database** (Using Entity Framework):
    1. Implement relevant entities, repositories and dependencies
    2. Database changes should be version controlled using ef core migrations
4.	**Filestorage**
    - Store files conveniently
5.  **Write** a short description of your solution

### Prerequisits
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Node.js](https://nodejs.org/en/download/)
- [SQL server](https://www.microsoft.com/sv-se/sql-server/sql-server-downloads)

#### Optional
- [Docker](https://www.docker.com/products/docker-desktop)

### IDEs
Obviously, you are allowed to use any IDE you like but we tend to use: 
* Visual Studio
* Jetbrains Rider 
* Visual Code 
* SQL Management Studio 

### What we like
* Clean, effective and self-documenting code
* Test-coverage (Unit tests are enough, but an example of some kind of end-to-end component test is a big plus)
* Inversion of control
* Unit of work

### DOs and DONTs
* Feel free to use any third party libraries (nuget or npm) you see fitting.
* Dont go all the way implementing everything production-ready. A nice proof of concept of the above task is enough as long you are ready to discuss possible improvements and/or alternative solutions.