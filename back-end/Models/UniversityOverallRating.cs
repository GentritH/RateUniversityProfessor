﻿namespace RateForProfessor.Models
{
    public class UniversityOverallRating
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public double OverallRating { get; set; }
        public int TotalRatings { get; set; }
    }
}
