# IdentityAuthWithDBFirst
This is a basic implementation of Identity Authentication with DB First. This project shows how to work with the scenario where the Database of a project already exists containing IdentityAuthentication tables.

The real catch is to inherit database Identity models and context with their corresponding Identity interfaces. Also, modify these database model classes in such a way that it contain only
custom properties and do not contain properties that it is inheriting from parent classes/interfaces.

The last thing, you may want to follow the Code First approach after this point. Because each time you import updated database changes, your models and context changes will be removed unless you have a 
way to import changes keeping existing manual changes to models and context. 

# Tools
EF Core Power Tools - https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools                                                   
I used this extension to add database model classes and context classes.
