using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TweeterSearchApp.Models;

namespace TweeterSearchApp
{
    public class SearchContext
    {
        public static async Task<IList<Tweet>> DoSearchAsync(TwitterContext twitterCtx, ulong lastId)
        {
            string searchTerm = "avianca";

            var searchResponse =
                await
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                     search.Query == searchTerm &&
                     search.IncludeEntities == true &&
                     search.TweetMode == TweetMode.Extended &&
                     search.Count == 100 &&
                     search.SinceID == lastId
                 select search)
                 .FirstOrDefaultAsync();
            var tweetList = new List<Tweet>();
            if (searchResponse?.Statuses != null)
                searchResponse.Statuses.ForEach(tweet =>
                {
                    tweetList.Add(new Tweet
                    {
                        UnsignedTweetId = tweet.StatusID,
                        Text = tweet.FullText,
                        CreatedAt = tweet.CreatedAt
                    });
                });
            return tweetList;
        }
    }
}
