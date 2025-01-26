namespace MentalApp
{
    internal class Conversation
    {
        private static int _nextId = 1;
        internal int Id { get; init; }
        public DateTime CreatedDate { get; init; }
        List<string> History { get; set; }
        

        public Conversation()
        {
            Id = _nextId++;
            CreatedDate = DateTime.Now;
            History = [];
        }

        public void AddMessage(string message)
        {
            History.Append(message);
        }
    }
}
