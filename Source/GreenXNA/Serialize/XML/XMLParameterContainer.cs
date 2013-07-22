#region File Description
//-----------------------------------------------------------------------------
// XMLParameterContainer.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
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
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.Serialize.XML
{
    /// <summary>
    /// Automatic Container Class with a pre-defined XML format, so that we can 
    /// automaticly load and read them
    /// Also refreshing runtime options are embedded in this class!
    /// </summary>
    public class XMLParameterContainer : ParameterContainer
    {
        /// <summary>
        /// Create the smart container and Load the file.
        /// </summary>
        /// <param name="fileName">Name of the XML file to use within the smart container.</param>
        public XMLParameterContainer(string fileName, string filePath)
            : base (fileName, filePath, FILE_TYPES.FILE_XML)
        {
        }

        /// <summary>
        /// Reload the XML file. This allows on run time parameter editing. 
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            XMLParser containerParser = new XMLParser(_FileName, _FilePath);
            containerParser.CopyContentToParameterContainer(ref _Parameters);
        }
    }
}