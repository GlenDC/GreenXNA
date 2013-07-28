#region File Description
//-----------------------------------------------------------------------------
// HashValue.cs
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
    public sealed class HashValue : IHashValue<HashValue, ulong, string>
    {
        /// <summary>
        /// unsigned long, generated automaticly, representing the original string value.
        /// </summary>
        private ulong m_Key;
        /// <summary>
        /// Original value saved as an HashValue.
        /// </summary>
        private string m_Value;

        /// <summary>
        /// Key of the HashValue, this will be used to compare the HashValue
        /// </summary>
        public ulong Key
        {
            get { return m_Key; }
        }

        /// <summary>
        /// Original value of the HashValues, this should be a string in most cases.
        /// </summary>
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                SetValue(value);
            }
        }

        /// <summary>
        /// Compare two HashValues, to define their order.
        /// </summary>
        /// <param name="hash">other hashValue to compare to this one.</param>
        /// <returns>Less then zero if this value is lower, zero if equal or more then zero if higher</returns>
        public int CompareTo(HashValue hash)
        {
            return Key.CompareTo(Key);
        }

        /// <summary>
        /// Check if two hashValues are equal
        /// </summary>
        /// <param name="other">other hashValue to compare to this one.</param>
        /// <returns>true if two hashValues are equal</returns>
        public bool Equals(HashValue other)
        {
            return Key.Equals(other.Key);
        }

        /// <summary>
        /// Assign a new value and generate a hash id for it.
        /// </summary>
        /// <param name="value">original value to be saved</param>
        public void SetValue(string value)
        {
            m_Value = value;
            m_Key = Helpers.GenerateLongHash(m_Value);
        }
    }
}