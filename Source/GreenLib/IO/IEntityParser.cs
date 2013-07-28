#region File Description
//-----------------------------------------------------------------------------
// IEntityParser.cs
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
using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO
{
    /// <summary>
    /// Defines the interface, to allow parsing per entity.
    /// </summary>
    interface IEntityParser
    {
        /// <summary>
        /// Read the next object
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <returns>a fresh created object, from type T, based on the content of the opened file</returns>
        T ReadNext<T>();
        /// <summary>
        /// Read the next object
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <param name="n">read n ammount of characters</param>
        /// <returns>a fresh created object, from type T, based on the content of the opened file</returns>
        T ReadNext<T>(uint n);
        /// <summary>
        /// read untill a character has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <param name="delimiter">delimiter that defines the end of this object</param>
        /// <returns>object, created with the filecontent, including the delimiter</returns>
        T ReadNext<T>(char delimiter);
        /// <summary>
        /// read untill a char has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <param name="delimiter">delimiter that defines the end of this object</param>
        /// <param name="include_delimiter">set true to include the delimiter in the result</param>
        /// <returns>object, created with the filecontent</returns>
        T ReadNext<T>(char delimiter, bool include_delimiter);
        /// <summary>
        /// read untill a delimiter has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <typeparam name="U">type of the delimiter</typeparam>
        /// <param name="delimiter">delimiter that defines the end of this object</param>
        /// <returns>object, created with the filecontent, including the delimiter</returns>
        T ReadNext<T, U>(U delimiter);
        /// <summary>
        /// read untill a char has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <typeparam name="U">type of the delimiter</typeparam>
        /// <param name="delimiter">delimiter that defines the end of this object</param>
        /// <param name="include_delimiter">set true to include the delimiter in the result</param>
        /// <returns>object, created with the filecontent</returns>
        T ReadNext<T, U>(U delimiter, bool include_delimiter);
        /// <summary>
        /// read untill a sequence of characters has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <param name="delimiters">delimiters that defines the end of this object</param>
        /// <param name="n">amount of characters in the sequence</param>
        /// <returns>object, created with the filecontent, including the delimiters</returns>
        T ReadNext<T>(char[] delimiters, uint n);
        /// <summary>
        /// read untill a sequence of characters has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <param name="delimiters">delimiters that defines the end of this object</param>
        /// <param name="n">amount of characters in the sequence</param>
        /// <param name="include_delimiters">set true to include the delimiters in the result</param>
        /// <returns>object, created with the filecontent</returns>
        T ReadNext<T>(char[] delimiters, uint n, bool include_delimiters);
        /// <summary>
        /// read untill a sequence of delimiters has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <typeparam name="U">type of the delimiters</typeparam>
        /// <param name="delimiters">delimiter that defines the end of this object</param>
        /// <param name="n">amount of characters in the sequence</param>
        /// <returns>object, created with the filecontent, including the delimiters</returns>
        T ReadNext<T, U>(U[] delimiters, uint n);
        /// <summary>
        /// read untill a sequence of delimiters has been reached.
        /// </summary>
        /// <typeparam name="T">type of the object to be parsed from the opened file</typeparam>
        /// <typeparam name="U">type of the delimiters</typeparam>
        /// <param name="delimiters">delimiters that defines the end of this object</param>
        /// <param name="n">amount of characters in the sequence</param>
        /// <param name="include_delimiter">set true to include the delimiters in the result</param>
        /// <returns>object, created with the filecontent</returns>
        T ReadNext<T, U>(U[] delimiters, uint n, bool include_delimiter);
    }
}