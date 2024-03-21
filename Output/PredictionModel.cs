using System;
using System.Collections.Generic;
using ep_models;

namespace ep_models
{
    /// <summary>
    /// The output model for a single Prediction (which may contain many scores, from many Calculation Engines)
    /// </summary>
    public class PredictionModel
    {

        /// <summary>
        /// List of Engine Results
        /// </summary>
        public List<EngineResultModel> EngineResults { get; set; } = new List<EngineResultModel>();


        /// <summary>
        /// Initializes a new instance of the <see cref="PredictionModel"/> class.
        /// </summary>
        public PredictionModel()
        {            
        }

        /// <summary>
        /// Metadata about this call
        /// </summary>
        public ServiceMeta ServiceMeta { get; set; } = new ServiceMeta();

        /// <summary>
        /// The data provided by the user for this Prediction.
        /// </summary>
        public EPInputModel EPInputModel { get; set; }

    }

    /// <summary>
    /// Whether the parameter was OK, Missing or Out of Range
    /// </summary>
    public enum ParameterQuality
    {
        OK, MISSING, OUT_OF_RANGE
    }


}


/// <summary>
/// Contains details of the "Quality" of the Input Parameters used, and details of any substitutions.
/// </summary>
public class DataQuality
{
    /// <summary>
    /// Name of the Parameter used by the Calculator
    /// </summary>
    public string Parameter { get; set; } = "";

    /// <summary>
    /// Was the parameter provided OK, out of range etc?
    /// </summary>
    public ParameterQuality Quality { get; set; }
    
    /// <summary>
    /// The value used by the calculator if the value provided was substituted
    /// </summary>
    public string SubstituteValue { get; set; } = "";

    /// <summary>
    /// Quality report from the calculator, showing any substituted values for missing or out of range parameters
    /// </summary>
    public DataQuality()
    { 
    }

}



/// <summary>
/// Contains MetaData about the prediction: timings, versions, etc
/// </summary>
public class ServiceMeta
{
    public ServiceMeta()
    {
    }
    /// <summary>
    /// Build version of the Service.
    /// </summary>
    public string ServiceVersion { get; set; }
    
    /// <summary>
    /// ISO DateTime (UTC) that the Service was invoked
    /// </summary>
    public DateTime RequestTimeStampUTC { get; set; }    
}

