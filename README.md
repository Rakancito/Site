# Site
ASP.NET Core Web Site

In my case the run dll is Site.dll and my folders are webSite and Site, you can change the names on your project.

Install on Linux Ubuntu 18.04 - x64

# INSTALLING LIBRARIES

Open a terminal and run the following commands (remember is important you location in /):
```
cd / 
wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

sudo add-apt-repository universe
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get install dotnet-sdk-3.0
dotnet --info


sudo apt-get install mysql-server mysql-client
sudo apt-get install apache2
sudo apt-get install supervisor
```
# PREPARING APACHE AND SUPERVISOR

Create in /etc/supervisor/conf.d/ a File web.conf with this program:

```
[program:Site]
command=/usr/bin/dotnet /webSite/Site.dll --urls "http://*:5000"
directory=/webSite/
autostart=true
autorestart=true
stderr_logfile=/var/log/WebSite.err.log
stdout_logfile=/var/log/WebSite.out.log
environment=ASPNETCORE_ENVIRONMENT=Production
user=www-data
stopsignal=INT
```
Open /etc/apache2/sites-available/000-default.conf and replace all for

```
<VirtualHost *:80>
	ServerAdmin webmaster@localhost
	# DocumentRoot /var/www/html

    ProxyPreserveHost On
    ProxyPass / http://127.0.0.1:5000/
    ProxyPassReverse / http://127.0.0.1:5000/

    ErrorLog /var/log/apache2/hellomvc-error.log
    CustomLog /var/log/apache2/hellomvc-access.log common

	ErrorLog ${APACHE_LOG_DIR}/error.log
	CustomLog ${APACHE_LOG_DIR}/access.log combined
</VirtualHost>
```
# COMPILING THE CODE

Upload de Site where you want, i advise in /
```
cd /Site
mkdir webSite
dotnet publish -c release -o ../webSite
cd ../webSite
sudo service supervisor restart
```

# CONFIGURING THE PAGE

In DBController.cs and appsetting.json search  server=IP; database=account; Uid=account; pwd=password; and edit for your data.
