﻿// 
//  PathLink.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2010-2012 Simon Mika
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
using Collection = Kean.Core.Collection;
using Kean.Core.Collection.Extension;

namespace Kean.Core.Uri
{
	public class PathLink :
		Collection.ILink<PathLink, string>,
		IEquatable<PathLink>
	{
		#region ILink<PathLink, string> Members
		public string Head { get; set; }
		public PathLink Tail { get; set; }
		#endregion
		public PathLink() { }
		public PathLink(string head, PathLink tail) :
			this()
		{
			this.Head = head;
			this.Tail = tail;
		}
		#region IEquatable<PathLink> Members
		public bool Equals(PathLink other)
		{
			return other.NotNull() && this.Head == other.Head && this.Tail == other.Tail;
		}
		#endregion
		#region Object Overrides
		public override bool Equals(object other)
		{
			return other is PathLink && this.Equals(other as PathLink);
		}
		public override int GetHashCode()
		{
			return this.Head.Hash() ^ this.Tail.Hash();
		}
		#endregion
		#region Equality Operators
		public static bool operator ==(PathLink left, PathLink right)
		{
			return left.SameOrEquals(right);
		}
		public static bool operator !=(PathLink left, PathLink right)
		{
			return !(left == right);
		}
		#endregion
	}
}