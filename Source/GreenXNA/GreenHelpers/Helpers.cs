#region File Description
//-----------------------------------------------------------------------------
// Helpers.cs
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
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace GreenXNA.GreenHelpers
{
    public static class Helpers
    {
        /// <summary>
        /// Generate hash value based on a string value
        /// </summary>
        /// <param name="str">string value to be converted to a hash value</param>
        /// <returns>generated hash value based on the input parameter (string)</returns>
        public static uint GenerateHash(string str)
        {
            uint hash = 0;
            for (int i = 0; i < str.Count(); ++i)
                hash = 65599 * hash + str[i];
            return hash ^ (hash >> 16);
        }
    }
}