#region File Description
//-----------------------------------------------------------------------------
// INIFileSmartParser.cs
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
using System.IO;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.INI
{
    /// <summary>
    /// A class, based on the IFileParser interface, 
    /// to parse INI formatted files. 
    /// </summary>
    public class INIFileSmartParser : INIFileParser, ISmartFileParser<INIFileContainer>
    {

        /// <summary>
        /// Open the file. This should be done in the heading of your using body.
        /// </summary>
        /// <param name="file">name of the file, including the extension</param>
        /// <param name="dictionary">dynamic path to the INI file.</param>
        public INIFileSmartParser(string file, string path)
            : base(file, path)
        {
        }

        protected override void ProcessElement(INIFileContainer container, string key, string value)
        {
            container[m_CurrentRoot].Add(key, new TObject(value));
        }

        public bool ReadSmart(INIFileContainer container)
        {
            throw new NotImplementedException();
        }
    }
}