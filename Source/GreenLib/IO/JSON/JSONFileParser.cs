#region File Description
//-----------------------------------------------------------------------------
// JSONFileParser.cs
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
using System.Xml.Schema;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.JSON
{
    public class JSONFileParser : BaseFileIO, IFileParser<JSONFileContainer, string>
    {
        /// <summary>
        /// seperator string to seperate values in a JSON File.
        /// </summary>
        protected const string JSON_SEPERATOR = ", ";
        /// <summary>
        /// Null Char Character
        /// </summary>
        protected const char NULL_CHAR = '\0';

        /// <summary>
        /// Open the file. This should be done in the heading of your using body.
        /// </summary>
        /// <param name="path">dynamic path to the JSON file.</param>
        /// <param name="file">name of the file, including the extension</param>
        public JSONFileParser(string path, string file)
            : base(path, file)
        {
        }

        public void Read(JSONFileContainer container)
        {
            if (File.Exists(m_File))
            {
                try
                {
                    using (StreamReader stream = new StreamReader(m_File))
                    {
                        TextHelpers.ReadUntillCharReached(stream, '{', true);

                        if (!stream.EndOfStream)
                        {
                            ReadObject(stream);
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
                throw new FileNotFoundException("Unable to locate " + m_File);
            }
        }

        /// <summary>
        /// Read a value in the JSON File. if their is anything left
        /// Can be any kind of value
        /// </summary>
        /// <param name="stream">stream input to be read from</param>
        private ITemplateType ReadValue(StreamReader stream)
        {
            //Step 1: get to know what we are looking for!
            char resultChar = NULL_CHAR;
            char[] charSet = { '{', 'n', 'N', '[', '"', '}', ']', 'f', 
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
            while (stream.Peek() == ' ')
            {
                peekChar = (char)stream.Read();
            }
            peekChar = (char)stream.Peek();
            char[] chars_to_check = { '"', '}', ']', ',' };
            int num = 4;
            if ((firstChar == ' ' || firstChar == '=') &&
                peekChar == ',' || peekChar == '\r')
            {
                return null;
            }
            if (firstChar == '"')
            {
                char[] newSet = { '"', '}', ']' };
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
        private ITemplateType ReadSeperator(StreamReader stream)
        {
            // read complete word
            char resultChar = NULL_CHAR;
            char[] charSet = { 'l', 'L' };
            TextHelpers.ReadUntillCharReached(stream, charSet, 2, true, out resultChar);

            System.Diagnostics.Debug.Assert(resultChar != NULL_CHAR, "Corrupted File.");

            // if everything is ok => return new Seperator object
            return null;
        }

        /// <summary>
        /// Read a pair from an object in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <param name="lastChar">last character that has been read</param>
        /// <returns>new parsed pair</returns>
        private TKeyValuePair<TObject, ITemplateType> ReadPair(StreamReader stream, char lastChar)
        {
            TObject name = null;
            ITemplateType type = null;

            // (1) check if object has a name => char == " OR :
            char resultChar = NULL_CHAR;
            char[] charSet = { '\"', ':' };
            string textResult;
            textResult = TextHelpers.ReadUntillCharReached(stream, charSet, 2, true, out resultChar);

            // if couldn't be read => Corrupted => return null!
            System.Diagnostics.Debug.Assert(resultChar != NULL_CHAR, "File is corrupt.");
            // (2) else check if char == '"' => we still need a name!
            if (resultChar == '\"')
            {
                if (lastChar == '"')
                {
                    name = new TObject(textResult);
                }
                else
                {
                    name = new TObject(ReadStringValue(stream));
                }
                // corrupted file => return null!
                System.Diagnostics.Debug.Assert(!stream.EndOfStream, "File is corrupt.");
                // an extra check for ':'
                textResult = TextHelpers.ReadUntillCharReached(stream, ':', true);
            }

            System.Diagnostics.Debug.Assert(!stream.EndOfStream, "File is corrupt.");
            if (!stream.EndOfStream)
            {
                type = ReadValue(stream);
            }

            KeyValuePair<TObject, ITemplateType> keyValuepair = 
                new KeyValuePair<TObject, ITemplateType>(name, type);
            TKeyValuePair<TObject, ITemplateType> pair =
                new TKeyValuePair<TObject, ITemplateType>(keyValuepair);


            return pair;
        }

        /// <summary>
        /// Read an object in the JSON File
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <returns>new parsed object</returns>
        private TList<ITemplateType> ReadObject(StreamReader stream)
        {
            List<ITemplateType> list = new List<ITemplateType>();
            char c = '\0';
            char[] chars = { ',', 'n', 'N', '}' };

            do
            {
                // seperator
                if (c == 'n' || c == 'N')
                {
                    var seperator = ReadSeperator(stream);
                    System.Diagnostics.Debug.Assert(seperator == null, "Corrupted file.");
                    if (seperator != null)
                    {
                        list.Add(seperator);
                    }
                }
                else if (c != '}')
                {
                    TKeyValuePair<TObject, ITemplateType> pair;
                    pair = ReadPair(stream, c);
                    list.Add(pair);
                }
                string result = TextHelpers.ReadUntillCharReached(stream, chars, 4, true, out c);
            } while (!stream.EndOfStream && c != '}');

            // (4) end of object reached!
            return new TList<ITemplateType>(list);
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

        /// <summary>
        /// Nothing to dispose
        /// </summary>
        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
