#region File Description
//-----------------------------------------------------------------------------
// JSONObject.cs
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
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Collections.Generic;

using GreenXNA.GreenInterfaces;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.JSON
{
    /// <summary>
    /// struct representing a JSONObject
    /// syntax: { string:value , ... , string:value }
    /// </summary>
    public class JSONObject : JSONBaseValue
    {
        /// <summary>
        /// List of Generic Typed Objects
        /// </summary>
        public List<JSONBaseValue> Values { get; protected set; }

        public JSONObject()
            : base(JSON_VALUE_TYPE.json_object)
        {
            Values = new List<JSONBaseValue>();
        }

        /// <summary>
        /// Clone a JSONValue to a new object
        /// </summary>
        /// <returns>independend clone of the original JSONObject</returns>
        public override JSONBaseValue Clone()
        {
            JSONObject element = new JSONObject();
            for (int i = 0; i < Values.Count; ++i)
            {
                element.Values.Add(Values[i]);
            }
            return element;
        }

        public override string ToString()
        {
            string result = "{ ";

            for (int i = 0; i < Values.Count; ++i)
            {
                string append = Values[i].ToString();
                result += append;
                if (i < Values.Count - 1)
                {
                    result += JSONParser.JSON_SEPERATOR;
                }
            }

            return result + " }";
        }

        protected override void Destroy()
        {
            Values.ForEach((JSONBaseValue value) => { value.Dispose(); });
            Values.Clear();
            Values = null;
        }
    }
}