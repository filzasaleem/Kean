﻿// 
//  ListStack.cs
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
	public class ListStack<T> :
		IStack<T>
	{
		IList<T> data;
		public ListStack(IList<T> data)
		{
			this.data = data;
		}
		#region IStack<T>
		public bool Empty { get { return this.data.Count < 1; } }
		public Collection.IStack<T> Push(T item)
		{
			return this.data.Add(item) as Collection.IStack<T>;
		}
		public T Pop()
		{
			try { return this.data.Remove(this.data.Count - 1); } catch (Exception.InvalidIndex e) { throw new Exception.Empty(e); }
		}
		public T Peek()
		{
			try { return this.data[this.data.Count - 1]; } catch (Exception.InvalidIndex e) { throw new Exception.Empty(e); }
		}
		#endregion
	}
}