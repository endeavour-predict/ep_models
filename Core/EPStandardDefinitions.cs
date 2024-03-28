using System.Text;
using System.Text.RegularExpressions;
/// <summary>
/// Library of classes expected to be used by most of the calculators and wrappers.
/// </summary>
namespace Core
{
    /// <summary>
    /// The EPStandardDefinitions is created as a superset of all the Oxford/ClinRisk "CRStandardDefinitions" files found in the individual calculators.
    /// In most cases each of the CRStandardDefinitions are the same, but small differences and additions between calculators are absorbed and resolved into this single EP library.
    /// The original CR files are left in the untouched source code for those calculators.
    /// </summary>
    public class EPStandardDefinitions
    {
        /// <summary>
        /// List of engines currently supported by EP
        /// </summary>
        public enum Engines {
            /// <summary>
            /// QRisk3 Engine
            /// </summary>
            QRisk3,
            /// <summary>
            /// Oesophageal cancer (CanPredict) known as X05
            /// </summary>
            X05
        };

        /// <summary>
        /// Calculation Engines always return one "result status".
        /// For example: this can tell you whether the input parameters were used in entirety, or whether any were substituted.
        /// Details about any parameter substitutions can be found in the DataQuality section of the Prediction Model
        /// </summary>
        public enum ResultStatus {
            /// <summary>
            /// If a calculator cannot perform a score, this result status is returned (e.g. Age over Max allowable, or Patient has already had a CVD event for a QRisk3 score)
            /// </summary>
            NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA,
            /// <summary>
            /// Score was calculated using the Input values provided, with no substitutions or corrections.
            /// </summary>
            CALCULATED_USING_PATIENTS_OWN_DATA,
            /// <summary>
            /// Score calculated using estimated or corrected data, see DataQuality section of the Prediction Model for details about which parameters have been substituted.
            /// </summary>
            CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA,
            /// <summary>
            /// No calculation possible as engine is locked (this is not used in the EP implementation, as all engines are unlocked).
            /// </summary>
            NO_CALCULATION_POSSIBLE_AS_ENGINE_LOCKED
        };

        /// <summary>
        /// If an Engine cannot perform a calculation, the reason is given here.
        /// </summary>
        public enum ReasonInvalid {
            /// <summary>
            /// No invalid reason, everything was fine!
            /// </summary>
            VALID,
            /// <summary>
            /// Age is either too high or too low. Min and Max values can be found in the Constants class
            /// </summary>
            AGE_OUT_OF_RANGE,
            /// <summary>
            /// QRiskl cannot perform a calculation if the patient has an existing CVD event
            /// </summary>
            ALREADY_HAD_A_CVD_EVENT,
            /// <summary>
            /// Ethnicity provided was not in the acceptable list (this should never been seen when used via the EP InputModel, since we are wrapping the calculator inputs with strongly typed parameters.
            /// </summary>
            ETHNICITY_OUT_OF_RANGE,
            /// <summary>
            /// Parameter was expected to be a Boolean, but something else was provided (this should never been seen when used via the EP InputModel, since we are wrapping the calculator inputs with strongly typed parameters.
            /// </summary>
            VARIABLE_NON_BOOLEAN,
            /// <summary>
            /// No calculation possible as the QRisk3 engine is locked (this is not used in the EP implementation, as all engines are unlocked).
            /// </summary>
            QRISK_ENGINE_LOCKED,
            /// <summary>            
            /// smoking status provided was not in the acceptable list (this should never been seen when used via the EP InputModel, since we are wrapping the calculator inputs with strongly typed parameters.            
            /// </summary>
            SMOKING_STATUS_OUT_OF_RANGE
        };

        /// <summary>
        /// Details about the parameters supplied to the calculation engine.
        /// </summary>
        public enum Data {
            /// <summary>
            /// Parameter is OK.
            /// </summary>
            OK,
            /// <summary>
            /// Parameter is Missing.
            /// </summary>
            MISSING,
            /// <summary>
            /// Parameter is out of range (see Min and Max values in the Constants class).
            /// </summary>
            OUT_OF_RANGE
        };


        /// <summary>
        /// Assigned Sex at Birth
        /// </summary>
        public enum Gender {
            /// <summary>
            /// Sex assigned at birth: Female
            /// </summary>
            Female,
            /// <summary>
            /// Sex assigned at birth: Male
            /// </summary>
            Male
        };

        /// <summary>
        /// Diabetes Status (Category)
        /// </summary>
        public enum DiabetesCat {
            /// <summary>
            /// No Diabetes
            /// </summary>
            None,
            /// <summary>
            /// Type1 Diabetes
            /// </summary>
            Type1,
            /// <summary>
            /// Type2 Diabetes
            /// </summary>
            Type2
        };

        /// <summary>
        /// Smoking Status, chosen from 6 options
        /// </summary>
        public enum SmokeCat { NonSmoker, ExSmoker, LightSmoker, ModerateSmoker, HeavySmoker, NotKnown };

        /// <summary>
        /// Ethnic group, chosen from the 17 categories.
        /// </summary>
        public enum Ethnicity
        {
            NotRecorded,
            British,
            Irish,
            OtherWhiteBackground,
            WhiteAndBlackCaribbeanMixed,
            WhiteAndBlackAfricanMixed,
            WhiteAndAsianMixed,
            OtherMixed,
            Indian,
            Pakistani,
            Bangladeshi,
            OtherAsian,
            Caribbean,
            BlackAfrican,
            OtherBlack,
            Chinese,
            OtherEthnicGroup,
            NotStated
        };


        /// <summary>
        /// A Map of 4 different Alcohol Categories (see also AlcoholCat6 used by some calculators)
        /// </summary>
        public enum AlcoholCat4
        {
            None,
            Less_than_1_unit_per_day,
            One_to_two_units_per_day,
            Three_or_more_units_per_day,
            Not_known
        };

        /// <summary>
        /// A Map of 6 different Alcohol Categories (see also AlcoholCat4 used by some calculators)
        /// </summary>
        public enum AlcoholCat6
        {
            None,
            Less_than_1_unit_per_day,
            One_to_two_units_per_day,
            Three_to_six_units_per_day,
            Seven_to_nine_units_per_day,
            Over_nine_units_per_day,
            Not_known
        };


        public enum PPICat
        {
            Never,
            OneToFiveScripts,
            SixToElevenScripts,
            TwelveToTwentyThreeScripts,
            TwentyFourToFortySevenScripts,
            FortyEightOrMoreScripts,
            NotKnown
        };

        ////Commented for now, possibly used in future calculators
        //public enum AdmitPriorCat { None, One, Two, ThreeOrMore };
        //public enum Sha1Cat { EastMidlands, EastOfEngland, London, NorthEast, NorthWest, SouthCentral, SouthEast, SouthWest, WestMidlands, YorksAndHumber, Wales, IsleOfMan, Other };
        //public enum HeartburnIndigestionCat { Neither, Heartburn, Indigestion };



        /// <summary>
        /// Utility class used to map some of the Enumerations together (SmokingCat6 to Cat4 for example)
        /// Also used by some calculators to get Ints from the Enums
        /// </summary>
        public class Utilities
        {
            public static int ethnicityToEthrisk(Ethnicity e)
            {
                int ethrisk = 0;
                switch (e)
                {
                    case Ethnicity.NotRecorded:
                    case Ethnicity.British:
                    case Ethnicity.Irish:
                    case Ethnicity.OtherWhiteBackground:
                        ethrisk = 1;
                        break;
                    case Ethnicity.WhiteAndBlackCaribbeanMixed:
                    case Ethnicity.WhiteAndBlackAfricanMixed:
                    case Ethnicity.WhiteAndAsianMixed:
                    case Ethnicity.OtherMixed:
                        ethrisk = 9;
                        break;
                    case Ethnicity.Indian:
                        ethrisk = 2;
                        break;
                    case Ethnicity.Pakistani:
                        ethrisk = 3;
                        break;
                    case Ethnicity.Bangladeshi:
                        ethrisk = 4;
                        break;
                    case Ethnicity.OtherAsian:
                        ethrisk = 5;
                        break;
                    case Ethnicity.Caribbean:
                        ethrisk = 6;
                        break;
                    case Ethnicity.BlackAfrican:
                        ethrisk = 7;
                        break;
                    case Ethnicity.OtherBlack:
                        ethrisk = 9;
                        break;
                    case Ethnicity.Chinese:
                        ethrisk = 8;
                        break;
                    case Ethnicity.OtherEthnicGroup:
                        ethrisk = 9;
                        break;
                    case Ethnicity.NotStated:
                        ethrisk = 1;
                        break;
                    default:
                        break;
                }
                return ethrisk;
            }
            public static int boolToInt(bool b)
            {
                if (b)
                    return 1;
                else
                    return 0;
            }
            public static int smokecatToInt(SmokeCat sc)
            {
                int smokecat = 0;
                switch (sc)
                {
                    case SmokeCat.NonSmoker:
                        smokecat = 0;
                        break;
                    case SmokeCat.ExSmoker:
                        smokecat = 1;
                        break;
                    case SmokeCat.LightSmoker:
                        smokecat = 2;
                        break;
                    case SmokeCat.ModerateSmoker:
                        smokecat = 3;
                        break;
                    case SmokeCat.HeavySmoker:
                        smokecat = 4;
                        break;
                    case SmokeCat.NotKnown:
                        smokecat = 0;
                        break;
                    default:
                        break;
                }
                return smokecat;
            }
            public static int diabetescatToType1(DiabetesCat dc)
            {
                int tmp = 0;
                if (dc == DiabetesCat.Type1)
                    tmp = 1;
                return tmp;
            }
            public static int diabetescatToType2(DiabetesCat dc)
            {
                int tmp = 0;
                if (dc == DiabetesCat.Type2)
                    tmp = 1;
                return tmp;
            }
            public static int genderToInt(Gender g)
            {
                if (g == Gender.Female)
                    return 0;
                else
                    return 1;
            }
            public static int alcoholcat4ToInt(AlcoholCat4 ac)
            {
                int tmp;
                switch (ac)
                {
                    case AlcoholCat4.None:
                        tmp = 0;
                        break;
                    case AlcoholCat4.Less_than_1_unit_per_day:
                        tmp = 1;
                        break;
                    case AlcoholCat4.One_to_two_units_per_day:
                        tmp = 2;
                        break;
                    case AlcoholCat4.Three_or_more_units_per_day:
                        tmp = 3;
                        break;
                    case AlcoholCat4.Not_known:
                        tmp = 1;
                        break;
                    default:
                        tmp = 1;
                        break;
                }
                return tmp;
            }
            public static int alcoholcat6ToInt(AlcoholCat6 ac)
            {
                int tmp;
                switch (ac)
                {
                    case AlcoholCat6.None:
                        tmp = 0;
                        break;
                    case AlcoholCat6.Less_than_1_unit_per_day:
                        tmp = 1;
                        break;
                    case AlcoholCat6.One_to_two_units_per_day:
                        tmp = 2;
                        break;
                    case AlcoholCat6.Three_to_six_units_per_day:
                        tmp = 3;
                        break;
                    case AlcoholCat6.Seven_to_nine_units_per_day:
                        tmp = 4;
                        break;
                    case AlcoholCat6.Over_nine_units_per_day:
                        tmp = 5;
                        break;
                    case AlcoholCat6.Not_known:
                        tmp = 1;
                        break;
                    default:
                        tmp = 1;
                        break;
                }
                return tmp;
            }


            /// <summary>
            /// Commented out for now, this code may be used in future calculators
            /// </summary>
            //public static int admitpriorcatToInt(AdmitPriorCat c)
            //{
            //    int cat = 0;
            //    switch (c)
            //    {
            //        case AdmitPriorCat.None:
            //            cat = 0;
            //            break;
            //        case AdmitPriorCat.One:
            //            cat = 1;
            //            break;
            //        case AdmitPriorCat.Two:
            //            cat = 2;
            //            break;
            //        case AdmitPriorCat.ThreeOrMore:
            //            cat = 3;
            //            break;
            //        default:
            //            break;
            //    }
            //    return cat;
            //}
            //public static int sha1catToInt(Sha1Cat c)
            //{
            //    int cat = 1;
            //    switch (c)
            //    {
            //        case Sha1Cat.EastMidlands:
            //            cat = 1;
            //            break;
            //        case Sha1Cat.EastOfEngland:
            //            cat = 2;
            //            break;
            //        case Sha1Cat.London:
            //            cat = 3;
            //            break;
            //        case Sha1Cat.NorthEast:
            //            cat = 4;
            //            break;
            //        case Sha1Cat.NorthWest:
            //            cat = 5;
            //            break;
            //        case Sha1Cat.SouthCentral:
            //            cat = 6;
            //            break;
            //        case Sha1Cat.SouthEast:
            //            cat = 7;
            //            break;
            //        case Sha1Cat.SouthWest:
            //            cat = 8;
            //            break;
            //        case Sha1Cat.WestMidlands:
            //            cat = 9;
            //            break;
            //        case Sha1Cat.YorksAndHumber:
            //            cat = 10;
            //            break;
            //        // NB we don't add Wales, IsleOfMan and Other here
            //        default:
            //            break;
            //    }
            //    return cat;
            //}


            // postcode verification
            // wikipedia "simple" regex for postcodes
            // modified to allow all chars in the street part (e.g. ZZ995VZ)
            static readonly private Regex regex = new Regex(@"(?<town>[A-Z]{1,2})(?<district>[0-9R][0-9A-Z]{0,1})(?<street>[0-9][A-Z]{2})", RegexOptions.Compiled);
            static readonly private Regex regexNewport = new Regex(@"(?<town>NPT)(?<street>[0-9][A-Z]{2})", RegexOptions.Compiled);
            private static string xValidateAndRegularisePostcode(string postcode)
            {
                // This is the same as the function in input_parameters.cs
                string tmp = postcode.Replace(" ", "").ToUpper();
                StringBuilder newpc = new StringBuilder(7);
                // Time to use regular expressions
                // There are two: one standard one, and one for Newport! See above.
                // if match
                // then
                //   put the matches in place
                // else
                //   set return string as ""
                // return
                Match m = regex.Match(tmp);
                Match m2 = regexNewport.Match(tmp);
                if (m.Success)
                {
                    string town = m.Result("${town}");
                    string district = m.Result("${district}");
                    string street = m.Result("${street}");
                    int cursor = 0;
                    newpc.Insert(cursor, town);
                    cursor += town.Length;
                    newpc.Insert(cursor, district);
                    cursor += district.Length;
                    while (cursor < 4)
                    {
                        newpc.Append(' ');
                        cursor++;
                    }
                    newpc.Append(street);
                    return newpc.ToString();
                }
                else if (m2.Success)
                {
                    string town = m2.Result("${town}");
                    string street = m2.Result("${street}");
                    return town + " " + street;
                }
                else
                {
                    return "postcode_invalid";
                }
            }
            public static string validateAndRegularisePostcode(string postcode)
            {
                return xValidateAndRegularisePostcode(postcode);
            }

        }


        /// <summary>
        /// Shared across many calculators, these are the max and min values allowed for different input parameters
        /// </summary>
        public class Constants
        {
            /// <summary>
            /// The Minimum Townsend score associated with the output area of a patient’s postcode.
            /// See: https://statistics.ukdataservice.ac.uk/dataset/2011-uk-townsend-deprivation-scores#:~:text=The%20Townsend%20Deprivation%20Index%20is,is%20available%20for%20that%20area).
            /// </summary>
            public const double minTown = -8.0;
            /// <summary>
            /// The Maximum Townsend score associated with the output area of a patient’s postcode.
            /// See: https://statistics.ukdataservice.ac.uk/dataset/2011-uk-townsend-deprivation-scores#:~:text=The%20Townsend%20Deprivation%20Index%20is,is%20available%20for%20that%20area).
            /// </summary>
            public const double maxTown = 12.0;

            /// <summary>
            /// The Minimum CholesterolRatio
            /// </summary>
            public const double minRati = 1.0;
            /// <summary>
            /// The Maximum CholesterolRatio
            /// </summary>
            public const double maxRati = 12.0;

            /// <summary>
            /// The Minimum BMI (Body Mass Index)
            /// </summary>
            public const double minBmi = 20.0;
            /// <summary>
            /// The Maximum BMI (Body Mass Index)
            /// </summary>
            public const double maxBmi = 40.0;

            /// <summary>
            /// The Minimum Systolic Blood Pressure Mean
            /// </summary>
            public const double minSbp = 70.0;
            /// <summary>
            /// The Maximum Systolic Blood Pressure Mean
            /// </summary>
            public const double maxSbp = 210.0;


            //Commented out for now, will be used in future calculators
            //public const double minHba1c = 15.0;
            //public const double maxHba1c = 47.99;
            //public const double minFbs = 2.0;
            //public const double maxFbs = 6.99;
        }
    }

}
