using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using ServiceClasifier.Models;

namespace ServiceClasifier
{
    class Program
    {
        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string _trainDataPath => Path.Combine(_appPath, "..", "..", "..", "Data", "issues_train.tsv");
        private static string _testDataPath => Path.Combine(_appPath, "..", "..", "..", "Data", "issues_test.tsv");
        private static string _modelPath => Path.Combine(_appPath, "..", "..", "..", "Models", "model.zip");

        private static MLContext _mlContext;
        private static PredictionEngine<Tweets, TweetCategoryPrediction> _predEngine;
        private static ITransformer _trainedModel;
        static IDataView _trainingDataView;

        static void Main(string[] args)
        {
            _mlContext = new MLContext(seed: 0);
            using (var contex = new TweetDataWarehouseContext())
            {
                _trainingDataView = _mlContext.Data.LoadFromEnumerable(contex.Tweets.ToList());
            }
            var pipeline = ProcessData();
            var trainingPipeline = BuildAndTrainModel(_trainingDataView, pipeline);
            Evaluate(_trainingDataView.Schema);
            SaveModelAsFile(_mlContext, _trainingDataView.Schema, _trainedModel);
            PredictIssue();
        }

        public static IEstimator<ITransformer> ProcessData()
        {
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Category", outputColumnName: "Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Text", outputColumnName: "TextFeaturized"))
                .Append(_mlContext.Transforms.Concatenate("Features", "TextFeaturized"))
                .AppendCacheCheckpoint(_mlContext);
            return pipeline;
        }

        public static IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedCategory"));
            _trainedModel = trainingPipeline.Fit(trainingDataView);
            _predEngine = _mlContext.Model.CreatePredictionEngine<Tweets, TweetCategoryPrediction>(_trainedModel);
            var tweet = new Tweets
            {
                Text = "Avianca es una aerolinea muy buena"
            };
            var prediction = _predEngine.Predict(tweet);
            Console.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.Category} ===============");
            return trainingPipeline;
        }

        public static void Evaluate(DataViewSchema trainingDataViewSchema)
        {
            //var testDataView = _mlContext.Data.LoadFromTextFile<GitHubIssue>(_testDataPath,hasHeader: true);
            //var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));
            //Console.WriteLine($"*************************************************************************************************************");
            //Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
            //Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            //Console.WriteLine($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
            //Console.WriteLine($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
            //Console.WriteLine($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
            //Console.WriteLine($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
            //Console.WriteLine($"*************************************************************************************************************");
        }

        private static void SaveModelAsFile(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            //mlContext.Model.Save(model, trainingDataViewSchema, _modelPath);
        }

        private static void PredictIssue()
        {
            ITransformer loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            var singleTweet = new Tweets() { Text = "Un avion de avianca se estrello" };
            _predEngine = _mlContext.Model.CreatePredictionEngine<Tweets, TweetCategoryPrediction>(loadedModel);
            var prediction = _predEngine.Predict(singleTweet);
            Console.WriteLine($"=============== Single Prediction - Result: {prediction.Category} ===============");
        }
    }
}
