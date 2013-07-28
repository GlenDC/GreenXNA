#region File Description
//-----------------------------------------------------------------------------
// IHashValue.cs
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
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// Interface for a Hashvalue. 
    /// </summary>
    /// <typeparam name="T">Type of the HashValue class.</typeparam>
    /// <typeparam name="U">Type of the Key of the HashValue.</typeparam>
    /// <typeparam name="V">Type of the Value of the HashValue.</typeparam>
    interface IHashValue<T, U, V> : IComparable<T>,  IEquatable<T>
    {  
        /// <summary>
        /// Key of the HashValue, this will be used to compare the HashValue
        /// </summary>
        U Key { get; }
        /// <summary>
        /// Original value of the HashValues, this should be a string in most cases.
        /// </summary>
        V Value { get; set; }

        /// <summary>
        /// Assign a new value and generate a hash id for it.
        /// </summary>
        /// <param name="value">original value to be saved</param>
        void SetValue(V value);
    }
}