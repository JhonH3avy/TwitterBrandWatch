using LinqToTwitter;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TweeterSearchApp
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                await ExtractTweetsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static async Task ExtractTweetsAsync()
        {
            IAuthorizer auth = DoSingleUserAuth();

            await auth.AuthorizeAsync();

            var twitterCtx = new TwitterContext(auth);
            ulong lastTweetId = 0;
            var tweetsAmount = 0;
            while (tweetsAmount < 3200)
            {
                using (var context = new TweetsDbContext())
                {
                    lastTweetId = await context.Tweets.CountAsync() > 0 ? (ulong) await context.Tweets.MaxAsync(tweet => tweet.TweetId) : 0;
                    var tweets = await SearchContext.DoSearchAsync(twitterCtx, lastTweetId);
                    if (tweets.Count <= 0)
                    {
                        break;
                    }
                    context.Tweets.AddRange(tweets);
                    await context.Database.OpenConnectionAsync();
                    try
                    {
                        await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Tweets ON");
                        await context.SaveChangesAsync();
                        await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Tweets OFF");
                    }
                    finally
                    {
                        await context.Database.CloseConnectionAsync();
                    }
                    tweetsAmount += tweets.Count;
                }
            }

        }

        static IAuthorizer DoSingleUserAuth()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = appSettings[OAuthKeys.TwitterConsumerKey],
                    ConsumerSecret = appSettings[OAuthKeys.TwitterConsumerSecret],
                    AccessToken = appSettings[OAuthKeys.TwitterAccessToken],
                    AccessTokenSecret = appSettings[OAuthKeys.TwitterAccessTokenSecret]
                }
            };

            return auth;
        }
    }
}
