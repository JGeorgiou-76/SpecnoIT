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

Three repositories are created, one is a UserRepository, a PostsRepository, and a CommentsRepository The repositories were created to get an instance of their respective tables in the database, making it easier to handle those tables and to facilitate CRUD.

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
AutoMapper is used to Map the Entities against their respective DTOs, or visa-versa.


HOW TO RUN SPECNOIT:

Create a Clone of the Application from Github:
https://github.com/JGeorgiou-76/SpecnoIT

Please confirm that Dotnet 6 Runtime is installed.

Build the API on Visual Studio, with "dotnet watch run". If the Database is not seen, Run the Command "dotnet ef database update".

Then go to Postman, to view the collection of API Calls available.
https://www.postman.com/JasonGeorgiou76/workspace/specnoit/collection/1069145-260174e0-d70e-4328-9dc9-012507ccc9f6

Using SpecnoIT:

Registration of a new User:
  - Register a New User with (Register New User) API call, this Call takes two arguments, "Username" and "Password", both fields are required.

User Login:
  - Registration of a new user causes the User to be logged in, and a Token gets created on successful registration, (User Login) will also produce a Token for Authentication. "Username" and "Password" is required.

Create new post:
  - Users are able to create new posts (Add New Post), one argument "Post" is needed for the API call. Post cannot be empty. 

Update an existing post:
  - Users are able to update posts that they have created, the API call (Update a Post), will prevent the creation of empty posts, a check is done to make sure that the selected post exists, and that the post belongs to the logged in User. Post "Id" and "Post" are the required arguments to update a post.

Delete post:
  - Users are able to delete their posts (Delete a Post by ID), the post "Id" is required, Checks are made to make sure Users cannot delete other users posts.

Get all Posts User has created:
  - Users are able to see all the posts that they have created (Get all Posts User has Created), attached to those posts are the Comments, and Likes. No arguments are needed for this API Call.

Get all Posts by another Users:
  - Users are able to view all posts by other Users (Get All Posts by Username), this API call gets the Username from the URL, enter the desired Username after the "user-posts/" in the URL. This API Call will return all Posts by that Username, with all Comments, and Likes.

Create a Comment on a Post:
  - Users are able to create Comments on existing posts (Create New Comment on Post), Two arguments are required, "PostsId" and "Comment", checks are done against Empty Strings, and to see if the Post exists.

Upvote or Down Vote a Post:
  - Users are able to Upvote or Downvote a post (Like a Post), Two arguments are required, "PostsId" and "Liked" which is a True or False value. Checks are done to see if post exists, and prevents a user from creating multiple Upvotes/Downvotes on Posts.

Upvote or Down Vote a Comment:
  - Users are able to Upvote or Downvote a Comment (Like a Comment), Two arguments are required, "CommentsId" and "Liked" which is a True or False value. Checks are done to see if the Comment exists, and prevents a user from creating multiple Upvotes/Downvotes on Posts.

Get All Posts User has Upvoted/Downvoted:
  - Users are able to get all the Posts that they have voted on (Get Posts User has Liked/Disliked). No arguments are required.


This concludes the explanation of SpecnoIT.

Thank you for this Opportunity.

Jason Georgiou.
