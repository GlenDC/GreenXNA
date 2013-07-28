#region File Description
//-----------------------------------------------------------------------------
// ISerializer.cs
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
    /// The serializator, implementing this interface, 
    /// should be writtin, so that is usuable within a 'using' block.
    /// <typeparam name="TValue">Type of the container</typeparam>
    /// </summary>
    interface ISerializer<TValue> : IDisposable 
    {
        /// <summary>
        /// Write to a new file, only if it doesn't exist yet
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to a new file was succesful</returns>
        bool TryToWrite(TValue container);
        /// <summary>
        /// Write to an excisting file.
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to an existing file was succesful</returns>
        bool TryToOverwrite(TValue container);
        /// <summary>
        /// Write to a new or an excisting file.
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if the serialization to a file was succesfull.</returns>
        bool Write(TValue container);
        /// <summary>
        /// Append to an excesting file, new content from a container
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if the serialization of the new content at the end of an existing file was succesfull.</returns>
        bool Append(TValue container);
    }
}
