# Real Time ChatApp

## 📝 Description
**RealTimeChatApp** is a real-time chat application built using **ASP.NET Core**, **SignalR**, **Redis**, and **RabbitMQ**, following **Clean Architecture** principles.

The project demonstrates how to build a scalable and maintainable chat system using:
- Real-time communication
- In-memory caching
- Message queue management
- Decoupled logging architecture

---

## 🚀 Technologies Used

| Technology         | Purpose                                                                 |
|--------------------|-------------------------------------------------------------------------|
| ASP.NET Core 9     | Backend Web API and SignalR hub                                         |
| SignalR            | Real-time messaging between connected clients                           |
| Redis (Docker)     | Used for in-memory storage of active users and last 50 messages         |
| RabbitMQ (Docker)  | Used for logging purposes: asynchronously tracking user connections     |
| Clean Architecture | Ensures modular, testable, and maintainable structure                   |
| Bootstrap 5        | Used for responsive frontend layout                                     |
| jQuery             | DOM interaction and SignalR event binding                               |

---

## 💡 Features

- ✅ **Real-Time Messaging** with SignalR  
- ✅ **User Connection Tracking** using Redis  
- ✅ **Last 50 Messages** per user cached with Redis  
- ✅ **Connection Logs** sent via RabbitMQ  
- ✅ **Docker-based Services** (Redis & RabbitMQ)  
- ✅ **Clean Architecture Structure**

---

## 🏗️ Project Structure

### Layers:
1. **Domain**: Business logic, entities, and core interfaces  
2. **Application**: Use cases, SignalR contracts, message DTOs  
3. **Infrastructure**: Implementations for Redis, RabbitMQ, and SignalR messaging  
4. **WebApi**: API controllers and SignalR hub endpoint

---

## ⚙️ Requirements

- .NET 9 SDK
- Docker
  - Redis
  - RabbitMQ (with management interface)

---

## 📈 Logging with RabbitMQ
### User connection events (login, disconnect) are published to a RabbitMQ queue.
### A separate consumer service (e.g. a BackgroundService or Worker) can consume these logs for:

- Console/File logging

- Database storage

- Analytics

## This architecture allows non-blocking, decoupled logging.

## 🧰 Setup Instructions

### 1. Clone the Project
```bash
git clone https://github.com/Metehanglsr/Real-Time-Chat-App.git
cd Real-Time-Chat-App
```
### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Start RabbitMQ and Redis
```bash
docker run -d --hostname rabbitmq-host --name chat-rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
docker run -d --name chat-redis -p 6379:6379 redis
```
### 4. Run The Application

### 5. Access The Frontend
You can open the index.html in the browser directly using Live Server or serve it with a static file server.

### 6. Send Messages

### Future Enhancements:
- **User Authentication**: Add JWT authentication or another method for user verification.
- **Chat Rooms**: Allow users to join different chat rooms.
- **Database Integration**: Store chat data in a database for permanent records.
