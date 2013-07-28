#region File Description
//-----------------------------------------------------------------------------
// ITemplateType.cs
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

using GreenXNA.GreenInterfaces;
using GreenXNA.GreenHelpers;
using GreenXNA.IO;
#endregion

namespace GreenXNA.Generic
{
    public interface ITemplateType : IDisposable, ICloneable, IEquatable<object>
    {
        /// <summary>
        /// Public get interface 
        /// </summary>
        T Get<T>();

        /// <summary>
        /// Public set interface
        /// </summary>
        /// <param name="value">value to assign to the value of this type</param>
        void Set(object value);

        /// <summary>
        /// Convert the value of this type to a string
        /// </summary>
        /// <returns>string, containing the type's value</returns>
        string ToString();
    }
}