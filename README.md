# aiDevTest
The ai Development Recruitment Test

In order to submit this test you must:
- Create a branch that clearly identifies your submission (e.g. "JohnDoe-DevTest")
- Complete the challenge described below and push your changes to your branch
- Open a Pull Request to merge your submission into the _main_ branch
- Reply to the HR email inviting you to complete this test to inform us that you have completed it

Commit to your branch as freely as you like; development is a process, you won't be judged by the number of commits you do or don't push to complete this task.

# Flat Data Reading
## Background
Flat file text formats like CSV are very simple and convenient ways to exchange data. They are simple to work with, can be opened and edited in most spread sheet editing software (like Microsoft Excel) and are very easy to generate programmatically.

However with real systems data is rarely flat in structure, and there are often hierarchical relationships between different data entites. It is therefore usually desirable for applications to be able to represent data in the correct hierarchical structure, which presents a challenge of converting this hierarchical data to a flat structure and parsing a file in a flat format into a hierarchical structure (which is the focus of this test question). 
When representing hierarchical data in a flat format, there are often a lot of duplicated data records, and so when this data is then read in, there is a lot of scope for performance issues sorting through the data back to a hierarchical structure.

##Question Details
Question Details
An implementation has already been developed. The performance of this implementation leaves much to be desired. This task is to rework this implementation so that the performance is greatly improved. The original classes are to be kept intact and unchanged so that any performance improvement can be measured against the original implementation. For this reason, they have been separated into their own namespace (aiCorporation.OriginalDoNotChange).

The test application has 2 sections - a section that allows a single CSV file to be parsed and a section that will generate a number of random CSV files, which will be attempted to be parsed and then validated

For use with the single CSV parsing section, there are 3 CSV files of varying complexity that can be used for initial testing:

- Simple.csv: a very simple CSV file
- Medium.csv: a more complex CSV file
- Complex.csv: a very complex CSV file
- See Table 1 for the defintion of the CSV file format.

Column Name | Description | Data Type | Structure | Is Structure Primary Key | Owned By
--- | --- | --- |--- |--- |--- 
SalesAgentName | The name of the sales agent who "owns" the relationship with the client | String | Sales agent	 | No | N/A 
SalesAgentEmailAddress | The email address of the sales agent who "owns" the relationship with the client - this is the unique key for the sales agent	 | String | Sales agent | Yes | N/A
ClientName | The name of the client	 | String | Client | No | Sales Agent (One to many) 
ClientIdentifier | A unique identifer for the client	 | String | Client | Yes | Sales Agent (One to many) 
BankName | The name of the bank that the client holds an account with	 | String | Bank account | Yes | Client (One to many) 
AccountNumber | The account number for the client's account	 | String | Bank account | Yes | Client (One to many) 
SortCode | The sort code for the client's account	 | String | Bank account | Yes | Client (One to many)  
Currency | The currency for the client's account	 | String | Bank account | No | Client (One to many)  

Table 1: CSV file format specification

The Run Bulk Test function will dynamically create CSV files based on the parameters in the form

**NOTE:** you should not need to change the default parameters to successfully pass the test. They have been presented in the form for completeness (and also for your curiousity to play around with)

**NOTE:** when running a bulk test - any files that fail to parse will be saved in a specific directory (the location of which can be controlled via a config setting in the app.config file). This is intended to allow you to reparse (and debug) any specific files using the "Single File Parse" that failed as part of the bulk operation

## Passing crieria
Your solution must be able to successfully read the flat data from a CSV file of the defined format into an instance of a (hierarchical) SalesAgentList object
Your solution can assume that the CSV file will be sorted by sales agent, client and bank account - it does not need to be able to parse a randomly sorted file. The test harness does allow for randomly generated files to be created (via a checkbox on the application form), but it is possible to pass this test even if your solution cannot correctly parse a randomly sorted file.
BULK PARSE: a successful run of a "Run Bulk Test" operation - using the default settings on the form. Please see figure 2 for an screenshot showing an example of a successful run of this function. This will show how much faster the new implementation is compared to the original implementation. In order to successfully pass the test, a performance improvement of at least 5x is required when the solution is running in a RELEASE build with the Randomise Record Order flag UNCHECKED. Extra credit will be given to any solution that can elicit a passing performance improvement for both pre-sorted and random CSV files.

## Task Restrictions
- You should mainly need to add code into the ToSalesAgentList() method of the SalesAgentFileRecordList class (in FlatData.cs) - you can make changes to the code elsewhere in FlatData.cs and HierarchicalData.cs so long as it adheres to the other task restrictions below
- Please do not modify any of the code in any of the classes except the ones in the aiCorporation.NewImproved namespace (FlatData.cs and HierarchicalData.cs). Your code will be tested in a harness where all the other files are unmodifed, and so it should still work against these original, unmodified files
- The SalesAgent, SalesAgentList, Client, ClientList, BankAccount and BankAccountList classes are deliberately immutable. The SalesAgentBuilder, SalesAgentListBuilder, ClientBuilder, ClientListBuilder, BankAccountBuilder and BankAccountListBuilder classes are mutable versions of these classes and are intended to be used to create instances of the immutable classes. **Please do NOT change immutability of the SalesAgent, SalesAgentList, Client, ClientList, BankAccount and BankAccountList classes**.

**A NOTE ABOUT CODING STYLE:** Additional credit will be given to solutions that maintain (and replicate) the style of the code. It is important for developers to be able to code to the standards of a given team, so that the style of any code added feels completely familiar and does not feel alien to anyone else working on the product
