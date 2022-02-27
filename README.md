README

# Educational Website Game

![Education Website Game - landing page](https://user-images.githubusercontent.com/60667206/155888796-427f8c93-22bb-48b4-8e9f-6bb4e5786cea.png)

1. [Website Overview](#website-overview)
    1. [Key Notes](#key-notes)
    2. [Tech Stack](#tech-stack)
3. [Getting Started](#getting-started)
    1. [Requirements](#requirements)
    2. [Steps](#steps)
    3. [Issues & Solutions](#issues-and-solutions)
4. [Research](#research)
    1. [Gamification](#gamification)
    2. [Dewey Decimal Classes](#dewey-decimal-classes)
5. [Games](#games)
6. [Issues encountered](#issues-encountered)
7. [Software Specs](#software-specs)

## Website Overview

Year: 2020 (Final Year of Studies)

LearnDewey is an online learning/training platform educating users about the Dewey Decimal System through various online actives. These activities include: Replacing Books, Identifying Areas & Finding Call Numbers.
  
The core purpose for this system is to make learning fun and engaging.
A leaderboard system has been integrated to aid in this purpose, also known as a gamification feature. According to my Research, Leaderboards has yielded the highest implementation across all learning platforms implying its high success rate and participation.

Any user can partake in these activities making it easily accessible, resulting in higher participation. Whenever a user wants to save their results to the leaderboard, they will need an account to do so. Once the user has logged in, they will then be able to save their results to the relevant leaderboard. 

Role functionality has been implemented, where an admin can CRUD leaderboards for future changes and references. When a user is redirected to the Leaderboards section, only the Admin will have access to the CRUD features. A general/anonymous user can just view the leaderboards result. 

The design is simplistic with a very basic layout due to the focus centering on a fully functional application which has met all criteria stipulated. While the design being basic, it also makes it easier for the user to use and navigate through the platform. 
>Sometimes less is more.

Each activity will have basic description of what it is and how to play it. It also includes links to resources which users can use to find out more about the Dewey Decimal System.

### Key Notes
* Data is stored in .mdf files under the App_Data folder. The connection string points to this attached file.
* You can play games anonymously, but to save results you need to create an account and login.
* There may be bugs and don't plan on making any updates. (unless there is a need for it).

### Tech Stack
The following tech stack was utilized:
1. dotnet framework (*back-end & front-end*)
2. SQL Server (*local database*)
3. Azure (*hosting*)

## Getting Started

### Requirements
-	[Visual Studio](https://visualstudio.microsoft.com/vs/community/)
-	[Asp.Net](https://dotnet.microsoft.com/apps/aspnet)
-	[IIS Express](https://www.microsoft.com/en-us/download/details.aspx?id=48264)
-	[Internet Browser](https://www.google.com/chrome/) (Google Chrome)

### Steps:
1.  [Clone](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository#cloning-a-repository)  or [Fork](https://docs.github.com/en/get-started/quickstart/fork-a-repo#forking-a-repository) the repo.
2.	In the folder, double-click the .sln file to open the solution.
3.	Clean and Rebuild the entire Solution.
5.	To run, click “ISS Express" (google chrome)

### Issues and Solutions

**1. Could not find path for 'roslyn\csc.exe'**

![image](https://user-images.githubusercontent.com/60667206/142729416-273fede8-357d-457e-a01b-63a7fc5b7a2b.png)
*   In the Solution Explorer, right-click on Solution 'Educational_Website_game'.
*   Select 'Clean Solution'.
*   Perform step 1 again, and select 'Rebuild Solution'.
*   Click the green arrow to run the project.

**2. Project file has been unloaded.**

![image](https://user-images.githubusercontent.com/60667206/142729574-c49dc20a-c061-416c-988b-6d6dc013ea83.png) 

*	In the Solution Explorer, right-click the missing project and select 'Reload Project'.
    
**3. Project file has been removed.**

![image](https://user-images.githubusercontent.com/60667206/142729630-9c5ce377-5b1d-4a5d-a56e-6638f0d4ae47.png)

*   Right-click the Solution and select 'Add' -> 'Existing Project'.
*   Navitgate to the cloned project to the following directory '[yourpath]\educational-website-game\Educational_Website_game'.
*   Find the LibraryDeweyApp.csproj file, and click 'Open'.
*   Click the green arrow to run the project.

**4. Asp.Net version is not supported.**
*	Right-click 'LibraryDeweyApp' project in the Solution Explorer and select 'Properties'.
*	In the left menu, make sure 'Applicaiton' is selected
*	Under Target Framework, change it to the desired framework.
*	Clean and rebuild the project in the Solution Explorer.

## Research

### Gamification
The Research documentation indicates the exploration of gamification features. In my research, it explains which feature I have chosen and my motivation for doing so.

### Dewey Decimal Classes
We are required to use the advanced data structure of a tree for our Finding Call Numbers activity. From my research, I have identified all the Dewey Decimal Classifcation Data with reasons to how I have set it up.

## Games

### 1. Replacing Books
* #### **Objective**: Help user to understand the hiearchy of Call Numbers.
* #### **Function**: User is required to reorganize the Call Numbers from **Lowest** to **Highest**. 
### 2. Identifying Areas
* #### **Objective**: Help user to Identify Top Level Call Numbers and their Descriptions.
* #### **Function**: User is required to match the coloumns from **Call Number** to **Description** and vice-versa. 
### 3. Finding Call Numbers
* #### **Objective**: Help user to Identify which Levels a Third Level Description falls under.
* #### **Function**: User is required to select the correct option from a multiple choice quiz indicating which level the description can be found. 

## Issues encountered

During the creation of this app, I ran into various issues along the way.

1. Two Different dB Contexts
    *	I wanted to separate the built in aspnet tables from the database of the application. This resulted in having two different contexts for my db and struggled with migrations and switching migrations. 
    *	I ended up just using one context to make it easier for Task 1, and in Task 2 or POE I can implement two db contexts.

2. Saving the user result
    *	When a user result is saved, I pass the result as a query string in the url to the specified controller. The problem with this is that the user can simply edit the querystring to their desired result and refresh the page
    *	Added a property to the model CallNumber to store the user result which can then be submitted in the form.

3. Return models to controller
    *	I was unable to pass some models to controllers due to the setup of my Global CallNumber model. This model is primarily used to store List<strings> of Call numbers and does not get stored in the database, a temporary value.
    *	Previously I used custom hidden fields and adding values manually, now I have adjusted the models in CallNumbers to do the hiddenfor field generation for me which can return the model to my controller. 

4. Stopwatch Timer for replacing books
    *	Unable to post back the value of the timer as it is created through JavaScript. Even if I add a hidden field and assign the final time to that hidden field within a form, the new value will not be posted back as it is initialized on the load of the webpage and can’t be altered within a @Html.BeginForm tag after page load.
    *	Added model value of type TimeSpan in CallNumber, on the load of the view I initialize this timespan. Javascript functions activate the timer and change the value for the HiddenFor TimeRunning, which allowed the javascript value to be passed to the controller.

5. Strongly Typed views with dictionaries
    *	Unable to use html.HiddenFor dictionary model, as a dictionary cannot hold an int, which is required for strongly typed views.
    *	Used hidden Fields seperated by ",", which is then split and assign to the necessary model. Also used TempData[] when switching views of new model. 

6. Converting JSON to Non-Binary Tree
    * Unable to bind json to tree using a dynamic object as each child & parent has to be strongly typed with no form of iterations.
    * Converted the JSON file to a Nest Dictionary. From this, I was able to iterate through each item and nested item to add to the Non-binary tree accordingly. 

## Software specs
    
These are are the software specifications when the project was created.
* Product Version - Microsoft Visual Studio Community 2019
* VS Version - 16.7.2
* .NET Framework- 4.8.03752
