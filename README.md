# **🌳 Portmarnock Garden Centre App 🌳**

Welcome to the Portmarnock Garden Centre App! This application provides a seamless shopping experience for garden enthusiasts. Users can browse products, manage their shopping cart, and complete purchases with ease.

---

### **💻 Project Overview**
The GardenCentreApp is a cross-platform mobile application built with .NET MAUI, targeting platforms such as iOS, Android, and Mac Catalyst. It allows users to:
- Browse products by category (plants, tools, garden care items).
- Add items to their shopping cart.
- Manage their cart and proceed to checkout.
- Handle corporate orders with monthly summaries.

---

### **✨ Features**
- **User Authentication:** Sign up and log in functionality for customers.
- **Product Browsing:** View categorised products such as plants, tools, and garden care items.
- **Shopping Cart Management:** Add, update, or remove items from the cart.
- **Checkout Process:** Complete purchases with order summaries.
- **Corporate Orders:** Special features for corporate users, including monthly billing summaries.
- **Cross-Platform Support:** Runs on iOS, Android, and Mac Catalyst.

---

### **Screenshots**
Login Page
Login Page
Login page for user authentication.

---

### **📋 Tech Stack**
The project is built using the following technologies:
- **Framework:** .NET MAUI
- **Language:** C#
- **Database:** SQLite (with Write-Ahead Logging disabled for better compatibility)
- **UI Design:** XAML

---

## 📋 Prerequisites

![.NET](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-3.39%2B-lightblue?logo=sqlite&logoColor=white)
![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-Required-purple?logo=microsoft&logoColor=white)
![Mac Catalyst](https://img.shields.io/badge/Mac%20Catalyst-Supported-green?logo=apple&logoColor=white)
![Android](https://img.shields.io/badge/Android-Supported-brightgreen?logo=android&logoColor=white)
![iOS](https://img.shields.io/badge/iOS-Supported-lightgrey?logo=apple&logoColor=white)

### Requirements:
1. **.NET 8.0 SDK**: Ensure you have the latest .NET SDK installed.
2. **SQLite 3.39+**: Used for database management.
3. **.NET MAUI**: Required for building and running the application.
4. **Mac Catalyst**: Fully supported for desktop use on macOS.
5. **Android**: Supported for mobile devices running Android.
6. **iOS**: Supported for mobile devices running iOS.

---

### **📦 Installation**
1. **Clone this repository:**
   ```bash
   git clone https://github.com/cwmcfeely/GardenCentreApp.git
   cd GardenCentreApp
  
2. **Install Prerequisites:**
Ensure you have the following installed:
- **.NET 8.0 SDK:** Download and install from here.
- **SQLite 3.39+:** SQLite is bundled with .NET MAUI, but ensure your system has at least version 3.39 or higher.
- **.NET MAUI Workload:** Install .NET MAUI using the following command:
   ```bash
   dotnet workload install maui
Verify that .NET MAUI is installed by running:
  ```bash
  dotnet --list-sdks
  ```
4. **Build and Run the App**
Choose your target platform and run the following commands:
Mac Catalyst
  ```bash
  dotnet build -t:Run -f net8.0-maccatalyst
  ```
Android
  ```bash
  dotnet build -t:Run -f net8.0-android
  ```
iOS
  ```bash
  dotnet build -t:Run -f net8.0-ios
