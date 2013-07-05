#region File Description
//-----------------------------------------------------------------------------
// GreenStatistics.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
// More information and details can be found at http://greenxna.glendc.com/
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
using System.Threading.Tasks;
using System.Collections.Generic;

using GreenXNA.GreenHelpers;

using Microsoft.Xna.Framework;
#endregion

namespace GreenXNA.Statistics
{
    /// <summary>
    /// Static class, that calculate and delivers statistics,
    /// targetted both to the developer and the end-user.
    /// </summary>
    public static class GreenStatistics
    {
        /// <summary>
        /// FPS Class, calculating the FPS (Most usefull for end-user)
        /// </summary>
        private static FPS _FPS;
        /// <summary>
        /// Get the last calculated FPS Value
        /// </summary>
        public static int FPS { get { return _FPS.GetFPS(); } }
        /// <summary>
        /// Get the last calculated FPS Value in a 2-digit string format
        /// </summary>
        public static string FPS_STRING { get { return Helpers.IntToString(FPS, 2); } }
        /// <summary>
        /// Get the average FPS value
        /// </summary>
        public static int AVERAGE_FPS { get { return _FPS.AverageFPS; } }
        /// <summary>
        /// Get the average FPS value in a 2-digit string format
        /// </summary>
        public static string AVERAGE_FPS_STRING { get { return Helpers.IntToString(AVERAGE_FPS, 2); } }
        /// <summary>
        /// Get the complete FPS History
        /// </summary>
        public static int[] FPS_HISTORY { get { return _FPS.FPSHistory; } }
        /// <summary>
        /// Get the cap for the history FPS array
        /// </summary>
        public static int FPS_HISTORY_CAP { get { return _FPS.History_FPS_CAP; } }

        /// <summary>
        /// initialize the Statistics.
        /// </summary>
        public static void Initialize()
        {
            // initialize the FPS-object
            _FPS = new FPS();
            _FPS.Initialize();
        }

        public static void Update(GameTime gameTime)
        {
            _FPS.Update(gameTime);
        }
    }
}
