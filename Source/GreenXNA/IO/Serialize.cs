#region File Description
//-----------------------------------------------------------------------------
// Serialize.cs
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
using System.IO;
using System.Collections;
using System.Collections.Generic;

using GreenXNA.IO.JSON;
#endregion

namespace GreenXNA.IO
{
    public static class Serialize
    {
        // enum, summerizing the different file
        // formats supported by GreenXNA
        private enum FileFormats
        {
            FMT_JSON = 0, 
            FMT_INI = 1,
            FMT_XML = 2,
            FMT_TXT = 3
        };

        // dictionary to save the format table (defines what extensions are allowed per type)
        private static Dictionary<FileFormats, uint> m_FormatTable = null;

        /// <summary>
        /// load the format table via the config file
        /// </summary>
        /// <param name="file">config file, containing the format table</param>
        public static void Initialize(string file)
        {
            using (JSONParser parser = new JSONParser(file, GREENXNA.PATH_CONFIG))
            {
                parser.Read();
            }
        }
    }
}
