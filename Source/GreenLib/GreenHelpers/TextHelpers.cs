#region File Description
//-----------------------------------------------------------------------------
// TextHelpers.cs
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
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace GreenXNA.GreenHelpers
{
    /// <summary>
    /// Static class containing helper functions for text
    /// </summary>
    public static class TextHelpers
    {
        /// <summary>
        /// Check wheter a character is whitespace or not
        /// </summary>
        /// <param name="c">character to be checked</param>
        /// <returns>true if c is a whitespace char</returns>
        public static bool IsWhiteSpaceChar(char c)
        {
            return c == ' ' || // space
                    c == '\n' || // new-line
                    c == '\t' || // horizontal tab
                    c == '\b' || // backspace
                    c == '\v';  // vertical tab
        }
        
        /// <summary>
        /// Check if a character is equal to a certain set of characters.
        /// </summary>
        /// <param name="c">character to be checked</param>
        /// <param name="chars">set of characters to test c for equality</param>
        /// <param name="n">amount of characters in the array</param>
        /// <param name="result">character c is equal to, \0 if equal to none</param>
        /// <returns>true if equality found, false if not</returns>
        public static bool CharIsEqualTo(char c, char[] chars, int n, out char result)
        {
            result = '\0';
            for (int i = 0; i < n; ++i)
            {
                if (c == chars[i])
                {
                    result = chars[i];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if a character is a numeric character.
        /// </summary>
        /// <param name="c">character to be checked</param>
        /// <param name="result">character c is equal to, \0 if equal to none</param>
        /// <returns>true if equality found, false if not</returns>
        public static bool CharIsNumeric(char c, out char result)
        {
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            return CharIsEqualTo(c, chars, 10, out result);
        }

        /// <summary>
        /// Check if a character is a numeric character.
        /// </summary>
        /// <param name="c">character to be checked</param>
        /// <param name="result">character c is equal to, \0 if equal to none</param>
        /// <returns>true if equality found, false if not</returns>
        public static bool CharIsNumeric(char c)
        {
            char result;
            return CharIsNumeric(c, out result);
        }

        /// <summary>
        /// Check if a character is equal to a certain set of characters.
        /// </summary>
        /// <param name="c">character to be checked</param>
        /// <param name="chars">set of characters to test c for equality</param>
        /// <param name="n">amount of characters in the array</param>
        /// <returns>true if equality found, false if not</returns>
        public static bool CharIsEqualTo(char c, char[] chars, int n)
        {
            char result;
            return CharIsEqualTo(c, chars, n, out result);
        }

        /// <summary>
        /// Read a stream untill char 'c' or 
        /// untill the end of the stream has been reached 
        /// </summary>
        /// <param name="stream">input to be read off</param>
        /// <param name="c">character to be searched for</param>
        /// <param name="wc">true to ignore white space characters. For performance reasons this should be your default choice.</param>
        /// <returns>string that has been read</returns>
        public static string ReadUntillCharReached(StreamReader stream, char c, bool wc)
        {
            string str = "";
            char checkC = ' ';
            do
            {
                checkC = (char)stream.Read();
                if (checkC != c && (!wc || !IsWhiteSpaceChar(checkC)))
                {
                    str += checkC;
                }
            } while (!stream.EndOfStream && checkC != c);
            return str;
        }

        /// <summary>
        /// Read a stream untill a char from set 'c' or 
        /// untill the end of the stream has been reached 
        /// </summary>
        /// <param name="stream">input to be read from</param>
        /// <param name="c">set of characters to be tested from</param>
        /// <param name="chars">amount of characters in set 'c'</param>
        /// <param name="wc">true to ignore whitespaces. For performance reasons this should be your default choice.</param>
        /// <param name="result">character for which equality was true, \0 if none has been found.</param>
        /// <returns>string that has been read</returns>
        public static string ReadUntillCharReached(StreamReader stream, char[] c, int chars, bool wc, out char result)
        {
            string str = "";
            result = '\0';
            char checkC = ' ';
            do
            {
                checkC = (char)stream.Read();
                if (!CharIsEqualTo(checkC, c, chars, out result) && (!wc || !IsWhiteSpaceChar(checkC)))
                {
                    str += checkC;
                }
            } while (!stream.EndOfStream && !CharIsEqualTo(checkC, c, chars, out result));
            return str;
        }

        /// <summary>
        /// Read a string untill char 'c' or 
        /// untill the end of the stream has been reached 
        /// </summary>
        /// <param name="stringStream">string to be read from</param>
        /// <param name="c">character to be searched for</param>
        /// <param name="wc">true to ignore white space characters. For performance reasons this should be your default choice.</param>
        /// <returns>string that has been read</returns>
        public static string ReadUntillCharReached(string stringStream, char c, bool wc)
        {
            string str = "";
            char checkC = ' ';
            do
            {
                checkC = stringStream[0];
                stringStream = stringStream.Substring(1, stringStream.Length - 1);
                if (checkC != c && (!wc || !IsWhiteSpaceChar(checkC)))
                {
                    str += checkC;
                }
            } while (stringStream.Length > 0 && checkC != c);
            return str;
        }

        /// <summary>
        /// Read a string untill a char from set 'c' or 
        /// untill the end of the stream has been reached 
        /// </summary>
        /// <param name="stringStream">string to be read from</param>
        /// <param name="c">set of characters to be tested from</param>
        /// <param name="chars">amount of characters in set 'c'</param>
        /// <param name="wc">true to ignore whitespaces. For performance reasons this should be your default choice.</param>
        /// <param name="result">character for which equality was true, \0 if none has been found.</param>
        /// <returns>string that has been read</returns>
        public static string ReadUntillCharReached(string stringStream, char[] c, int chars, bool wc, out char result)
        {
            string str = "";
            result = '\0';
            char checkC = ' ';
            do
            {
                checkC = stringStream[0];
                stringStream = stringStream.Substring(1, stringStream.Length - 1);
                if (!CharIsEqualTo(checkC, c, chars, out result) && (!wc || !IsWhiteSpaceChar(checkC)))
                {
                    str += checkC;
                }
            } while (stringStream.Length > 0 && !CharIsEqualTo(checkC, c, chars, out result));
            return str;
        }

        /// <summary>
        /// Filter the string from unwanted characters
        /// </summary>
        /// <param name="str">string to be filtered from spaces</param>
        /// <param name="c">array of characters to be filtered</param>
        /// <param name="n">number of characters to be filtered</param>
        /// <returns>new filtered created string</returns>
        public static string FilterString(string str, char[] c, int n)
        {
            string newString = "";
            do
            {
                char checkC = str[0];
                str = str.Substring(1, str.Length - 1);
                if (!CharIsEqualTo(checkC, c, n))
                {
                    newString += checkC.ToString();
                }
            } while (str.Length > 0);
            return newString;
        }

        /// <summary>
        /// Parse the string without the spaces
        /// </summary>
        /// <param name="str">string to be filtered from spaces</param>
        /// <returns>new filtered created string</returns>
        public static string RemoveSpaces(string str)
        {
            char [] c = { ' ' };
            return FilterString(str, c, 1);
        }

        /// <summary>
        /// Get first character that is not equal to one of the characters of c
        /// </summary>
        /// <param name="str">string to get character from</param>
        /// <param name="c">characters to test</param>
        /// <param name="n">amount of characters</param>
        /// <returns>first character found, else null character</returns>
        public static char GetFirstCharNotEqualTo(string str, char[] c, int n)
        {
            char result = '\0';
            if (str.Length > 0)
            {
                do
                {
                    result = str[0];
                    str = str.Substring(1, str.Length - 1);
                } while (CharIsEqualTo(result, c, n) && str.Length > 0);
            }
            return result;
        }

        /// <summary>
        /// Get first character that is not a space
        /// </summary>
        /// <param name="str">string to get character from</param>
        /// <returns>first character found, else null character</returns>
        public static char GetFirstNonSpace(string str)
        {
            char result = '\0';
            if (str.Length > 0)
            {
                do
                {
                    result = str[0];
                    str = str.Substring(1, str.Length - 1);
                } while (result == ' ' && str.Length > 0);
            }
            return result;
        }

        /// <summary>
        /// Retrieve the extension of a file name
        /// </summary>
        /// <param name="file">name of the file</param>
        /// <returns>extension of the file as a string object</returns>
        public static string GetFileExtension(string file)
        {
            int ext_start = file.LastIndexOf('.');
            return file.Substring(ext_start);
        }

        /// <summary>
        /// Retrieve the name of a file name ( without the exension 
        /// </summary>
        /// <param name="file">name of the file</param>
        /// <returns>name of the file as a string object</returns>
        public static string GetFileName(string file)
        {
            int name_length = file.LastIndexOf('.');
            return file.Substring(0, name_length);
        }

        /// <summary>
        /// Get the extension and name seperatly of a filename
        /// </summary>
        /// <param name="file">name of the file</param>
        /// <param name="name">name of the file without the extension (result)</param>
        /// <param name="extension">extenion of the file without the point (result)</param>
        public static void SplitFileName(string file, out string name, out string extension)
        {
            int ext_start = file.LastIndexOf('.');
            extension = file.Substring(ext_start+1);
            name = file.Substring(0, ext_start);
        }
    }
}