# calorie-counter
A calorie counting app where users can keep track of the amount of calories consumed each day.
They can input the name of the food and the amount of calories, and the backend keeps track of the total calories for the day.

Project created using React (JavaScript) and .Net Core, with a Postgres database and Adminer to ease development.
Uses REST HTTP requests to communicate between frontend and backend.
Uses JSON Web Tokens to authenticate and authorise the requests from the frontend to the backend.
Databases were designed and created using Entity Framework Core.

Project was written in VS Code.
Endpoints were tested using Postman.

Project has been mostly Dockerised.
Docker and Docker-Compose is required to run the project.

To run the project simply enter 'docker-compose up' in the root directory of the project.

NOTE:
    Im having some issues Dockerising the React project.
    To run that, you must have Node installed and type 'npm install' in /frontend.
    Then type 'npm start' to run the project


Future work:
    Add a progress bar to show the total calories and the remaining calories for each calendar day.
    Call an external API to get a list of foods/calories for the user to choose.




backend-api JSON Web Token authentication and authorisation adapted from:
	https://github.com/evgomes/jwt-api
