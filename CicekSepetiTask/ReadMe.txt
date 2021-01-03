Merhaba, 

-Bu proje .Net Core 3.1 sürümüyle geliştirilmiştir. VS 2019 sürümlerinde çalışır. Daha alt sürümlerde SDK problemleri ile karşılabilirsiniz.(ref: https://dotnet.microsoft.com/download/dotnet-core/3.1)
-Api test için örnek ekleme verisi AddedItem.json dosyasıdır.

-Api'ye istek atmak için 

	IIS; https://localhost:44383/api/basket 
	Docker; https://localhost:49155/api/basket (no response data hatası alınırsa portlar kontrol edilmelidir.)

- Proje test coverage için terminalde "dotnet test /p:CollectCoverage=true" komutu çalıştırılmalıdır. Sonuç ekran görüntüsü eklenmiştir.


