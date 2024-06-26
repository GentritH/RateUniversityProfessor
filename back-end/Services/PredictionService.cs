using RateForProfessor.Extensions;
using RateForProfessor.ML.DataModels;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly PredictionEnginePool<SampleObservation, SamplePrediction> _predictionEnginePool;

        public PredictionService(PredictionEnginePool<SampleObservation, SamplePrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        public async Task<PredictionResult> PredictToxicityAsync(string feedback)
        {
            var sampleData = new SampleObservation { SentimentText = feedback };

            var prediction = await Task.Run(() => _predictionEnginePool.Predict(sampleData));

            var isToxic = prediction.IsToxic;
            var probability = CalculateMethods.CalculatePercentage(prediction.Score);
            var message = $"Prediction: Is Toxic?: '{isToxic}' with {probability}% probability of toxicity for the text '{feedback}'";

            return new PredictionResult
            {
                IsToxic = isToxic,
                Message = message
            };
        }
    }
}
