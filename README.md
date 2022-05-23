1 - open the TaskManager.sln file with vs studio 2019

2 - compile the solution

3 - change the SQL server database connection in the appsettings.json file of the TaskManager.Api web project

4 - Set the TaskManager.Api as a start up project 

5 - Initialize the database by running the following command in the Package manager Console 
	Update-Database -Context "TaskManagerContext"  -Project "TaskManager.DataStore"  -StartupProject "TaskManager.Api"
	

Use the Postman link for demonstrating the azure API add task and update task features 

https://www.postman.com/planetary-shadow-274267/workspace/assignment/collection/2268284-3e241fea-e9dc-41a3-aa6e-7a35023de46b?action=share&creator=2268284
