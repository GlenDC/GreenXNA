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

using GreenXNA.GreenInterfaces;
using GreenXNA.GreenHelpers;
using GreenXNA.Serialize;
#endregion

namespace GreenXNA
{
    /// <summary>
    /// generic type abstact base class.
    /// </summary>
    public abstract class GenericType : CloneInterface<GenericType>
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
            GenericTypeString generic_type = new GenericTypeString();
            generic_type.Value = Value;
            return generic_type;
        }

        public override string ToString()
        {
            return _Value;
        }
    }

    /// <summary>
    /// char version of the generic type class.
    /// </summary>
    public sealed class GenericTypeChar : GenericType
    {
        private char _Value;
        public override object Value
        {
            get
            {
                return (char)_Value;
            }
            set
            {
                _Value = (char)value;
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
        private int _Value;
        public override object Value
        {
            get
            {
                return (int)_Value;
            }
            set
            {
                _Value = (int)value;
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
            return Converter.ToString(_Value);
        }
    }

    /// <summary>
    /// float version of the generic type class.
    /// </summary>
    public sealed class GenericTypeFloat : GenericType
    {
        private float _Value;
        public override object Value
        {
            get
            {
                return (float)_Value;
            }
            set
            {
                _Value = (float)value;
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
            return Converter.ToString(_Value);
        }
    }

    /// <summary>
    /// double version of the generic type class.
    /// </summary>
    public sealed class GenericTypeDouble : GenericType
    {
        private double _Value;
        public override object Value
        {
            get
            {
                return (double)_Value;
            }
            set
            {
                _Value = (double)value;
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
            return Converter.ToString(_Value);
        }
    }

    /// <summary>
    /// boolean version of the generic type class.
    /// </summary>
    public sealed class GenericTypeBool : GenericType
    {
        private bool _Value;
        public override object Value
        {
            get
            {
                return (bool)_Value;
            }
            set
            {
                _Value = (bool)value;
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
            return Converter.ToString(_Value);
        }
    }
}