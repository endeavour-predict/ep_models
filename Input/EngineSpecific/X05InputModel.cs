using ep_core;
using System.ComponentModel.DataAnnotations;


namespace ep_models
{
    /// <remarks>
    /// Strict SNOMED definitions for all the below parameters are maintained by Endeavour Predict
    /// Using the definitions will ensure the highest possible data quality and consistency being fed to the API
    /// Please consult our website for further details on the definitions, which are also available in 
    /// alternative terminologies.
    /// In Attended Mode, clinicians may use their clinical judgement to over-ride automated searches, 
    /// however this may change the outcome of the risk estimation, and the user assumes all risk for this.    
    /// </remarks>    
    public class X05InputModel
    {
        /// <summary>
        /// Assigned sex at birth.
        /// </summary>        
        /// <example>Female</example>
        [Required]
        public EPStandardDefinitions.Gender sex { get; set; }

        /// <summary>
        /// Patients age in years calculated on the search date.
        /// </summary>
        /// <example>45</example>
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid positive number.")]
        [Required]
        public int age { get; set; }

        /// <summary>
        /// Diabetes status
        /// </summary>
        /// <example>None</example>
        public EPStandardDefinitions.DiabetesCat diabetesStatus { get; set; }

        /// <summary>
        /// Body Mass Index (kg/m^2).
        /// Acceptable/Credible Range: 18 to 47.
        /// The most recently recorded body mass index for the patient recorded prior to the search date recorded in the last 5 years.
        /// </summary>
        /// <example>29.1</example>  
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid positive number.")]
        public double? BMI { get; set; }

        /// <summary>
        /// Ethnic group, chosen from the 17 categories used by QRisk3.
        /// </summary>
        /// <example>OtherWhiteBackground</example>
        public EPStandardDefinitions.Ethnicity ethnicity { get; set; }
        
        /// <summary>
        /// Most recent confirmed smoking status.
        /// </summary>
        /// <example>NonSmoker</example>
        public EPStandardDefinitions.SmokeCat smokingStatus { get; set; }

        /// <summary>
        /// Most recent confirmed alcohol status.
        /// </summary>
        /// <example>One_to_two_units_per_day</example>
        public EPStandardDefinitions.AlcoholCat6 alcoholStatus { get; set; }

        /// <summary>
        /// Number of years to calculate risk over, not all Engines support this and will use a default.
        /// </summary>
        /// <example>10</example>
        public int predictionYears { get; set; }

        /// <summary>
        /// Townsend score. 
        /// The Townsend score associated with the output area of a patient’s postcode.
        /// See: https://statistics.ukdataservice.ac.uk/dataset/2011-uk-townsend-deprivation-scores#:~:text=The%20Townsend%20Deprivation%20Index%20is,is%20available%20for%20that%20area).
        /// </summary>
        /// <example>0</example>        
        public double? townsendScore { get; set; }


        /// <summary>
        /// Barrett’s Oesophagus
        /// </summary>
        public bool barrettsOesophagus { get; set; }

        /// <summary>
        /// Cancer of the blood or bone marrow such as leukaemia, myelodysplastic syndromes, lymphoma or myeloma and are at any stage of treatment
        /// </summary>
        public bool bloodCancer { get; set; }

        /// <summary>
        /// Breast cancer and are at any stage of treatment
        /// </summary>
        public bool breastCancer { get; set; }

        /// <summary>
        /// Hiatus Hernia
        /// </summary>
        public bool hiatusHernia { get; set; }

        /// <summary>
        /// Helicobacter Pylori Infection
        /// </summary>
        public bool hPyloriInfection { get; set; }

        /// <summary>
        /// lung cancer and are at any stage of treatment
        /// </summary>
        public bool lungCancer { get; set; }

        /// <summary>
        /// Anaemia (haemoglobin less than 110 g/L)
        /// </summary>
        public bool anaemia { get; set; }

        /// <summary>
        /// Currently taking antiacids (proton pump inhibitors)
        /// </summary>
        public EPStandardDefinitions.PPICat protonPumpInhibitorStatus { get; set; }



    }
}
