# Linking Match 2D - Necati Akpınar

[English](#english) | [Türkçe](#türkçe)

## English

### Project Overview
Linking Match 2D is a Unity-based game project that implements a modern, scalable architecture using various design patterns and best practices. The project is structured to be maintainable, extensible, and follows clean code principles. It uses Universal Render Pipeline (URP) for high-quality 2D rendering and visual effects.

### Key Features
- **Modern Architecture**: Utilizes SOLID principles and design patterns
- **State Management**: Implements state machines for game flow control
- **Event System**: Custom event bus implementation for decoupled communication
- **Resource Management**: Addressables integration for efficient asset loading
- **UI System**: Modular UI implementation with TextMeshPro integration
- **Object Pooling**: Efficient memory management for game objects
- **Factory Pattern**: For object creation and management
- **Logging System**: Custom logging implementation for debugging
- **2D Gameplay**: Optimized for 2D game mechanics and performance
- **Visual Effects**: Enhanced particle systems and animations
- **Save System**: Persistent data management for game progress

### Technical Details
- **Rendering**: URP-based 2D rendering with post-processing effects
- **Physics**: 2D physics system with optimized collision detection
- **Animation**: Smooth animations using DOTween and Unity's animation system
- **Performance**: Optimized for mobile and desktop platforms
- **Memory Management**: Efficient object pooling and resource handling
- **Input System**: Cross-platform input handling

### Project Structure
```
Assets/
├── Scripts/
│   ├── Abstracts/      # Abstract classes and interfaces
│   ├── Adapters/       # Adapter pattern implementations
│   ├── Addressables/   # Addressable asset system
│   ├── Controllers/    # Game controllers
│   ├── Data/          # Data structures and models
│   ├── Editor/        # Custom editor tools
│   ├── EventBus/      # Event system implementation
│   ├── Extensions/    # Extension methods
│   ├── Factories/     # Factory pattern implementations
│   ├── Helpers/       # Utility classes
│   ├── Interfaces/    # Interface definitions
│   ├── Loggers/       # Logging system
│   ├── Managers/      # Game managers
│   ├── Miscs/         # Miscellaneous utilities
│   ├── Pools/         # Object pooling system
│   ├── StateMachines/ # State machine implementations
│   ├── UI/            # User interface components
│   └── UnityObjects/  # Unity-specific implementations
├── Scenes/            # Game scenes
├── Prefabs/           # Prefabricated game objects
├── Resources/         # Resources folder
├── Sprites/           # Sprite assets
└── ScriptableObjects/ # Scriptable object assets
```

### Dependencies
- Unity 2021.3.22 or later
- Universal Render Pipeline (URP)
- TextMeshPro
- UniTask
- DOTween
- Addressables

### Development Guidelines
- Follow SOLID principles
- Use the existing architecture patterns
- Write clean, documented code
- Use the event system for communication between components
- Implement proper error handling and logging
- Maintain consistent coding style
- Document all public APIs
- Write unit tests for critical components
- Optimize for performance and memory usage

### Linking Logic
The game implements a sophisticated state machine system to manage the core matching mechanics:

#### State Machine Flow
1. **Input State**
   - Handles player input and tile selection
   - Manages tile selection chain creation
   - Validates tile connections (neighbors only)
   - Requires minimum 3 tiles for a valid match
   - Transitions to Decision State when a valid chain is created

2. **Decision State**
   - Processes the selected tile chain
   - Activates matched tiles
   - Tracks move count
   - Transitions to Refill State after processing

3. **Refill State**
   - Manages tile refill mechanics
   - Handles tile dropping from above
   - Spawns new tiles for empty spaces
   - Returns to Input State after refill

#### Tile System
- **BaseTileMono**: Core tile implementation
  - Manages tile connections and neighbors
  - Handles tile selection/deselection
  - Controls tile element spawning and dropping
  - Implements pointer events for user interaction

#### Key Features
- **Chain Validation**: Ensures only adjacent tiles can be connected
- **Tile Refill**: Automatic refill system with gravity simulation
- **Spawner Tiles**: Special tiles that generate new elements
- **Element Types**: Different tile elements with unique properties
- **Move Tracking**: Keeps count of player moves

### Editor Tools
The project includes custom editor tools for development and testing:

#### Linking Game Editor
- **Level Management**
  - Reset current level progress
  - Set specific level index
  - Clear all saved data
- **Currency Management**
  - Increase coin amount
  - Decrease coin amount
  - Reset coin balance
- **Development Features**
  - Quick access through Unity's Tools menu
  - Real-time data modification
  - Persistent data management

## Türkçe

### Proje Genel Bakış
Linking Match 2D, çeşitli tasarım desenleri ve en iyi uygulamaları kullanarak modern, ölçeklenebilir bir mimari uygulayan Unity tabanlı bir oyun projesidir. Proje, bakımı kolay, genişletilebilir ve temiz kod prensiplerini takip edecek şekilde yapılandırılmıştır. Yüksek kaliteli 2D render ve görsel efektler için Universal Render Pipeline (URP) kullanmaktadır.

### Temel Özellikler
- **Modern Mimari**: SOLID prensipleri ve tasarım desenleri kullanır
- **Durum Yönetimi**: Oyun akışı kontrolü için durum makineleri
- **Olay Sistemi**: Bileşenler arası iletişim için özel olay sistemi
- **Kaynak Yönetimi**: Verimli varlık yükleme için Addressables entegrasyonu
- **UI Sistemi**: TextMeshPro entegrasyonlu modüler UI uygulaması
- **Nesne Havuzlama**: Oyun nesneleri için verimli bellek yönetimi
- **Fabrika Deseni**: Nesne oluşturma ve yönetimi için
- **Loglama Sistemi**: Hata ayıklama için özel loglama uygulaması
- **2D Oynanış**: 2D oyun mekanikleri ve performans için optimize edilmiş
- **Görsel Efektler**: Gelişmiş parçacık sistemleri ve animasyonlar
- **Kayıt Sistemi**: Oyun ilerlemesi için kalıcı veri yönetimi

### Teknik Detaylar
- **Render**: Post-processing efektleri ile URP tabanlı 2D render
- **Fizik**: Optimize edilmiş çarpışma algılama ile 2D fizik sistemi
- **Animasyon**: DOTween ve Unity'nin animasyon sistemi ile akıcı animasyonlar
- **Performans**: Mobil ve masaüstü platformlar için optimize edilmiş
- **Bellek Yönetimi**: Verimli nesne havuzlama ve kaynak yönetimi
- **Girdi Sistemi**: Platformlar arası girdi yönetimi

### Proje Yapısı
```
Assets/
├── Scripts/
│   ├── Abstracts/      # Soyut sınıflar ve arayüzler
│   ├── Adapters/       # Adaptör deseni uygulamaları
│   ├── Addressables/   # Addressable varlık sistemi
│   ├── Controllers/    # Oyun kontrolcüleri
│   ├── Data/          # Veri yapıları ve modeller
│   ├── Editor/        # Özel editör araçları
│   ├── EventBus/      # Olay sistemi uygulaması
│   ├── Extensions/    # Genişletme metodları
│   ├── Factories/     # Fabrika deseni uygulamaları
│   ├── Helpers/       # Yardımcı sınıflar
│   ├── Interfaces/    # Arayüz tanımlamaları
│   ├── Loggers/       # Loglama sistemi
│   ├── Managers/      # Oyun yöneticileri
│   ├── Miscs/         # Çeşitli yardımcılar
│   ├── Pools/         # Nesne havuzlama sistemi
│   ├── StateMachines/ # Durum makinesi uygulamaları
│   ├── UI/            # Kullanıcı arayüzü bileşenleri
│   └── UnityObjects/  # Unity'ye özel uygulamalar
├── Scenes/            # Oyun sahneleri
├── Prefabs/           # Hazır oyun nesneleri
├── Resources/         # Kaynaklar klasörü
├── Sprites/           # Sprite varlıkları
└── ScriptableObjects/ # Scriptable object varlıkları
```

### Bağımlılıklar
- Unity 2021.3.22 veya üzeri
- Universal Render Pipeline (URP)
- TextMeshPro
- UniTask
- DOTween
- Addressables

### Geliştirme Kuralları
- SOLID prensiplerini takip edin
- Mevcut mimari desenleri kullanın
- Temiz, dokümante edilmiş kod yazın
- Bileşenler arası iletişim için olay sistemini kullanın
- Uygun hata yönetimi ve loglama uygulayın
- Tutarlı kodlama stilini koruyun
- Tüm public API'leri dokümante edin
- Kritik bileşenler için unit test yazın
- Performans ve bellek kullanımı için optimize edin

### Eşleştirme Mantığı
Oyun, temel eşleştirme mekaniklerini yönetmek için gelişmiş bir durum makinesi sistemi kullanır:

#### Durum Makinesi Akışı
1. **Girdi Durumu (Input State)**
   - Oyuncu girdisini ve karo seçimini yönetir
   - Karo seçim zincirinin oluşturulmasını sağlar
   - Karo bağlantılarını doğrular (sadece komşular)
   - Geçerli bir eşleşme için minimum 3 karo gerektirir
   - Geçerli bir zincir oluşturulduğunda Karar Durumuna geçiş yapar

2. **Karar Durumu (Decision State)**
   - Seçilen karo zincirini işler
   - Eşleşen karoları aktifleştirir
   - Hamle sayısını takip eder
   - İşlem sonrası Doldurma Durumuna geçiş yapar

3. **Doldurma Durumu (Refill State)**
   - Karo doldurma mekaniklerini yönetir
   - Üstten karo düşürme işlemlerini kontrol eder
   - Boş alanlar için yeni karolar oluşturur
   - Doldurma sonrası Girdi Durumuna döner

#### Karo Sistemi
- **BaseTileMono**: Temel karo uygulaması
  - Karo bağlantılarını ve komşularını yönetir
  - Karo seçimi/seçim kaldırmayı kontrol eder
  - Karo elemanı oluşturma ve düşürme işlemlerini yönetir
  - Kullanıcı etkileşimi için pointer olaylarını uygular

#### Temel Özellikler
- **Zincir Doğrulama**: Sadece bitişik karoların bağlanabilmesini sağlar
- **Karo Doldurma**: Yerçekimi simülasyonlu otomatik doldurma sistemi
- **Oluşturucu Karolar**: Yeni elemanlar üreten özel karolar
- **Eleman Tipleri**: Benzersiz özelliklere sahip farklı karo elemanları
- **Hamle Takibi**: Oyuncu hamlelerinin sayısını tutar

### Editör Araçları
Proje, geliştirme ve test için özel editör araçları içerir:

#### Linking Game Editörü
- **Seviye Yönetimi**
  - Mevcut seviye ilerlemesini sıfırlama
  - Belirli seviye indeksini ayarlama
  - Tüm kayıtlı verileri temizleme
- **Para Birimi Yönetimi**
  - Jeton miktarını artırma
  - Jeton miktarını azaltma
  - Jeton bakiyesini sıfırlama
- **Geliştirme Özellikleri**
  - Unity'nin Araçlar menüsünden hızlı erişim
  - Gerçek zamanlı veri değişikliği
  - Kalıcı veri yönetimi 
