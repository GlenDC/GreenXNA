#region File Description
//-----------------------------------------------------------------------------
// IFileDictionary.cs
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
using System.Collections;
using System.Collections.Generic;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// Wrapper interface for a specific dictionary interface
    /// </summary>
    interface IFileDictionary : IDictionary<Tuple<uint, uint, string>, ITemplateType>, IDisposable, ICloneable
    {
        /// <summary>
        /// Get the last value of this dictionary
        /// </summary>
        /// <param name="key">get the last value of which the pair's Key is equal to this parameter.</param>
        /// <returns>last value if found; null if not found.</returns>
        ITemplateType GetLastValue(HashValue key);
        /// <summary>
        /// Try get the last value of this dictionary
        /// </summary>
        /// <param name="key">get the last value of which the pair's Key is equal to this parameter.</param>
        /// <returns>last value if found; null if not found.</returns>
        ITemplateType TryGetLastValue(HashValue key);
        /// <summary>
        /// Get a list of values of this dictionary.
        /// </summary>
        /// <param name="key">get the list of values of which the pair's Key is equal to this parameter.</param>
        /// <returns>List of values; if none found it will be empty</returns>
        List<ITemplateType> GetValues(HashValue key);
        /// <summary>
        /// Get a list of values of this dictionary.
        /// </summary>
        /// <param name="key">get the list of values of which the pair's Key is equal to this parameter.</param>
        /// <returns>List of values; if none found it will be empty</returns>
        List<ITemplateType> TryGetValues(HashValue key);
        /// <summary>
        /// Add an array to the container.
        /// </summary>
        /// <param name="array">array of pairs to be added</param>
        /// <param name="n">amount of pairs in the array</param>
        void AddRange(KeyValuePair<string, ITemplateType>[] array, int n);
        /// <summary>
        /// Add (part of) an array to the container.
        /// </summary>
        /// <param name="array">array of pairs to be added</param>
        /// <param name="n">amount of pairs in the array</param>
        /// <param name="index">index to start adding from</param>
        void AddRange(KeyValuePair<string, ITemplateType>[] array, int n, int index);
    }
}