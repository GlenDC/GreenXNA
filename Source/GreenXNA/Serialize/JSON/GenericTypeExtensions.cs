#region File Description
//-----------------------------------------------------------------------------
// GenericType.cs
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
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Collections.Generic;
#endregion

namespace GreenXNA.Serialize.JSON
{
    /// <summary>
    /// JSONArray version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONArray : GenericType
    {
        private JSONArray _Value;
        public override object Value
        {
            get
            {
                return (JSONArray)_Value;
            }
            set
            {
                _Value = (JSONArray)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONArray generic_type = new GenericTypeJSONArray();
            generic_type.Value = _Value.Clone();
            return generic_type;
        }

        public override string ToString()
        {
            return _Value.ToString();
        }
    }

    /// <summary>
    /// JSONObject version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONObject : GenericType
    {
        private JSONObject _Value;
        public override object Value
        {
            get
            {
                return (JSONObject)_Value;
            }
            set
            {
                _Value = (JSONObject)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONObject generic_type = new GenericTypeJSONObject();
            generic_type.Value = _Value.Clone();
            return generic_type;
        }

        public override string ToString()
        {
            return _Value.ToString();
        }
    }

    /// <summary>
    /// JSONSeperator version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONSeperator : GenericType
    {
        private JSONSeperator _Value;
        public override object Value
        {
            get
            {
                return (JSONSeperator)_Value;
            }
            set
            {
                _Value = (JSONSeperator)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONSeperator generic_type = new GenericTypeJSONSeperator();
            generic_type.Value = _Value.Clone();
            return generic_type;
        }

        public override string ToString()
        {
            return _Value.ToString();
        }
    }

    /// <summary>
    /// JSONString version of the generic type class.
    /// </summary>
    public sealed class GenericTypeJSONString : GenericType
    {
        private string _Value;
        public override object Value
        {
            get
            {
                return (string)_Value;
            }
            set
            {
                _Value = (string)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeJSONString generic_type = new GenericTypeJSONString();
            generic_type.Value = _Value;
            return generic_type;
        }

        public override string ToString()
        {
            return '"' + _Value + '"';
        }
    }
}