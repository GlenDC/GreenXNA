#region File Description
//-----------------------------------------------------------------------------
// XMLWriter.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
// More information and details can be found at http://greenxna.glendc.com/
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
using System.Collections.Generic;
#endregion

namespace GreenXNA.GreenXML
{
    /// <summary>
    /// Class used to serialize an xml file. 
    /// The requirements for this is that you already have a parser. 
    /// This is mostly used when saving changes of an excisting xml file.
    /// </summary>
    public static class XMLWriter
    {
        /// <summary>
        /// Main (public) function to write to an xml parser, given a parser which
        /// was loaded via the Parser class and may have been edited with new information. 
        /// </summary>
        /// <param name="parser">input to be written to an outputstream</param>
        static public void Write(Parser parser)
        {
            using (XmlWriter writer = XmlWriter.Create(parser.DocumentPath))
            {
                int tabSpace = 0;
                writer.WriteStartDocument();
                writer.WriteRaw("\n");
                writer.WriteStartElement(parser.GetRootInformation().Name);
                foreach(KeyValuePair<string,string> attribute in parser.GetRootInformation().Attributes)
                    writer.WriteAttributeString(attribute.Key, attribute.Value);
                ++tabSpace;
                foreach (KeyValuePair<string, List<ParserLayer>> layerPerName in parser.GetRoot().LayerList)
                {
                    foreach (ParserLayer childLayer in layerPerName.Value)
                    {
                        WriteLayer(writer, layerPerName.Key, childLayer,tabSpace);
                    }
                }
                --tabSpace;
                writer.WriteRaw("\n" + DebugParser.GetTabSpace(tabSpace));
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// This function allows you to write a layer and in a recursive way, its children. 
        /// </summary>
        /// <param name="file">used as outputstream</param>
        /// <param name="name">name of the layer</param>
        /// <param name="layer">layer to be used as input</param>
        /// <param name="tabSpace">amount of tabspaces to be written in front of the output</param>
        static private void WriteLayer(XmlWriter file, string name, ParserLayer layer, int tabSpace)
        {
            file.WriteRaw("\n" + DebugParser.GetTabSpace(tabSpace));
            file.WriteStartElement(name);
            if (layer.Attributes != null)
            {
                foreach (KeyValuePair<string, string> attribute in layer.Attributes)
                    file.WriteAttributeString(attribute.Key, attribute.Value);
            }
            ++tabSpace;
            if (CheckIfLayerHasChildren(layer))
            {
                foreach (KeyValuePair<string, List<ParserLayer>> layerPerName in layer.Children)
                {
                    foreach (ParserLayer childLayer in layerPerName.Value)
                    {
                        WriteLayer(file, layerPerName.Key, childLayer, tabSpace);
                    }
                }
            }
            else if (layer.InnerValue != "")
                file.WriteValue(layer.InnerValue);
            --tabSpace;
            if (CheckIfLayerHasChildren(layer))
                file.WriteRaw("\n" + DebugParser.GetTabSpace(tabSpace));
            file.WriteEndElement();
        }

        /// <summary>
        /// Check if a layer has children.
        /// </summary>
        /// <param name="layer">layer to be checked</param>
        /// <returns>retrns true if the layer has children</returns>
        static public bool CheckIfLayerHasChildren(ParserLayer layer)
        {
            if (layer.Children != null && layer.Children.Count > 0)
            {
                return true;

                #region this code was wrong. Unfold to read the comments why...
                // Why was it wrong?
                // We only need to know if the current layer had children...
                // what this code did, was checking if a child within this layer has children
                // It looked like it was working, untill you have a node with children, 
                // which children don't have children.

                //foreach (KeyValuePair<string, List<ParserLayer>> layerPerName in layer.Children)
                //{
                //    foreach (ParserLayer childLayer in layerPerName.Value)
                //    {
                //        if (childLayer.Children != null)
                //            return true;
                //    }
                //}
                #endregion
            }
            return false;
        }
    }
}