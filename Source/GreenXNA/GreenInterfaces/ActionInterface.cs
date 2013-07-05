#region File Description
//-----------------------------------------------------------------------------
// ActionInterface.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
// More information and details can be found at http://greenxna.glendc.com/
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Math Public License as published by
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

namespace GreenXNA.GreenInterfaces
{
    /// <summary>
    /// Interface used to record actions, 
    /// in most cases triggered by user input
    /// </summary>
    interface ActionInterface
    {
        /// <summary>
        /// The action as it has been recorded.
        /// </summary>
        void DoAction();
        /// <summary>
        /// The same recorded action, but reversed.
        /// </summary>
        void DoReverse();
    }
}