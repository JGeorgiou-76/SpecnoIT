# SpecnoIT
Specno Technical Assessment (Junior Back End Developer)

The created API uses Dot Net 6, Entity Framework has been used for the database, with SQLite as the host.

Within the Project I am making use of the Hash and Salt functionality to protect the users passwords within the database. Validation is used against the Username and Password to make sure that both the username and password are provided for when registering and logging in.

JSON Web Tokens are used for Authorisation when a User completes a successful login, on a successful login or registration the users is given a token which is used for authentication when other API calls are seen.

Extention Methods are also used to clean up the Startup class, one is the Application Service which holds the connection string to the database and also the Service for Token creation, and the other extention method is the Identity Service which is used to authenticate the users with the Token key that is presented to them on successful Login and registrations.
