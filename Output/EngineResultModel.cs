﻿using System;
using System.Collections.Generic;
using System.Reflection;
using static Globals;
using System.Linq;
using static ep_core.EPStandardDefinitions;
using ep_core;

namespace ep_models
{

    /// <summary>
    /// Calculation Result Model
    /// Contains the results from a single Calculator Engine
    /// </summary>
    public class EngineResultModel
    {

        /// <summary>
        /// Name of the Engine that supplied this result
        /// </summary>
        public Engines EngineName { get; set; }

        /// <summary>
        /// Version of the Engine that supplied this result
        /// </summary>
        public string EngineVersion { get; set; }

        /// <summary>
        /// List of Prediction Scores
        /// Some calculator engines will provide multiple scores (Patient Score, Typical Score, QHeart age etc)
        /// </summary>
        public List<PredictionResult> Results { get; set; } = new List<PredictionResult>();

        /// <summary>
        /// List of DataQuality checks, showing any substituted values
        /// </summary>
        public List<DataQuality> Quality { get; set; } = new List<DataQuality>();

        /// <summary>
        /// Metadata about this call to the Engine
        /// </summary>
        public EngineMeta CalculationMeta { get; set; } = new EngineMeta();

        /// <summary>
        /// The data provided by the user for this Prediction. Identifiable fields are stripped of data and marked as **PI** 
        /// </summary>
        public EPInputModel EngineInputModel { get; set; }


        /// <summary>
        /// Contains the ID, Score and Typical score (if available)
        /// </summary>
        public class PredictionResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PredictionResult"/> class.
            /// </summary>
            public PredictionResult()
            {
            }
            /// <summary>
            /// The identifier for this score.
            /// </summary>            
            public Uri id { get; set; }
            /// <summary>
            /// The score calculated by the Engine.
            /// </summary>            
            public double score { get; set; }
            /// <summary>
            /// A typical score for someone of the same age/sex (if the calculator supports this)
            /// </summary>            
            public double? typicalScore { get; set; }
            /// <summary>
            /// How many years the Risk score is calculated for.
            /// </summary>            
            public int predictionYears { get; set; }
        }


        /// <summary>
        /// Class containing details about supplied parameter "quality", including details of any substritutions or corrections made before the Score was calculated.
        /// </summary>
        public class DataQuality
        {
            /// <summary>
            /// Name of the Parameter used by the Calculator.
            /// </summary>
            public string Parameter { get; set; } = "";
            /// <summary>
            /// Was the parameter provided OK, out of range etc?
            /// </summary>
            public ParameterQuality Quality { get; set; }
            /// <summary>
            /// The value used by the calculator if the value provided was substituted.
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
        public class EngineMeta
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EngineMeta"/> class.
            /// </summary>
            public EngineMeta()
            {
            }            
            /// <summary>
            /// Status result from the Calculator.
            /// </summary>
            public ResultStatus EngineResultStatus { get; set; }
            /// <summary>
            /// Used to hold extra information if the Calculator was unable to produce a Score (reason for failure).
            /// </summary>
            public ReasonInvalid EngineResultStatusReason { get; set; }
        }







        /// <summary>
        /// Empty Constructor
        /// </summary>
        public EngineResultModel()
        {            
        }
        /// <summary>
        /// Constructor for the QRisk3 engine
        /// </summary>        
        public EngineResultModel(QRISK3Engine.QRiskCVDResults calcResult, QRisk3InputModel calcInputModel)
        {            
            var globals = new Globals();
            this.EngineName = Engines.QRisk3;
            Engine engine = GetEngine(globals, Engines.QRisk3.ToString());            
            this.EngineVersion = engine.EngineVersion;

            var meta = new EngineMeta();
            meta.EngineResultStatus = (ResultStatus)calcResult.resultStatus;
            meta.EngineResultStatusReason = (ReasonInvalid)calcResult.reason;
            this.CalculationMeta = meta;

            // map the calculator specific results output to the generic API engine results model
            // QRisk3 returns 2 scores 
            var result1 = new PredictionResult();
            result1.id = new Uri(engine.EngineUri);
            result1.score = calcResult.score; 
            result1.typicalScore = calcResult.typical_score;
            result1.predictionYears = 10;            
            this.Results.Add(result1);
            
            if (calcResult.qHeartAge != null)
            {
                var result2 = new PredictionResult();
                result2.id = new Uri(engine.EngineUri + "HeartAge");
                result2.score = (double)calcResult.qHeartAge;
                result2.predictionYears = 10;
                this.Results.Add(result2);
            }            
            
            var smokingQuality = new DataQuality();
            smokingQuality.Parameter = "smokingStatus";
            smokingQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.smokingStatus.data.ToString());
            smokingQuality.SubstituteValue = calcResult.DataQuality.smokingStatus.substitute_value;
            Quality.Add(smokingQuality);

            var sbpQuality = new DataQuality();
            sbpQuality.Parameter = "systolicBloodPressureMean";
            sbpQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.sbp.data.ToString());
            sbpQuality.SubstituteValue = calcResult.DataQuality.sbp.substitute_value.ToString();
            Quality.Add(sbpQuality);

            var sbp5sQuality = new DataQuality();
            sbp5sQuality.Parameter = "systolicBloodPressureStDev";
            sbp5sQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.sbps5.data.ToString());
            sbp5sQuality.SubstituteValue = calcResult.DataQuality.sbps5.substitute_value.ToString();
            Quality.Add(sbp5sQuality);

            var ratioQuality = new DataQuality();
            ratioQuality.Parameter = "ratio";
            ratioQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.ratio.data.ToString());
            ratioQuality.SubstituteValue = calcResult.DataQuality.ratio.substitute_value.ToString();
            Quality.Add(ratioQuality);

            var ethnicityQuality = new DataQuality();
            ethnicityQuality.Parameter = "ethnicity";
            ethnicityQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.ethnicity.data.ToString());
            ethnicityQuality.SubstituteValue = calcResult.DataQuality.ethnicity.substitute_value.ToString();
            Quality.Add(ethnicityQuality);


            var bmiQuality = new DataQuality();
            bmiQuality.Parameter = "BMI";
            bmiQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.bmi.data.ToString());
            bmiQuality.SubstituteValue = calcResult.DataQuality.bmi.substitute_value.ToString();
            Quality.Add(bmiQuality);

            var townsendQuality = new DataQuality();
            townsendQuality.Parameter = "townsendScore";
            townsendQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.townsend.data.ToString());
            townsendQuality.SubstituteValue = calcResult.DataQuality.townsend.substitute_value.ToString();
            Quality.Add(townsendQuality);

            // use the calculator specific input model to load back into the API input model, this will show the data used in the calc, and nothing more.
            PropertyInfo[] calcInputProperties = typeof(QRisk3InputModel).GetProperties();
            PropertyInfo[] apiInputProperties = typeof(EPInputModel).GetProperties();
            EPInputModel epInputModel = new EPInputModel();
            foreach (PropertyInfo calcProperty in calcInputProperties)
            {
                // map the calc param to the genmeric input param, showing the ones used
                var apiInputProperty = apiInputProperties.Where(p => p.Name == calcProperty.Name).SingleOrDefault();
                if (apiInputProperty != null)
                {
                    apiInputProperty.SetValue(epInputModel, calcProperty.GetValue(calcInputModel));
                }
            }
            epInputModel.requestedEngines.Add(Engines.QRisk3);
            this.EngineInputModel = epInputModel;

        }


        public EngineResultModel(QDiabetesEngine.QDiabetesResults calcResult, QDiabetesInputModel calcInputModel)
        {
            var globals = new Globals();
            this.EngineName = Engines.QDiabetes;
            Engine engine = GetEngine(globals, Engines.QDiabetes.ToString());
            this.EngineVersion = engine.EngineVersion;



            var meta = new EngineMeta();
            meta.EngineResultStatus = (ResultStatus)calcResult.resultStatus;
            meta.EngineResultStatusReason = (ReasonInvalid)calcResult.reason;
            this.CalculationMeta = meta;


            var result1 = new PredictionResult();
            result1.id = new Uri(engine.EngineUri);
            result1.score = calcResult.patient_score;
            result1.typicalScore = calcResult.reference_score;
            result1.predictionYears = 10;
            this.Results.Add(result1);

            var smokingQuality = new DataQuality();
            smokingQuality.Parameter = "smokingStatus";
            smokingQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.smokingStatus.data.ToString());
            smokingQuality.SubstituteValue = calcResult.dataQuality.smokingStatus.substitute_value;
            Quality.Add(smokingQuality);

            var ethnicityQuality = new DataQuality();
            ethnicityQuality.Parameter = "ethnicity";
            ethnicityQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.ethnicity.data.ToString());
            ethnicityQuality.SubstituteValue = calcResult.dataQuality.ethnicity.substitute_value.ToString();
            Quality.Add(ethnicityQuality);


            var bmiQuality = new DataQuality();
            bmiQuality.Parameter = "BMI";
            bmiQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.bmi.data.ToString());
            bmiQuality.SubstituteValue = calcResult.dataQuality.bmi.substitute_value.ToString();
            Quality.Add(bmiQuality);

            var townsendQuality = new DataQuality();
            townsendQuality.Parameter = "townsendScore";
            townsendQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.town.data.ToString());
            townsendQuality.SubstituteValue = calcResult.dataQuality.town.substitute_value.ToString();
            Quality.Add(townsendQuality);

            // use the calculator specific input model to load back into the API input model, this will show the data used in the calc, and nothing more.

            // use the calculator specific input model to load back into the API input model, this will show the data used in the calc, and nothing more.
            PropertyInfo[] calcInputProperties = typeof(QDiabetesInputModel).GetProperties();
            PropertyInfo[] apiInputProperties = typeof(EPInputModel).GetProperties();
            EPInputModel epInputModel = new EPInputModel();
            foreach (PropertyInfo calcProperty in calcInputProperties)
            {
                // map the calc param to the genmeric input param, showing the ones used
                var apiInputProperty = apiInputProperties.Where(p => p.Name == calcProperty.Name).SingleOrDefault();
                if (apiInputProperty != null)
                {
                    apiInputProperty.SetValue(epInputModel, calcProperty.GetValue(calcInputModel));
                }
            }
            epInputModel.requestedEngines.Add(Engines.QDiabetes);
            this.EngineInputModel = epInputModel;
        }


        //public EngineResultModel(qfrac::QFractureEngine.QFractureResults calcResult, QFractureInputModel calcInputModel)
        //{
        //    var globals = new Globals();
        //    var engineName = EPStandardDefinitions.Engines.QFracture.ToString();
        //    Engine engine = GetEngine(globals, engineName);
        //    this.EngineName = engine.EngineName;
        //    this.EngineVersion = engine.EngineVersion;

        //    var meta = new Meta();
        //    meta.EngineResultStatus = (EPStandardDefinitions.ResultStatus)calcResult.resultStatus;
        //    meta.EngineResultStatusReason = (EPStandardDefinitions.ReasonInvalid)calcResult.reason;
        //    this.CalculationMeta = meta;

        //    // map the calculator specific results output to the generic API engine results model
        //    // QFrac returns two scores
        //    var result1 = new PredictionResult();
        //    result1.id = new Uri(engine.EngineUri);
        //    result1.score = calcResult.fracture4_score;
        //    result1.typicalScore = calcResult.reference_fracture4_score;
        //    result1.predictionYears = calcInputModel.predictionYears;
        //    this.Results.Add(result1);

        //    var result2 = new PredictionResult();
        //    result2.id = new Uri(engine.EngineUri + "Hip");
        //    result2.score = calcResult.nof_score;
        //    result2.typicalScore = calcResult.reference_nof_score;
        //    result2.predictionYears = calcInputModel.predictionYears;
        //    this.Results.Add(result2);

        //    var smokingQuality = new DataQuality();
        //    smokingQuality.Parameter = "smokingStatus";
        //    smokingQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.smokingStatus.data.ToString());
        //    smokingQuality.SubstituteValue = calcResult.dataQuality.smokingStatus.substitute_value;
        //    Quality.Add(smokingQuality);

        //    var ethnicityQuality = new DataQuality();
        //    ethnicityQuality.Parameter = "ethnicity";
        //    ethnicityQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.ethnicity.data.ToString());
        //    ethnicityQuality.SubstituteValue = calcResult.dataQuality.ethnicity.substitute_value.ToString();
        //    Quality.Add(ethnicityQuality);


        //    var bmiQuality = new DataQuality();
        //    bmiQuality.Parameter = "BMI";
        //    bmiQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.bmi.data.ToString());
        //    bmiQuality.SubstituteValue = calcResult.dataQuality.bmi.substitute_value.ToString();
        //    Quality.Add(bmiQuality);

        //    // use the calculator specific input model to load back into the API input model, this will show the data used in the calc, and nothing more.
        //    PropertyInfo[] calcInputProperties = typeof(QFractureInputModel).GetProperties();
        //    PropertyInfo[] apiInputProperties = typeof(InputModel).GetProperties();
        //    InputModel apiInputModel = new InputModel();
        //    foreach (PropertyInfo calcProperty in calcInputProperties)
        //    {
        //        // map the calc param to the genmeric input param, showing the ones used
        //        var apiInputProperty = apiInputProperties.Where(p => p.PropertyType.Name == calcProperty.Name).SingleOrDefault();
        //        if (apiInputProperty != null)
        //        {
        //            calcProperty.SetValue(calcInputModel, apiInputProperty.GetValue(apiInputModel));
        //        }
        //    }
        //    this.InputModel = apiInputModel;

        //}



        /// <summary>
        /// Constructor for the X05 engine
        /// </summary>        
        public EngineResultModel(X05_oesophagealcancerEngine.X05_oesophagealcancerResults calcResult, X05InputModel calcInputModel)
        {
            var globals = new Globals();
            this.EngineName = Engines.X05;
            Engine engine = GetEngine(globals, Engines.X05.ToString());
            this.EngineVersion = engine.EngineVersion;

            var meta = new EngineMeta();
            meta.EngineResultStatus = (ResultStatus)calcResult.resultStatus;
            meta.EngineResultStatusReason = (ReasonInvalid)calcResult.reason;
            this.CalculationMeta = meta;

            // map the calculator specific results output to the generic API engine results model
            // X05 returns 3 scores (each with their own Typical "reference" Score)
            var result1 = new PredictionResult();
            result1.id = new Uri(engine.EngineUri + "#" + "oesophagealcancer_5_score");
            result1.score = calcResult.oesophagealcancer_5_score;
            result1.typicalScore = calcResult.reference_oesophagealcancer_5_score;
            result1.predictionYears = calcInputModel.predictionYears;
            this.Results.Add(result1);

            var result2 = new PredictionResult();
            result2.id = new Uri(engine.EngineUri + "#" + "oesophagealcancer_6_score");
            result2.score = calcResult.reference_oesophagealcancer_6_score;
            result2.typicalScore = calcResult.reference_oesophagealcancer_6_score;
            result2.predictionYears = calcInputModel.predictionYears;
            this.Results.Add(result2);


            var result3 = new PredictionResult();
            result3.id = new Uri(engine.EngineUri + "#" + "oesophagealcancer_7_score");
            result3.score = calcResult.oesophagealcancer_7_score;
            result3.typicalScore = calcResult.reference_oesophagealcancer_7_score;
            result3.predictionYears = calcInputModel.predictionYears;
            this.Results.Add(result3);


            var smokingQuality = new DataQuality();
            smokingQuality.Parameter = "smokingStatus";
            smokingQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.smoke_cat.data.ToString());
            smokingQuality.SubstituteValue = calcResult.dataQuality.smoke_cat.substitute_value;
            Quality.Add(smokingQuality);

            var ethnicityQuality = new DataQuality();
            ethnicityQuality.Parameter = "ethnicity";
            ethnicityQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.ethnicity.data.ToString());
            ethnicityQuality.SubstituteValue = calcResult.dataQuality.ethnicity.substitute_value.ToString();
            Quality.Add(ethnicityQuality);

            var bmiQuality = new DataQuality();
            bmiQuality.Parameter = "BMI";
            bmiQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.bmi.data.ToString());
            bmiQuality.SubstituteValue = calcResult.dataQuality.bmi.substitute_value.ToString();
            Quality.Add(bmiQuality);

            var townsendQuality = new DataQuality();
            townsendQuality.Parameter = "townsendScore";
            townsendQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.dataQuality.town.data.ToString());
            townsendQuality.SubstituteValue = calcResult.dataQuality.town.substitute_value.ToString();
            Quality.Add(townsendQuality);



            // use the calculator specific input model to load back into the API input model, this will show the data used in the calc, and nothing more.
            PropertyInfo[] calcInputProperties = typeof(X05InputModel).GetProperties();
            PropertyInfo[] apiInputProperties = typeof(EPInputModel).GetProperties();
            EPInputModel epInputModel = new EPInputModel();
            foreach (PropertyInfo calcProperty in calcInputProperties)
            {
                // map the calc param to the generic input param, showing the ones used
                var apiInputProperty = apiInputProperties.Where(p => p.Name == calcProperty.Name).SingleOrDefault();
                if (apiInputProperty != null)
                {
                    apiInputProperty.SetValue(epInputModel, calcProperty.GetValue(calcInputModel));
                }
            }
            epInputModel.requestedEngines.Add(Engines.X05);
            this.EngineInputModel = epInputModel;
        }



        private static Engine GetEngine(Globals globals, string engineName)
        {
            var engine = globals.AvailableEngines.Where(p => p.EngineName == engineName).SingleOrDefault();
            if (engine == null) throw new ApplicationException("No engine was found with the name: " + engineName);
            return engine;
        }



    }


}



