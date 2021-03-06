﻿// 
//  IFactory.cs
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
using Geometry2D = Kean.Math.Geometry2D;

namespace Kean.Draw.Gpu.Backend
{
	public interface IFactory
	{
		ITexture CreateTexture(TextureType type, Geometry2D.Integer.Size size, CoordinateSystem coordinateSystem);
		ITexture CreateTexture(Draw.Raster.Image image);
		IFrameBuffer CreateFrameBuffer(params ITexture[] textures);
		void FreeRecycled();

        IShader ConvertMonochromeToBgr { get; }
        IShader ConvertBgrToMonochrome { get; }
		IShader ConvertBgrToU { get; }
		IShader ConvertBgrToV { get; }
		IShader ConvertBgrToYuv420 { get; }
		IShader ConvertYuv420ToBgr { get; }
	}
}
