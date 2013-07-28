#region File Description
//-----------------------------------------------------------------------------
// IFileIO.cs
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
    /// Small interface that defines methods, usable in classes
    /// that implement (de)serialization functionalities.
    /// </summary>
    interface IFileIO
    {
        /// <summary>
        /// Open an excisting file or start a new one
        /// </summary>
        /// <param name="file">name of the file, including extension</param>
        /// <param name="path">dynamic directory path of the file</param>
        /// <returns>true if succesfull</returns>
        bool Open(string file, string path);
        /// <summary>
        /// Close a file, so that i will become unlocked.
        /// </summary>
        /// <returns>true if succesfull</returns>
        bool Close();
    }
}