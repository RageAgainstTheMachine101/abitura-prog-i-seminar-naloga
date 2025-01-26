namespace MentalApp
{
    class BasicAgent
    {
        private static int _nextId = 1;
        internal int Id { get; init; }
        public string Name { get; init; }
        public string Prompt { get; init; }
        private static readonly string[] _greetings = { "Zdravo!", "Dober dan!!", "Oj!" };

        BasicAgent(string name, string prompt)
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
}
