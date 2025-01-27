namespace MentalApp
{
    internal class Insight
    {
        private static int _nextId = 1;
        internal int Id { get; init; }
        public DateTime CreatedDate { get; init; }
        public int UserId { get; init; }
        public int ConversationId { get; init; }
        public string Content { get; set; }

        public Insight(int userId, int conversationId, string content)
        {
            Id = _nextId++;
            CreatedDate = DateTime.Now;
            UserId = userId;
            ConversationId = conversationId;
            Content = content;
        }
    }

    internal class InsightCollection
    {
        private readonly System.Collections.Concurrent.ConcurrentDictionary<int, List<Insight>> _userInsights;

        public InsightCollection()
        {
            _userInsights = new System.Collections.Concurrent.ConcurrentDictionary<int, List<Insight>>();
        }

        public Insight CreateInsight(int userId, int conversationId, string content)
        {
            var insight = new Insight(userId, conversationId, content);
            _userInsights.AddOrUpdate(
                userId,
                new List<Insight> { insight },
                (_, insights) =>
                {
                    insights.Add(insight);
                    return insights;
                });
            return insight;
        }

        public IReadOnlyList<Insight> GetUserInsights(int userId)
        {
            return _userInsights.TryGetValue(userId, out var insights)
                ? insights.AsReadOnly()
                : new List<Insight>().AsReadOnly();
        }

        public Insight? GetInsight(int userId, int insightId)
        {
            return _userInsights.TryGetValue(userId, out var insights)
                ? insights.FirstOrDefault(i => i.Id == insightId)
                : null;
        }
    }
}
