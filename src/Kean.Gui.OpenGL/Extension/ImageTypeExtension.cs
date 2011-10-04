﻿// 
//  ImageTypeExtension.cs
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
using GL = OpenTK.Graphics.OpenGL.GL;
using Error = Kean.Core.Error;
using Log = Kean.Extra.Log;
using Draw = Kean.Draw;
using Gpu = Kean.Draw.Gpu;
using Raster = Kean.Draw.Raster;
using Kean.Gui.OpenGL.Extension;

namespace Kean.Gui.OpenGL.Extension
{
	public static class ImageTypeExtension
	{
		public static OpenTK.Graphics.OpenGL.PixelInternalFormat PixelInternalFormat(this Gpu.Backend.ImageType me)
		{
			OpenTK.Graphics.OpenGL.PixelInternalFormat result;
			switch (me)
			{
				default:
				case Gpu.Backend.ImageType.Argb:
					result = OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba;
					break;
				case Gpu.Backend.ImageType.Rgb:
					result = OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb;
					break;
				case Gpu.Backend.ImageType.Monochrome:
					result = OpenTK.Graphics.OpenGL.PixelInternalFormat.Luminance8;
					break;
			}
			return result;
		}
		public static OpenTK.Graphics.OpenGL.PixelFormat PixelFormat(this Gpu.Backend.ImageType me)
		{
			OpenTK.Graphics.OpenGL.PixelFormat result;
			switch (me)
			{
				default:
				case Gpu.Backend.ImageType.Argb:
					result = OpenTK.Graphics.OpenGL.PixelFormat.Bgra;
					break;
				case Gpu.Backend.ImageType.Rgb:
					result = OpenTK.Graphics.OpenGL.PixelFormat.Bgr;
					break;
				case Gpu.Backend.ImageType.Monochrome:
					result = OpenTK.Graphics.OpenGL.PixelFormat.Luminance;
					break;
			}
			return result;
		}
		public static Gpu.Backend.ImageType GetImageType(this Raster.Image me)
		{
			Gpu.Backend.ImageType result;
			if (me is Raster.Bgra)
				result = Gpu.Backend.ImageType.Argb;
			else if (me is Raster.Bgr)
				result = Gpu.Backend.ImageType.Rgb;
			else if (me is Raster.Monochrome)
				result = Gpu.Backend.ImageType.Monochrome;
			else
				result = Gpu.Backend.ImageType.Monochrome;
			return result;
		}
	}
}
