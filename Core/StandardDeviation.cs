﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// Helper functions related to Statistics
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// The is the SAMPLE version of StDev, rather than the POPULATION version. This is the correct one for use in QRisk3
        /// See: https://stackoverflow.com/questions/3141692/standard-deviation-of-generic-list        
        /// </summary>  
        /// <remarks>Not currently used for the EP-QRisk3 implementation. Used to be a part of the prototype API and code is being kep here in case we need it again.</remarks>
        /// <param name="values">The list of values to calculate the Standard Deviation for</param>
        /// <returns>SAMPLE version Standard Deviation</returns>
        public static double? StandardDeviation(IEnumerable<double> values)
        {
            if (values.Count() == 1) return null;

            double standardDeviation = 0;
            if (values.Any())
            {
                // Compute the average.     
                double avg = values.Average();

                // Perform the Sum of (value-avg)_2_2.      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));

                // Put it all together.      
                standardDeviation = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return standardDeviation;
        }
    }
    

}
