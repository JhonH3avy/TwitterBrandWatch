using System;
using MyMLAppML.Model.DataModels;
using Microsoft.ML;

namespace myMLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeModel();
        }

        public static void ConsumeModel()
        {
            // Load the model
            var mlContext = new MLContext();

            var mlModel = mlContext.Model.Load("MLModel.zip", out var modelInputSchema);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Use the code below to add input data
            var input = new ModelInput {SentimentText = "Nah, i don't like this thing"};

            // Try model on sample data
            // True is toxic, false is non-toxic
            var result = predictionEngine.Predict(input);

            Console.WriteLine($"Text: {input.SentimentText} | Prediction: {(Convert.ToBoolean(result.Prediction) ? "Toxic" : "Non Toxic")} sentiment");
        }
    }
}