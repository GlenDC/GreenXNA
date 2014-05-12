#region File Description
//-----------------------------------------------------------------------------
// Helpers.cs
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
using System.Text;
using System.Collections.Generic;
#endregion

namespace GreenXNA.GreenHelpers
{
    public static class Helpers
    {
        /// <summary>
        /// Generate hash value based on a string value
        /// </summary>
        /// <param name="str">string value to be converted to a hash value</param>
        /// <returns>generated hash value based on the input parameter (string)</returns>
        public static uint GenerateHash(string str)
        {
            uint hash = 0;
            for (int i = 0; i < str.Length; ++i)
                hash = 65599 * hash + str[i];
            return hash ^ (hash >> 16);
        }

        /// <summary>
        /// Generate long hash value based on a string value
        /// </summary>
        /// <param name="str">string value to be converted to a hash value</param>
        /// <returns>generated hash value based on the input parameter (string)</returns>
        public static ulong GenerateLongHash(string str)
        {
            ulong hash = 0;
            for (int i = 0; i < str.Length; ++i)
                hash = 65599 * hash + str[i];
            return hash ^ (hash >> 16);
        }

        /// <summary>
        /// Convert an integer to a string
        /// </summary>
        /// <param name="integer">integer to be converted to a string</param>
        /// <returns>string, containing the value of the interger in a string format</returns>
        public static string IntToString(int integer)
        {
            return integer.ToString();
        }

        /// <summary>
        /// Convert an integer to a string with x amount of digits
        /// </summary>
        /// <param name="integer">integer to be converted to a string</param>
        /// <param name="digits">amount of digits required in the string</param>
        /// <returns>string, containing the value of the interger in a string format with x amount of digits</returns>
        public static string IntToString(int integer, int digits)
        {
            digits = Math.Max(0, digits);
            string result = "";
            if (digits > 0)
            {
                int checkNumber = (int)Math.Pow(10, digits-1);
                for (int i = digits; i > 1; --i)
                {
                    if (integer < checkNumber)
                    {
                        result += "0";
                        checkNumber /= 10;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            result += IntToString(integer);
            return result;
        }
    }
}