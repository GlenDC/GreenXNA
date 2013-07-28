#region File Description
//-----------------------------------------------------------------------------
// JSONBaseValue.cs
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
    /// base value defining the commen content,
    /// found in every type, found in a JSON file.
    /// </summary>
    public abstract class JSONBaseValue : IClone<JSONBaseValue>, IDisposable
    {
        public readonly JSON_VALUE_TYPE ValueType;

        public JSONBaseValue(JSON_VALUE_TYPE type)
        {
            ValueType = type;
        }

        /// <summary>
        /// Convert the value to a json correct string
        /// </summary>
        /// <returns>string correct version</returns>
        public abstract new string ToString();

        /// <summary>
        /// Clone a JSONPair to a new object
        /// </summary>
        /// <returns>independend clone of the original JSONSeperator</returns>
        public abstract JSONBaseValue Clone();

        public void Dispose()
        {
            Destroy();
        }

        protected abstract void Destroy();
    }
}
