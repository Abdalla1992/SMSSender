
## Instructions on how to set up  application

- ensure u install .net6 sdk 
- change ConnectionString in app settings file 
-  i use Code first approch ,  run "Update-Database" in package console  to create DB schema
- run The application
-------------------------------------------------------------------------------
 # Used Enum
 - SMS Status enum values 
	   {
		   Sent = 1,
		   NotSent = 2
	   }
  - Provider Status Enum 
		 {
			 Active = 1,
			 NotActive = 2
		 }  
----------------------------------------------------------------------------------
## Test and Run the Application
-  i use Nexmo and Twilio as SMS providers
-  - You must change All keys in APP Settings file for Nexmo and Twilio SMS providers because the current keys become blocked. 
- Note When sent by Twilio  you can send for this number only 201064600617 
   In incase testing you must verify mobile numbers by Twilio System before sending sms
- Open Swagger like  'https://localhost:7201/swagger/index.html'
- in Swagger => got to HttpPost action called API/Provider and added the following SMS providers as separate 
-  don't Change Providers' name when you  add them 

		 1- {
		  "name": "Nexmo",
		  "status": 1, 
		  "cost": 0.2,
		  "apiUrl": ""
		}
		
		2- {
		  "name": "Twilio",
		  "status": 1, 
		  "cost": 0.2,
		  "apiUrl": ""
		}


-  you can check SMS providers Secerts in APP settings file  for Nexmo and Twilio 
- Now can test all APIS by Swagger 


Thanks 
 
 
