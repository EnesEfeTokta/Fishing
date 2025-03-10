![Jet Demo Image](https://github.com/EnesEfeTokta/Fishing/blob/main/FishingLogo1.png)

# Fishing
> * A fun spear throwing game about fishing in a lake

## General 🙌
Fishing is a game project launched on 15/08/2024 using Unity v2022.3.25f1. Gameplay involves the player throwing spears at fish that appear in a lake. Basic mechanics include spear throwing, fish spawning, XP system and sound settings. Assets used include 3D models of stone age animals and weapons, casual music, sound effects and user interface. The project has a GitHub repository, will be released on itch.io and has some unspecified system requirements for Android, Web and Windows platforms. The page also includes developer contact information, screenshots, task list, code used, and developer notes.

Translated with DeepL.com (free version)

## Gameplay 🎮
The fish in the lake shoot spears at the point where the player clicks on the screen.

## Dynamics & Mechanics 🔥
- Throwing a spear.
- Fish coming out of the lake.
- Red damage to fish when hit with a spear.
- Sound on/off and adjustment options.
- XP is earned for each fish killed. Then the XP written object animatedly moves to the XP bar.

## Varlıklar 💎
- 3D
	- [Low Poly Animals 3D assets](https://assetstore.unity.com/packages/3d/characters/animals/low-poly-animals-3d-assets-258476 "Low Poly Animals 3D assets")
	- [Stone Age Weapons Pack](https://assetstore.unity.com/packages/3d/props/weapons/stone-age-weapons-pack-41874 "Stone Age Weapons Pack")
- Ses
	- [Casual Game Music: Happy, Bouncy, Social](https://assetstore.unity.com/packages/audio/music/electronic/casual-game-music-happy-bouncy-social-214104 "Casual Game Music: Happy, Bouncy, Social")
	[Casual Game Fx One Shot](https://assetstore.unity.com/packages/audio/sound-fx/casual-game-fx-one-shot-264212 "Casual Game Fx One Shot")
- UI
	- [2D Casual UI HD](https://assetstore.unity.com/packages/2d/gui/icons/2d-casual-ui-hd-82080 "2D Casual UI HD")
	- [Game Interface kit a for free](https://mmkhlv.itch.io/game-interface-ui-kit "Game Interface kit a for free")
	- [Free Jungle Cartoon GUI](https://free-game-assets.itch.io/free-jungle-cartoon-gui "Free Jungle Cartoon GUI")
	- [Free Bubble Game Button Pack](https://pzuh.itch.io/free-bubble-game-button-pack "Free Bubble Game Button Pack")
- VFX & Particles
	- [Cartoon FX Remaster Free](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565 "Cartoon FX Remaster Free")

## GitHub ⛓️‍💥
[GitHub](https://github.com/EnesEfeTokta/Fishing "Go to GitHub")

## Drawio 🪢
[Draw.io](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/ProjectDiagrams.drawio "Go to Draw.io diagram")

## Itchio 🕹️
[Itchio](https://enes-efe-tokta.itch.io/fish "Itchio")
Itchio özel HTML indirme düğmesi;
```html
<iframe frameborder="0" src="https://itch.io/embed/2902718?linkback=true&amp;bg_color=5aaaff&amp;fg_color=ffffff&amp;link_color=ffffff&amp;border_color=5aaaff" width="552" height="167"><a href="https://enes-efe-tokta.itch.io/fish">Fishing by EnesEfeTokta</a></iframe>
```

## İletişim ☎️
- [Email](mailto:enesefetokta009@gmail.com)
- (+90) 541 586 9564
- [Linkedin](https://www.linkedin.com/in/enes-efe-tokta-6567151b5/)
- [GitHub](https://github.com/EnesEfeTokta)

## Game controls 🛂
- Android
	- Spear Throwing: Finger touch.

## Oynanış 🚀
Bu Alan Daha Doldurulmadı !!!

## Requirements ⚠️
- Android
	- Version 7
	- Storage 1 GB
	- Ram: 2 GB

## Images 🖼️
null

## Kullanılan Kodlar 👩‍💻
### Balık Yönetimi
[Fish](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/Fish/Fish.cs) - [FishData](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/Fish/FishData.cs) - [HealthFish](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/Fish/HealthFish.cs)
### Oyuncu Kontrolleri
[SpearThrowing](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/Fish/HealthFish.cs)
### Oyun Yönetimi
[GameManager](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/GameManager/GameManager.cs) - 
[FishIconMovement](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/Fish/FishIconMovement.cs) - [Timer](https://github.com/EnesEfeTokta/Fishing/blob/main/Fishing/Assets/GameManager/Timer.cs)

**BURASI OYUNUN KARARLI SÜRÜMÜ YAYINLANDIĞINDA DOLDURULACAK**

## Balık Türleri ve Özellikleri ✨
| İsim | Seviye | Sağlık | Money | Puan | Hız | Hasar | Görseller |
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|Balık1|Başlangıç|80|20|40|Yavaş|60|Null|
|Balık2|Başlangıç|100|33|50|Yavaş|50|Null|
|Balık3|Orta|120|40|60|Yavaş|45|Null|
|Balık4|Orta|140|46|70|Orta|40|Null|
|Balık5|Orta|160|53|80|Orta|35|Null|
|Balık6|Orta|180|60|90|Orta|30|Null|
|Balık7|Zor|200|66|100|Hızlı|25|Null|
|Balık8|Zor|250|83|125|Hızlı|20|Null|

## Oyun İçi Görevler/Seviyeler Listesi ve Detayları 📒

| Seviye | Zorluk | Balık Türleri ve Sayısı                    | Bitirme Süresi | Puan | Balık | Money | Göl             | Açıklama                                                                 |
|:------:|:------:|:-----------------------------------------:|:--------------:|:----:|:-----:|:-----:|:---------------:|:------------------------------------------------------------------------:|
| Seviye 1 | Kolay  | (1 x Balık 1)                            | 60             | 40   | 1     | 26    | W(-5, 5) & H(-5, 5) | Temel mekanikleri öğrenme, rahat oyun akışı.                            |
| Seviye 2 | Kolay  | (5 x Balık 1), (1 x Balık 2)            | 90             | 250  | 6     | 163   | W(-5, 5) & H(-5, 5) | Balıklarla tanışma, biraz daha dikkat gerektirir.                        |
| Seviye 3 | Kolay  | (5 x Balık 1), (2 x Balık 2)            | 90             | 450  | 10    | 295   | W(-5, 5) & H(-5, 5) | Balıklarla tanışma, biraz daha dikkat gerektirir.                        |
| Seviye 4 | Kolay  | (5 x Balık 1), (3 x Balık 2)            | 90             | 510  | 11    | 335   | W(-5, 5) & H(-5, 5) | Balıklarla tanışma, biraz daha dikkat gerektirir.                        |
| Seviye 5 | Kolay  | (5 x Balık 1), (5 x Balık 2)            | 100            | 750  | 15    | 495   | W(-5, 5) & H(-5, 5) | Oyuncu bir çok balığı kaçırmamaya çalışacak.                             |
| Seviye 6 | Kolay  | (7 x Balık 1), (5 x Balık 2), (3 x Balık 3) | 110         | 880  | 18    | 580   | W(-5, 5) & H(-5, 5) | Balık sayısı arttıkça oyuncuda oyuna bağlılık artırılması hedefleniyor.  |
| Seviye 7 | Kolay  | (7 x Balık 1), (6 x Balık 2), (5 x Balık 3) | 110         | 1050 | 21    | 693   | W(-5, 5) & H(-5, 5) | Kısa sürede çok fazla balığın avlanması bekleniliyor oyuncunun.          |
| Seviye 8 | Orta   | (4 x Balık 1), (5 x Balık 2), (5 x Balık 3), (1 x Balık 4) | 110 | 780  | 15    | 515   | W(-5, 5) & H(-5, 5) | Oyuncu artık orta seviyelerdeki balıklar ile etkileşime girebilecek.     |
| Seviye 9 | Orta   | (4 x Balık 1), (5 x Balık 2), (5 x Balık 3), (3 x Balık 4) | 110 | 920  | 17    | 607   | W(-5, 5) & H(-5, 5) | Oyuncuya ek zorluklar çıkarılıyor.                                       |
| Seviye 10 | Orta  | (3 x Balık 1), (5 x Balık 2), (7 x Balık 3), (5 x Balık 4) | 130 | 1120 | 20    | 739   | W(-5, 5) & H(-5, 5) | Artık balık sayısı arttı ve oyuncu stres altında tutuluyor.              |
| Seviye 11 | Orta  | (4 x Balık 1), (5 x Balık 2), (7 x Balık 3), (5 x Balık 4) | 130 | 1170 | 21    | 772   | W(-5, 5) & H(-5, 5) | Kısa sürede çok fazla balığı avlaması bekleniliyor oyuncunun.            |
| Seviye 12 | Orta  | (4 x Balık 1), (5 x Balık 2), (7 x Balık 3), (5 x Balık 4) | 130 | 1180 | 21    | 779   | W(-5, 5) & H(-5, 5) | Kısa sürede fazla balığı avlaması bekleniyor oyuncunun.                  |
| Seviye 13 | Orta  | (4 x Balık 1), (5 x Balık 2), (5 x Balık 3), (1 x Balık 4), (1 x Balık 5) | 140 | 1260 | 22 | 832 | W(-5, 5) & H(-5, 5) | Balık sayısının artışı oyuncuyu zorlama amaçlıdır.                       |
| Seviye 14 | Orta  | (4 x Balık 1), (5 x Balık 2), (5 x Balık 3), (3 x Balık 4), (5 x Balık 5) | 160 | 1580 | 26 | 1044  | W(-5, 5) & H(-5, 5) | Yeni balık sayısının artışı oyuncuyu strese sokacak.                     |
| Seviye 15 | Orta  | (4 x Balık 1), (5 x Balık 2), (5 x Balık 3), (5 x Balık 4), (1 x Balık 6) | 160 | 1700 | 26 | 1124  | W(-5, 5) & H(-5, 5) | Balık sayısının artışı oyuncuyu zorlama amaçlıdır.                       |
| Seviye 16 | Zor   | (2 x Balık 1), (3 x Balık 2), (5 x Balık 3), (5 x Balık 4), (5 x Balık 5) | 170 | 1530 | 23 | 1012  | W(-5, 5) & H(-5, 5) | Oyuncuya artık zor seviyede ki balıklar çıkarılıyor.                     |
| Seviye 17 | Zor   | (2 x Balık 1), (3 x Balık 2), (5 x Balık 3), (5 x Balık 4), (7 x Balık 5), (1 x Balık 6) | 170 | 1710 | 24 | 1132  | W(-5, 5) & H(-5, 5) | Oyuncuya fazla balık sunarak onu zorlanıyor.                             |
| Seviye 18 | Zor   | (2 x Balık 1), (3 x Balık 2), (5 x Balık 3), (5 x Balık 4), (7 x Balık 5), (5 x Balık 6), (1 x Balık 7) | 180 | 1990 | 28 | 1318  | W(-5, 5) & H(-5, 5) | Oyuncuya artık zor seviyede ki balıklar çıkarılıyor.                     |
| Seviye 19 | Zor   | (2 x Balık 1), (3 x Balık 2), (5 x Balık 3), (5 x Balık 4), (7 x Balık 5), (5 x Balık 6), (5 x Balık 7) | 170 | 2390 | 32 | 1582  | W(-5, 5) & H(-5, 5) | Süreyi düşürerek oyuncuya ekstra zorluk çıkartıyor.                      |
| Seviye 20 | Zor   | (2 x Balık 1), (3 x Balık 2), (5 x Balık 3), (5 x Balık 4), (7 x Balık 5), (5 x Balık 6), (5 x Balık 7), (5 x Balık 8) | 170 | 3015 | 37 | 1997 | W(-5, 5) & H(-5, 5) | Oyuncunun en zorlandığı seviyedir.                                      |


## Başarı Hesaplamaları 🌟
Başarı hesaplama algoritması, oyuncunun seviyeyi tamamlama süresine ve her bir balık için tanımlı olan puan ve para değerine dayalı olarak iki ana faktör üzerinden hesaplanmaktadır. Her bir balığın önceden belirlenmiş sabit puan (P) ve para (M) değeri bulunmaktadır. Bu değerler, oyuncunun seviyeyi ne kadar sürede tamamladığına göre değişkenlik göstermektedir. Hesaplama süresi, seviye süresi (S) baz alınarak dört bölüme ayrılmakta ve buna göre kazanç oranları belirlenmektedir.

Bu hesaplama modelinin matematiksel tanımı aşağıdaki gibidir:

- **S:** Seviye süresi
- **T:** Oyuncunun tamamladığı süre
- **P:** Balık başına verilen sabit puan değeri
- **M:** Balık başına verilen sabit para değeri

**Kazanç Oranları:**

1. **T ≤ S/4:** Tam P & M kazanılır.
2. **S/4 < T ≤ S/2:** P & M ’in yarısı verilir (P/2 & M/2).
3. **S/2 < T ≤ 3S/4:** P & M ’in çeyreği verilir (P/4 & M/4).
4. **T > 3S/4:** P’nin çeyreği, M ’in ise sabit çeyrek oranı verilir (P/4 & M/4).

**Toplam Kazançlı Puan & Para:**

Toplam kazanılan puan ve para, yukarıdaki kurallar çerçevesinde her bir balık için hesaplanıp toplanarak elde edilir.

Bu algoritma, oyuncunun tamamladığı süreye göre esnek bir ödüllendirme sağlar ve başarı seviyesini doğrudan oyuncunun performansına göre ölçmeye olanak tanır.

## Yasak ve Hukuki Uyarılar ⚠️
* Logo, Banner ve arka plan görseller OpenAI LLC araştırma şirketinin üretken yapay zeka aracı olan ChatGPT-4o ürünü kullanılarak üretilmiştir. OpenAI kullanıcı sözleşmesi gereği üretilen içeriklerin mülkü şahsımıza aittir.

	*Subject to the Content Policy and Terms, you own the output you create with ChatGPT, including the right to reprint, sell, and merchandise – regardless of whether output was generated through a free or paid plan.*
	**What is ChatGPT? | OpenAI Help Center**

* VCS (Version Control System) olarak kullanılan *Git* ‘in kullanım şartları gereği açık kaynaklı oluşundan dolayı mülkiyetsiz bir yapıdır.

* Kodların depolandığı *GitHub* platformu ise ücretsiz sürümü kullanılıyor ve her hangi bir mülkiyet sorunu teşkil etmiyor.

* Projenin lisansı *GPL-3.0*  ‘dır. Bu lisans ile proje açık kaynaklı olup öğretici ve kar amacı gütmeyen her amaçla kullanılabilir. Ancak gelir elde etme amacıyla hiç bir kod proje sahibinden izin alınmadan kullanılamaz.

* Oyunun motoru olarak kullanılan Unity Engine ‘nın Personal lisansı kullanılmıştır. Bundan dolayısıyla Personal lisansında değinilen kullanım politikasına uyuluyor.

	*This license applies to the Visual Studio Code product. Source code for Visual Studio Code is available at https://github.com/Microsoft/vscode under the MIT license agreement. The software is licensed, not sold. This agreement only gives you some rights to use the software. Microsoft reserves all other rights. Unless applicable law gives you more rights despite this limitation, you may use the software only as expressly permitted in this agreement. In doing so, you must comply with any technical limitations in the software that only allow you to use it in certain ways.*
	**code.visualstudio.com**

* Oyun yapımında kullanılan tüm araçlar ve varlıklar Unity Asset Store ‘dan temin edilmiştir. Bu süreçte Asset Store ‘un *Unity Asset Store Terms of Service and EULA* lisansına tabi tutuluyoruz.

## Pazarlama & Tanıtım Stratejileri 🖊️
### Hedef Kitle Analizi
Bir grup katılımcıyla gerçekleştirilen anket ve soru-cevap oturumları sonucunda hedef kitlenin ihtiyaçları tespit edilmiştir. Belirlenen bu kriterler, oyunun geliştirme sürecinde dikkate alınmış ve oyunun şekillendirilmesinde önemli bir rol oynamıştır.
### Sosyal Medya ve Topluluk
Oyun LinkedIn ve Tik Tok ‘ta oynanış videoları paylaşılacaktır. Bu süreç oyun yayınlandıktan sadece üç aya kadar sürecektir. Üç aydan sonra paylaşımlar ve destek sona erecek.
### PR ve Medya Çalışmaları
null
### Beta Testi ve Erken Erişim
null
### Platformlara Özgü Startejiler
null
### Kullanıcı Geri Bildirimlerine Dayalı İyileştirmeler
Kullanıcılar, **E-Posta**, **LinkedIn** ve **GitHub Issues** aracılığıyla geri bildirim sağlayabileceklerdir. Bu geri bildirimler, hem oyunun kendisinin hem de geliştirme sürecinin iyileştirilmesine katkıda bulunacaktır.

🚨 Proje, uzun vadeli bir hedef taşımadığından, geri bildirimler yalnızca oyunun yayınlanmasından sonraki beş ay boyunca kabul edilecektir. Ancak, projenin açık kaynaklı olması nedeniyle **GitHub Issues** üzerinden uzun süre geri bildirimde bulunmak mümkün olacaktır. Proje sahibi tarafından tam destek sağlanmasa da, projeye katkıda bulunan diğer gönüllü geliştiriciler bu geri bildirimlere yanıt verebilir ve gerekli aksiyonları alabilirler.
### Lokalizasyon
Oyunun birincil dil ayarı _İngilizcedir_ ve tüm sosyal medya iletişimleri İngilizce olarak yürütülecektir. Oyun içindeki diğer dil seçenekleri arasında _Türkçe_, _Fransızca_, _İspanyolca_ ve _Almanca_ yer almaktadır. Teknik bir sorun yaşanmaması halinde _Arapça_, _Rusça_ ve _Korece_ desteği de gelecek güncellemelerde sunulacaktır.

## Lisans 🔐
[Lisans](https://github.com/EnesEfeTokta/Fishing/blob/main/LICENSE "Lisans")