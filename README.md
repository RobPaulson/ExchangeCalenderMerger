# Introduction
Exchance Calendar Merge (ECM) is a small console application used to keep my Clarity Exchange calendar sync'd up with my client's.  If you set it up as a schedule task 
(to run whether you are logged in or not), you won't see the console window pop-up and interrupt you when it runs.  Requires EWS (Exchange Web Services) to be turned on
at your client site.

*** USE AT YOUR OWN RISK ***

This worked for me and a few others at AJG, but you know how it goes.  

# Getting Started
Pull down the code.  You'll need to edit the app.config file.  Read the comments in there and it should get you up and running.  For your first run, you'll need to set
'promptForPassword' to be true.  Once you've provided your credentials, you can edit it back to false.  Whenever you change your password, you'll need to repeat this 
process.  Don't try entering anything into 'Password1' or 'Password2' the application will prompt you and manage them internally.  

By default - it will generate log files into C:\Logs\ECM - if you have problems, that's the best place to look first.

# Contribute
Feel free to make any changes/log any tickets/do whatever you want.  
