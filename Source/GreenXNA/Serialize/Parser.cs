#region File Description
//-----------------------------------------------------------------------------
// Parser.cs
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
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Collections.Generic;

using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.Serialize
{
    public enum FILE_STATE
    {
        FILE_NOT_LOADED = 0,
        FILE_CORRUPT = 1,
        FILE_OK = 2
    };

    public abstract class Parser
    {
        /// <summary>
        /// String to be used as a default "not found" string, 
        /// mostly as return values of functions.
        /// </summary>
        public const string NOT_FOUND = "NOT_FOUND";
        /// <summary>
        /// name of the file to be parsed
        /// </summary>
        protected string _FileName;
        /// <summary>
        /// Extension of the file to be parsed
        /// </summary>
        protected string _FileExtension;
        /// <summary>
        /// default path of xml files in the project.
        /// </summary>
        protected string _DirectoryPath;

        public FILE_STATE FileState { get; protected set; }
        /// <summary>
        /// get value, combining the path, path and file-extension
        /// </summary>
        public string DocumentPath { get { return _DirectoryPath + _FileName + _FileExtension; } }

        public Parser(string file, string path, string extension)
        {
            _FileName = file;
            _DirectoryPath = path;
            _FileExtension = extension;

            FileState = FILE_STATE.FILE_NOT_LOADED;
        }

        /// <summary>
        /// Change the directory path of the file
        /// </summary>
        /// <param name="path">path to be used as directory path</param>
        public void SetDirectory(string path)
        {
            _DirectoryPath = path;
        }

        //Abstract functions to be overriden:
        /// <summary>
        /// Read and save values always as a string, 
        /// converting still has to be done!
        /// </summary>
        /// <summary>
        /// Read and convert the values to their correct type in 
        /// an automated GreeNXNA-integreted way. 
        /// Note that this requires the values to be written in the correct format!
        /// </summary>
        public abstract void ReadSmart();
        public abstract void Save();
        public abstract void CopyContentToParameterContainer(ref Dictionary<uint, Dictionary<uint, string>> container);

        //Virtual functions to be overriden:
        public virtual void Read()
        {
            FileState = FILE_STATE.FILE_OK;
        }

        public virtual void Save(string newFileName)
        {
            _FileName = newFileName;
        }
    }
}
