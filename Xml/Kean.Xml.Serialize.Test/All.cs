﻿// 
//  All.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2012 Simon Mika
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

namespace Kean.Xml.Serialize.Test
{
	public static class All
	{
		public static void Test()
		{
			Serialize.Test.BasicTypes.Test();
			Serialize.Test.SystemTypes.Test();
			Serialize.Test.CoreTypes.Test();
			Serialize.Test.NullableTypes.Test();
			Serialize.Test.CollectionTypes.Test();
			Serialize.Test.Missing.Test();
			Serialize.Test.Named.Test();
			Serialize.Test.Preprocessor.Test();
		}
	}
}
