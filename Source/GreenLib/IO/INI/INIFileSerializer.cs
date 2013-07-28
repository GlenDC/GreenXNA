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
using System.Collections;
using System.Collections.Generic;
using System.IO;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.INI
{
    public class INIFileSerializer : BaseFileIO, ISerializer<INIFileContainer>
    {
        /// <summary>
        /// Open the file. This should be done in the heading of your using body.
        /// </summary>
        /// <param name="path">dynamic path to the INI file.</param>
        /// <param name="file">name of the file, including the extension</param>
        public INIFileSerializer (string path, string file)
            : base ( path, file )
        {
        }

        /// <summary>
        /// Write to a new file, only if it doesn't exist yet
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to a new file was succesful</returns>
        public bool TryToWrite(INIFileContainer container)
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
        public bool TryToOverwrite(INIFileContainer container)
        {
            if (File.Exists(m_File))
            {
                return Write(container);
            }
            return false;
        }

        /// <summary>
        /// Protected assist function to write the containers element
        /// to a correct formatted string.
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>correct formatted string, ready to bewritten to a file</returns>
        protected string GetText(INIFileContainer container)
        {
            string tmpValue = "";
            string strToSave = "";

            var section = container.GetEnumerator();
            while (section.MoveNext())
            {
                var currentSection = section.Current;
                strToSave += ("[" + currentSection.Key.Item3 + "]\r\n");
                var pairs = currentSection.Value.GetEnumerator();
                while (pairs.MoveNext())
                {
                    var pair = pairs.Current;
                    var value = pairs.Current.Value;
                    tmpValue = (string)value.Get<string>();

                    if (tmpValue != null)
                    {
                        tmpValue = "=" + tmpValue;
                        strToSave += (pair.Key.Item3 + tmpValue + "\r\n");
                    }

                }

                strToSave += "\r\n";
            }

            return strToSave;
        }

        /// <summary>
        /// Write to a new or an existing file.
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if serialization to an existing or new file was succesful</returns>
        public bool Write(INIFileContainer container)
        {
            string strToSave = GetText(container);

            if (strToSave != null)
            {
                try
                {
                    TextWriter writer = new StreamWriter(m_File);
                    writer.Write(strToSave);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Append to an exisiting file, new content from a container
        /// </summary>
        /// <param name="container">container to be serialized</param>
        /// <returns>true if the serialization of the new content at the end of an existing file was succesfull.</returns>
        public bool Append(INIFileContainer container)
        {
            if (File.Exists(m_File))
            {
                using (StreamWriter writer = File.AppendText(m_File))
                {
                    string strToSave = GetText(container);
                    writer.Write(strToSave);
                    writer.Close();
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
    }
}