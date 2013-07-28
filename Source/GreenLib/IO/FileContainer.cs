#region File Description
//-----------------------------------------------------------------------------
// FileContainer.cs
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
using System.Collections;
using System.Collections.Generic;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// Base class for all File Containers!
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    public abstract class FileContainer<TContainer> : 
        GenericDictionary<TContainer> where TContainer:ICloneable
    {
        /// <summary>
        /// name of the file to be parsed
        /// </summary>
        protected string m_FileName;
        /// <summary>
        /// Extension of the file to be parsed
        /// </summary>
        protected string m_FileExtension;
        /// <summary>
        /// default path of xml files in the project.
        /// </summary>
        protected string m_DirectoryPath;
        /// <summary>
        /// public interface to set the path of the default directory to be used.
        /// </summary>
        public string Path { set { m_DirectoryPath = value; } get { return m_DirectoryPath; } }
        /// <summary>
        /// public interface to set the default file to be used.
        /// </summary>
        public string File { 
            get 
            { 
                return GetFileName(m_FileName, m_FileExtension); 
            } 
            set 
            { 
                TextHelpers.SplitFileName(value, out m_FileName, out m_FileExtension); 
            }
        }
        /// <summary>
        /// get value, combining the path, path and file-extension
        /// </summary>
        public string FilePath { get { return GetPath(m_DirectoryPath, m_FileName, m_FileExtension); } }

        #region Asset Functions

        /// <summary>
        /// get the filename: name + extension
        /// </summary>
        /// <param name="name">name of the file</param>
        /// <param name="extension">extension, without the '.'</param>
        /// <returns>filename with extension</returns>
        protected string GetFileName(string name, string extension) 
        {
            return name + "." + extension;
        }

        /// <summary>
        /// get the complete path
        /// </summary>
        /// <param name="path">dynamic directory path</param>
        /// <param name="file">file, including extension</param>
        /// <returns>full file path</returns>
        protected string GetPath(string path, string file) 
        {
            return path + file;
        }

        /// <summary>
        /// get the complete path
        /// </summary>
        /// <param name="path">dynamic directory path</param>
        /// <param name="file">file, without extension</param>
        /// <param name="extension">extension of the file, without the dot '.'</param>
        /// <returns>full file path</returns>
        protected string GetPath(string path, string file, string extension)
        {
            return GetFileName(GetPath(path, file),extension);
        }

        #endregion

        /// <summary>
        /// Set the default path and file.
        /// </summary>
        /// <param name="file">default file to be used</param>
        /// <param name="path">default directory to be used</param>
        public FileContainer(string file, string path)
            : base()
        {
            File = file;
            Path = path;
        }

        #region Load File (readng)

        /// <summary>
        /// Load the variables from the default file, to this container.
        /// </summary>
        /// <returns>true if file loaded correctly and container is initialized</returns>
        public void Load()
        {
            LoadFile(Path, File);
        }

        /// <summary>
        /// Load the variables from a new file and set is as default file.
        /// </summary>
        /// <param name="file">new default file</param>
        /// <param name="path">new directory path</param>
        /// <returns>true if file loaded correctly and container is initialized</returns>
        public void Load(string file, string path)
        {
            Load(file, path, true);
        }

        /// <summary>
        /// Load the variables from a new file and if wanted, set is as default file.
        /// </summary>
        /// <param name="file">Load this file</param>
        /// <param name="path">File can be found in this directory</param>
        /// <param name="def">true if you want to set ths as the new default file</param>
        /// <returns>true if file loaded correctly and container is initialized</returns>
        public void Load(string file, string path, bool def) 
        {
            if (def)
            {
                File = file;
                Path = path;
            }
            LoadFile(Path, File);
        }

        /// <summary>
        /// Abstract protected inner function to load the file
        /// </summary>
        /// <param name="path">dynamic directory path to the file</param>
        /// <param name="file">name of the file, including the extension</param>
        /// <returns>true if loaded without any problems</returns>
        protected abstract void LoadFile(string path, string file);

        #endregion

        #region Save File (writing)

        /// <summary>
        /// Write the variables from the container to the default file.
        /// </summary>
        public void Save()
        {
            SaveFile(Path, File);
        }

        /// <summary>
        /// Write the variables from the container to a new file and save it as default file
        /// </summary>
        /// <param name="file">new default file</param>
        /// <param name="path">new directory path</param>
        /// <returns>true if file saved correctly and container is initialized</returns>
        public void Save(string file, string path)
        {
            Save(file, path, true);
        }

        /// <summary>
        /// Write the variables from the container to a new file and save it as default file, if wanted.
        /// </summary>
        /// <param name="file">Save this file</param>
        /// <param name="path">File can be found in this directory</param>
        /// <param name="def">true if you want to set ths as the new default file</param>
        public void Save(string file, string path, bool def)
        {
            if (def)
            {
                File = file;
                Path = path;
            }
            SaveFile(Path, File);
        }

        /// <summary>
        /// Abstract protected inner function to save the file
        /// </summary>
        /// <param name="file">file to be saved to</param>
        /// <param name="path">dynamic directory path of the file</param>
        protected abstract void SaveFile(string path, string file);

        #endregion
    }
}