# RealTimeChatApp

## Description
RealTimeChatApp is a **real-time chat application** built using modern technologies such as **SignalR**, **Redis**, and **RabbitMQ**. The project follows the principles of **Clean Architecture** and aims to showcase technologies like **asynchronous communication**, **message queue management**, and **real-time data transmission** in a scalable and maintainable way.

### Technologies:
- **SignalR**: Used for real-time communication. It allows instant messaging between users.
- **RabbitMQ**: Used for message queue management. It processes messages asynchronously.
- **Redis**: Used to store active user states and chat history in memory.
- **.NET Core / ASP.NET Core**: The primary technology used for backend development.

## Features
- **Real-time Messaging**: Instant message sending with SignalR.
- **Asynchronous Message Queue Management**: Using RabbitMQ to enqueue and process messages asynchronously.
- **User State Tracking**: Track active users and their states using Redis.
- **Clean Architecture**: The application is built using Clean Architecture principles for a modular and maintainable structure.
- **Minimalistic Design**: A simple, lightweight structure to demonstrate core chat functionality.

## Project Structure

### Layers:
1. **Domain**: Contains all business logic and core components independent of external systems.
2. **Application**: Contains application services and use cases such as message handling through SignalR.
3. **Infrastructure**: Contains external dependencies such as RabbitMQ, Redis, and SignalR.
4. **WebApi**: Contains API controllers and SignalR hubs.

## Requirements
- **.NET 9.0**
- **RabbitMQ**: RabbitMQ must be installed for message queuing.
- **Redis**: Redis must be installed to store user states and chat data.

## Getting Started

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

### 4. Run The Application

### 5. Access The Application

### 6. Send Messages

### Future Enhancements:
- **Message History**: Store message history in Redis or a database for persistent storage.
- **User Authentication**: Add JWT authentication or another method for user verification.
- **Chat Rooms**: Allow users to join different chat rooms.
- **Database Integration**: Store chat data in a database for permanent records.
