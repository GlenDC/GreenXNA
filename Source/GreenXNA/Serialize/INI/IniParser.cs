#region File Description
//-----------------------------------------------------------------------------
// IniParser.cs
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
using System.IO;
using System.Collections;
using System.Collections.Generic;

using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.Serialize.INI
{
    /// <summary>
    /// Class to be used to (de)serialize INI files.
    /// </summary>
    public class IniParser : Parser
    {
        private Hashtable _KeyPairs;
 
        private struct SectionPair
        {
            public string Section;
            public string Key;
        }
 
        /// <summary>
        /// Opens the INI file at the given path and enumerates the values in the IniParser.
        /// </summary>
        /// <param name="iniPath">Full path to INI file.</param>
        public IniParser(string file, string path)
            : base(file, path, ".ini")
        {
            _KeyPairs = new Hashtable();
        }

        /// <summary>
        /// Read the ini file (parse it)
        /// </summary>
        public override void Read()
        {
            base.Read();

            TextReader iniFile = null;
            string strLine = null;
            string currentRoot = null;
            string[] keyPair = null;

            if (File.Exists(DocumentPath))
            {
                try
                {
                    iniFile = new StreamReader(DocumentPath);

                    strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim();

                        if (strLine != "")
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                currentRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            // ignore comments
                            else if (!strLine.StartsWith(";") && !strLine.StartsWith("#") &&
                                !strLine.StartsWith("'"))
                            {
                                keyPair = strLine.Split(new char[] { '=' }, 2);

                                SectionPair sectionPair;
                                string value = null;

                                if (currentRoot == null)
                                    currentRoot = "ROOT";

                                sectionPair.Section = currentRoot;
                                sectionPair.Key = keyPair[0];

                                if (keyPair.Length > 1)
                                    value = keyPair[1];

                                _KeyPairs.Add(sectionPair, value);
                            }
                        }

                        strLine = iniFile.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                }
            }
            else
            {
                throw new FileNotFoundException("Unable to locate " + DocumentPath);
            }
        }

        /// <summary>
        /// Read the settings parsed from the ini file
        /// and convert them before saving to their correct type!
        /// </summary>
        public override void ReadSmart()
        {
            throw new NotImplementedException();
        }

        #region Get Settings of parser
        /// <summary>
        /// Returns the value for the given section, key pair.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="settingName">Key name.</param>
        public string GetSetting(string sectionName, string settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
 
            return (string)_KeyPairs[sectionPair];
        }

        /// <summary>
        /// Enumerates all lines for given section.
        /// </summary>
        /// <param name="sectionName">Section to enum.</param>
        public string[] EnumSection(string sectionName)
        {
            ArrayList tmpArray = new ArrayList();
 
            foreach (SectionPair pair in _KeyPairs.Keys)
            {
                if (pair.Section == sectionName)
                    tmpArray.Add(pair.Key);
            }
 
            return (string[])tmpArray.ToArray(typeof(string));
        }
        #endregion

        #region Edit INI File
        /// <summary>
        /// Adds or replaces a setting to the table to be saved.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        public void AddSetting(string sectionName, string settingName, string settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();
 
            if (_KeyPairs.ContainsKey(sectionPair))
                _KeyPairs.Remove(sectionPair);
 
            _KeyPairs.Add(sectionPair, settingValue);
        }
 
        /// <summary>
        /// Adds or replaces a setting to the table to be saved with a null value.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void AddSetting(string sectionName, string settingName)
        {
            AddSetting(sectionName, settingName, null);
        }
 
        /// <summary>
        /// Remove a setting.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void DeleteSetting(string sectionName, string settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();
 
            if (_KeyPairs.ContainsKey(sectionPair))
                _KeyPairs.Remove(sectionPair);
        }
        #endregion

        #region Save INI File
        /// <summary>
        /// Save settings to new file.
        /// </summary>
        /// <param name="newFilePath">New file path.</param>
        public override void Save(string newFileName)
        {
            base.Save(newFileName);

            ArrayList sections = new ArrayList();
            string tmpValue = "";
            string strToSave = "";
 
            foreach (SectionPair sectionPair in _KeyPairs.Keys)
            {
                if (!sections.Contains(sectionPair.Section))
                    sections.Add(sectionPair.Section);
            }
 
            foreach (string section in sections)
            {
                strToSave += ("[" + section + "]\r\n");
 
                foreach (SectionPair sectionPair in _KeyPairs.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        tmpValue = (string)_KeyPairs[sectionPair];
 
                        if (tmpValue != null)
                            tmpValue = "=" + tmpValue;
 
                        strToSave += (sectionPair.Key + tmpValue + "\r\n");
                    }
                }
 
                strToSave += "\r\n";
            }
 
            try
            {
                TextWriter tw = new StreamWriter(DocumentPath);
                tw.Write(strToSave);
                tw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        /// <summary>
        /// Save settings back to ini file.
        /// </summary>
        public override void Save()
        {
            Save(DocumentPath);
        }
        #endregion

        /// <summary>
        /// Save all the parameters into a container!
        /// </summary>
        /// <param name="container">container for the parameters</param>
        public override void CopyContentToParameterContainer(ref Dictionary<uint, Dictionary<uint, string>> container)
        {
            IDictionaryEnumerator entry = _KeyPairs.GetEnumerator();
            DictionaryEntry currentEntry;
            SectionPair currentPair;

            while (entry.MoveNext())
            {
                currentEntry = (DictionaryEntry)entry.Current;
                currentPair = (SectionPair)entry.Key;
                uint category = Helpers.GenerateHash((string)currentPair.Section);
                uint name = Helpers.GenerateHash((string)currentPair.Key);
                if (!container.ContainsKey(category))
                {
                    container.Add(category, new Dictionary<uint, string>());
                }
                if (!container[category].ContainsKey(name))
                {
                    container[category].Add(name, (string)currentEntry.Value);
                }
                else
                {
                    //TODO: Send Warning for double value! 
                }
            }
        }
    }
}