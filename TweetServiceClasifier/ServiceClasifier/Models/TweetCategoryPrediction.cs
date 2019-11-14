using Microsoft.ML.Data;

namespace ServiceClasifier.Models
{
    public class TweetCategoryPrediction
    {
        [ColumnName("PredictedCategory")]
        public string Category;
    }
}
