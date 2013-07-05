#region File Description
//-----------------------------------------------------------------------------
// XMLConvert.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
// More information and details can be found at http://greenxna.glendc.com/
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
#endregion

namespace GreenXNA.GreenXML
{
    /// <summary>
    /// Convert a value from or to a string, using
    /// one of the static functions.
    /// </summary>
    public static class XMLConvert
    {
        /// <summary>
        /// Culture will be used to force a certain format. 
        /// TODO: Implement the cultureinfo in a global *.ini file
        /// </summary>
        private static CultureInfo culture = new CultureInfo("en-US"); 

        // XML (string) => XNA's Vector classes
        // Supperted Vector classes: Vector2, Vector3 and Vector4 and Quaternion
        // Format in XML: x;y;z;w
        #region XML Saved vector transformed to a XNA Vector object
        /// <summary>
        /// Convert an XML String to a Vector2 value.
        /// </summary>
        /// <param name="value">Original value string defined in an xml format.</param>
        public static Vector2 ToVector2(string value)
        {
            Vector2 vec = new Vector2();
            int index = value.IndexOf(';',0);
            vec.X = XMLConvert.ToFloat(value.Substring(0, index));
            vec.Y = XMLConvert.ToFloat(value.Substring(++index,value.Length-index));
            return vec;
        }

        /// <summary>
        /// Convert an XML String to a Vector3 value.
        /// </summary>
        /// <param name="value">Original value string defined in an xml format.</param>
        public static Vector3 ToVector3(string value)
        {
            Vector3 vec = new Vector3();
            int index = value.IndexOf(';', 0);
            vec.X = XMLConvert.ToFloat(value.Substring(0, index));
            int index2 = value.IndexOf(';', ++index);
            vec.Y = XMLConvert.ToFloat(value.Substring(index, index2 - index));
            vec.Z = XMLConvert.ToFloat(value.Substring(++index2, value.Length - index2));
            return vec;
        }

        /// <summary>
        /// Convert an XML String to a Vector4 value.
        /// </summary>
        /// <param name="value">Original value string defined in an xml format.</param>
        public static Vector4 ToVector4(string value)
        {
            Vector4 vec = new Vector4();
            int index = value.IndexOf(';', 0);
            vec.X = XMLConvert.ToFloat(value.Substring(0, index));
            int index2 = value.IndexOf(';', ++index);
            vec.Y = XMLConvert.ToFloat(value.Substring(index, index2 - index));
            index = value.IndexOf(';', ++index2);
            vec.Z = XMLConvert.ToFloat(value.Substring(index2, index - index2));
            vec.W = XMLConvert.ToFloat(value.Substring(++index, value.Length - index));
            return vec;
        }

        /// <summary>
        /// Convert an XML String to a Quaternion value.
        /// </summary>
        /// <param name="value">Original value string defined in an xml format.</param>
        public static Quaternion ToQuaternion(string value)
        {
            Quaternion qua = new Quaternion();
            int index = value.IndexOf(';', 0);
            qua.X = XMLConvert.ToFloat(value.Substring(0, index));
            int index2 = value.IndexOf(';', ++index);
            qua.Y = XMLConvert.ToFloat(value.Substring(index, index2 - index));
            index = value.IndexOf(';', ++index2);
            qua.Z = XMLConvert.ToFloat(value.Substring(index2, index - index2));
            qua.W = XMLConvert.ToFloat(value.Substring(++index, value.Length - index));
            return qua;
        }
        #endregion

        // XNA's Vector Classes => XNA's String class => Linq => XML
        // Supperted Vector classes: Vector2, Vector3, Vector4 and Quaternion
        #region XNA vector to a XML syntax correct string ready to save to a text(xml) file
        /// <summary>
        /// Convert a vector2 value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original vector value that will be converted to a string.</param>
        public static string ToXMLString(Vector2 vec)
        {
            return ToString(vec.X) + ";" + ToString(vec.Y);
        }

        /// <summary>
        /// Convert a vector3 value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original vector value that will be converted to a string.</param>
        public static string ToXMLString(Vector3 vec)
        {
            return ToString(vec.X) + ";" + ToString(vec.Y) + ";" + ToString(vec.Z);
        }

        /// <summary>
        /// Convert a vector4 value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original vector value that will be converted to a string.</param>
        public static string ToXMLString(Vector4 vec)
        {
            return ToString(vec.X) + ";" + ToString(vec.Y) + ";" + ToString(vec.Z) + ";" + ToString(vec.W);
        }

        /// <summary>
        /// Convert a quaternion value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original quaternion value that will be converted to a string.</param>
        public static string ToXMLString(Quaternion qua)
        {
            return ToString(qua.X) + ";" + ToString(qua.Y) + ";" + ToString(qua.Z) + ";" + ToString(qua.W);
        }
        #endregion

        #region implementing default Convert so that we can force a correct setting!
        /// <summary>
        /// Convert a xml-format string to a float value.
        /// </summary>
        /// <param name="value">Original float value that will be converted to a string.</param>
        public static float ToFloat(string value)
        {
            return (float)Convert.ToDouble(value, culture);
        }

        /// <summary>
        /// Convert a xml-format string to a double value.
        /// </summary>
        /// <param name="value">Original double value that will be converted to a string.</param>
        public static double ToDouble(string value)
        {
            return Convert.ToDouble(value, culture);
        }

        /// <summary>
        /// Convert a xml-format string to an int value.
        /// </summary>
        /// <param name="value">Original int value that will be converted to a string.</param>
        public static int ToInt(string value)
        {
            return Convert.ToInt32(value, culture);
        }

        /// <summary>
        /// Convert a xml-format string to a bool value.
        /// </summary>
        /// <param name="value">Original bool value that will be converted to a string.</param>
        public static bool ToBoolean(string value)
        {
            return Convert.ToBoolean(value, culture);
        }

        /// <summary>
        /// Convert a xml-format string to a uint32 value.
        /// </summary>
        /// <param name="value">Original uint32 value that will be converted to a string.</param>
        public static uint ToUInt(string value)
        {
            return Convert.ToUInt32(value, culture);
        }
        #endregion

        #region Providing convert functions to string to force localalization settings
        /// <summary>
        /// Convert a float value to an xml-format string.
        /// </summary>
        /// <param name="value">Original float value that will be converted to a string.</param>
        public static String ToString(float value)
        {
            return value.ToString(culture);
        }

        /// <summary>
        /// Convert a double value to an xml-format string.
        /// </summary>
        /// <param name="value">Original double value that will be converted to a string.</param>
        public static String ToString(double value)
        {
            return value.ToString(culture);
        }

        /// <summary>
        /// Convert an int value to an xml-format string.
        /// </summary>
        /// <param name="value">Original int value that will be converted to a string.</param>
        public static String ToString(int value)
        {
            return value.ToString(culture);
        }

        /// <summary>
        /// Convert a uint value to an xml-format string.
        /// </summary>
        /// <param name="value">Original uint value that will be converted to a string.</param>
        public static String ToString(uint value)
        {
            return value.ToString(culture);
        }

        /// <summary>
        /// Convert a uint value to an xml-format string.
        /// </summary>
        /// <param name="value">Original uint value that will be converted to a string.</param>
        public static String ToString(bool value)
        {
            return value.ToString(culture);
        }
        #endregion
    }
}