#region File Description
//-----------------------------------------------------------------------------
// GenericType.cs
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
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Collections.Generic;

using GreenXNA.Generic;
#endregion

namespace GreenXNA.IO.JSON
{
    /// <summary>
    /// JSONArray version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONArray : GenericType
    {
        private JSONArray m_Value;
        public override object Value
        {
            get
            {
                return (JSONArray)m_Value;
            }
            set
            {
                m_Value = (JSONArray)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONArray generic_type = new GenericTypeJSONArray();
            generic_type.Value = m_Value.Clone();
            return generic_type;
        }

        public override string ToString()
        {
            return m_Value.ToString();
        }
    }

    /// <summary>
    /// JSONObject version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONObject : GenericType
    {
        private JSONObject m_Value;
        public override object Value
        {
            get
            {
                return (JSONObject)m_Value;
            }
            set
            {
                m_Value = (JSONObject)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONObject generic_type = new GenericTypeJSONObject();
            generic_type.Value = m_Value.Clone();
            return generic_type;
        }

        public override string ToString()
        {
            return m_Value.ToString();
        }
    }

    /// <summary>
    /// JSONSeperator version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONSeperator : GenericType
    {
        private JSONSeperator m_Value;
        public override object Value
        {
            get
            {
                return (JSONSeperator)m_Value;
            }
            set
            {
                m_Value = (JSONSeperator)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONSeperator generic_type = new GenericTypeJSONSeperator();
            generic_type.Value = m_Value.Clone();
            return generic_type;
        }

        public override string ToString()
        {
            return m_Value.ToString();
        }
    }

    /// <summary>
    /// JSONString version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONString : GenericType
    {
        private string m_Value;
        public override object Value
        {
            get
            {
                return (string)m_Value;
            }
            set
            {
                m_Value = (string)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONString generic_type = new GenericTypeJSONString();
            generic_type.Value = m_Value;
            return generic_type;
        }

        public override string ToString()
        {
            return '"' + m_Value + '"';
        }
    }
}