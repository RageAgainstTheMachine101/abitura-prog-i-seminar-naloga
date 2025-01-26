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
    2.1 AssistantAgent
    2.2 AnalystAgent
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
