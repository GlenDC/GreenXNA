#region File Description
//-----------------------------------------------------------------------------
// INIFileParser.cs
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
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.XML
{
    /// <summary>
    /// A class, based on the IFileParser interface, 
    /// to parse INI formatted files. 
    /// </summary>
    public class XMLFileParser : BaseFileIO, IFileParser<XMLFileContainer, string>
    {
        /// <summary>
        /// Open the file. This should be done in the heading of your using body.
        /// </summary>
        /// <param name="path">dynamic path to the XML file.</param>
        /// <param name="file">name of the file, including the extension</param>
        public XMLFileParser(string path, string file)
            : base(path, file)
        {
        }

        public void Read(XMLFileContainer container)
        {
            if (File.Exists(m_File))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    container.DocumentType = xmlDoc.DocumentType;

                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.IgnoreComments = true;
                    XmlReader reader = XmlReader.Create(m_File, readerSettings);
                    xmlDoc.Load(reader);

                    XmlNode node = xmlDoc.DocumentElement;
                    if (node != null)
                    {
                        container.Name = node.Name;
                        GetAttributes(node.Attributes, container.Attributes);
                        node = node.FirstChild;
                        if (node != null)
                        {
                            do
                            {
                                uint layer_id = container.Add(node.Name, new XMLElement());
                                GetElement(node, container.GetValue(node.Name, layer_id));
                                node = node.NextSibling;
                            } while (node != null);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new FileNotFoundException("Unable to locate " + m_File);
            }
        }

        /// <summary>
        /// Nothing to dispose
        /// </summary>
        public void Dispose()
        {
            // nothing to dispose
        }

        private void GetAttributes(XmlAttributeCollection xml_attributes, GenericTypeDictionary attributes_container)
        {
            if (xml_attributes != null && xml_attributes.Count != 0)
            {
                foreach (XmlAttribute attribute in xml_attributes)
                {
                    attributes_container.Add(attribute.Name, new TObject(attribute.InnerText));
                }
            }
        }

        private void GetElement(XmlNode node, XMLElement element)
        {
            //Get properties of layer
            GetAttributes(node.Attributes, element.Attributes);

            XmlNode newNode = node.FirstChild;
            if ((node.ChildNodes.Count >= 1 || (newNode != null && newNode.ChildNodes.Count > 0)) && newNode.Name != "#text")
            {
                do
                {
                    uint element_id = element.Add(newNode.Name, new XMLElement());
                    GetElement(newNode, element.GetValue(newNode.Name, element_id));
                    newNode = newNode.NextSibling;
                } while (newNode != null);
            }
            else
            {
                //get innervalue
                element.Value = new TObject(node.InnerText);
            }
        }
    }
}
