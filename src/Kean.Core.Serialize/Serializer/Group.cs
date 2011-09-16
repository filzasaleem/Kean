// 
//  Group.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2011 Simon Mika
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

namespace Kean.Core.Serialize.Serializer
{
	public class Group :
		ISerializer
	{
		Collection.Dictionary<Reflect.Type, ISerializer> cache = new Collection.Dictionary<Reflect.Type, ISerializer>();

		ISerializer[] serializers;
		public Group(params ISerializer[] serializers)
		{
			this.serializers = serializers;
		}

		public ISerializer Find(Reflect.Type type)
		{
			ISerializer result = null;
			if (cache.Contains(type))
				result = cache[type];
			else
			{
				MethodAttribute[] attributes;
				if (type.Category != Reflect.TypeCategory.Primitive && (attributes = type.GetAttributes<MethodAttribute>()).Length == 1)
					result = attributes[0].Serializer;
				else
				{
					foreach (ISerializer serializer in this.serializers)
						if ((result = serializer.Find(type)).NotNull())
							break;
				}
				cache[type] = result;
			}
			return result;
		}
		public Data.Node Serialize<T>(Storage storage, T data)
		{
			ISerializer serializer = this.Find(typeof(T));
			Data.Node result = null;
			if (serializer.NotNull())
				result = serializer.Serialize<T>(storage, data);
			return result;
		}
		public T Deserialize<T>(Storage storage, Data.Node data)
		{
			ISerializer serializer = this.Find(data.Type ?? typeof(T));
			T result = default(T);
			if (serializer.NotNull())
				result = serializer.Deserialize<T>(storage, data);
			return result;
		}
	}
}
