namespace RateForProfessor.Extensions
{
    public class CalculateMethods
    {
        public static float CalculatePercentage(double value)
        {
            return 100 * (1.0f / (1.0f + (float)Math.Exp(-value)));
        }
    }
}
