namespace MentalApp
{
    internal class User
    {
        private static int _nextId = 1;
        internal int Id { get; init; }
        public DateTime RegistrationDate { get; init; }
        public String Name { get; init; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int NextLevelExperience { get; set; }

        public User()
        {
            Id = _nextId++;
            RegistrationDate = DateTime.Now;
            Console.WriteLine("What is your name?");
            var input = Console.ReadLine();
            Name = string.IsNullOrWhiteSpace(input) ? "Anonymous" : input;
            Level = 0;
            Experience = 0;
            NextLevelExperience = 30;
        }

        public void AddExperience()
        {
            Experience += 10;
            if (Experience >= NextLevelExperience)
            {
                Experience -= NextLevelExperience;
                NextLevelExperience += 10;
                Level ++;
            }
        }
    }
}
