﻿// 
//  Map.cs
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
using Geometry2D = Kean.Math.Geometry2D;

namespace Kean.Draw.Gpu
{
	public class Map :
		Draw.Map
	{
		protected internal Backend.IShader Backend { get; private set; }
		public Map(Backend.IShader backend)
		{
			this.Backend = backend;
		}
        public static Map MonochromeToBgr { get { return new Map(Gpu.Backend.Factory.ConvertMonochromeToBgr); } }
        public static Map BgrToMonochrome { get { return new Map(Gpu.Backend.Factory.ConvertBgrToMonochrome); } }
        public static Map BgrToU { get { return new Map(Gpu.Backend.Factory.ConvertBgrToU); } }
		public static Map BgrToV { get { return new Map(Gpu.Backend.Factory.ConvertBgrToV); } }
		public static Map BgrToYuv420 { get { return new Map(Gpu.Backend.Factory.ConvertBgrToYuv420); } }
		public static Map Yuv420ToBgr { get { return new Map(Gpu.Backend.Factory.ConvertYuv420ToBgr); } }
	}
}
