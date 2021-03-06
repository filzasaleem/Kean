﻿// 
//  ListExtension.cs
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
using Kean.Core.Extension;
namespace Kean.Core.Collection.Extension
{
	public static class ListExtension
	{
		public static IList<T> Add<T>(this IList<T> me, params T[] items)
		{
			foreach (T item in items)
				me.Add(item);
			return me;
		}
		public static IList<T> Add<T>(this IList<T> me, System.Collections.Generic.IEnumerable<T> items)
		{
			foreach (T item in items)
				me.Add(item);
			return me;
		}
		public static bool Remove<T>(this IList<T> me, Func<T, bool> predicate)
		{
			bool result = false;
			int i = 0;
			while (i < me.Count)
			{
				T item = me[i];
				if (predicate(item))
					result = item.NotNull() ? item.Equals(me.Remove(i)) : (me.Remove(i) == null);
				else
					i++;
			}
			return result;
		}
		public static T RemoveFirst<T>(this IList<T> me, Func<T, bool> predicate)
		{
			T result = default(T);
			int i = 0;
			while (i < me.Count)
			{
				T item = me[i];
				if (predicate(item) && (item.NotNull() ? item.Equals(me.Remove(i)) : (me.Remove(i) == null)))
				{
					result = item;
					break;
				}
				else
					i++;
			}
			return result;
		}
		public static IList<T> Clear<T>(this IList<T> me)
		{
			while (me.Count > 0)
				me.Remove();
			return me;
		}
	}
}
