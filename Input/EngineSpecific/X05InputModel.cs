extern alias X05;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using X05::CRStandardDefinitions;
using static Core.EPStandardDefinitions;

namespace ep_models
{

    public class X05InputModel
    {




        /// <summary>
        /// Assigned sex at birth.
        /// </summary>        
        /// <example>Female</example>
        [Required]
        public Core.EPStandardDefinitions.Gender sex { get; set; }

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
        public Core.EPStandardDefinitions.DiabetesCat diabetesStatus { get; set; }

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
        public Core.EPStandardDefinitions.Ethnicity ethnicity { get; set; }
        
        /// <summary>
        /// Most recent confirmed smoking status.
        /// </summary>
        /// <example>NonSmoker</example>
        public Core.EPStandardDefinitions.SmokeCat smokingStatus { get; set; }

        /// <summary>
        /// Most recent confirmed alcohol status.
        /// </summary>
        /// <example>One_to_two_units_per_day</example>
        public Core.EPStandardDefinitions.AlcoholCat6 alcoholStatus { get; set; }

        /// <summary>
        /// Number of years to calculate risk over, not all Engines support this and will use a default.
        /// </summary>
        /// <example>10</example>
        public int predictionYears { get; set; }


        public bool barrettsOesophagus { get; set; }
        public bool bloodCancer { get; set; }
        public bool breastCancer { get; set; }
        public bool hiatusHernia { get; set; }
        public bool hPyloriInfection { get; set; }
        public bool lungCancer { get; set; }
        public bool anaemia { get; set; }
        public Core.EPStandardDefinitions.PPICat protonPumpInhibitorStatus { get; set; }
        

    }
}
