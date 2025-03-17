## Project Overview

SimpleMessenger is a cross-platform mobile messaging application built with Xamarin.Forms, targeting .NET Standard 2.0. The app provides core messaging functionality with local data storage using SQLite.

## Architecture

The application follows a simplified architecture with the following components:

- **UI Layer**: Xamarin.Forms XAML-based user interface
- **Business Logic**: Code-behind files containing application logic
- **Data Access**: `DatabaseService` class for database operations
- **Data Models**: `Contact` and `Message` classes

## Core Components

### Data Models

#### Contact

```csharp
public class Contact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; }
}

```

#### Message

```csharp
public class Message
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}

```

### Database Service

The `DatabaseService` class provides data access methods using SQLite-net-pcl:

- **Contact Management**: Add, edit, delete, and retrieve contacts
- **Message Management**: Store and retrieve chat history, add/delete/update messages
- **Database Administration**: Initialize tables, clear chat history

### Key Features

- **Contact Management**: Add, edit, and delete contacts
- **Messaging**: Send and receive messages with persistent storage
- **Chat History**: View and manage conversation history for each contact
- **Cross-Platform**: Android support with Xamarin.Forms

### Pages

1. **MainPage**: Displays the list of contacts with options to add, edit, or delete
2. **AddEditContactPage**: Form for creating or editing contacts
3. **ChatPage**: Message conversation interface for a specific contact

## Technical Highlights

- Asynchronous database operations with `SQLiteAsyncConnection`
- Event-driven UI updates (using page events like `Disappearing`)
- Debug utilities for testing and troubleshooting
- LINQ queries for data filtering
- Consistent error handling for database operations
