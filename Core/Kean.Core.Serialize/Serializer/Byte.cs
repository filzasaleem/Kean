﻿// 
//  Byte.cs
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

namespace Kean.Core.Serialize.Serializer
{
	public class Byte :
		ISerializer
	{
		public Byte()
		{ }
		#region ISerializer Members
		public ISerializer Find(Reflect.Type type)
		{
			return type == "byte" ? this : null;
		}
		public Data.Node Serialize(Storage storage, Reflect.Type type, object data, Uri.Locator locator)
		{
			return new Data.Byte(data, type);
		}
		public object Deserialize(Storage storage, Data.Node data, object result)
		{
			return data is Data.Byte ? (data as Data.Byte).Value :
				data is Data.Binary ? (data as Data.Binary).Value[0] :
				data is Data.String ? byte.Parse((data as Data.String).Value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat) :
				(byte)0;
		}
		#endregion
	}
}

