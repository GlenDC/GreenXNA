#region File Description
//-----------------------------------------------------------------------------
// ParameterContainer.cs
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
    public class ParameterContainer
    {
        #region variables
        /// <summary>
        /// container of parameters
        /// </summary>
        Dictionary<uint, string> _Parameters;
        /// <summary>
        /// this file should have the correct format predefined for containers.
        /// </summary>
        private String _FileName;
        #endregion

        /// <summary>
        /// Create the smart container and Load the file.
        /// </summary>
        /// <param name="fileName">Name of the XML file to use within the smart container.</param>
        public ParameterContainer(String fileName)
        {
            Load(fileName);
        }

        /// <summary>
        /// Load an xml file to use within the smart container.
        /// </summary>
        /// <param name="fileName">Name of the XML file to use within the smart container.</param>
        public void Load(String fileName)
        {
            _FileName = fileName;
            Refresh();
        }

        /// <summary>
        /// Reload the XML file. This allows on run time parameter editing. 
        /// </summary>
        public void Refresh()
        {
            Parser containerParser = new Parser();
            containerParser.ReadXML(_FileName);
            _Parameters = new Dictionary<uint, string>();
            foreach (ParserLayer layer in containerParser.GetParserLayers_0("Parameter"))
            {
                _Parameters.Add(Helpers.GenerateHash(layer.GetAttribute("name")), layer.GetAttribute("value"));
            }
        }

        /// <summary>
        /// Get integer parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public int GetInt(String parameter)
        {
            return XMLConvert.ToInt(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get float parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public float GetFloat(String parameter)
        {
            return XMLConvert.ToFloat(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get double parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public double GetDouble(String parameter)
        {
            return XMLConvert.ToDouble(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get bool parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public bool GetBool(String parameter)
        {
            return XMLConvert.ToBoolean(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get string parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public String GetString(String parameter)
        {
            return _Parameters[Helpers.GenerateHash(parameter)];
        }

        /// <summary>
        /// Get vector2 parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Vector2 GetVector2(String parameter)
        {
            return XMLConvert.ToVector2(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get vector3 parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Vector3 GetVector3(String parameter)
        {
            return XMLConvert.ToVector3(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get vector4 parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Vector4 GetVector4(String parameter)
        {
            return XMLConvert.ToVector4(_Parameters[Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get quaternion parameter from the container.
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Quaternion GetQuaternion(String parameter)
        {
            return XMLConvert.ToQuaternion(_Parameters[Helpers.GenerateHash(parameter)]);
        }
    }
}