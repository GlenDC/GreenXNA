#region File Description
//-----------------------------------------------------------------------------
// XMLFileSerializer.cs
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
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.XML
{
    public class XMLFileSerializer : BaseFileIO, ISerializer<XMLFileContainer>
    {
        /// <summary>
        /// Open the file. This should be done in the heading of your using body.
        /// </summary>
        /// <param name="path">dynamic path to the INI file.</param>
        /// <param name="file">name of the file, including the extension</param>
        public XMLFileSerializer(string path, string file)
            : base(path, file)
        {
        }

        /// <summary>
        /// Write to a new file, only if it doesn't exist yet
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to a new file was succesful</returns>
        public bool TryToWrite(XMLFileContainer container)
        {
            if (!File.Exists(m_File))
            {
                return Write(container);
            }
            return false;
        }

        /// <summary>
        /// Write to an excisting file.
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to an existing file was succesful</returns>
        public bool TryToOverwrite(XMLFileContainer container)
        {
            if (File.Exists(m_File))
            {
                return Write(container);
            }
            return false;
        }

        protected void WriteAttributes(XmlWriter file, GenericTypeDictionary attributes)
        {
            var enumerator = attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                file.WriteAttributeString(current.Key.Item3, current.Value.Get<string>());
            }
        }

        protected void WriteChildren(XmlWriter file, 
            IEnumerator<KeyValuePair<Tuple<uint, uint, string>, XMLElement>> child, int tabSpace)
        {
            while (child.MoveNext())
            {
                var current = child.Current;
                WriteElement(file, current.Key.Item3, current.Value, tabSpace);
            }
        }

        protected void WriteElement(XmlWriter file, string name, XMLElement element, int tabSpace)
        {
            file.WriteRaw("\n" + GetTabSpace(tabSpace));
            file.WriteStartElement(name);
            WriteAttributes(file, element.Attributes);
            ++tabSpace;
            if (element.HasChildren)
            {
                WriteChildren(file, element.GetEnumerator(), tabSpace);
            }
            else
            {
                file.WriteValue(element.Value.Get<string>());
            }
            --tabSpace;
            if (element.HasChildren)
            {
                file.WriteRaw("\n" + GetTabSpace(tabSpace));
            }
            file.WriteEndElement();
        }

        /// <summary>
        /// Write to a new or an existing file.
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to an existing or new file was succesful</returns>
        public bool Write(XMLFileContainer container)
        {
            using (XmlWriter writer = XmlWriter.Create(m_File))
            {
                int tabSpace = 0;
                if (container.DocumentType != null)
                {
                    container.DocumentType.WriteContentTo(writer);
                }
                else
                {
                    writer.WriteStartDocument();
                }
                writer.WriteRaw("\n");
                writer.WriteStartElement(container.Name);
                WriteAttributes(writer, container.Attributes);
                ++tabSpace;
                WriteChildren(writer, container.GetEnumerator(), tabSpace);
                --tabSpace;
                writer.WriteRaw("\n" + GetTabSpace(tabSpace));
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return true;
        }

        /// <summary>
        /// Append to an exisiting file, new content from a container
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if the serialization of the new content at the end of an existing file was succesfull.</returns>
        public bool Append(XMLFileContainer container)
        {
            if (File.Exists(m_File))
            {
                Stream xmlFile = new FileStream(m_File, FileMode.Append);
                using (XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8))
                {
                    int tabSpace = 0;
                    writer.WriteStartElement(container.Name);
                    WriteAttributes(writer, container.Attributes);
                    ++tabSpace;
                    WriteChildren(writer, container.GetEnumerator(), tabSpace);
                    --tabSpace;
                    writer.WriteRaw("\n" + GetTabSpace(tabSpace));
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Nothing real to clean up here.
        /// </summary>
        public void Dispose()
        {
            // nothing to clean up here.
        }

        protected string GetTabSpace(int spaces)
        {
            string tabs = "";
            for (int i = 0; i < spaces; ++i)
                tabs += "   ";
            return tabs;
        }
    }
}