#region File Description
//-----------------------------------------------------------------------------
// JSONArray.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
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
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Collections.Generic;

using GreenXNA.GreenInterfaces;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.Serialize.JSON
{
    /// <summary>
    /// struct representing a JSONArray
    /// syntax: string [ value , ... , value ]
    /// </summary>
    public class JSONArray : JSONBaseValue
    {
        /// <summary>
        /// List of Generic Typed Objects
        /// </summary>
        public List<GenericType> Values;

        public JSONArray()
            : base(JSON_VALUE_TYPE.json_array)
        {
            Values = new List<GenericType>();
        }

        /// <summary>
        /// Clone a JSONValue to a new object
        /// </summary>
        /// <returns>independend clone of the original JSONObject</returns>
        public override JSONBaseValue Clone()
        {
            JSONArray element = new JSONArray();
            for (int i = 0; i < Values.Count; ++i)
            {
                element.Values.Add(Values[i].Clone());
            }
            return element;
        }

        public override string ToString()
        {
            string result = "[ ";

            for (int i = 0; i < Values.Count; ++i)
            {
                result += Values[i].ToString();
                if (i < Values.Count - 1)
                {
                    result += JSONParser.JSON_SEPERATOR;
                }
            }

            return result + " ]";
        }
    }
}