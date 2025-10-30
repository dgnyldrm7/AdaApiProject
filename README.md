# AdaYazilim – Train Reservation Minimal API

## 🧩 Overview
Bu proje, **.NET 9 Minimal API** kullanılarak geliştirilmiş bir **tren rezervasyon sistemidir**.  
**Amaç:** Her vagonun **%70 doluluk kuralına** uyan, **dinamik koltuk yerleşimi** hesaplaması yapmaktır.  
Servis, **REST tabanlı** olarak istek alır ve **JSON formatında** yanıt döner.

---

## ⚙️ Technologies Used

| Katman | Teknoloji |
|--------|------------|
| **Backend Framework** | .NET 9 Minimal API |
| **Language** | C# 12 |
| **Architecture** | Clean Architecture + Layered Structure |
| **Dependency Injection** | Built-in .NET DI |
| **Serialization** | System.Text.Json (Source Generator destekli) |
| **Containerization** | Docker |
| **Deployment** | Render Cloud Service |
| **Build Tool** | Dockerfile (multi-stage) |
| **Logging / Middleware** | Custom Global Exception Middleware |

---

## 🏗️ Project Structure
<img width="282" height="377" alt="image" src="https://github.com/user-attachments/assets/eb43baab-91f7-4f50-90f7-d48915fc400c" />

---

## Örnek Postman üzerinden istek senaryosu
<img width="1401" height="802" alt="image" src="https://github.com/user-attachments/assets/39efe499-4f92-43a0-a045-c3d3d54c206b" />
