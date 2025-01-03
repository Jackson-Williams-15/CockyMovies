# CockyMovies
The Cocky Movies website is a place to purchase tickets and review movies. This read me focuses on how to run our app. To Run our web app, you need npm and Docker installed.<br>
Read our [wiki](https://github.com/Jackson-Williams-15/CockyMovies/wiki/Architecture) for the architecture!
## CM.API

For an API [solution overview](https://github.com/Jackson-Williams-15/CockyMovies/blob/main/CM.API/README.md), navigate to `CM.API/README.md` for more details.

### Environment variables (Config)
- Create a `.env` in the root directory `/cm.api`.
- This set the connection variables for the docker container and the mail service.
- Add the following to the `.env` file:<br>

      MYSQL_DATABASE=cm_db
      MYSQL_USER=cm_user
      MYSQL_PASSWORD=CockyMoviePassword
      MYSQL_ROOT_PASSWORD=STRONGPASSWORD
      EMAIL_SENDER=CockyMoviesusc@gmail.com
      EMAIL_PASSWORD=lfcr fxql ktzy fpln

### Downloading the Docker container database
- First, download Docker if you haven't already.
 - Navigate to project directory `cd cm.api/cm.api`.
 - Run `docker-compose up --build`.
 - This will download the database and start it. To stop the container you run either `docker-compose down` or `docker-compose stop`.
 - Alternatively, you can use Docker Desktop to start and stop containers.

 ### Apply the migrations
- After downloading the container (make sure it is running), navigate to the `cm.api/cm.api` directory.
-  Run `dotnet database update` to apply the migrations, this should add the entities and seed initial data.

 ### Starting the API
 - After starting the containers, run the command `dotnet run` in the project folder `cm.api/cm.api`
 - Make sure the database container is running before the API for proper connection.

## CM.SPA
To start the frontend make sure the database and API is running before starting the frontend.
- Leave the API folders and navigate to `cd cm.spa`
- If you don't have npm installed already
    - run `npm install`
    - run `npm install axios` so you can make API requests
- To run front end for development run `npm run dev`
- To format frontend code run `npm run format`

# Demo

Here is the [Link](https://youtu.be/dXfNUK3pAYc) to our demo!