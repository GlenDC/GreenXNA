#region File Description
//-----------------------------------------------------------------------------
// XMLFileContainer.cs
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
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.XML
{
    /// <summary>
    /// A class, to save INIFileContent in
    /// </summary>
    public class XMLFileContainer : FileContainer<XMLElement>
    {
        public XmlDocumentType DocumentType { get; set; }
        public string Name { get; set; }
        public GenericTypeDictionary Attributes { get; private set; }

        /// <summary>
        /// Save the default file's name, including extension, and 
        /// dynamic directory path as members of this class.
        /// </summary>
        /// <param name="file">name, including the extension, of the file</param>
        /// <param name="path">dynamic directory path of the file</param>
        public XMLFileContainer(string file, string path)
            : base(file, path)
        {
            Name = null;
            DocumentType = null;
            Attributes = new GenericTypeDictionary();
        }

        /// <summary>
        /// Load the file via the XML Parser.
        /// </summary>
        /// <param name="path">dynamic directory path of the file</param>
        /// <param name="file">name of the file, including the name extension</param>
        /// <returns>true if loaded without any problems</returns>
        protected override void LoadFile(string path, string file)
        {
            using (XMLFileParser parser = new XMLFileParser(path, file))
            {
                parser.Read(this);
            }
        }

        protected override void SaveFile(string path, string file)
        {
            using (XMLFileSerializer serializer = new XMLFileSerializer(path, file))
            {
                serializer.Write(this);
            }
        }
    }
}