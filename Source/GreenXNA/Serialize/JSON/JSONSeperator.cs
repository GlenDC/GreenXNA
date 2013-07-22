#region File Description
//-----------------------------------------------------------------------------
// JSONSeperator.cs
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
    /// JSON Seperator Type
    /// </summary>
    public class JSONSeperator : JSONBaseValue
    {
        public JSONSeperator()
            : base(JSON_VALUE_TYPE.json_seperator)
        { /* DO NOTHING */ }

        public override string ToString()
        {
            return "null";
        }

        /// <summary>
        /// Clone a JSONSeperator to a new object
        /// </summary>
        /// <returns>independend clone of the original JSONSeperator</returns>
        public override JSONBaseValue Clone()
        {
            JSONSeperator element = new JSONSeperator();
            return element;
        }
    }
}