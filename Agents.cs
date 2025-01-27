namespace MentalApp
{
    class BasicAgent
    {
        private static int _nextId = 1;
        internal int Id { get; init; }
        public string Name { get; init; }
        public string Prompt { get; init; }
        private static readonly string[] _greetings = { "Zdravo!", "Dober dan!!", "Oj!" };

        public BasicAgent(string name, string prompt)
        {
            Id = _nextId++;
            Name = name;
            Prompt = prompt;
        }

        public string Reply()
        {
            Random random = new();
            return _greetings[random.Next(_greetings.Length)];
        }
    }

    class CompanionAgent : BasicAgent
    {
        private static readonly string[] _companionPhrases = { "I see... And what?", "How can I help you?" };

        public CompanionAgent(string name, string prompt) : base(name, prompt) { }

        public string PushPhrase()
        {
            Random random = new();
            return _companionPhrases[random.Next(_companionPhrases.Length)];
        }
    }

    class AnalystAgent : BasicAgent
    {
        private static readonly string[] _insightContents = { "You are a very honest person.", "You are so responsible." };

        public AnalystAgent(string name, string prompt) : base(name, prompt) { }

        public Insight CreateInsight(int userId, int conversationId)
        {
            Random random = new();
            string content = _insightContents[random.Next(_insightContents.Length)];
            return new Insight(userId, conversationId, content);
        }
    }
}
