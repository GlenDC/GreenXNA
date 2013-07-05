#region File Description
//-----------------------------------------------------------------------------
// DebugParser.cs
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

namespace GreenXNA.Serialize.XML
{
    /// <summary>
    /// Static class that can be used to write a parsed XML File
    /// to a debug output window. This class should only be used in a debug build. 
    /// </summary>
    static public class DebugParser
    {
        /// <summary>
        /// Main public function to get a mirror of your xml file into the debugging screen. 
        /// </summary>
        /// <param name="file">XMLFile object to be used as input, to written.</param>
        static public void OutputFileList(XMLFile file)
        {
#if DEBUG
            System.Diagnostics.Debug.Write("======================================== BEGIN XML ==============================================\n");
            int tabs = 0;
            OutputElement(GetTabSpace(tabs), file.Root.Name, file.Root.Attributes, false, true);
            ++tabs;
            if (file.LayerList != null && file.LayerList.Count != 0)
            {
                foreach (KeyValuePair<string, List<ParserLayer>> layers in file.LayerList)
                {
                    foreach (ParserLayer layer in layers.Value)
                    {
                        OutputLayer(tabs, layers.Key, layer);
                    }
                }
            }
            --tabs;
            System.Diagnostics.Debug.Write(GetTabSpace(tabs) + "</" + file.Root.Name + ">\n");
            System.Diagnostics.Debug.Write("======================================== END XML ==============================================\n");
#endif
        }

        /// <summary>
        /// Outputting an element/node
        /// can be used seperetaly to test if you have the correct element, 
        /// which is the main reason why it is publicly available as well!
        /// </summary>
        /// <param name="tabSpace">tabspaces to be written infront of the lines</param>
        /// <param name="elementName">name of the element/layer</param>
        /// <param name="elementProperties">properties of the element/layer</param>
        /// <param name="closeTag">true if the layer doesn't have children</param>
        /// <param name="addNewLine">true if a newline character should be added to the end</param>
        static public void OutputElement(string tabSpace, string elementName, List<KeyValuePair<string, string>> elementProperties, bool closeTag, bool addNewLine)
        {
#if DEBUG
            System.Diagnostics.Debug.Write(tabSpace + "<" + elementName + " ");
            if (elementProperties != null)
            {
                foreach (KeyValuePair<string, string> pair in elementProperties)
                {
                    System.Diagnostics.Debug.Write(pair.Key + "='" + pair.Value + "' ");
                }
            }
            System.Diagnostics.Debug.Write((closeTag ? "/>" : ">") + (addNewLine ? "\n" : ""));
#endif
        }

        /// <summary>
        /// Outputting a layer to the output node ( in short: a node which has children ) 
        /// this function is made so that it's possible to have an output of unlimited amount fof children.
        /// </summary>
        /// <param name="tab">amount of tabspaces to be added in front of the outputted layer</param>
        /// <param name="name">name of the layer</param>
        /// <param name="layer">layer to be used as input for this output function</param>
        static public void OutputLayer(int tab, string name, ParserLayer layer)
        {
#if DEBUG
            if (XMLWriter.CheckIfLayerHasChildren(layer))
            {
                OutputElement(GetTabSpace(tab), name, layer.Attributes, false, true);
                ++tab;
                foreach (KeyValuePair<string, List<ParserLayer>> children in layer.Children)
                {
                    foreach (ParserLayer child in children.Value)
                    {
                        OutputLayer(tab, children.Key, child);
                    }
                }
                --tab;
                System.Diagnostics.Debug.Write(GetTabSpace(tab) + "</" + name + ">\n");
            }
            else
            {
                if (layer.InnerValue == "")
                    OutputElement(GetTabSpace(tab), name, layer.Attributes, true, true);
                else
                {
                    OutputElement(GetTabSpace(tab), name, layer.Attributes, false, false);
                    System.Diagnostics.Debug.Write(layer.InnerValue + "</" + name + ">\n");
                }
            }
#endif
        }

        /// <summary>
        /// Transform an integer of x amount of tabspaces to a real mirrored tab string
        /// This is way more performat than removing and adding this character
        /// on a text based way.
        /// </summary>
        /// <param name="spaces">amount of tab characters to be outputted as a string</param>
        /// <returns>a string containing the necacary tab characters</returns>
        static public string GetTabSpace(int spaces)
        {
            string tabs = "";
            for (int i = 0; i < spaces; ++i)
                tabs += "   ";
            return tabs;
        }
    }
}