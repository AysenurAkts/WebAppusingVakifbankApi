# WebAppusingVakifbankApi

Vakıfbank Api Store'daki Altın Fiyatları Api'ı kullanılmıştır. Client tarafında kullanıcıdan alınan tarih bilgisi server'a iletilir. Yazılan Web Servisi ile Altın Fiyatları Api'ına Rest çağrısında bulunarak, tarihe ait altın fiyatları bilgileri kullanıcıya gösterilen bir web uygulamasıdır. Önyüz Angular ve Bootstrap frameworkleri ile arkayüz ASP.Net Core ile geliştirilmiştir. Uygulamanın loglanma işlemleri için MSSQL kullanılmıştır.

Kullanılan teknoloji yığını (kütüphane/çerçeve vb.):

• ASP.NET CORE
• ANGULAR JS
• BOOTSTRAP
• CSS
• HTML
• JAVASCRIPT

   ![image](https://user-images.githubusercontent.com/81049064/141126658-43a74edd-210f-4df3-ac86-3836e4eb57be.png)
  
  Client credential yöntemi kullanılarak yetkilendirme işlemleri gerçekleştirilmiştir. Auth Server'dan token elde edildikten sonra, ilgili token'ı kullanarak API'lara istekte bulunulmaktadır. 
  
Altın Fiyatları Api'ından alınan verilerin içeriği:  
  ![image](https://user-images.githubusercontent.com/81049064/141126754-bec047fa-d3b7-4c65-bfc5-fc6bad897295.png)

Veritabanı loglama işlemleri gerçekleştirilmiştir. InsertGoldLog fonksiyonu ile loglama işlemleri gerçekleştirilmektedir. Sorgu sonuçları, sorgunun yapıldığı tarih ile birlikte loglanır.
![image](https://user-images.githubusercontent.com/81049064/141126834-5823fc7d-f406-4c13-9b2b-fab8e5aadd7d.png)


  ![image](https://user-images.githubusercontent.com/81049064/141125641-a09c30b2-c0d8-4b2c-ac06-63547cb56d0b.png)

