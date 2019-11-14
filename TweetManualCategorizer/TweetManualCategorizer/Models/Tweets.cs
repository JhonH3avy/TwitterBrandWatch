using System;
using System.Collections.Generic;

namespace TweetManualCategorizer.Models
{
    public partial class Tweets
    {
        public string Text { get; set; }
        public int Category { get; set; }
        public float SentimentPolarization { get; set; }
        public long TweetId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
