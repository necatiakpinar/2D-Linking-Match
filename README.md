# Linking Match 2D - Necati Akpınar

[English](#english) | [Türkçe](#türkçe)

## English

### Project Overview
Linking Match 2D is a Unity-based game project that implements a modern, scalable architecture using various design patterns and best practices. The project is structured to be maintainable, extensible, and follows clean code principles.

### Key Features
- **Modern Architecture**: Utilizes SOLID principles and design patterns
- **State Management**: Implements state machines for game flow control
- **Event System**: Custom event bus implementation for decoupled communication
- **Resource Management**: Addressables integration for efficient asset loading
- **UI System**: Modular UI implementation with TextMeshPro integration
- **Object Pooling**: Efficient memory management for game objects
- **Factory Pattern**: For object creation and management
- **Logging System**: Custom logging implementation for debugging

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
- Unity 2022.3 or later
- TextMeshPro
- UniTask
- DOTween
- Addressables

### Getting Started
1. Clone the repository
2. Open the project in Unity 2022.3 or later
3. Import required packages (TextMeshPro, UniTask, DOTween)
4. Open the main scene from Assets/Scenes

### Development Guidelines
- Follow SOLID principles
- Use the existing architecture patterns
- Write clean, documented code
- Use the event system for communication between components
- Implement proper error handling and logging

## Türkçe

### Proje Genel Bakış
Linking Match 2D, çeşitli tasarım desenleri ve en iyi uygulamaları kullanarak modern, ölçeklenebilir bir mimari uygulayan Unity tabanlı bir oyun projesidir. Proje, bakımı kolay, genişletilebilir ve temiz kod prensiplerini takip edecek şekilde yapılandırılmıştır.

### Temel Özellikler
- **Modern Mimari**: SOLID prensipleri ve tasarım desenleri kullanır
- **Durum Yönetimi**: Oyun akışı kontrolü için durum makineleri
- **Olay Sistemi**: Bileşenler arası iletişim için özel olay sistemi
- **Kaynak Yönetimi**: Verimli varlık yükleme için Addressables entegrasyonu
- **UI Sistemi**: TextMeshPro entegrasyonlu modüler UI uygulaması
- **Nesne Havuzlama**: Oyun nesneleri için verimli bellek yönetimi
- **Fabrika Deseni**: Nesne oluşturma ve yönetimi için
- **Loglama Sistemi**: Hata ayıklama için özel loglama uygulaması

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
- Unity 2022.3 veya üzeri
- TextMeshPro
- UniTask
- DOTween
- Addressables

### Başlangıç
1. Depoyu klonlayın
2. Projeyi Unity 2022.3 veya üzeri sürümde açın
3. Gerekli paketleri içe aktarın (TextMeshPro, UniTask, DOTween)
4. Ana sahneyi Assets/Scenes klasöründen açın

### Geliştirme Kuralları
- SOLID prensiplerini takip edin
- Mevcut mimari desenleri kullanın
- Temiz, dokümante edilmiş kod yazın
- Bileşenler arası iletişim için olay sistemini kullanın
- Uygun hata yönetimi ve loglama uygulayın 