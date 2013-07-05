#region File Description
//-----------------------------------------------------------------------------
// MathGeneral.cs
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

namespace GreenXNA.GreenMath
{
    public static class MathGeneral
    {
        /// <summary>
        /// round up or down, depending on the value
        /// </summary>
        /// <param name="originalValue">original value to be rounded</param>
        /// <param name="moduloValue">modulo value to check</param>
        /// <returns>rounded value</returns>
        public static float RoundToNearest(float originalValue, float moduloValue)
        {
            if (originalValue % moduloValue > moduloValue / 2.0f)
            {
                return originalValue + moduloValue - (originalValue % moduloValue);
            }
            else
            {
                return originalValue - (originalValue % moduloValue);
            }
        }
    }
}