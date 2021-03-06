﻿// 
//  Cache.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2011-2012 Simon Mika
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
using Kean.Core.Reflect.Extension;

namespace Kean.Core.Serialize.Serializer
{
	public class Cache :
		ISerializer
	{
		Core.Collection.Dictionary<Reflect.Type, ISerializer> cache = new Core.Collection.Dictionary<Reflect.Type, ISerializer>();

		ISerializer serializer;
		public Cache(ISerializer serializer)
		{
			this.serializer = serializer;
		}
		#region ISerializer Members
		public ISerializer Find(Reflect.Type type)
		{
			ISerializer result = null;
			if (type.NotNull())
			{
				if (this.cache.Contains(type))
					result = this.cache[type];
				else
				{
					MethodAttribute[] attributes;
					if (type.Category != Reflect.TypeCategory.Primitive && (attributes = type.GetAttributes<MethodAttribute>()).Length == 1)
						result = attributes[0].Serializer;
					else
						result = serializer.Find(type);
					if (result.NotNull())
						cache[type] = result;
				}
			}
			return result;
		}
		public Data.Node Serialize(Storage storage, Reflect.Type type, object data, Uri.Locator locator)
		{
			ISerializer serializer = this.Find(data.Type());
			return serializer.NotNull() ? serializer.Serialize(storage, type, data, locator) : null;
		}
		public object Deserialize(Storage storage, Data.Node data, object result)
		{
			ISerializer serializer = data.NotNull() ? this.Find(data.Type) : null;
			return  serializer.NotNull() ? serializer.Deserialize(storage, data, result) : null;
		}
		#endregion
	}
}

