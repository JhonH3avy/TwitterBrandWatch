using System;
using System.IO;
using System.Linq;
using Twitter.Models;

namespace TweetManualCategorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TweetsDbContext())
            {
                var uncategorizedTweets = context.Tweets.Where(tweet => tweet.Category == ServiceCategory.None).ToList();
                if (uncategorizedTweets.Count <= 0)
                {
                    return;
                }
                foreach (var tweet in uncategorizedTweets)
                {
                    var category = ServiceCategory.None;
                    do
                    {
                        Console.WriteLine($"Tweet: {tweet.Text}");
                        Console.WriteLine($"\t0 - UserExperience");
                        Console.WriteLine($"\t1 - UserAttention");
                        Console.WriteLine($"\t2 - General");
                        Console.WriteLine($"\t3 - Tickets");
                        Console.WriteLine($"\tQ - To quit application");
                        Console.WriteLine("Press the category this tweet fit on");
                        var option = Console.ReadKey(true);
                        if (option.KeyChar == 'Q' || option.KeyChar == 'q')
                        {
                            break;
                        }
                        category = GetCategory(option);
                    } while (category == ServiceCategory.None);
                    if (category == ServiceCategory.None)
                    {
                        break;
                    }
                    tweet.Category = category;
                }
                context.SaveChanges();
            }
        }

        private static ServiceCategory GetCategory(ConsoleKeyInfo option)
        {
            switch (option.KeyChar)
            {
                case '0': return ServiceCategory.UserExperience;
                case '1': return ServiceCategory.UserAttention;
                case '2': return ServiceCategory.General;
                case '3': return ServiceCategory.Tickets;
                default: return ServiceCategory.None;
            }
        }
    }
}
