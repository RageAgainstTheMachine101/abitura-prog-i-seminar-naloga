# Requirements
- Minimum number of classes is 8 (more if needed).
- Minimum number of elements in a class is 5 (e.g. fields, properties, methods, etc.).
- Minimum number of objects in a solution is 10 (e.g. cars, products, players, etc.).
- Minimum number of loops to go through with objects is 2 (e.g. a foreach loop to output values ​​associated with objects).

# Plan
## Classes
1. User()
    - id
    - registration_date
    - name
    - level
2. BasicAgent()
    - id
    - name
    - prompt
    - .Reply()
    - 2.1 AssistantAgent
    - 2.2 AnalystAgent
3. Conversation()
    - id
    - created_date
    - user_id
    - agent_id
    - history
    - .AddMessage()
4. Insight()
    - id
    - created_date
    - title
    - content
    - is_completed
5. Menu()
    - .Run()
    - .CreateConversation()
    - .CreateInsight()

# Result
The following classes were implemented in the project:

1. `User` - Represents a user of the mental health application
   - Manages user identity, registration date, name, and experience level
   - Tracks user experience points and level progression
   - Handles user creation and experience gain

2. `BasicAgent` - Base class for all AI agents in the system
   - Contains core agent functionality
   - Implements basic reply mechanism
   - Serves as parent class for specialized agents

3. `CompanionAgent` - (fake) Specialized agent for user interaction (extends `BasicAgent`)
   - Provides supportive responses
   - Manages conversation flow
   - Implements phrase generation functionality

4. `AnalystAgent` - (fake) Specialized agent for analysis (extends `BasicAgent`)
   - Creates insights from conversations
   - Provides analytical capabilities
   - Helps users understand their mental state

5. `Conversation` - Manages individual conversations
   - Stores conversation history
   - Handles message addition
   - Tracks conversation metadata (ID, dates, participants)

6. `ConversationCollection` - Manages multiple conversations
   - Creates new conversations
   - Retrieves user conversations
   - Provides conversation lookup functionality

7. `Insight` - Represents fake insights from conversations
   - Stores insight content and metadata
   - Links insights to users and conversations
   - Tracks creation dates and completion status

8. `InsightCollection` - Manages multiple insights
   - Stores insights per user in a thread-safe manner
   - Creates and adds new insights
   - Provides access to user-specific insights
   - Uses concurrent data structures for safe multi-threaded access

9. `Menu` - Handles user interface and program flow
   - Manages application navigation
   - Handles user input
   - Coordinates between different components
   - Provides user, conversation and insight creation interfaces

10. `Program` - Main entry point of the application
   - Initializes the application
   - Starts the main menu loop

# What could be improved
One notable improvement opportunity in the current implementation lies in the similarity between `InsightCollection` and `ConversationCollection` classes. Both classes:
- Manage collections of items (insights and conversations respectively)
- Use `ConcurrentDictionary` for thread-safe storage
- Map user IDs to lists of items
- Implement similar patterns for creating and retrieving items

This parallel structure suggests an opportunity for abstraction through the creation of a generic base class, for example:

```csharp
internal abstract class UserCollection<T>
{
    protected readonly ConcurrentDictionary<int, List<T>> _items;

    protected UserCollection()
    {
        _items = new ConcurrentDictionary<int, List<T>>();
    }

    protected List<T> GetUserItems(int userId)
    {
        return _items.GetOrAdd(userId, _ => new List<T>());
    }

    public IReadOnlyList<T> GetAllForUser(int userId)
    {
        return GetUserItems(userId).AsReadOnly();
    }
}
```

This abstraction would:
1. Reduce code duplication
2. Enforce consistent behavior across different types of collections
3. Make it easier to add new user-specific collections in the future
4. Provide a single place to implement common collection operations
5. Simplify testing by allowing mock implementations of the base class

Both `InsightCollection` and `ConversationCollection` could then inherit from this base class, focusing only on their specific functionality while sharing common collection management code.
