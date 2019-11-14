using System;
using System.IO;
using System.Linq;
using TweetManualCategorizer.Models;

namespace TweetManualCategorizer
{
    class Program
    {
        static async void Main(string[] args)
        {
            var checkpointData = await File.ReadAllTextAsync(@"LastCheckpoint.txt");
            var lastCheckpoint = string.IsNullOrEmpty(checkpointData) ? DateTime.Now.AddDays(-7) : DateTime.Parse(checkpointData);
            using (var context = new TweetDataWarehouseContext())
            {
                var uncategorizedTweets = context.Tweets.Where(tweet => tweet.CreatedAt > lastCheckpoint);
                if (uncategorizedTweets.Count() <= 0)
                {
                    return;
                }
                foreach (var tweet in uncategorizedTweets)
                {
                    do
                    {
                        Console.WriteLine($"");
                        Console.WriteLine($"\t0 - UserExperience");
                        Console.WriteLine($"\t1 - UserAttention");
                        Console.WriteLine($"\t2 - General");
                        Console.WriteLine($"\t3 - Tickets");
                        Console.WriteLine("Press the category this tweet fit on");
                        var option = Console.ReadKey(true); 
                    } while ();
                }
            }
        }
    }
}
