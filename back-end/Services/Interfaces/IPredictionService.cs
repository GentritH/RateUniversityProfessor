using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface IPredictionService
    {
        Task<PredictionResult> PredictToxicityAsync(string feedback);
    }
}
