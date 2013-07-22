#region File Description
//-----------------------------------------------------------------------------
// ParameterContainer.cs
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
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.Serialize
{
    /// <summary>
    /// enumerator for file types.
    /// </summary>
    public enum FILE_TYPES
    {
        FILE_XML = 0,   //XML Files
        FILE_INI = 1,   //INI Files
        FILE_JSON = 2,  //JSON Files
    };

    /// <summary>
    /// Automatic Container Class For Options. With PreDefined format!
    /// Also refreshing runtime options are embedded in this class!
    /// </summary>
    public abstract class ParameterContainer
    {
        #region variables
        /// <summary>
        /// container of parameters
        /// </summary>
        protected Dictionary<uint, Dictionary<uint, string>> _Parameters;
        /// <summary>
        /// this file should have the correct format predefined for containers.
        /// </summary>
        protected string _FileName;
        /// <summary>
        /// the path to the file.
        /// </summary>
        protected string _FilePath;

        public readonly FILE_TYPES FileType;
        #endregion

        /// <summary>
        /// Create the smart container and Load the file.
        /// </summary>
        /// <param name="fileName">Name of the XML file to use within the smart container.</param>
        public ParameterContainer(string fileName, string filePath, FILE_TYPES fileType)
        {
            FileType = fileType;
            _FilePath = filePath;
            Load(fileName);
        }

        /// <summary>
        /// Load a file to use within the smart container.
        /// </summary>
        /// <param name="fileName">Name of the file to use within the smart container.</param>
        public void Load(string fileName)
        {
            _FileName = fileName;
            Refresh();
        }

        /// <summary>
        /// Reload the file. This allows on run time parameter editing. 
        /// </summary>
        public virtual void Refresh()
        {
            _Parameters = new Dictionary<uint, Dictionary<uint, string>>();
        }

        /// <summary>
        /// Get integer parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public int GetInt(string category, string parameter)
        {
            return Converter.ToInt(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get float parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public float GetFloat(string category, string parameter)
        {
            return Converter.ToFloat(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get double parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public double GetDouble(string category, string parameter)
        {
            return Converter.ToDouble(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get bool parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public bool GetBool(string category, string parameter)
        {
            return Converter.ToBoolean(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get string parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public string GetString(string category, string parameter)
        {
            return _Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)];
        }

        /// <summary>
        /// Get vector2 parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Vector2 GetVector2(string category, string parameter)
        {
            return XNAConverter.ToVector2(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get vector3 parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Vector3 GetVector3(string category, string parameter)
        {
            return XNAConverter.ToVector3(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get vector4 parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Vector4 GetVector4(string category, string parameter)
        {
            return XNAConverter.ToVector4(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }

        /// <summary>
        /// Get quaternion parameter from the container.
        /// <param name="category">Category of the parameter saved within the container.</param>
        /// <param name="parameter">Name of the parameter saved within the container.</param>
        /// </summary>
        public Quaternion GetQuaternion(string category, string parameter)
        {
            return XNAConverter.ToQuaternion(_Parameters[Helpers.GenerateHash(category)][Helpers.GenerateHash(parameter)]);
        }
    }
}