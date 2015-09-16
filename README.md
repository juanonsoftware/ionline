# Live Websites

These websites are deployed from this source repository

1. **About DocumentDB** http://documentdb.apphb.com/

2. **General Software News** http://softnews.apphb.com/

3. **Testing** http://ionline.azurewebsites.net
This maynot work if the account I am using is expired.

A simple ASP.NET MVC application allow internet users to leave a message of anything he wants.
Other users can view all messages in the system.

#Requirements:
- There is a textarea which allow user to write a message
- Save the message and display it in the list
- Having a captcha using recaptcha

#Technical requirements:
- Deploy the app on Microsoft Azure
- Use EF code first with migration
- Use MS SQL Server DB

#Configuration

##1. DatabaseSystem
Eg: <add key="DatabaseSystem" value="DocumentDb" />

Specify which database system the application will use. It supports below types:

- SQLServer - using EF Code First approach

*If your application uses this option, you must provide connection string value for IOnlineDb*

- RavenDb - RavenDB

*If your application uses this option, you must provide value for two below keys*
<add key="RavenDbUrl" value="{ENV}" />
<add key="RavenDbApiKey" value="{ENV}" />

- DocumentDb - Azure DocumentDB

*If your application uses this option, you must provide value for two below keys*
<add key="DocumentDbAppKey" value="{ENV}" />
<add key="DocumentDbUri" value="{ENV}" />


##2. CategoryDataFilePath
Specify a URL to file which contains categories for the system

Eg: <add key="CategoryDataFilePath" value="https://raw.githubusercontent.com/juanonsoftware/ionline/master/CategoryData.txt" />

### Caching with Redis

All system categories can be cache if you prefer to do that. The cache will depends on Redis.
To enable it, set value of this key to *true*

<add key="UseRedis" value="{ENV}" />

Then you must provide Redis connection values

<add key="RedisEndPoint" value="{ENV}" />
<add key="RedisPassword" value="{ENV}" />
