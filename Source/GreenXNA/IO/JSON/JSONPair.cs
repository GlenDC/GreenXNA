#region File Description
//-----------------------------------------------------------------------------
// JSONPair.cs
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
using GreenXNA.Generic;
#endregion

namespace GreenXNA.IO.JSON
{
    /// <summary>
    /// JSON PAIR
    /// </summary>
    public class JSONPair : JSONBaseValue
    {
        public readonly GenericTypeJSONString Name;
        public readonly GenericType Value;

        public JSONPair(string name, GenericType value)
            : base(JSON_VALUE_TYPE.json_pair)
        {
            Name = new GenericTypeJSONString();
            Name.Value = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name.ToString() + " : " + Value.ToString();
        }

        /// <summary>
        /// Clone a JSONPair to a new object
        /// </summary>
        /// <returns>independend clone of the original JSONSeperator</returns>
        public override JSONBaseValue Clone()
        {
            JSONPair element = new JSONPair((string)Name.Value, Value.Clone());
            return element;
        }

        protected override void Destroy() { }
    }
}
