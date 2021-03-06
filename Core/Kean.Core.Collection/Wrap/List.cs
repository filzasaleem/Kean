﻿// 
//  List.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2010 Simon Mika
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

namespace Kean.Core.Collection.Wrap
{
	public class List<T> :
		Vector<T>,
		IList<T>
	{
		IList<T> data;
		#region Constructors
		public List(IList<T> data) :
			base(data)
		{
			this.data = data;
		}
		#endregion
		#region IList<T> Members
		public Collection.IList<T> Add(T item)
		{
			return this.data.Add(item);
		}
		public T Remove()
		{
			return this.data.Remove();
		}
		public Collection.IList<T> Insert(int index, T item)
		{
			return this.data.Insert(index, item);
		}
		public T Remove(int index)
		{
			return this.data.Remove(index);
		}
		#endregion
	}
}
