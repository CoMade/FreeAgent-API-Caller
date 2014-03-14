FreeAgent-API-Caller
====================

Call the FreeAgent API to retrieve data from a FreeAgent account and return as an Enumerable for use in a .Net Environment. 

====================

The code consists of 2 projects. The first is the FreeAgentApiCaller project. This is actually where all of the magic happens. 

The second if a command line application for testing the use of the first project. It will return an object to the commandline window as a string. 

This can be run by navigating to the relevant bin folder within the project and running the commaned "TestFreeAgentApiCaller.exe "2014-01-31" "2014-02-26" ". 

Before running this, you should ensure that you have amended the configuration file for the FreeAgentApiCaller application to reflect your connection details. 

Details on where to find these details are found in the "Setting up Atomic OAuth for Free Agent.docx" document found in the same folder as this document. 

When using the DLL from within another code base, it will simply return an Enumerable of the type that you have requested.

Good luck and enjoy! 

Co Made
http://www.comade.co.uk