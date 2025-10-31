                  Toyota Servis Yonetimi 

 projenin ana dizininde (windows için powershell / cmd) "docker-compose up --build"  komutunu çalýþtýrarak projeyi kaldýrabilirsiniz

 Docker Compose ile baþlatýlmýþ servisleri ve iliþkili volumeleri temizlemek için  "docker-compose down -v" kullanabilirsiniz

                   Proje hakkýnda    

 Onion Architecture yaklaþýmýnda bir mimariye sahiptir

 Api olarak belirtilen klasörün içinde datanýn aktýðý Toyota.Api ve frontend projesi olarak Toyota.Web ve toyota.react-web bulunmaktadýr

 "docker-compose up --build" komutu ile mssql, api ve web projeleri ayaða kaldýrýlýr

    
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

    --mssql eriþim bilgileri
       server name : localhost,14330
       login : sa
       password : Password1*


 Proje baþlatýlýrken, veritabaný oluþturulur ve ilk yönetici kullanýcýsý eklenir.

 Toyota.Api / Data / cities.txt db ye aktarýlýr

 projelerde .NET 8 kullanýlmýþtýr 

 web projesi .NET 8 MVC olarak oluþturulmuþtur fakat MVC yapýsý sadece frontende navigasyon ve auth saðlamak için kullanýlmýþtýr

 react web projesi ise doðrudan apiye eriþmektedir

 Projede servis kayýtlarý ve appalication loglarý olmak üzere 2 adet loglama bulunur

 servis kayýtlarý dbde saklanýr application loglarý ise Toyota.Api/Data/ApplicationLogs.txt de saklanýr ve her bir log 1 satýrlýk alana yazýlýr
 
 servis kayýtlarý için listeleme, insert, update ve delete iþlemleri yapýlabilir, 
        
    
 