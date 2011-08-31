﻿// 
//  Y.cs
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
//  You should have received a copy of the GNU Lesser General Public Licenseusing System;
using System;
using Kean.Core;
using Kean.Core.Extension;
using Math = Kean.Math;

namespace Kean.Draw.Color
{
	public struct Y :
		IColor
	{
		public byte y;
		public Y(byte y)
		{
			this.y = y;
		}
		public Y(float y)
		{
			this.y = (byte)Math.Single.Clamp(y * 255, 0, 255);
		}
		public Y(double y)
		{
			this.y = (byte)Math.Double.Clamp(y * 255, 0, 255);
		}
		#region Casts
		public static implicit operator Y(byte value)
		{
			unsafe { return *((Y*)&value); }
		}
		public static implicit operator byte(Y value)
		{
			unsafe { return *((byte*)&value); }
		}
		public static implicit operator Y(float value)
		{
			return new Y(value);
		}
		public static implicit operator float(Y value)
		{
			return value.y / 255.0f;
		}
		public static implicit operator Y(double value)
		{
			return new Y(value);
		}
		public static implicit operator double(Y value)
		{
			return value.y / 255.0;
		}
		#endregion
	}
}
