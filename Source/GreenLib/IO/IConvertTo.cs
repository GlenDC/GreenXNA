#region File Description
//-----------------------------------------------------------------------------
// IConvertTo.cs
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
    /// Interface to implement a class that allows you to convert a 
    /// type of container or the content of that container to another type of container.
    /// </summary>
    /// <typeparam name="TSource">type of the source container</typeparam>
    /// <typeparam name="TTarget">type of the target container</typeparam>
    interface IConvertTo<TSource, TTarget>
    {
        /// <summary>
        /// Generates a new target container and fills it with the 
        /// source container variables.
        /// </summary>
        /// <param name="source">source container</param>
        /// <returns>returns the created target container, containing the values of the source</returns>
        TTarget ConvertTo(TSource source);
        /// <summary>
        /// Generates a new target container and fills it with the
        /// target container variables.
        /// </summary>
        /// <param name="source">source container to copy values from</param>
        /// <param name="target">target container to copy values to</param>
        void ConvertTo(TSource source, TTarget target);
    }
}
