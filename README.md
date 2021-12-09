# SpecnoIT
Specno Technical Assessment (Junior Back End Developer)

The created API uses Dot Net 6, Entity Framework has been used for the database, with SQLite as the host. I used SQLite for it's Portability, I could have used other databases but choose to use this one.

Within the Project I am making use of the Hash and Salt functionality to protect the users passwords within the database. Validation is used against the Username and Password to make sure that both the username and password are provided for when registering and logging in.

JSON Web Tokens are used for Authorisation when a User completes a successful login, on a successful login or registration the users is given a token which is used for authentication when other API calls are seen.

Extention Methods are also used to clean up the Startup class, one is the Application Service which holds the connection string to the database and also the Service for Token creation, and the other extention method is the Identity Service which is used to authenticate the users with the Token key that is presented to them on successful Login and registrations.

3 Controllers are created, BaseApiController which is the Parent Controller and all other controllers inherit from this controller. 
AccountController was created to control the creation of new users and handle the creation of the PasswordHash and PasswordSalt to protect the users password in the database, this controller also controls the login functionality for registered users, creating a token for each user so that authentication can be checked with each API Call. 
UsersController was created to handle the API Calls that users can make:
  - Create, Update, and Delete Posts.
  - Comment on Posts.
  - Upvote or Downvote Posts.
  - Upvote or Downvote Comments.
  - View all posts the user has created.
  - View all posts of another user.
  - View all posts the user has Upvoted or Downvoted.

Two repositories are created, one is a UserRepository, and the other a PostsRepository. The repositories were created to get an instance of the respective tables in the database, making it easier to handle those tables and to facilitate CRUD.

As for the Database, 5 Entities were created:
  -AppUser
  -Posts
  -Comments
  -LikedPosts
  -LikedComments

AppUser has a One-to-Many relationship with Posts
Posts has a One-toMany relationship with Comments
Posts has a One-to-Many relationship with LikedPosts
Comments has a On-to-Many relationship with LikedComments

Each entity has their own DTO to help with the CRUD procedures.
AutoMapper is used to Map the Entities against their respective DTOS, or visa-versa.


HOW TO RUN SPECNOIT:



https://www.postman.com/JasonGeorgiou76/workspace/specnoit/collection/1069145-9808ec9b-0762-4605-b013-6a27f9db7f0f

