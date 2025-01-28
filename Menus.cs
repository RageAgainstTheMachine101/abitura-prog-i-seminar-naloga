namespace MentalApp
{
    public class Menu
    {
        const string welcomeMessage = "\nPress any key to Feel Better";
        const string exit = "\n0 - Exit";
        const string loginMenu = "\nInsert choice number:\n1 - Sign Up" + exit;
        const string startConversation = "\nInsert choice number:\n1 - Start Conversation";
        const string viewConversations = "\n2 - View Previous Conversations";
        const string getInsight = "\n3 - Get Insight";
        const string mainMenu = startConversation + viewConversations + exit;
        const string mainMenuExpanded = startConversation + viewConversations + getInsight + exit;
        const string goodByeMessage = "\nThank you for using MentalApp. Goodbye!\n";

        public static void Run()
        {
            Console.WriteLine(welcomeMessage);
            Console.ReadKey();

            User? user = null;
            Insight? insight = null;
            ConversationCollection conversations = new();
            InsightCollection insights = new();
            var analyst = new AnalystAgent("Analyst", "Analyze conversations and provide insights.");
            
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
                        Console.WriteLine($"\nWelcome! Your user ID is: {user.Id}");
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
                        case 0: // Exit program
                            Console.WriteLine(goodByeMessage);
                            return;
                        case 1: // Start new conversation
                            var conversation = conversations.CreateConversation(user.Id);
                            Insight? insightVar = null;
                            HandleConversation(conversation, analyst, insights, ref insightVar);
                            insight = insightVar;
                            break;
                        case 2: // View previous conversations
                            ShowConversationHistory(conversations, user.Id);
                            break;
                        case 3: // Get insight (only if available)
                            if (insight != null)
                            {
                                Console.WriteLine("\nYour insights:");
                                var userInsights = insights.GetUserInsights(user.Id);
                                foreach (var ins in userInsights)
                                {
                                    Console.WriteLine($"Insight from conversation #{ins.ConversationId}:");
                                    Console.WriteLine($"  {ins.Content}");
                                    Console.WriteLine($"  Created on: {ins.CreatedDate}\n");
                                }
                                Console.WriteLine("Press any key to return to main menu...");
                                Console.ReadKey();
                            }
                            break;
                    }
                }
            }
            
            Console.WriteLine(goodByeMessage);
        }

        private static void HandleConversation(Conversation conversation, AnalystAgent analyst, InsightCollection insights, ref Insight? insight)
        {
            Console.WriteLine($"\nConversation #{conversation.Id} started. Type 'exit' to end the conversation.");
            var companion = new CompanionAgent("Assistant", "Be a helpful assistant.");
            
            while (true)
            {
                Console.Write("You: ");
                string? input = Console.ReadLine();
                
                if (input?.ToLower() == "exit")
                    break;
                    
                conversation.AddMessage($"User: {input}");
                string aiResponse = companion.PushPhrase();
                Console.WriteLine($"Assistant: {aiResponse}");
                conversation.AddMessage($"Assistant: {aiResponse}");
            }

            // Create insight after conversation ends using the analyst agent
            var insightContent = analyst.CreateInsight(conversation.UserId, conversation.Id);
            insight = insights.CreateInsight(conversation.UserId, conversation.Id, insightContent.Content);
            Console.WriteLine($"\nAnalyst created an insight from your conversation: {insight.Content}");
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
