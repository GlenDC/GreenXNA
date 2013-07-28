#region File Description
//-----------------------------------------------------------------------------
// BaseFileIO.cs
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

#region using statements
using System;
#endregion

namespace GreenXNA.IO
{
    public abstract class BaseFileIO
    {
        /// <summary>
        /// name of the file to be parsed, including the extension
        /// </summary>
        protected string m_FileName;
        /// <summary>
        /// path to the ini file (dynamic)
        /// </summary>
        protected string m_DirectoryPath;
        /// <summary>
        /// combine the directory and filename to get the full name
        /// </summary>
        protected string m_File { get { return m_DirectoryPath + m_FileName; } }
        /// <summary>
        /// constructor, to save the filename and path.
        /// <param name="file">file's name, including the extension.</param>
        /// <param name="path">dynamic path to the file</param>
        /// </summary>
        public BaseFileIO(string path, string file) 
        {
            m_DirectoryPath = path;
            m_FileName = file;
        }
    }
}
