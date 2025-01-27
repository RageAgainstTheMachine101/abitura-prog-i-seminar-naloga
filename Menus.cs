namespace MentalApp
{
    public class Menu
    {
        const string welcomeMessage = "Press any key to Feel Better";
        const string exit = "\n0 - Exit";
        const string loginMenu = "\nInsert choice number:\n1 - Sign Up" + exit;
        const string startConversation = "\nInsert choice number:\n1 - Start Conversation";
        const string viewConversations = "\n2 - View Previous Conversations";
        const string getInsight = "\n3 - Get Insight";
        const string mainMenu = startConversation + viewConversations + exit;
        const string mainMenuExpanded = startConversation + viewConversations + getInsight + exit;

        public static void Run()
        {
            Console.WriteLine(welcomeMessage);
            Console.ReadKey();

            User user = null;
            Insight insight = null;
            ConversationCollection conversations = new();
            
            while (true)
            {
                if (user == null)
                {
                    Console.WriteLine(loginMenu);
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        continue;
                    }
                    
                    if (choice == 0)
                        break;
                    
                    if (choice == 1)
                    {
                        user = new();
                        Console.WriteLine($"Welcome! Your user ID is: {user.Id}");
                    }
                }
                else
                {
                    if (insight == null)
                    {
                        Console.WriteLine(mainMenu);
                    }
                    else
                    {
                        Console.WriteLine(mainMenuExpanded);
                    }
                    
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1: // Start new conversation
                            var conversation = conversations.CreateConversation(user.Id);
                            HandleConversation(conversation);
                            break;
                        case 2: // View previous conversations
                            ShowConversationHistory(conversations, user.Id);
                            break;
                        case 3: // Get insight (only if available)
                            if (insight != null)
                            {
                                // Handle insight logic
                            }
                            break;
                    }
                }
            }
            
            Console.WriteLine("Thank you for using MentalApp. Goodbye!");
        }

        private static void HandleConversation(Conversation conversation)
        {
            Console.WriteLine($"\nConversation #{conversation.Id} started. Type 'exit' to end the conversation.");
            var companion = new CompanionAgent("Assistant", "I am your companion");
            
            while (true)
            {
                Console.Write("You: ");
                string input = Console.ReadLine();
                
                if (input?.ToLower() == "exit")
                    break;
                    
                conversation.AddMessage($"User: {input}");
                string aiResponse = companion.PushPhrase();
                Console.WriteLine($"Assistant: {aiResponse}");
                conversation.AddMessage($"Assistant: {aiResponse}");
            }
        }

        private static void ShowConversationHistory(ConversationCollection conversations, int userId)
        {
            var userConversations = conversations.GetUserConversations(userId);
            
            if (userConversations.Count == 0)
            {
                Console.WriteLine("\nNo previous conversations found.");
                return;
            }

            Console.WriteLine("\nYour conversations:");
            foreach (var conv in userConversations)
            {
                Console.WriteLine($"Conversation #{conv.Id} - Created on {conv.CreatedDate}");
                foreach (var message in conv.GetHistory())
                {
                    Console.WriteLine($"  {message}");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
