using ep_models;
using System.Collections.Generic;


namespace ep_models
{
    public class AvailableScoresModel
    {

        public AvailableScoresModel()
        {

        }

        /// <summary>
        /// List of Prediction Scores
        /// </summary>
        public List<Score> Scores { get; set; } = new List<Score>();

    }

    public class Score
    {
        public string EngineVersion { get; set; }
        public string EngineName { get; set; }
        public string EngineUri { get; set; }
    }


}



