#region File Description
//-----------------------------------------------------------------------------
// IParseJumper.cs
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
    /// Defines the interface, to allow to jump to a certain part of a file
    /// </summary>
    interface IParseJumper
    {
        /// <summary>
        /// Jump to an identifier in the file
        /// </summary>
        /// <typeparam name="T">type of the identifier to search for</typeparam>
        /// <param name="identifier">value to look for in the text</param>
        void JumpTo<T>(T identifier);
        /// <summary>
        /// Jump to a sequence of identifiers in the file
        /// </summary>
        /// <typeparam name="T">type of the identifier to search for</typeparam>
        /// <param name="identifiers">sequence of values to look for in the text</param>
        /// <param name="n">amount of identifiers in the array sequence</param>
        void JumpTo<T>(T[] identifiers, uint n);
    }
}
