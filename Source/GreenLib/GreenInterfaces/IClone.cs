#region File Description
//-----------------------------------------------------------------------------
// IClone.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace GreenXNA.GreenInterfaces
{
    public interface IClone<T>
    {
        /// <summary>
        /// Clone Function, copies all the content of the object to a new object. 
        /// </summary>
        /// <returns>indepentend copy, containing all the content of the original object</returns>
        T Clone();
    }
}