# RSA Konsolenanwendung in C#

Dieses Projekt implementiert den RSA-Verschlüsselungsalgorithmus in einer Konsolenanwendung mit C#. Es bietet Funktionen zur Verschlüsselung und Entschlüsselung von Daten mittels RSA.

## Projektstruktur

- **RSA/RSA.csproj**: Projektdatei, die die Konfiguration und Abhängigkeiten des Projekts definiert.

## Voraussetzungen

- .NET 9.0 SDK oder höher

## Installation

1. Klone das Repository:
   ```bash
   git clone https://github.com/dein-benutzername/csharp-rsa.git
   ```
2. Navigiere in das Projektverzeichnis:
   ```bash
   cd csharp-rsa
   ```
3. Baue das Projekt:
   ```bash
   dotnet build
   ```

## Nutzung

1. Führe die Anwendung aus:
   ```bash
   dotnet run --project RSA
   ```

## Funktionen

### Verschlüsselung

Die Methode `Encrypt` verschlüsselt die angegebenen Daten mit dem öffentlichen Schlüssel (e, n).

```csharp
public static byte[] Encrypt(byte[] data, BigInteger e, BigInteger n)
```

### Entschlüsselung

Die Methode `Decrypt` entschlüsselt die angegebenen Daten mit dem privaten Schlüssel (d, n).

```csharp
public static byte[] Decrypt(byte[] data, BigInteger d, BigInteger n)
```
