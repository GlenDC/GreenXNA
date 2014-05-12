#region File Description
//-----------------------------------------------------------------------------
// GreenDelegates.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014       ***     Last Edit: July 2013
// More information and details can be found at http://www.greenxna.com/
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Math Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Text;
using System.Collections.Generic;
#endregion

namespace GreenXNA.GreenInterfaces
{
    /// <summary>
    /// A function with a void return value and no parameters
    /// </summary>
    public delegate void DVoid();
    /// <summary>
    /// Delegate to be used to check something.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public delegate bool DIsTrue(object obj);
}