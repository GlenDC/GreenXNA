#region File Description
//-----------------------------------------------------------------------------
// FPS.cs
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

namespace GreenXNA.Statistics
{
    public class FPS
    {
        /// <summary>
        /// Current time in seconds
        /// </summary>
        private double    m_CurrentTime = 0;
        /// <summary>
        /// Last calculated FPS
        /// </summary>
        private int[]    m_FPS;
        /// <summary>
        /// Counter to calculate FPS each second
        /// </summary>
        private int      m_COUNTER = 0;
        /// <summary>
        /// Current FPS Slot ID to be used 
        /// in the FPS Hitory Array
        /// </summary>
        private int     m_FPS_ID;
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
                    average += m_FPS[i];
                }
                return average / History_FPS_CAP;
            }
        }
        /// <summary>
        /// Get the array of predefined x amount of FPS captures.
        /// </summary>
        public int[] FPSHistory { get { return m_FPS; } }
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
            m_FPS = new int[History_FPS_CAP];
            for (int i = 0; i < History_FPS_CAP; ++i)
            {
                m_FPS[i] = 60;
            }
            m_FPS_ID = 0;
        }

        /// <summary>
        /// Update FPS Calculation
        /// </summary>
        /// <param name="dt">time in seconds, since last cpu cycle</param>
        public void Update(double dt)
        {
            ++m_COUNTER;
            m_CurrentTime += dt;
            if (m_CurrentTime >= 1.0)
            {
                if (m_FPS_ID >= History_FPS_CAP)
                {
                    for (int i = 0; i < History_FPS_CAP - 2; ++i)
                    {
                        m_FPS[i] = m_FPS[i + 1];
                    }
                    --m_FPS_ID;
                }
                m_FPS[m_FPS_ID] = m_COUNTER;
                ++m_FPS_ID;
                Reset();
            }
        }

        /// <summary>
        /// Returns the FPS, recalculated each second
        /// </summary>
        /// <returns>last calculated FPS</returns>
        public int GetFPS()
        {
            return m_FPS[Math.Max(0,m_FPS_ID - 1)];
        }

        /// <summary>
        /// Reset the values, necessary for the FPS calculation
        /// </summary>
        public void Reset()
        {
            m_CurrentTime = 0;
            m_COUNTER = 0;
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
                    newHistory[i] = m_FPS[i];
                }
                else
                {
                    newHistory[i] = 0;
                }
            }
            m_FPS = newHistory;
        }
    }
}