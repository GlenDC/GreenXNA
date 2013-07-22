#region File Description
//-----------------------------------------------------------------------------
// GreenXNAMain.cs
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
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GreenXNA.Serialize;
using GreenXNA.Serialize.INI;
#endregion

namespace GreenXNA
{
    /// <summary>
    /// Main Entry Point for the GreenXNA FrameWork
    /// </summary>
    public static class GREENXNA
    {
        public static INIParameterContainer Settings { get; private set; }

        public static void Initialize()
        {
            LoadConfig("../Config/");
            ConfigFramework();
        }

        /// <summary>
        /// Load all the config files!
        /// </summary>
        private static void LoadConfig(string configPath)
        {
            Settings = new INIParameterContainer("GreenXNA", configPath);
        }

        private static void ConfigFramework()
        {
            Converter.SetCulture(Settings.GetString("FileParsing", "culture"));
        }
    }
}
