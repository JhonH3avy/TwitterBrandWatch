using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Tweet
    {
        public long TweetId { get; set; }

        [NotMapped]
        public ulong UnsignedTweetId
        {
            get
            {
                unchecked
                {
                    return (ulong)TweetId;
                }
            }

            set
            {
                unchecked
                {
                    TweetId = (long)value;
                }
            }
        }

        public string Text { get; set; }

        public ServiceCategory Category { get; set; }

        public float SentimentPolarization { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
