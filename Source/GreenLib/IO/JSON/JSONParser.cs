#region File Description
//-----------------------------------------------------------------------------
// JSONParser.cs
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
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Collections.Generic;

using GreenXNA.GreenInterfaces;
using GreenXNA.GreenHelpers;
using GreenXNA.Generic;
#endregion

namespace GreenXNA.IO.JSON
{
    /// <summary>
    /// enumerator defining the different types found in a JSON file
    /// </summary>
    public enum JSON_VALUE_TYPE
    {
        json_object = 0,
        json_array = 1,
        json_seperator = 2,
        json_pair = 3
    }

    /// <summary>
    /// class, to be used to parse JSON files and save its content.
    /// This object can then be saved again via the JSONWriter class. 
    /// </summary>
    public sealed class JSONParser : Parser
    {
        #region variables
        /// <summary>
        /// seperator string to seperate values in a JSON File.
        /// </summary>
        public const string JSON_SEPERATOR = ", ";
        /// <summary>
        /// Null Char Character
        /// </summary>
        public const char NULL_CHAR = '\0';

        /// <summary>
        /// The Root element of the JSON File
        /// </summary>
        public JSONObject RootElement { get; private set; }

        private enum JSON_SEARCH_STATE
        {
            root, element_name, element_value, element_child
        };
        #endregion

        /// <summary>
        /// public constructor of the Parser, creating the XMLFile object.
        /// </summary>
        public JSONParser(string name, string path)
            : base(name, path)
        {
            RootElement = new JSONObject();
        }

        protected override void Destroy()
        {
            RootElement.Dispose();
        }

        #region Read JSON File
        /// <summary>
        /// Read the JSON File and parse it values.
        /// </summary>
        public override void Read()
        {
            base.Read();

            if (File.Exists(DocumentPath))
            {
                try
                {
                    using (StreamReader stream = new StreamReader(DocumentPath))
                    {
                        TextHelpers.ReadUntillCharReached(stream, '{', true);

                        if (!stream.EndOfStream)
                        {
                            RootElement = ReadObject(stream);
                            if (FileState != FILE_STATE.FILE_CORRUPT)
                            {
                                System.Diagnostics.Debug.Write(RootElement.ToString() + "\n");
                            }
                        }
                        else
                        {
                            FileState = FILE_STATE.FILE_CORRUPT;
                            //Warning corrupt file!
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new FileNotFoundException("Unable to locate " + DocumentPath);
            }
        }

        /// <summary>
        /// Due to JSON File's format, it is already smart with the default Read() function. 
        /// This function should not be used. 
        /// </summary>
        public override void ReadSmart()
        {
            Read();
            //set warning!
        }

        /// <summary>
        /// Read a value in the JSON File. if their is anything left
        /// Can be any kind of value
        /// </summary>
        /// <param name="stream">stream input to be read from</param>
        private GenericType ReadValue(StreamReader stream)
        {
            //Step 1: get to know what we are looking for!
            char resultChar = NULL_CHAR;
            char [] charSet = { '{', 'n', 'N', '[', '"', '}', ']', 'f', 
                                  'F', 't', 'T', '0', '1', '2', '3', '4', '5', '6',
                                    '7', '8', '9', '.'};
            string textResult;
            textResult = TextHelpers.ReadUntillCharReached(stream, charSet, 22, true, out resultChar);

            if (resultChar != NULL_CHAR)
            {
                //another object
                if (resultChar == '{')
                {
                    JSONObject json_object = ReadObject(stream);
                    if (json_object != null)
                    {
                        GenericTypeJSONObject generic_json_object = new GenericTypeJSONObject();
                        generic_json_object.Value = json_object;
                        return generic_json_object;
                    }
                }
                // Get Array
                else if (resultChar == '[')
                {
                    JSONArray json_array = ReadArray(stream);
                    if (json_array != null)
                    {
                        GenericTypeJSONArray generic_json_array = new GenericTypeJSONArray();
                        generic_json_array.Value = json_array;
                        return generic_json_array;
                    }
                }
                // Get Seperator
                else if (resultChar == 'n' || resultChar == 'N')
                {
                    JSONSeperator json_seperator = ReadSeperator(stream);
                    if (json_seperator != null)
                    {
                        GenericTypeJSONSeperator generic_json_seperator = new GenericTypeJSONSeperator();
                        generic_json_seperator.Value = json_seperator;
                        return generic_json_seperator;
                    }
                }
                //string value
                else if (resultChar != '}' && resultChar != ']')
                {
                    GenericType newGenericType = ReadGenericValue(stream, resultChar);
                    return newGenericType;
                }
            }
            return null;
        }
        /// <summary>
        /// Read a string value in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <returns>new parsed string value</returns>
        private string ReadStringValue(StreamReader stream)
        {
            string value = TextHelpers.ReadUntillCharReached(stream, '\"', false);
            return value;
        }

        /// <summary>
        /// Read a generic value in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <returns>new parsed string value</returns>
        private GenericType ReadGenericValue(StreamReader stream, char firstChar)
        {
            char peekChar;
            while (stream.Peek() == ' ' )
            {
                peekChar = (char)stream.Read();
            }
            peekChar = (char)stream.Peek();
            char[] chars_to_check = { '"', '}', ']', ','};
            int num = 4;
            if ((firstChar == ' ' || firstChar == '=') && 
                peekChar == ',' || peekChar == '\r')
            {
                return null;
            }
            if (firstChar == '"')
            {
                char[] newSet = { '"', '}', ']'};
                chars_to_check = newSet;
                --num;
            }
            char check_result;
            string str = "";
            if (firstChar != '"')
            {
                str += firstChar;
            }
            str += TextHelpers.ReadUntillCharReached(stream, chars_to_check, num, false, out check_result);
            if (stream.EndOfStream)
            {
                return null;
            }
            return SmartConvert(str);
        }

        /// <summary>
        /// Read a seperator in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <returns>new parsed seperator</returns>
        private JSONSeperator ReadSeperator(StreamReader stream)
        {
            // read complete word
            char resultChar = NULL_CHAR;
            char[] charSet = { 'l', 'L' };
            TextHelpers.ReadUntillCharReached(stream, charSet, 2, true, out resultChar);

            // if couldn't be read => Corrupted => return null!
            if (resultChar == NULL_CHAR)
            {
                FileState = FILE_STATE.FILE_CORRUPT;
                return null;
            }

            // if everything is ok => return new Seperator object
            return new JSONSeperator();
        }

        /// <summary>
        /// Read a pair from an object in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <param name="lastChar">last character that has been read</param>
        /// <returns>new parsed seperator</returns>
        private JSONPair ReadPair(StreamReader stream, char lastChar)
        {
            string name = "";
            GenericType type = null;

            // (1) check if object has a name => char == " OR :
            char resultChar = NULL_CHAR;
            char[] charSet = { '\"', ':' };
            string textResult;
            textResult = TextHelpers.ReadUntillCharReached(stream, charSet, 2, true, out resultChar);

            // if couldn't be read => Corrupted => return null!
            if (resultChar == NULL_CHAR)
            {
                //TODO: Add warning!
                FileState = FILE_STATE.FILE_CORRUPT;
                return null;
            }
            // (2) else check if char == '"' => we still need a name!
            if (resultChar == '\"')
            {
                if (lastChar == '"')
                {
                    name = textResult;
                }
                else
                {
                    name = ReadStringValue(stream);
                }
                // corrupted file => return null!
                if (stream.EndOfStream)
                {
                    //TODO: Add warning!
                    FileState = FILE_STATE.FILE_CORRUPT;
                    return null;
                }

                // an extra check for ':'
                textResult = TextHelpers.ReadUntillCharReached(stream, ':', true);
            }

            if (!stream.EndOfStream)
            {

                type = ReadValue(stream);

                //if (type == null)
                //{
                //    //TODO: Add warning!
                //    FileState = FILE_STATE.FILE_CORRUPT;
                //    return new KeyValuePair<string, GenericType>("", null);
                //}
            }
            else
            {
                //TODO: Add warning!
                FileState = FILE_STATE.FILE_CORRUPT;
                return null;
            }

            return new JSONPair(name, type);
        }

        /// <summary>
        /// Read an object in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <returns>new parsed object</returns>
        private JSONObject ReadObject(StreamReader stream)
        {
            JSONObject json_object = new JSONObject();

            char c = '\0';
            char[] chars = { ',', 'n', 'N', '}' };

            do
            {
                // seperator
                if(c == 'n' || c == 'N')
                {
                    JSONSeperator seperator = ReadSeperator(stream);
                    if (seperator != null)
                    {
                        json_object.Values.Add(seperator);
                    }
                    else
                    {
                        FileState = FILE_STATE.FILE_CORRUPT;
                        //corrupt file TODO WARNING ?!
                    }
                }
                else if(c != '}')
                {
                    JSONPair pair;
                    pair = ReadPair(stream, c);
                    json_object.Values.Add(pair);
                }
                string result = TextHelpers.ReadUntillCharReached(stream, chars, 4, true, out c);
            } while (!stream.EndOfStream && c != '}');

            // (4) end of object reached!
            return json_object;
        }

        /// <summary>
        /// Read an array in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <param name="skipNameSearch">true if the array has no name</param>
        /// <returns>new parsed array</returns>
        private JSONArray ReadArray(StreamReader stream)
        {
            JSONArray json_array = new JSONArray();

            char c = '\0';

            do
            {
                json_array.Values.Add(ReadValue(stream));
                char[] chars = { ',', ']' };
                string result = TextHelpers.ReadUntillCharReached(stream, chars, 2, true, out c);
            } while (c == ',');

            // (4) end of array reached!
            return json_array;
        }
        #endregion

        #region Save JSON File
        /// <summary>
        /// Save the JSON File to the original file (overwrite)
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save the JSON File to a new file
        /// or overwrite the same file, if the name is the same as the original one.
        /// </summary>
        /// <param name="newFileName">save file to this file</param>
        public override void Save(string newFileName)
        {
            base.Save(newFileName);
            throw new NotImplementedException();
        }
        #endregion

        #region JSON File => Parameter Container
        /// <summary>
        /// Copy JSON file content to a parameter container
        /// </summary>
        /// <param name="container">container to contain all the parameters</param>
        public override void CopyContentToParameterContainer(ref Dictionary<uint, Dictionary<uint, string>> container)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Smart Convert System
        private GenericType SmartConvert(string value)
        {
            char c = TextHelpers.GetFirstNonSpace(value);
            string nss = "";
            switch (c)
            {
                // return false boolean value
                case 'f':
                    nss = TextHelpers.RemoveSpaces(value);
                    if (nss.Length >= 5 && nss[0] == 'f' &&
                        nss[1] == 'a' && nss[2] == 'l' &&
                        nss[3] == 's' && nss[4] == 'e')
                    {
                        GenericTypeBool type_bool = new GenericTypeBool();
                        type_bool.Value = false;
                        return type_bool;
                    }
                    break;
                // return true boolean value
                case 't':
                    nss = TextHelpers.RemoveSpaces(value);
                    if (nss.Length >= 4 && nss[0] == 't' &&
                        nss[1] == 'r' && nss[2] == 'u' &&
                        nss[3] == 'e')
                    {
                        GenericTypeBool type_bool = new GenericTypeBool();
                        type_bool.Value = false;
                        return type_bool;
                    }
                    break;
                // return null
                case 'n':
                    GenericTypeJSONSeperator type_null = new GenericTypeJSONSeperator();
                    type_null.Value = new JSONSeperator();
                    return type_null;
            }
            // numeric value (int)
            if (TextHelpers.CharIsNumeric(c))
            {
                GenericTypeInt type_int = new GenericTypeInt();
                int int_value = Converter.ToInt(value);
                type_int.Value = int_value;
                return type_int;
            }
            //string
            GenericTypeJSONString type_string = new GenericTypeJSONString();
            type_string.Value = value;
            return type_string;
        }

        #region Get One Value from a Pair (found in objects only!)
        /// <summary>
        /// Get a value from the first layer
        /// </summary>
        /// <param name="name"><name of the object/param>
        /// <returns>object if found, else null</returns>
        GenericType GetValue(string name)
        {
            return GetValueFromObject(name, RootElement);
        }

        /// <summary>
        /// Get Function via a child
        /// </summary>
        /// <param name="names">names to go trought, to find the correct value</param>
        /// <param name="n">amount of strings in the nams array</param>
        /// <param name="child">child to take from</param>
        /// <returns>found value if found, else null</returns>
        private GenericType GetValueFromChild(string name, JSONBaseValue child)
        {
            if (child.ValueType == JSON_VALUE_TYPE.json_array)
            {
                return GetValueFromArray(name, (JSONArray)child);
            }
            else if (child.ValueType == JSON_VALUE_TYPE.json_object)
            {
                return GetValueFromObject(name, (JSONObject)child);
            }
            else if (child.ValueType == JSON_VALUE_TYPE.json_pair)
            {
                return GetValueFromPair(name, (JSONPair)child);
            }
            //no valid value
            return null;
        }

        /// <summary>
        /// See if we can find the value in an object
        /// </summary>
        /// <param name="name">name of the key of the correct pair</param>
        /// <param name="root">root to take from</param>
        /// <returns>found value if found, else null</returns>
        private GenericType GetValueFromObject(string name, JSONObject root)
        {
            GenericType foundValue = null;
            if (root.Values.Count > 0)
            {
                foreach (JSONBaseValue child in root.Values)
                {
                    foundValue = GetValueFromChild(name, child);
                    if (foundValue != null)
                    {
                        //Found!
                        return foundValue;
                    }
                }
            }
            //Value not found in this JSONValue!
            return null;
        }

        /// <summary>
        /// See if we can find the value in an array
        /// </summary>
        /// <param name="name">name of the key of the correct pair</param>
        /// <param name="root">child to take from</param>
        /// <returns>found value if found, else null</returns>
        private GenericType GetValueFromArray(string name,  JSONArray root)
        {
            GenericType foundValue = null;
            if (root.Values.Count > 0)
            {
                foreach (GenericType child in root.Values)
                {
                    
                    if (child.GetType() == new GenericTypeJSONArray().GetType())
                    {
                        GenericTypeJSONArray genericType = (GenericTypeJSONArray)child;
                        foundValue = GetValueFromArray(name, (JSONArray)genericType.Value);
                    }
                    else if (child.GetType() == new GenericTypeJSONObject().GetType())
                    {
                        GenericTypeJSONObject genericType = (GenericTypeJSONObject)child;
                        foundValue = GetValueFromObject(name, (JSONObject)genericType.Value);
                    }
                    if (foundValue != null)
                    {
                        //Found!
                        return foundValue;
                    }
                }
            }
            //Value not found in this JSONValue!
            return null;
        }

        /// <summary>
        /// See if we can find pair
        /// </summary>
        /// <param name="name">name of the key of the correct pair</param>
        /// <param name="pair">pair to take from</param>
        /// <returns>found value if found, else null</returns>
        private GenericType GetValueFromPair(string name, JSONPair pair)
        {
            if ((string)pair.Name.Value == name)
            {
                return pair.Value;
            }
            return null;
        }
        #endregion

        #region Get a raw ValueList (unfiltred, pure parser-content)
        /// <summary>
        /// Get the complete rootLayer (unfiltred, pure parser-content)
        /// </summary>
        /// <returns>complete rootLayer</returns>
        public List<JSONBaseValue> GetObjectList()
        {
            return RootElement.Values;
        }

        /// <summary>
        /// Get the complete Layer (unfiltred, pure parser-content)
        /// </summary>
        /// <returns>complete layer</returns>
        public List<JSONBaseValue> GetObjectList(string name)
        {
            return RootElement.Values;
        }
        #endregion

        //TODO:
        //  + GetValues () Get all values for which key is equal
        //  + GetArrayList()
        //  + GetFilteredObjectList()
        //  + GetFilteredArrayList()
    }
}