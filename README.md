                  Toyota Servis Yonetimi 

 projenin ana dizininde (windows i�in powershell / cmd) "docker-compose up --build"  komutunu �al��t�rarak projeyi kald�rabilirsiniz

 Docker Compose ile ba�lat�lm�� servisleri ve ili�kili volumeleri temizlemek i�in  "docker-compose down -v" kullanabilirsiniz

                   Proje hakk�nda    

 Onion Architecture yakla��m�nda bir mimariye sahiptir

 Api olarak belirtilen klas�r�n i�inde datan�n akt��� Toyota.Api ve frontend projesi olarak Toyota.Web ve toyota.react-web bulunmaktad�r

 "docker-compose up --build" komutu ile mssql, api ve web projeleri aya�a kald�r�l�r

    
    --Api  Url;
        http://localhost:5006/swagger/index.html

            {
              "Username": "admin",
              "Password": "TestAdmin312"
            }

    --Web Url;
        http://localhost:5008

    --React Web Url;
        http://localhost:5010

    --mssql eri�im bilgileri
       server name : localhost,14330
       login : sa
       password : Password1*


 Proje ba�lat�l�rken, veritaban� olu�turulur ve ilk y�netici kullan�c�s� eklenir.

 Toyota.Api / Data / cities.txt db ye aktar�l�r

 projelerde .NET 8 kullan�lm��t�r 

 web projesi .NET 8 MVC olarak olu�turulmu�tur fakat MVC yap�s� sadece frontende navigasyon ve auth sa�lamak i�in kullan�lm��t�r

 react web projesi ise do�rudan apiye eri�mektedir

 Projede servis kay�tlar� ve appalication loglar� olmak �zere 2 adet loglama bulunur

 servis kay�tlar� dbde saklan�r application loglar� ise Toyota.Api/Data/ApplicationLogs.txt de saklan�r ve her bir log 1 sat�rl�k alana yaz�l�r
 
 servis kay�tlar� i�in listeleme, insert, update ve delete i�lemleri yap�labilir, 
        
    
 