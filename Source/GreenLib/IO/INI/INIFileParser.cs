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
using System.Runtime.Serialization;
using System.IO;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.INI
{
    /// <summary>
    /// A class, based on the IFileParser interface, 
    /// to parse INI formatted files. 
    /// </summary>
    public class INIFileParser : BaseFileIO, IFileParser<INIFileContainer, string>
    {
        /// <summary>
        /// the ini file text reader
        /// </summary>
        protected TextReader m_IniFile;
        /// <summary>
        /// Current root being used
        /// </summary>
        protected string m_CurrentRoot;

        /// <summary>
        /// Open the file. This should be done in the heading of your using body.
        /// </summary>
        /// <param name="path">dynamic path to the INI file.</param>
        /// <param name="file">name of the file, including the extension</param>
        public INIFileParser ( string path, string file)
            : base ( path, file )
        {
            m_IniFile = null;
            m_CurrentRoot = null;
        }

        public void Read(INIFileContainer container)
        {
            string strLine = null;
            string[] keyPair = null;

            if (File.Exists(m_File))
            {
                try
                {
                    m_IniFile = new StreamReader ( m_File );

                    strLine = m_IniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim();

                        if (strLine != "")
                        {
                            if ( strLine.StartsWith ( "[" ) && strLine.EndsWith ( "]" ) )
                            {
                                m_CurrentRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            // ignore comments
                            else if ( !strLine.StartsWith ( ";" ) && !strLine.StartsWith ( "#" ) &&
                                !strLine.StartsWith("'"))
                            {
                                keyPair = strLine.Split(new char[] { '=' }, 2);

                                if (m_CurrentRoot == null)
                                    m_CurrentRoot = "ROOT";

                                if(!container.ContainsKey(m_CurrentRoot))
                                {
                                    GenericTypeDictionary rootContainer = new GenericTypeDictionary();
                                    container.Add(m_CurrentRoot, rootContainer);
                                }

                                if (keyPair.Length > 1)
                                {
                                    ProcessElement(container, keyPair[0], keyPair[1]);
                                }
                            }
                        }
                        strLine = m_IniFile.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (m_IniFile != null)
                    {
                        m_IniFile.Close();
                        m_IniFile = null;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Unable to locate " + m_File);
            }
        }

        /// <summary>
        /// Clean up the File out of the memory and unlock it.
        /// </summary>
        public void Dispose()
        {
            if (m_IniFile != null)
            {
                m_IniFile.Close();
            }
        }

        /// <summary>
        /// Seperating this logic from your main reading logic allows you to handle 
        /// the original read values in different ways. (e.g. normal and smart parser)
        /// </summary>
        /// <param name="container">main container to add the elements to</param>
        /// <param name="key">key of the new  element</param>
        /// <param name="value">value the new element</param>
        protected virtual void ProcessElement(INIFileContainer container, string key, string value)
        {
            container[m_CurrentRoot].Add(key, new TObject(value));
        }
    }
}
