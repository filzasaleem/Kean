﻿// 
//  ColorExtension.cs
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

namespace Kean.Draw.Cairo.Extension
{
	public static class ColorExtension
	{
		public static global::Cairo.Color ToCairo(this IColor me)
		{
			Color.Bgra bgra = me.Convert<Color.Bgra>();
			return new global::Cairo.Color(bgra.color.red / 255.0, bgra.color.green / 255.0, bgra.color.blue / 255.0, bgra.alpha / 255.0);
		}
		public static IColor FromCairo(this global::Cairo.Color me)
		{
			return new Color.Bgra((byte)Math.Integer.Round(me.B * 255), (byte)Math.Integer.Round(me.G * 255), (byte)Math.Integer.Round(me.R * 255), (byte)Math.Integer.Round(me.A * 255));
		}
	}
}