1. Open the Solution in Visual Studio
2. Build the project 
3. Navigate to tools ans select Nuget Package manager -> Package Manager Console (PMC)
4. On the console execute the following command
 Update-Database -Context Information_Wiki_IdentityContext


5. On the console execute the following command

 Update-Database -Context Information_Wiki_DataContext



6. After migration is successful Run the project 





7. You can login with the following credentials to visit the site as an Author or Register as Author 
Can add and View Wiki pages 

 User : frank@gmail.com
Password: P@ssword12



7. You can login with the following credentials to visit the site as a viewer or Register as a Viewer
Can View Wiki pages 

 User : lily@gmail.com
Password: P@ssword12





The identity  authentication code used in the project were obtained by following URLS

Introduction to Identity on ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.0&tabs=visual-studio
