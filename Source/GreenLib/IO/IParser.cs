#region File Description
//-----------------------------------------------------------------------------
// IParser.cs
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
#endregion

namespace GreenXNA.Serialize
{
    // General interface for a file-parser (any format)
    interface IParser
    {
        void Read();

        void Write();
    }
}
