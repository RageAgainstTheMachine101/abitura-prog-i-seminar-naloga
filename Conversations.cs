namespace MentalApp
{
    internal class Conversation
    {
        private static int _nextId = 1;
        internal int Id { get; init; }
        public DateTime CreatedDate { get; init; }
        public int UserId { get; init; }
        List<string> History { get; set; }
        

        public Conversation(int userId)
        {
            Id = _nextId++;
            CreatedDate = DateTime.Now;
            UserId = userId;
            History = new List<string>();
        }

        public void AddMessage(string message)
        {
            History.Add(message);
        }

        public IReadOnlyList<string> GetHistory() => History.AsReadOnly();
    }

    internal class ConversationCollection
    {
        private readonly System.Collections.Concurrent.ConcurrentDictionary<int, List<Conversation>> _userConversations;

        public ConversationCollection()
        {
            _userConversations = new System.Collections.Concurrent.ConcurrentDictionary<int, List<Conversation>>();
        }

        public Conversation CreateConversation(int userId)
        {
            var conversation = new Conversation(userId);
            _userConversations.AddOrUpdate(
                userId,
                new List<Conversation> { conversation },
                (_, conversations) =>
                {
                    conversations.Add(conversation);
                    return conversations;
                });
            return conversation;
        }

        public IReadOnlyList<Conversation> GetUserConversations(int userId)
        {
            return _userConversations.TryGetValue(userId, out var conversations)
                ? conversations.AsReadOnly()
                : new List<Conversation>().AsReadOnly();
        }

        public Conversation? GetConversation(int userId, int conversationId)
        {
            return _userConversations.TryGetValue(userId, out var conversations)
                ? conversations.FirstOrDefault(c => c.Id == conversationId)
                : null;
        }
    }
}
