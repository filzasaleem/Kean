﻿// 
//  Scheme.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2010-2011 Simon Mika
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
using Kean.Core;
using Kean.Core.Extension;

namespace Kean.Core.Uri
{
    public class Scheme :
        Core.Collection.ILink<Scheme, string>,
        IString,
        IEquatable<Scheme>
    {

        #region ILink<Scheme, string> Members
		public string Head { get; set; }
		public Scheme Tail { get; set; }
        #endregion

        #region IString Members
        public string String
        {
            get 
            {
                System.Text.StringBuilder result = new System.Text.StringBuilder(this.Head);
                if (this.Tail != null)
                {
                    result.Append("+");
                    result.Append(this.Tail.String);
                }
                return result.ToString();
            }
            set
            {
                if (value.IsEmpty())
                {
                    this.Head = null;
                    this.Tail = null;
                }
                else
                {
                    string[] splitted = value.Split(new char[] { '+' }, 2);
                    this.Head = splitted[0];
                    if (splitted.Length > 1)
                        this.Tail = new Scheme() { String = splitted[1] };
                    else
                        this.Tail = null;
                }
            }
        }
        #endregion
		public Scheme()
		{ }
		public Scheme(string head, Scheme tail) :
			this()
		{
			this.Head = head;
			this.Tail = tail;
		}
		public Scheme Copy()
		{
			return new Scheme(this.Head, this.Tail.NotNull() ? this.Tail.Copy() : null);
		}
        #region Object overrides
        public override bool Equals(object other)
        {
            return other is Scheme && this.Equals(other as Scheme);
        }
        public override int GetHashCode()
        {
			return this.Head.Hash() ^ this.Tail.Hash();
        }
        public override string ToString()
        {
            return this.String;
        }
        #endregion
        #region IEquatable<Scheme> Members
        public bool Equals(Scheme other)
        {
            return other.NotNull() && this.Head == other.Head && this.Tail == other.Tail;
        }
        #endregion
		#region Operators
		public static bool operator ==(Scheme left, Scheme right)
        {
            return object.ReferenceEquals(left, right) || (!object.ReferenceEquals(left, null) && left.Equals(right));
        }
        public static bool operator !=(Scheme left, Scheme right)
        {
            return !(left == right);
        }
        public static implicit operator string(Scheme scheme)
        {
            return scheme.IsNull() ? null : scheme.String;
        }
        public static implicit operator Scheme(string scheme)
        {
            return scheme.IsEmpty() ? null : new Scheme() { String = scheme };
        }
        #endregion
    }
}
