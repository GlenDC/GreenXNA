#region File Description
//-----------------------------------------------------------------------------
// Converter.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014       ***     Last Edit: July 2013
// More information and details can be found at http://www.greenxna.com/
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// Extendng of the excisting converter
    /// </summary>
    public class Converter
    {

        /// <summary>
        /// Culture will be used to force a certain format. 
        /// TODO: Implement the cultureinfo in a global *.ini file
        /// </summary>
        private static CultureInfo CULTURE = null;

        /// <summary>
        /// Set the culture for the file
        /// Check http://msdn.microsoft.com/en-us/goglobal/bb896001.aspx  
        /// if your not sure what code you need.
        /// </summary>
        /// <param name="culture">set the culture code</param>
        public static void SetCulture(string culture)
        {
            CULTURE = new CultureInfo(culture);
        }

        #region implementing default Convert so that we can force a correct setting!
        /// <summary>
        /// Convert a xml-format string to a float value.
        /// </summary>
        /// <param name="value">Original float value that will be converted to a string.</param>
        public static float ToFloat(string value)
        {
            return (float)Convert.ToDouble(value, CULTURE);
        }

        /// <summary>
        /// Convert a xml-format string to a double value.
        /// </summary>
        /// <param name="value">Original double value that will be converted to a string.</param>
        public static double ToDouble(string value)
        {
            return Convert.ToDouble(value, CULTURE);
        }

        /// <summary>
        /// Convert a xml-format string to an int value.
        /// </summary>
        /// <param name="value">Original int value that will be converted to a string.</param>
        public static int ToInt(string value)
        {
            return Convert.ToInt32(value, CULTURE);
        }

        /// <summary>
        /// Convert a xml-format string to a bool value.
        /// </summary>
        /// <param name="value">Original bool value that will be converted to a string.</param>
        public static bool ToBoolean(string value)
        {
            return Convert.ToBoolean(value, CULTURE);
        }

        /// <summary>
        /// Convert a xml-format string to a uint32 value.
        /// </summary>
        /// <param name="value">Original uint32 value that will be converted to a string.</param>
        public static uint ToUInt(string value)
        {
            return Convert.ToUInt32(value, CULTURE);
        }
        #endregion

        #region Providing convert functions to string to force localalization settings
        /// <summary>
        /// Convert a float value to an xml-format string.
        /// </summary>
        /// <param name="value">Original float value that will be converted to a string.</param>
        public static String ToString(float value)
        {
            return value.ToString(CULTURE);
        }

        /// <summary>
        /// Convert a double value to an xml-format string.
        /// </summary>
        /// <param name="value">Original double value that will be converted to a string.</param>
        public static String ToString(double value)
        {
            return value.ToString(CULTURE);
        }

        /// <summary>
        /// Convert an int value to an xml-format string.
        /// </summary>
        /// <param name="value">Original int value that will be converted to a string.</param>
        public static String ToString(int value)
        {
            return value.ToString(CULTURE);
        }

        /// <summary>
        /// Convert a uint value to an xml-format string.
        /// </summary>
        /// <param name="value">Original uint value that will be converted to a string.</param>
        public static String ToString(uint value)
        {
            return value.ToString(CULTURE);
        }

        /// <summary>
        /// Convert a uint value to an xml-format string.
        /// </summary>
        /// <param name="value">Original uint value that will be converted to a string.</param>
        public static String ToString(bool value)
        {
            return value.ToString(CULTURE).ToLower();
        }
        #endregion
    }
}