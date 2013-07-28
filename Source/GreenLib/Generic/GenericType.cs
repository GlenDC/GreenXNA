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

using GreenXNA.GreenInterfaces;
using GreenXNA.GreenHelpers;
using GreenXNA.IO;
#endregion

namespace GreenXNA.Generic
{
    /// <summary>
    /// generic type abstact base class.
    /// </summary>
    public abstract class GenericType : IClone<GenericType>
    {
        public virtual object Value { get; set; }

        public abstract GenericType Clone();

        public abstract new string ToString();
    }

    /// <summary>
    /// string version of the generic type class.
    /// </summary>
    public sealed class GenericTypeString : GenericType
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
            GenericTypeString generic_type = new GenericTypeString();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return m_Value;
        }
    }

    /// <summary>
    /// char version of the generic type class.
    /// </summary>
    public sealed class GenericTypeChar : GenericType
    {
        private char m_Value;
        public override object Value
        {
            get
            {
                return (char)m_Value;
            }
            set
            {
                m_Value = (char)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeChar generic_type = new GenericTypeChar();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// int version of the generic type class.
    /// </summary>
    public sealed class GenericTypeInt : GenericType
    {
        private int m_Value;
        public override object Value
        {
            get
            {
                return (int)m_Value;
            }
            set
            {
                m_Value = (int)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeInt generic_type = new GenericTypeInt();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return Converter.ToString(m_Value);
        }
    }

    /// <summary>
    /// float version of the generic type class.
    /// </summary>
    public sealed class GenericTypeFloat : GenericType
    {
        private float m_Value;
        public override object Value
        {
            get
            {
                return (float)m_Value;
            }
            set
            {
                m_Value = (float)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeFloat generic_type = new GenericTypeFloat();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return Converter.ToString(m_Value);
        }
    }

    /// <summary>
    /// double version of the generic type class.
    /// </summary>
    public sealed class GenericTypeDouble : GenericType
    {
        private double m_Value;
        public override object Value
        {
            get
            {
                return (double)m_Value;
            }
            set
            {
                m_Value = (double)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeDouble generic_type = new GenericTypeDouble();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return Converter.ToString(m_Value);
        }
    }

    /// <summary>
    /// boolean version of the generic type class.
    /// </summary>
    public sealed class GenericTypeBool : GenericType
    {
        private bool m_Value;
        public override object Value
        {
            get
            {
                return (bool)m_Value;
            }
            set
            {
                m_Value = (bool)value;
            }
        }

        public override GenericType Clone()
        {
            GenericTypeBool generic_type = new GenericTypeBool();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return Converter.ToString(m_Value);
        }
    }
}