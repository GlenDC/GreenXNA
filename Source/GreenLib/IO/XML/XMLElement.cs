#region File Description
//-----------------------------------------------------------------------------
// XMLElement.cs
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

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.IO.XML
{
    public class XMLElement : GenericDictionary<XMLElement>
    {
        public GenericTypeDictionary Attributes { get; private set; }
        public TObject Value { get; set; }

        public bool HasChildren { get { return m_Container.Count != 0; } }
        public bool HasAttributes { get { return !Attributes.IsEmpty; } }

        /// <summary>
        /// Create a dictionary, usable with all classes
        /// based on the ITemplateType interface.
        /// </summary>
        public XMLElement()
            : base()
        {
            Attributes = new GenericTypeDictionary();
            Value = null;
        }
    }
}
