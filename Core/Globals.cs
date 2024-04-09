
using ep_core;
using System.Collections.Generic;

/// <summary>
/// Class to hold some Constants
/// </summary>
public class Globals
{
    /// <summary>
    /// The QDiabetes score URI
    /// </summary>
    public const string QDiabetesScoreUri   = "http://endhealth.info/im#QDiabetes";
    /// <summary>
    /// The QFracture score URI
    /// </summary>
    public const string QFractureScoreUri   = "http://endhealth.info/im#QFracture";
    /// <summary>
    /// The QRisk3 score URI
    /// </summary>
    public const string QRiskScoreUri       = "http://endhealth.info/im#Qrisk3";

    /// <summary>
    /// List of engines implemented with this release
    /// </summary>
    public List<Engine> AvailableEngines = new List<Engine>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Globals"/> class.
    /// Constructor is responsible for setting the available engine implementations list
    /// </summary>
    public Globals()
    {
        this.AvailableEngines.Add(new Engine
            {   EngineName = EPStandardDefinitions.Engines.QRisk3.ToString(),
                EngineVersion = QRISK3Engine.QRiskCVDAlgorithmCalculator.version(),
                EngineUri = "http://endhealth.info/im#Qrisk3"
            });

        //this.AvailableEngines.Add(new Engine
        //{
        //    EngineName = EPStandardDefinitions.Engines.QDiabetes.ToString(),
        //    EngineVersion = qdiab::QDiabetesEngine.QDiabetesAlgorithmCalculator.version(), 
        //    EngineUri = "http://endhealth.info/im#QDiabetes"
        //});

        //this.AvailableEngines.Add(new Engine
        //{
        //    EngineName = EPStandardDefinitions.Engines.QFracture.ToString(),
        //    EngineVersion = qfrac::QFractureEngine.QFractureAlgorithmCalculator.version(),
        //    EngineUri = "http://endhealth.info/im#QFracture"
        //});

        this.AvailableEngines.Add(new Engine
        {
            EngineName = EPStandardDefinitions.Engines.X05.ToString(),
            EngineVersion = X05_oesophagealcancerEngine.X05_oesophagealcancerAlgorithmCalculator.version(),
            EngineUri = "http://endhealth.info/im#X05"
        });
    }



    /// <summary>
    /// Risk Calculation Engine
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// The name of the engine. This is set by the calculator Engine itself, not the wrapper classes.
        /// </summary>        
        public string EngineName { get; set; }
        /// <summary>
        /// The version of the engine. This is set by the calculator Engine itself, not the wrapper classes.
        /// </summary>        
        public string EngineVersion { get; set; }
        /// <summary>
        /// The URI of the engine. This is set by wrapper classes.
        /// </summary>        
        public string EngineUri { get; set; }
    }

}