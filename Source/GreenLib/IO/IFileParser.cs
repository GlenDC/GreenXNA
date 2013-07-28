#region File Description
//-----------------------------------------------------------------------------
// IFileParser.cs
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
using System.Runtime.Serialization;
using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// Defines the interface, to serialize a FileContainer to a file.
    /// The fileparser, implementing this interface, 
    /// should be writting, so that is usuable within a 'using' block.
    /// <typeparam name="TValue">Type of the container</typeparam>
    /// <typeparam name="TElementValue">Type of the parsed key and value</typeparam>
    /// </summary>
    public interface IFileParser<TValue, TElementValue> : IDisposable
    {
        /// <summary>
        /// Parse the content of a file and save it in the container        
        /// </summary>
        /// <param name="container">container to save the file content to</param>
        void Read(TValue container);
    }
}