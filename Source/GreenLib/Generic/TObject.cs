#region File Description
//-----------------------------------------------------------------------------
// TObject.cs
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
    public sealed class TObject : ITemplateType
    {
        object m_Value;

        public TObject(object value)
        {
            Set(value);
        }

        /// <summary>
        /// Dispose this object. Cleaning is guaranteed with this function.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// public interface to clone this class
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            object original = Get<object>();
            TObject copy = new TObject(original);
            return copy;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public new bool Equals(object other)
        {
            return other.Equals(m_Value);
        }

        /// <summary>
        /// Get the value
        /// </summary>
        /// <typeparam name="T">type of the value</typeparam>
        /// <returns>value of the element, if everything is ok</returns>
        public T Get<T>()
        {
            bool typeMatch = m_Value.GetType() == typeof(T);
            //TODO: embed error handling
            System.Diagnostics.Debug.Assert(typeMatch, "Types don't match!");
            return (T)m_Value;
        }

        /// <summary>
        /// Set the value
        /// </summary>
        /// <param name="value">assign value to this objecs' value</param>
        public void Set(object value)
        {
            m_Value = value;
        }

        /// <summary>
        /// Convert the value of this type to a string
        /// </summary>
        /// <returns>string, containing the type's value</returns>
        public override string ToString()
        {
            return m_Value.ToString();
        }
    }
}
