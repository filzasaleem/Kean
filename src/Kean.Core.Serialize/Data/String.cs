// 
//  String.cs
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
using Collection = Kean.Core.Collection;
using Kean.Core.Collection.Extension;

namespace Kean.Core.Serialize.Data
{
	public class String :
		Leaf<string>
	{
		public override string Text { get { return this.Value; } }
		public override byte[] Binary { get { return System.Text.Encoding.UTF8.GetBytes(this.Value); } }
		public String(string value) :
			base(value)
		{ }
		public static String Create(string value)
		{
			return new String(value);
		}
		public static String Create(byte[] value)
		{
			return new String(System.Text.Encoding.UTF8.GetString(value));
		}
	}
}