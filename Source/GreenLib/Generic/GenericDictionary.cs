#region File Description
//-----------------------------------------------------------------------------
// GenericDictionary.cs
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
using System.Runtime.Serialization;

using GreenXNA.Generic;
using GreenXNA.GreenHelpers;
#endregion

namespace GreenXNA.Generic
{
    /// <summary>
    /// Representing a generic dictionary, with many options.
    /// <typeparam name="TValue"></typeparam>
    /// </summary>
    public class GenericDictionary<TValue> : ICloneable, IDisposable, IEnumerable, 
        IEnumerable<KeyValuePair<Tuple<uint, uint, string>, TValue>>, 
        ISerializable where TValue:ICloneable
    {
        /// <summary>
        /// Container, containing the values and keys from the container
        /// </summary>
        protected Dictionary<Tuple<uint, uint, string>, TValue> m_Container;
        /// <summary>
        /// Dictionary, containing the amount of elements in the container
        /// </summary>
        protected Dictionary<uint, uint> m_ContainerIndex;

        public int Count
        {
            get { return m_Container.Count; }
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        /// <summary>
        /// Create two containers, one for the keys and one for the values
        /// </summary>
        public GenericDictionary()
        {
            m_Container = new Dictionary<Tuple<uint, uint, string>, TValue>();
            m_ContainerIndex = new Dictionary<uint, uint>();
        }

        public TValue this[string key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                SetValue(key, value);
            }
        }

        #region protected: assist methods
        protected uint GenerateID(string value)
        {
            return Helpers.GenerateHash(value);
        }

        protected Tuple<uint, uint, string> GenerateTuple(uint id, string key)
        {
            return GenerateTuple(id, 0, key);
        }

        protected Tuple<uint, uint, string> GenerateTuple(uint id, uint index, string key)
        {
            return new Tuple<uint, uint, string>(id, index, key);
        }

        protected KeyValuePair<Tuple<uint, uint, string>, TValue> CreatePair
            (Tuple<uint, uint, string> tuple)
        {
            return new KeyValuePair<Tuple<uint, uint, string>, TValue>(tuple, m_Container[tuple]);
        }

        #endregion

        #region protected: (un)register element in the m_ContainerIndex container
        protected void RegisterElement(uint id)
        {
            if (!m_ContainerIndex.ContainsKey(id))
            {
                m_ContainerIndex.Add(id, 1);
                return;
            }
            ++m_ContainerIndex[id];
        }

        protected bool UnregisterElement(uint id)
        {
            bool containsKey = m_ContainerIndex.ContainsKey(id);
            System.Diagnostics.Debug.Assert(containsKey, "Couldn't find key!");
            if (containsKey && m_ContainerIndex[id] > 0)
            {
                --m_ContainerIndex[id];
                return true;
            }
            return false;
        }
        #endregion

        #region protected: get a single pair
        protected KeyValuePair<Tuple<uint, uint, string>, TValue> GetPair(string key)
        {
            return GetPair(key, 0);
        }

        protected KeyValuePair<Tuple<uint, uint, string>, TValue> GetPair(string key, uint index)
        {
            uint id = GenerateID(key);
            bool isValid = m_ContainerIndex.ContainsKey(id) &&
                index < m_ContainerIndex[id];
            System.Diagnostics.Debug.Assert(isValid, "Key couldn't be found!");
            if (isValid)
            {
                Tuple<uint, uint, string> tuple
                    = new Tuple<uint, uint, string>(id, index, key);
                return new KeyValuePair<Tuple<uint, uint, string>, TValue>(
                    tuple, m_Container[tuple]);
            }
            return new KeyValuePair<Tuple<uint, uint, string>, TValue>();
        }

        protected KeyValuePair<Tuple<uint, uint, string>, TValue> GetLastPair(string key)
        {
            uint id = GenerateID(key);
            bool isValid = m_ContainerIndex.ContainsKey(id);
            System.Diagnostics.Debug.Assert(isValid, "Key couldn't be found!");
            if (isValid)
            {
                Tuple<uint, uint, string> tuple
                    = new Tuple<uint, uint, string>(id, m_ContainerIndex[id] - 1, key);
                return new KeyValuePair<Tuple<uint, uint, string>, TValue>(
                    tuple, m_Container[tuple]);
            }
            return new KeyValuePair<Tuple<uint, uint, string>, TValue>();
        }
        #endregion

        #region public: get a single value
        public TValue GetValue(string key)
        {
            return GetValue(key, 0);
        }

        public TValue GetValue(string key, uint index)
        {
            KeyValuePair<Tuple<uint, uint, string>, TValue>
                pair = GetPair(key, index);
            return pair.Value;
        }

        public TValue GetLastValue(string key)
        {
            KeyValuePair<Tuple<uint, uint, string>, TValue>
                pair = GetLastPair(key);
            return pair.Value;
        }
        #endregion

        #region public: get a single cloned value
        public TValue GetClonedValue(string key)
        {
            return GetClonedValue(key, 0);
        }

        public TValue GetClonedValue(string key, uint index)
        {
            KeyValuePair<Tuple<uint, uint, string>, TValue>
                pair = GetPair(key, index);
            return (TValue)pair.Value.Clone();
        }

        public TValue GetClonedLastValue(string key)
        {
            KeyValuePair<Tuple<uint, uint, string>, TValue>
                pair = GetLastPair(key);
            return (TValue)pair.Value.Clone();
        }
        #endregion

        #region public: get an array of values
        public List<TValue> GetValues(string key)
        {
            List<TValue> valueList = new List<TValue>();
            uint id = GenerateID(key);
            bool isValid = m_ContainerIndex.ContainsKey(id);
            System.Diagnostics.Debug.Write(isValid, "Couldn't find the key.");
            if (isValid)
            {
                for (int i = 0; i < m_ContainerIndex[id]; ++i)
                {
                    valueList.Add(GetValue(key, id));
                }
            }
            return valueList;
        }
        #endregion

        #region public: get an array of cloned values
        public List<TValue> GetClonedValues(string key)
        {
            List<TValue> valueList = new List<TValue>();
            uint id = GenerateID(key);
            bool isValid = m_ContainerIndex.ContainsKey(id);
            System.Diagnostics.Debug.Write(isValid, "Couldn't find the key.");
            if (isValid)
            {
                for (int i = 0; i < m_ContainerIndex[id]; ++i)
                {
                    valueList.Add((TValue)GetValue(key, id).Clone());
                }
            }
            return valueList;
        }
        #endregion

        #region public: set a single value
        public bool SetValue(string key, TValue value)
        {
            return SetValue(key, 0, value);
        }

        public bool SetValue(string key, uint index, TValue value)
        {
            KeyValuePair<Tuple<uint, uint, string>, TValue>
                pair = GetPair(key, index);
            if (pair.Key != null)
            {
                m_Container[pair.Key] = value;
                return true;
            }
            return false;
        }

        public bool SetLastValue(string key, TValue value)
        {
            KeyValuePair<Tuple<uint, uint, string>, TValue>
                pair = GetLastPair(key);
            if (pair.Key != null)
            {
                m_Container[pair.Key] = value;
                return true;
            }
            return false;
        }
        #endregion

        #region public: set multiple values
        public bool SetValues(string key, TValue[] values, uint n)
        {
            return SetValues(key, 0, values, n);
        }

        public bool SetValues(string key, uint index, TValue[] values, uint n)
        {
            uint id = GenerateID(key);
            uint maxValue = index + n;
            if (m_ContainerIndex[id] - 1 < maxValue)
            {
                return false;
            }
            for (uint i = index; i < maxValue; ++i)
            {
                m_Container[GenerateTuple(id, i, key)] = values[i - index];
            }
            return true;
        }

        public bool SetLastValues(string key, TValue[] values, uint n)
        {
            uint id = GenerateID(key);
            if (!m_ContainerIndex.ContainsKey(id))
            {
                return false;
            }
            return SetValues(key, m_ContainerIndex[id] - n - 1, values, n);
        }
        #endregion

        #region public: clone, dispose, clear
        public object Clone()
        {
            GenericDictionary<TValue> copy = new GenericDictionary<TValue>();
            copy.m_Container =
                ContainerHelpers.CloneDictionaryCloningValues<Tuple<uint, uint, string>, TValue>
                    (m_Container);
            copy.m_ContainerIndex =
                ContainerHelpers.CloneDictionary<uint, uint>(m_ContainerIndex);
            return copy;
        }

        public void Dispose()
        {
            Clear();
        }

        public void Clear()
        {
            m_Container.Clear();
            m_ContainerIndex.Clear();
        }
        #endregion

        #region public: try get methods
        public bool TryGetValue(string key, out TValue value)
        {
            return TryGetValue(key, 0, out value);
        }

        public bool TryGetValue(string key, uint index, out TValue value)
        {
            uint id = GenerateID(key);
            Tuple<uint, uint, string> tuple =
                new Tuple<uint, uint, string>(id, index, key);
            return m_Container.TryGetValue(tuple, out value);
        }

        public bool TryGetLastValue(string key, out TValue value)
        {
            uint id = GenerateID(key);
            uint num = m_ContainerIndex.ContainsKey(id) ? m_ContainerIndex[id] - 1 : 0;
            Tuple<uint, uint, string> tuple =
                new Tuple<uint, uint, string>(id, num, key);
            return m_Container.TryGetValue(tuple, out value);
        }
        #endregion

        #region public: contains ... ?
        public bool ContainsKey(string key)
        {
            return ContainsKey(key, 0);
        }

        public bool ContainsKey(string key, uint index)
        {
            uint id = GenerateID(key);
            Tuple<uint, uint, string> tuple =
                new Tuple<uint, uint, string>(id, index, key);
            return m_Container.ContainsKey(tuple);
        }

        public bool ContainsValue(TValue value)
        {
            return m_Container.ContainsValue(value);
        }
        #endregion

        #region public: GetEnumerator
        public IEnumerator<KeyValuePair<Tuple<uint, uint, string>, TValue>> GetEnumerator()
        {
            return m_Container.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region public: CopyTo
        public void CopyTo(TValue[] array, int arrayIndex)
        {
            foreach (TValue value in m_Container.Values)
            {
                array[arrayIndex] = value;
                ++arrayIndex;
            }
        }

        public void CopyTo(TValue[] array, int arrayIndex, int count)
        {
            foreach (TValue value in m_Container.Values)
            {
                --count;
                if (count < 0)
                {
                    return;
                }
                array[arrayIndex] = value;
                ++arrayIndex;
            }
        }

        public void CopyTo(GenericDictionary<TValue> other)
        {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                other.Add(current.Key.Item3, current.Value);
            }
        }
        #endregion

        #region public: CloneTo
        public void CloneTo(TValue[] array, int arrayIndex)
        {
            foreach (TValue value in m_Container.Values)
            {
                array[arrayIndex] = (TValue)value.Clone();
                ++arrayIndex;
            }
        }

        public void CloneTo(TValue[] array, int arrayIndex, int count)
        {
            foreach (TValue value in m_Container.Values)
            {
                --count;
                if (count < 0)
                {
                    return;
                }
                array[arrayIndex] = (TValue)value.Clone();
                ++arrayIndex;
            }
        }

        public void CloneTo(GenericDictionary<TValue> other)
        {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                other.Add(current.Key.Item3, (TValue)current.Value.Clone());
            }
        }
        #endregion

        #region protected: make room for a new value
        protected void CreateRoom(uint id, string key, uint index)
        {
            CreateRoom(id, key, index, 1);
        }

        protected void CreateRoom(uint id, string key, uint index, uint n)
        {
            uint difference = m_ContainerIndex[id] - index;
            uint max = m_ContainerIndex[id] + n;
            for (uint i = m_ContainerIndex[id]; i < n; ++i)
            {
                AddElement(id, key, m_Container[GenerateTuple(id, i - difference, key)]);
            }
        }
        #endregion

        #region protected: give elements correct id based on new order
        protected void ReordenElements(uint id, string key, uint index)
        {
            ReordenElements(id, key, index, 1);
        }

        protected void ReordenElements(uint id, string key, uint index, uint n)
        {
            for (uint i = index; index < m_ContainerIndex[id] - n; ++i)
            {
                m_Container[GenerateTuple(id, i, key)] =
                    m_Container[GenerateTuple(id, i + n, key)];
            }
        }
        #endregion

        #region protected: remove a single value
        protected bool RemoveElement(uint id, string key)
        {
            return RemoveElement(id, 0, key);
        }

        protected bool RemoveElement(uint id, uint index, string key)
        {
            if (index > m_ContainerIndex[id] - 1)
            {
                return false;
            }
            else if (index == m_ContainerIndex[id] - 1)
            {
                RemoveLastElement(id, key);
            }
            else
            {
                ReordenElements(id, key, index);
                RemoveLastElement(id, key);
            }
            return false;
        }

        protected bool RemoveLastElement(uint id, string key)
        {
            bool isOK = m_Container.Remove(GenerateTuple(id, m_ContainerIndex[id]--, key));
            System.Diagnostics.Debug.Assert(isOK, "Couldnt remove value!");
            return isOK;
        }
        #endregion

        #region public: remove a single value
        public bool RemoveValue(string key)
        {
            return RemoveValue(key, 0);
        }

        public bool RemoveValue(string key, uint index)
        {
            return RemoveElement(GenerateID(key), index, key);
        }

        public bool RemoveLastValue(string key)
        {
            return RemoveLastElement(GenerateID(key), key);
        }
        #endregion

        #region protected: remove multiple values
        protected bool RemoveMultipleElements(uint id, string key, uint n)
        {
            return RemoveMultipleElements(id, 0, key, n);
        }

        protected bool RemoveMultipleElements(uint id, uint index, string key, uint n)
        {
            if (index + n > m_ContainerIndex[id] - 1)
            {
                return false;
            }
            ReordenElements(id, key, index, n);
            for (uint i = index + n; i < m_ContainerIndex[id]; ++i)
            {
                m_Container.Remove(GenerateTuple(id, i, key));
                --m_ContainerIndex[id];
            }
            return true;
        }
        #endregion

        #region public: remove multiple values
        public bool RemoveValues(uint id, string key, uint n)
        {
            return RemoveValues(id, 0, key, n);
        }

        public bool RemoveValues(uint id, uint index, string key, uint n)
        {
            return RemoveMultipleElements(id, index, key, n);
        }
        #endregion

        #region protected: add a single value
        protected uint AddElement(uint id, string key, TValue value)
        {
            uint index = m_ContainerIndex[id]++;
            m_Container.Add(GenerateTuple(id, index, key), value);

            return index;
        }

        protected bool AddElement(string key, uint id, TValue value, uint index)
        {
            uint length = m_ContainerIndex[id]++;
            bool isValid = index <= length;
            System.Diagnostics.Debug.Assert(isValid, "Index is out of range!");
            if (!isValid)
            {
                --m_ContainerIndex[id];
                return false;
            }
            if (index == length)
            {
                Add(key, value);
            }
            else
            {
                CreateRoom(id, key, index);
                SetValue(key, index, value);
            }
            return true;
        }

        protected bool AddFElement(uint id, string key, TValue value)
        {
            return AddElement(key, id, value, 0);
        }
        #endregion

        #region public: add a single value
        public uint Add(string key, TValue value)
        {
            uint id = GenerateID(key);
            if (!m_ContainerIndex.ContainsKey(id))
            {
                m_ContainerIndex.Add(id, 0);
            }
            return AddElement(id, key, value);
        }

        public bool Add(string key, TValue value, uint index)
        {
            uint id = GenerateID(key);
            if (!m_ContainerIndex.ContainsKey(id))
            {
                return AddElement(key, id, value, index);
            }
            return false;
        }

        public bool AddFirst(string key, TValue value)
        {
            return Add(key, value, 0);
        }

        #endregion

        #region protected: add multiple values
        protected uint AddElements(uint id, string key, TValue[] values, uint n)
        {
            uint index = 0;
            for (int i = 0; i < n; ++i)
            {
                index = AddElement(id, key, values[i]);
            }
            return index;
        }

        protected bool AddElements(string key, uint id, TValue[] values, uint n, uint index)
        {
            uint length = m_ContainerIndex[id];
            bool isValid = index <= length;
            System.Diagnostics.Debug.Assert(isValid, "Index is out of range!");
            if (!isValid)
            {
                return false;
            }
            if (index == length)
            {
                AddRange(key, values, n);
                return true;
            }
            CreateRoom(id, key, index, n);
            return SetValues(key, index, values, n);
        }

        protected bool AddElementsFirst(uint id, string key, TValue[] values, uint n)
        {
            return AddElements(key, id, values, n, 0);
        }
        #endregion

        #region public: add multiple values
        protected uint AddRange(string key, TValue[] values, uint n)
        {
            uint id = GenerateID(key);
            if (!m_ContainerIndex.ContainsKey(id))
            {
                m_ContainerIndex.Add(id, 0);
            }
            return AddElements(id, key, values, n);
        }

        protected bool AddRange(string key, TValue[] values, uint n, uint index)
        {
            uint id = GenerateID(key);
            if (m_ContainerIndex.ContainsKey(id))
            {
                return AddElements(key, id, values, n, index);
            }
            return false;
        }

        protected bool AddRangeFirst(string key, TValue[] values, uint n)
        {
            uint id = GenerateID(key);
            if (!m_ContainerIndex.ContainsKey(id))
            {
                return AddElementsFirst(id, key, values, n);
            }
            return false;
        }
        #endregion

        #region serializable implementation
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}