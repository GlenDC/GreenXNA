#region File Description
//-----------------------------------------------------------------------------
// FPS.cs
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
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace GreenXNA.Statistics
{
    public class FPS
    {
        /// <summary>
        /// Current time in seconds
        /// </summary>
        private double    _CurrentTime = 0;
        /// <summary>
        /// Last calculated FPS
        /// </summary>
        private int[]    _FPS;
        /// <summary>
        /// Counter to calculate FPS each second
        /// </summary>
        private int      _COUNTER = 0;
        /// <summary>
        /// Current FPS Slot ID to be used 
        /// in the FPS Hitory Array
        /// </summary>
        private int     _FPS_ID;
        /// <summary>
        /// Get a fresh calculation of the average FPS over time, 
        /// based on a predefined x amount of frames.
        /// </summary>
        public int AverageFPS
        {
            get
            {
                int average = 0;
                for (int i = 0; i < History_FPS_CAP; ++i)
                {
                    average += _FPS[i];
                }
                return average / History_FPS_CAP;
            }
        }
        /// <summary>
        /// Get the array of predefined x amount of FPS captures.
        /// </summary>
        public int[] FPSHistory { get { return _FPS; } }
        /// <summary>
        /// Amount of calculated FPS's that has to be saved in memory
        /// </summary>
        public int History_FPS_CAP { get; private set; }

        /// <summary>
        /// Initialize the FPS clas Variables
        /// </summary>
        public void Initialize()
        {
            History_FPS_CAP = 25;
            _FPS = new int[History_FPS_CAP];
            for (int i = 0; i < History_FPS_CAP; ++i)
            {
                _FPS[i] = 60;
            }
            _FPS_ID = 0;
        }

        /// <summary>
        /// Update FPS Calculation
        /// </summary>
        /// <param name="dt">time in seconds, since last cpu cycle</param>
        public void Update(double dt)
        {
            ++_COUNTER;
            _CurrentTime += dt;
            if (_CurrentTime >= 1.0)
            {
                if (_FPS_ID >= History_FPS_CAP)
                {
                    for (int i = 0; i < History_FPS_CAP - 2; ++i)
                    {
                        _FPS[i] = _FPS[i + 1];
                    }
                    --_FPS_ID;
                }
                _FPS[_FPS_ID] = _COUNTER;
                ++_FPS_ID;
                Reset();
            }
        }

        /// <summary>
        /// Returns the FPS, recalculated each second
        /// </summary>
        /// <returns>last calculated FPS</returns>
        public int GetFPS()
        {
            return _FPS[Math.Max(0,_FPS_ID - 1)];
        }

        /// <summary>
        /// Reset the values, necessary for the FPS calculation
        /// </summary>
        public void Reset()
        {
            _CurrentTime = 0;
            _COUNTER = 0;
        }

        /// <summary>
        /// Redefine the cap of the history fps array. 
        /// </summary>
        /// <param name="cap">defines how many fps captures will be saved in an array of 'cap' size.</param>
        public void SetFPSHistoryCap(int cap)
        {
            int[] newHistory = new int[cap];
            for (int i = 0; i < cap; ++i)
            {
                if (i < History_FPS_CAP)
                {
                    newHistory[i] = _FPS[i];
                }
                else
                {
                    newHistory[i] = 0;
                }
            }
            _FPS = newHistory;
        }
    }
}