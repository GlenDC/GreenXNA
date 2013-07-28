#region File Description
//-----------------------------------------------------------------------------
// ConverterExtensions.cs
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

using Microsoft.Xna.Framework;

using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// This helper class provides convertions for XNA specific build-in Type Objects.
    /// </summary>
    public static class XNAConverter
    {
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
            int index = value.IndexOf(';', 0);
            vec.X = Converter.ToFloat(value.Substring(0, index));
            vec.Y = Converter.ToFloat(value.Substring(++index, value.Length - index));
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
            vec.X = Converter.ToFloat(value.Substring(0, index));
            int index2 = value.IndexOf(';', ++index);
            vec.Y = Converter.ToFloat(value.Substring(index, index2 - index));
            vec.Z = Converter.ToFloat(value.Substring(++index2, value.Length - index2));
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
            vec.X = Converter.ToFloat(value.Substring(0, index));
            int index2 = value.IndexOf(';', ++index);
            vec.Y = Converter.ToFloat(value.Substring(index, index2 - index));
            index = value.IndexOf(';', ++index2);
            vec.Z = Converter.ToFloat(value.Substring(index2, index - index2));
            vec.W = Converter.ToFloat(value.Substring(++index, value.Length - index));
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
            qua.X = Converter.ToFloat(value.Substring(0, index));
            int index2 = value.IndexOf(';', ++index);
            qua.Y = Converter.ToFloat(value.Substring(index, index2 - index));
            index = value.IndexOf(';', ++index2);
            qua.Z = Converter.ToFloat(value.Substring(index2, index - index2));
            qua.W = Converter.ToFloat(value.Substring(++index, value.Length - index));
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
        public static string ToString(Vector2 vec)
        {
            return Converter.ToString(vec.X) + ";" + Converter.ToString(vec.Y);
        }

        /// <summary>
        /// Convert a vector3 value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original vector value that will be converted to a string.</param>
        public static string ToString(Vector3 vec)
        {
            return Converter.ToString(vec.X) + ";" + Converter.ToString(vec.Y) + ";" + Converter.ToString(vec.Z);
        }

        /// <summary>
        /// Convert a vector4 value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original vector value that will be converted to a string.</param>
        public static string ToString(Vector4 vec)
        {
            return Converter.ToString(vec.X) + ";" + Converter.ToString(vec.Y) + ";" + 
                Converter.ToString(vec.Z) + ";" + Converter.ToString(vec.W);
        }

        /// <summary>
        /// Convert a quaternion value to a xml-format string.
        /// </summary>
        /// <param name="vec">Original quaternion value that will be converted to a string.</param>
        public static string ToString(Quaternion qua)
        {
            return Converter.ToString(qua.X) + ";" + Converter.ToString(qua.Y) + ";" + 
                Converter.ToString(qua.Z) + ";" + Converter.ToString(qua.W);
        }
        #endregion
    }
}