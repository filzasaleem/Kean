// 
//  Vector.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2009-2011 Simon Mika
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using InteropServices = System.Runtime.InteropServices;

namespace Kean.Core.Buffer
{
    public class Vector<T> :
        Sized,
        Collection.IVector<T>
        where T : struct
    {
        T[] data;
        protected internal T[] Data 
        {
            get { lock (this.Lock) return this.data; }
            set { lock (this.Lock) this.data = value; }
        }
        bool isPinned = false;
        InteropServices.GCHandle handle;
        public Vector(int size) : this(new T[size], null) { }
        public Vector(int size, Action<IntPtr> free) : this(new T[size], free) { }
        public Vector(T[] data) : this(data, null) { }
        public Vector(T[] data, Action<IntPtr> free) :
            this(InteropServices.GCHandle.Alloc(data, InteropServices.GCHandleType.Pinned), InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), data.Length * InteropServices.Marshal.SizeOf(typeof(T)), free)
        {
            this.Data = data;
        }
        Vector(InteropServices.GCHandle handle, IntPtr data, int size, Action<IntPtr> free) :
            base(data, size, free)
        {
            this.handle = handle;
            this.isPinned = true;
        }
        protected override void Dispose(bool disposing)
        {
            lock (this.Lock)
            {
                base.Dispose(disposing);
                if (this.isPinned && this.handle.IsAllocated)
                {
                    this.handle.Free();
                    this.isPinned = false;
                }
            }
        }

        #region IVector<T> Members
        T Collection.IVector<T>.this[int index]
        {
            get { lock (this.Lock) return this.Data[index]; }
            set { lock (this.Lock) this.Data[index] = value; }
        }
        int Collection.IVector<T>.Count
        {
            get { lock (this.Lock) return this.Data.Length; }
        }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < this.Data.Length; i++)
                yield return this.Data[i];
        }
        #endregion
        #region IEquatable<IVector<T>> Members
        bool IEquatable<Collection.IVector<T>>.Equals(Collection.IVector<T> other)
        {
            bool result = this.Data.Length == other.Count;
            for (int i = 0; result && i < this.Data.Length; i++)
                result &= this.Data[i].Equals(other[i]);
            return result;
        }
        #endregion
        public unsafe static implicit operator void*(Vector<T> pointer)
        {
            return ((IntPtr)pointer).ToPointer();
        }
        public static implicit operator IntPtr(Vector<T> pointer)
        {
            return (IntPtr)(Pointer)pointer;
        }
    }
}