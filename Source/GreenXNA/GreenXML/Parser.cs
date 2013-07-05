#region File Description
//-----------------------------------------------------------------------------
// Parser.cs
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
    /// struct to be used, to contain the root element of an xml element.
    /// </summary>
    public struct XMLRootInformation
    {
        /// <summary>
        /// name of the Root element
        /// </summary>
        public string Name;
        /// <summary>
        /// attributes/properties of the Root element
        /// </summary>
        public List<KeyValuePair<string, string>> Attributes;

        /// <summary>
        /// Get an attribute/property of the Root element
        /// </summary>
        /// <param name="attributeName">name of the property</param>
        /// <returns>value of the wanted property, if not found it will return a default string</returns>
        public string GetAttribute(string attributeName)
        {
            foreach (KeyValuePair<string, string> pair in Attributes)
            {
                if (pair.Key == attributeName)
                    return pair.Value;
            }
            return Parser.ATTRIBUTE_NOT_FOUND;
        }

        /// <summary>
        /// Get an attribute/property of the Root element
        /// </summary>
        /// <param name="attributeName">name of the property</param>
        /// <param name="succesfull">true if element found</param>
        /// <returns>value of the wanted property, if not found it will return a default string</returns>
        public string GetAttribute(string attributeName, out bool succesfull)
        {
            string value = GetAttribute(attributeName);
            succesfull = value != Parser.ATTRIBUTE_NOT_FOUND;
            return value;
        }
        
        /// <summary>
        /// Set the attribute/property of the Root element
        /// Note that this can't be used to add a new attribute
        /// </summary>
        /// <param name="attributeName">name of the wanted attribute</param>
        /// <param name="value">new value to be assigned to the attribute</param>
        /// <returns>true if attribute has been found</returns>
        public bool SetAttribute(string attributeName, string value)
        {
            for(int i = 0 ; i < Attributes.Count ; ++i)
            {
                if (Attributes[i].Key == attributeName)
                {
                    Attributes[i] = new KeyValuePair<string, string>(Attributes[i].Key, value);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Public constructor, initializing the struct.
        /// </summary>
        /// <param name="name">name of the Root element</param>
        public XMLRootInformation(string name)
        {
            Name = name;
            Attributes = new List<KeyValuePair<string, string>>();
        }
    }

    /// <summary>
    /// struct, representing a parsed xml file.
    /// </summary>
    public struct XMLFile
    {
        /// <summary>
        /// Root elemet of the XML File
        /// </summary>
        public XMLRootInformation Root;
        /// <summary>
        /// Dictionary containing a list of the layers, ordered by their name.
        /// </summary>
        public Dictionary<string, List<ParserLayer>> LayerList;
    }

    /// <summary>
    /// struct, representing a layer ( == element/node + children )
    /// </summary>
    public struct ParserLayer
    {
        /// <summary>
        /// InnerValue of the node/element/layer
        /// </summary>
        public string InnerValue;
        /// <summary>
        /// List of attributes/properties
        /// </summary>
        public List<KeyValuePair<string, string>> Attributes;
        /// <summary>
        /// List of children, saved by name of layer
        /// </summary>
        public Dictionary<string, List<ParserLayer>> Children;

        /// <summary>
        /// Get the attribute of an element/property
        /// </summary>
        /// <param name="attributeName">name of the attribute</param>
        /// <returns>value of the wanted attribute/property, if not true: returns a default not found string</returns>
        public string GetAttribute(string attributeName)
        {
            foreach (KeyValuePair<string, string> pair in Attributes)
            {
                if (pair.Key == attributeName)
                    return pair.Value;
            }
            return Parser.ATTRIBUTE_NOT_FOUND;
        }

        /// <summary>
        /// Get the attribute of an element/property
        /// </summary>
        /// <param name="attributeName">name of the attribute</param>
        /// <param name="succesfull">true if the attribute has been found</param>
        /// <returns>value of the wanted attribute/property, if not true: returns a default not found string</returns>
        public string GetAttribute(string attributeName, out bool succesfull)
        {
            string value = GetAttribute(attributeName);
            succesfull = value != Parser.ATTRIBUTE_NOT_FOUND;
            return value;
        }

        /// <summary>
        /// Edit the value of an excisting attribute/property
        /// </summary>
        /// <param name="attributeName">name of the attribute/property</param>
        /// <param name="value">new value to be assigned to the attribute/property</param>
        /// <returns>true if an attribute has been found</returns>
        public bool SetAttribute(string attributeName, string value)
        {
            for (int i = 0; i < Attributes.Count; ++i)
            {
                if (Attributes[i].Key == attributeName)
                {
                    Attributes[i] = new KeyValuePair<string, string>(Attributes[i].Key, value);
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// class, to be used to parse xml files and save its content.
    /// This object can then be saved again via the XMLWriter class. 
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// String to be used as a default "not found" string, 
        /// mostly as return values of functions.
        /// </summary>
        public const string ATTRIBUTE_NOT_FOUND = "ATTRIBUTE NOT FOUND";
        /// <summary>
        /// name of the XML file to be parsed
        /// </summary>
        string _FileName;
        /// <summary>
        /// default path of xml files in the project.
        /// </summary>
        static string _DirectoryPath;
        /// <summary>
        /// get value, combining the path, path and xml file-extension
        /// </summary>
        public string DocumentPath { get { return _DirectoryPath + _FileName + ".xml"; } }
        /// <summary>
        /// XMLFile object, used to save the content of the xml file to.
        /// </summary>
        private XMLFile FileList;

        /// <summary>
        /// public constructor of the Parser, creating the XMLFile object.
        /// </summary>
        public Parser()
        {
            FileList = new XMLFile();
        }

        /// <summary>
        /// Set the global directory path of the xml file
        /// </summary>
        /// <param name="path">path to be used as global directory path</param>
        public static void SetDirectory(string path)
        {
            _DirectoryPath = path;
        }

        //======================================================================================
        //================== READ XML FILE =====================================================
        #region Read XML File Functions
        /// <summary>
        /// Function to be used to read an xml file
        /// </summary>
        /// <param name="name">name of the xml file</param>
        public void ReadXML(string name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            _FileName = name;
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(DocumentPath, readerSettings);
            xmlDoc.Load(reader);

            XmlNode node = xmlDoc.DocumentElement;
            if (node != null)
            {
                FileList.Root = new XMLRootInformation(node.Name);
                GetAttributes(node.Attributes, out FileList.Root.Attributes);
                if (FileList.LayerList == null)
                    FileList.LayerList = new Dictionary<string, List<ParserLayer>>();
                else
                    FileList.LayerList.Clear();
                node = node.FirstChild;
                if (node != null)
                {
                    do
                    {
                        //check if list of layers already excists
                        if (!FileList.LayerList.ContainsKey(node.Name))
                            FileList.LayerList.Add(node.Name, new List<ParserLayer>());

                        ParserLayer layer;
                        GetLayer(node, out layer);
                        FileList.LayerList[node.Name].Add(layer);

                        node = node.NextSibling;
                    } while (node != null);
                }
            }
            //Use this public static function to get a mirrored output of the xml file. (without the header)
            //As it's public you can also use it in any other class that uses this namespace.
            //DebugParser.OutputFileList(FileList);
        }

        /// <summary>
        /// Function used for reading xml files. It get's all attribute information and puts it in a list
        /// </summary>
        /// <param name="attributes">attributes</param>
        /// <param name="list">list to save the attributes in</param>
        private void GetAttributes(XmlAttributeCollection attributes, out List<KeyValuePair<string, string>> list)
        {
            list = null;
            if (attributes != null && attributes.Count != 0)
            {
                list = new List<KeyValuePair<string, string>>();
                foreach (XmlAttribute attribute in attributes)
                {
                    list.Add(new KeyValuePair<string, string>(attribute.Name, attribute.InnerText));
                }
            }
        }

        //
        //
        /// <summary>
        /// Private function to get a complete layer. Thanks to its recursive nature,
        /// it allows to read the children, its children, and so on.
        /// </summary>
        /// <param name="node">node to be parsed</param>
        /// <param name="layer">object to save the node to</param>
        private void GetLayer(XmlNode node, out ParserLayer layer)
        {
            layer = new ParserLayer();
            //Get properties of layer
            GetAttributes(node.Attributes, out layer.Attributes);
            if (layer.Children == null)
                layer.Children = new Dictionary<string, List<ParserLayer>>();

            XmlNode newNode = node.FirstChild;
            if ((node.ChildNodes.Count >= 1 || (newNode != null && newNode.ChildNodes.Count > 0)) && newNode.Name != "#text")
            {
                do
                {
                    if (!layer.Children.ContainsKey(newNode.Name))
                    {
                        layer.Children.Add(newNode.Name, new List<ParserLayer>());
                    }
                    ParserLayer innerLayer;
                    GetLayer(newNode, out innerLayer);
                    layer.Children[newNode.Name].Add(innerLayer);
                    newNode = newNode.NextSibling;
                } while (newNode != null);
            }
            else
            {
                //get innervalue
                layer.InnerValue = node.InnerText;
            }
        }
        #endregion
        //======================================================================================

        //======================================================================================
        //================== GET PARSER CONTENT, READ FROM AN XML FILE =========================
        #region To Do
        //  + More options
        //  + deeper layers
        //  + smarter function parameter system
        #endregion

        #region Get Parser Content Functions
        /// <summary>
        /// Get content of a complete xml file
        /// </summary>
        /// <returns>XML File, containing the root and its children</returns>
        public XMLFile GetRoot()
        {
            return FileList;
        }

        /// <summary>
        /// Get Root information
        /// </summary>
        /// <returns>root element and its attributes/properties</returns>
        public XMLRootInformation GetRootInformation()
        {
            return FileList.Root;
        }

        /// <summary>
        /// (0_A) Get all layers on layer_0 with the name 'layer_0_name'
        /// </summary>
        /// <param name="layer_0_name">name of the layer</param>
        /// <returns>layer if found, null if not</returns>
        public List<ParserLayer> GetParserLayers_0(string layer_0_name)
        {
            if (FileList.LayerList.ContainsKey(layer_0_name))
            {
                return FileList.LayerList[layer_0_name];
            }
            return null;
        }

        /// <summary>
        /// (0_B) Get all layers on layer_0 with the name 'layer_0_name', 
        /// with property_field 'attribute_name' that contains 'attribute_value' 
        /// </summary>
        /// <param name="layer_0_name">name of the layer</param>
        /// <param name="attribute_name">name of the attribute to be checked</param>
        /// <param name="attribute_value">value of the attribute to be checked</param>
        /// <returns>layer if found, null if not</returns>
        public List<ParserLayer> GetParserLayers_0(string layer_0_name, string attribute_name, string attribute_value)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_0(layer_0_name));
            if (layers == null)
                return null;
            for (int i = 0; i < layers.Count; )
            {
                if (layers[i].Attributes == null || layers[i].Attributes.Count == 0)
                    layers.Remove(layers[i]);
                else
                {
                    bool removed = false;
                    foreach (KeyValuePair<string, string> pair in layers[i].Attributes)
                    {
                        if (pair.Key == attribute_name)
                        {
                            if (pair.Value != attribute_value)
                            {
                                layers.Remove(layers[i]);
                                removed = true;
                            }
                            break;
                        }
                    }
                    if (!removed)
                        ++i;
                }
            }
            if (layers.Count != 0) return layers;
            return null;
        }

        /// <summary>
        /// (0_C) Get all layers on layer_0 with the name 'layer_0_name', 
        /// with innerField that contains 'inner_field_value' 
        /// </summary>
        /// <param name="layer_0_name">name of the layer</param>
        /// <param name="inner_field_value">string to be compared with the inner value</param>
        /// <returns>layer if found, null if not</returns>
        public List<ParserLayer> GetParserLayers_0(string layer_0_name, string inner_field_value)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_0(layer_0_name));
            if (layers == null)
                return null;
            for (int i = 0; i < layers.Count; )
            {
                if (layers[i].InnerValue == "" || layers[i].InnerValue != inner_field_value)
                    layers.RemoveAt(i);
                else
                    ++i;
            }
            if (layers.Count != 0) return layers;
            return null;
        }

        /// <summary>
        /// (1_A) Get all children of 0_B with name 'layer_1_name'
        /// </summary>
        /// <param name="layer_0_name">name of the layer</param>
        /// <param name="attribute_name">name of the attribute to be checked</param>
        /// <param name="attribute_value">value of the attribute to be checked</param>
        /// <param name="layer_1_name">name of the second layer</param>
        /// <returns>first correct layer to be found, null if none can be found</returns>
        public List<ParserLayer> GetParserLayers_1(string layer_0_name, 
            string attribute_0_name, string attribute_0_value, string layer_1_name)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_0(layer_0_name, attribute_0_name, attribute_0_value));
            if (layers == null)
                return null;
            List<ParserLayer> returnLayers = new List<ParserLayer>();
            for (int i = 0; i < layers.Count; ++i)
            {
                if (layers[i].Children != null &&
                    layers[i].Children.Count != 0 &&
                    layers[i].Children.ContainsKey(layer_1_name))
                {
                    returnLayers.AddRange(layers[i].Children[layer_1_name]);
                }
            }
            if (returnLayers.Count != 0) return returnLayers;
            return null;
        }

        /// <summary>
        /// (1_A_B) Get all children of 0_B with name 'layer_1_name', 
        /// with property_field 'attribute_name' that contains 'attribute_value' 
        /// </summary>
        /// <param name="layer_0_name">name of the first layer</param>
        /// <param name="attribute_0_name">name of the attribute of the first layer to be checked</param>
        /// <param name="attribute_0_value">value of the attribute of the first layer to be checked</param>
        /// <param name="layer_1_name">name of the second layer</param>
        /// <param name="attribute_1_name">name of the attribute of the second layer to be checked</param>
        /// <param name="attribute_1_value">value of the attribute of the second layer to be checked</param>
        /// <returns>first correct layer to be found, null if none can be found</returns>
        public List<ParserLayer> GetParserLayers_1_by_property(string layer_0_name, 
            string attribute_0_name, string attribute_0_value, string layer_1_name, 
            string attribute_1_name, string attribute_1_value)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_0(layer_0_name, attribute_0_name, attribute_0_value));
            if (layers == null)
                return null;
            List<ParserLayer> returnLayers = new List<ParserLayer>();
            for (int i = 0; i < layers.Count; ++i)
            {
                if (layers[i].Children != null &&
                    layers[i].Children.Count != 0 &&
                    layers[i].Children.ContainsKey(layer_1_name))
                {
                    if (layers[i].Attributes != null & layers[i].Attributes.Count != 0)
                    {
                        foreach (KeyValuePair<string, string> pair in layers[i].Attributes)
                        {
                            if (pair.Key == attribute_1_name && pair.Value == attribute_1_value)
                                returnLayers.AddRange(layers[i].Children[layer_1_name]);
                        }
                    }
                }
            }
            if (returnLayers.Count != 0) return returnLayers;
            return null;
        }

        /// <summary>
        /// (1_A_B) Get all children of 0_B with name 'layer_1_name', 
        /// with inner_field that contains 'inner_value'
        /// </summary>
        /// <param name="layer_0_name">name of the first layer</param>
        /// <param name="attribute_0_name">name of the attribute of the first layer to be checked</param>
        /// <param name="attribute_0_value">value of the attribute of the first layer to be checked</param>
        /// <param name="layer_1_name">name of the second layer</param>
        /// <param name="inner_field_value">string to be compared with the inner value</param>
        /// <returns>first correct layer to be found, null if none can be found</returns>
        public List<ParserLayer> GetParserLayers_1_by_innerValue(string layer_0_name, 
            string attribute_0_name, string attribute_0_value, string layer_1_name, string inner_value)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_0(layer_0_name, attribute_0_name, attribute_0_value));
            if (layers == null)
                return null;
            List<ParserLayer> returnLayers = new List<ParserLayer>();
            for (int i = 0; i < layers.Count; ++i)
            {
                if (layers[i].Children != null &&
                    layers[i].Children.Count != 0 &&
                    layers[i].Children.ContainsKey(layer_1_name))
                {
                    foreach (ParserLayer layer in layers[i].Children[layer_1_name])
                    {
                        if (layer.InnerValue != "" && layer.InnerValue == inner_value)
                        {
                            returnLayers.Add(layer);
                        }
                    }
                }
            }
            if (returnLayers.Count != 0) return returnLayers;
            return null;
        }

        /// <summary>
        /// (2_A_A) Get all children of 1_B with name 'layer_2_name'
        /// </summary>
        /// <param name="layer_0_name">name of the first layer</param>
        /// <param name="attribute_0_name">name of the attribute of the first layer to be checked</param>
        /// <param name="attribute_0_value">value of the attribute of the first layer to be checked</param>
        /// <param name="layer_1_name">name of the second layer</param>
        /// <param name="layer_2_name">name of the third layer</param>
        /// <returns>first correct layer to be found, null if none can be found</returns>
        public List<ParserLayer> GetParserLayers_2(string layer_0_name, string attribute_0_name, 
            string attribute_0_value, string layer_1_name, string layer_2_name)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_1(layer_0_name, attribute_0_name, attribute_0_value, layer_1_name));
            if (layers == null)
                return null;
            List<ParserLayer> returnLayers = new List<ParserLayer>();
            for (int i = 0; i < layers.Count; ++i)
            {
                if (layers[i].Children != null &&
                    layers[i].Children.Count != 0 &&
                    layers[i].Children.ContainsKey(layer_2_name))
                {
                    returnLayers.AddRange(layers[i].Children[layer_2_name]);
                }
            }
            if (returnLayers.Count != 0) return returnLayers;
            return null;
        }

        /// <summary>
        /// (2_A_A) Get all children of 1_B with name 'layer_2_name'
        /// </summary>
        /// <param name="layer_0_name">name of the first layer</param>
        /// <param name="layer_1_name">name of the second layer</param>
        /// <returns>first correct layer to be found, null if none can be found</returns>
        public List<ParserLayer> GetParserLayers_1_2(string layer_0_name, string layer_1_name)
        {
            List<ParserLayer> layers = new List<ParserLayer>();
            layers.AddRange(GetParserLayers_0(layer_0_name));
            if (layers == null)
                return null;
            if (layers[0].Children != null && layers[0].Children.ContainsKey(layer_1_name))
            {
                return layers[0].Children[layer_1_name];
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer_0_name">name of the first layer</param>
        /// <param name="layer_1_name">name of the second layer</param>
        /// <param name="attribute_1_name">name of the attribute of the second layer to be checked</param>
        /// <param name="attribute_1_value">value of the attribute of the second layer to be checked</param>
        /// <param name="layer_2_name">name of the third layer</param>
        /// <returns>first correct layer to be found, null if none can be found</returns>
        public List<ParserLayer> GetParserLayers_1_2(string layer_0_name, string layer_1_name, string attribute_1_name, string attribute_1_value, string layer_2_name)
        {
            List<ParserLayer> layers = GetParserLayers_1_2(layer_0_name, layer_1_name);
            if (layers != null)
            {
                foreach (ParserLayer layer in layers)
                {
                    if (layer.Attributes != null)
                    {
                        foreach (KeyValuePair<string, string> attribute in layer.Attributes)
                        {
                            if (attribute.Key == attribute_1_name &&
                                attribute.Value == attribute_1_value)
                            {
                                if (layer.Children != null && layer.Children.ContainsKey(layer_2_name))
                                {
                                    return layer.Children[layer_2_name];
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        #endregion
        //======================================================================================

        //======================================================================================
        //======================= ADD NEW PARSER CONTENT =======================================
        #region Add parser content Functions
        /// <summary>
        /// Add layers to level 0
        /// </summary>
        /// <param name="layer_0_name">name of the layer(s) to be added</param>
        /// <returns>true if layer has been added</returns>
        public bool AddParserLayer_0(string layer_0_name)
        {
            if (!FileList.LayerList.ContainsKey(layer_0_name))
            {
                FileList.LayerList.Add(layer_0_name, new List<ParserLayer>());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 0 (default, empty layer)
        /// </summary>
        /// <param name="layer_0_name">name of the layer(s) to be added</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_0_child(string layer_0_name)
        {
            if (FileList.LayerList.ContainsKey(layer_0_name))
            {
                ParserLayer newLayer = new ParserLayer();
                FileList.LayerList[layer_0_name].Add(newLayer);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 0 (given layer via parameter)
        /// </summary>
        /// <param name="layer_0_name">name of the layer(s) to be added</param>
        /// <param name="layer">layer to add the child to</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_0_child(string layer_0_name, ParserLayer layer)
        {
            if (FileList.LayerList.ContainsKey(layer_0_name))
            {
                FileList.LayerList[layer_0_name].Add(layer);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 0 with attribute_value 
        /// in the field with name attribue_name (default, empty layer)
        /// </summary>
        /// <param name="layer_0_name">name of the layer to add the new layers to</param>
        /// <param name="attribute_name">name of the attribute to be checked</param>
        /// <param name="attribute_value">value of the attribute to be checked</param>
        /// <param name="layer_1_name">name of the layer(s) to be added</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_1(string layer_0_name, string attribute_name, 
            string attribute_value, string layer_1_name)
        {
            List<ParserLayer> layerList = GetParserLayers_0(layer_0_name, attribute_name, attribute_value);
            if (layerList != null && layerList.Count > 0)
            {
                if(layerList[0].Children.ContainsKey(layer_1_name))
                {
                    ParserLayer newLayer = new ParserLayer();
                    layerList[0].Children[layer_1_name].Add(newLayer);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 0 with attribute_value in 
        /// the field with name attribue_name (given layer via parameter)
        /// </summary>
        /// <param name="layer_0_name">name of the layer to add the new layers to</param>
        /// <param name="attribute_name">name of the attribute to be checked</param>
        /// <param name="attribute_value">value of the attribute to be checked</param>
        /// <param name="layer_1_name">name of the layer(s) to be added</param>
        /// <param name="layer">layer to be added to a layer</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_1(string layer_0_name, string attribute_name, 
            string attribute_value, string layer_1_name, ParserLayer layer)
        {
            List<ParserLayer> layerList = GetParserLayers_0(layer_0_name, attribute_name, attribute_value);
            if (layerList != null && layerList.Count > 0)
            {
                if (layerList[0].Children.ContainsKey(layer_1_name))
                {
                    layerList[0].Children[layer_1_name].Add(layer);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 0 with 
        /// value inner_field_value (default, empty layer)
        /// </summary>
        /// <param name="layer_0_name">name of the layer to add the new layers to</param>
        /// <param name="inner_field_value">inner value to checked</param>
        /// <param name="layer_1_name">name of the layer(s) to be added</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_1(string layer_0_name, string inner_field_value, string layer_1_name)
        {
            List<ParserLayer> layerList = GetParserLayers_0(layer_0_name, inner_field_value);
            if (layerList != null && layerList.Count > 0)
            {
                if (layerList[0].Children.ContainsKey(layer_1_name))
                {
                    ParserLayer newLayer = new ParserLayer();
                    layerList[0].Children[layer_1_name].Add(newLayer);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 0 with value 
        /// inner_field_value (given layer via parameter)
        /// </summary>
        /// <param name="layer_0_name">name of the layer to add the new layers to</param>
        /// <param name="inner_field_value">inner value to checked</param>
        /// <param name="layer_1_name">name of the layer(s) to be added</param>
        /// <param name="layer">layer to be added to a layer</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_1(string layer_0_name, string inner_field_value, string layer_1_name, ParserLayer layer)
        {
            List<ParserLayer> layerList = GetParserLayers_0(layer_0_name, inner_field_value);
            if (layerList != null && layerList.Count > 0)
            {
                if (layerList[0].Children.ContainsKey(layer_1_name))
                {
                    layerList[0].Children[layer_1_name].Add(layer);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 1 (default, empty layer)
        /// </summary>
        /// <param name="layer_0_name">name of the layer to add the new layers to</param>
        /// <param name="layer_1_name">name of the layer(s) to be added</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_0_child(string layer_0_name, string layer_1_name)
        {
            if (FileList.LayerList.ContainsKey(layer_0_name))
            {
                if (FileList.LayerList[layer_0_name][0].Children != null &&
                    FileList.LayerList[layer_0_name][0].Children.Count > 0 &&
                    FileList.LayerList[layer_0_name][0].Children.ContainsKey(layer_1_name))
                {
                    ParserLayer layer = new ParserLayer();
                    FileList.LayerList[layer_0_name][0].Children[layer_1_name].Add(layer);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add layers add child level of level 1 (given layer via parameter)
        /// </summary>
        /// <param name="layer_0_name">name of the layer to add the new layers to</param>
        /// <param name="layer_1_name">name of the layer(s) to be added</param>
        /// <param name="layer">layer to be added to a layer</param>
        /// <returns>true if child has been added</returns>
        public bool AddParserLayer_0_child(string layer_0_name, string layer_1_name, ParserLayer layer)
        {
            if (FileList.LayerList.ContainsKey(layer_0_name))
            {
                if (FileList.LayerList[layer_0_name][0].Children != null &&
                    FileList.LayerList[layer_0_name][0].Children.Count > 0 &&
                    FileList.LayerList[layer_0_name][0].Children.ContainsKey(layer_1_name))
                {
                    FileList.LayerList[layer_0_name][0].Children[layer_1_name].Add(layer);
                    return true;
                }
            }
            return false;
        }
        #endregion  
        //======================================================================================
    }
}