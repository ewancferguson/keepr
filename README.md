# 📌 Keepr

Keepr is a **Pinterest-inspired** web application that allows users to save, organize, and share images and ideas. Built with a **.NET backend** and **MySQL database**, Keepr provides an intuitive and visually appealing way to manage collections of content.

## 🚀 Features
- 📷 **Save & Organize Images** – Users can create boards and pin images.
- 👥 **User Authentication** – Secure login and profile management.
- 🔎 **Explore Content** – Browse and discover images from other users.
- 🛠 **Full CRUD Functionality** – Create, Read, Update, and Delete operations.
- 🔒 **Private & Public Boards** – Control the visibility of saved content.

## 🏗️ Tech Stack
- **Backend:** .NET Core, MySQL
- **Frontend:** Vue.js (Planned Future Enhancement)
- **Authentication:** JWT-based authentication
- **ORM:** Entity Framework Core
- **Database:** MySQL

## 🔧 Setup & Installation
1. **Clone the repository**
   ```sh
   git clone https://github.com/ewancferguson/keepr.git
   cd keepr
   ```
2. **Set up the database**
   - Ensure MySQL is installed and running.
   - Create a new database named `keepr_db`.
   - Run migrations using Entity Framework:
     ```sh
     dotnet ef database update
     ```
3. **Configure environment variables**
   - Create an `appsettings.json` file and add your database connection string.

4. **Run the application**
   ```sh
   dotnet run
   ```

## 📜 API Endpoints
| Method | Endpoint            | Description                |
|--------|--------------------|----------------------------|
| GET    | `/api/keeps`       | Fetch all keeps           |
| POST   | `/api/keeps`       | Create a new keep         |
| GET    | `/api/keeps/{id}`  | Get a specific keep       |
| DELETE | `/api/keeps/{id}`  | Delete a keep             |
| GET    | `/api/vaults`      | Fetch user vaults         |

## 📫 Contact
- **GitHub:** [Ewan Ferguson](https://github.com/ewancferguson)
- **Portfolio:** [My Work](https://ewanferguson.org/)
- **Email:** ewanferg01@gmail.com

---
🔹 *Contributions and feedback are welcome! Feel free to fork and improve the project.*
