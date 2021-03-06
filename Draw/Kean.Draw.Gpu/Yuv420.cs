﻿// 
//  Yuv420.cs
//  
//  Author:
//       Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2011 Anders Frisk
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
	public class Yuv420 : 
		Planar
	{
		public Monochrome Y { get { return this.Channels[0] as Monochrome; } }
		public Monochrome U { get { return this.Channels[1] as Monochrome; } }
		public Monochrome V { get { return this.Channels[2] as Monochrome; } }

		public Yuv420(Geometry2D.Integer.Size size) :
			this(size, CoordinateSystem.Default)
		{ }
		public Yuv420(Geometry2D.Integer.Size size, CoordinateSystem coordinateSystem) :
			this(
			size, coordinateSystem,
			new Monochrome(Gpu.Backend.Factory.CreateImage(Gpu.Backend.TextureType.Monochrome, size, coordinateSystem)),
			new Monochrome(Gpu.Backend.Factory.CreateImage(Gpu.Backend.TextureType.Monochrome, size / 2, coordinateSystem)),
			new Monochrome(Gpu.Backend.Factory.CreateImage(Gpu.Backend.TextureType.Monochrome, size / 2, coordinateSystem))
			)
		{ }
		public Yuv420(Raster.Yuv420 image) :
			this(
			image.Size, image.CoordinateSystem,
			new Monochrome(image.Y),
			new Monochrome(image.U),
			new Monochrome(image.V)
			)
		{ }
		public Yuv420(Gpu.Monochrome image) :
			this(image.Size, image.CoordinateSystem)
		{
			this.Y.Canvas.Draw(image);
			this.U.Canvas.Draw(new Kean.Draw.Color.Monochrome(128));
			this.V.Canvas.Draw(new Kean.Draw.Color.Monochrome(128));
		}
		public Yuv420(Gpu.Bgr image) :
			this(image.Size, image.CoordinateSystem)
		{
			this.Y.Canvas.Draw(Map.BgrToMonochrome, image);
			this.U.Canvas.Draw(Map.BgrToU, image);
			this.V.Canvas.Draw(Map.BgrToV, image);
		}
		public Yuv420(Gpu.Bgra image) :
			this(image.Size, image.CoordinateSystem)
		{
			this.Y.Canvas.Draw(Map.BgrToMonochrome, image);
			this.U.Canvas.Draw(Map.BgrToU, image, new Kean.Math.Geometry2D.Single.Box(0,0, image.Size.Width, image.Size.Height), new Kean.Math.Geometry2D.Single.Box(0,0, this.U.Size.Width, this.U.Size.Height));
			this.V.Canvas.Draw(Map.BgrToV, image, new Kean.Math.Geometry2D.Single.Box(0,0, image.Size.Width, image.Size.Height), new Kean.Math.Geometry2D.Single.Box(0,0, this.V.Size.Width, this.V.Size.Height));
			// TODO: color space conversion goes here (use Backend.IImage or Backend.IFactory)
		}
		Yuv420(Geometry2D.Integer.Size size, CoordinateSystem coordinateSystem, Monochrome y, Monochrome u, Monochrome v) :
			base(size, coordinateSystem, y, u, v)
		{ }
		protected Yuv420(Yuv420 original) :
			base(original)
		{ }
		internal override void Render(Geometry2D.Single.Box source, Geometry2D.Single.Box destination)
		{
			this.U.Backend.Use(1);
			this.V.Backend.Use(2);
			this.Y.Backend.Use(0);
			this.Y.Render(source, destination);
			this.V.Backend.Unuse(2);
			this.U.Backend.Unuse(1);
			this.Y.Backend.Unuse(0);
		}
		#region Draw.Image Overrides
		public override T Convert<T>()
		{
			T result = null;
			if (typeof(T).IsSubclassOf(typeof(Raster.Image)))
			{
				Raster.Monochrome y = this.Y.Backend.Read() as Raster.Monochrome;
				Raster.Monochrome u = this.U.Backend.Read() as Raster.Monochrome;
				Raster.Monochrome v = this.V.Backend.Read() as Raster.Monochrome;
				byte[] buffer = new byte[y.Length + u.Length + v.Length];
				System.Runtime.InteropServices.Marshal.Copy(y.Pointer, buffer, 0, y.Length);
				System.Runtime.InteropServices.Marshal.Copy(u.Pointer, buffer, y.Length, u.Length);
				System.Runtime.InteropServices.Marshal.Copy(v.Pointer, buffer, y.Length+u.Length, v.Length);
				Raster.Yuv420 raster = new Kean.Draw.Raster.Yuv420(buffer, this.Size);
				result = raster.Convert<T>() as T;
			}
			else
			{
				if (typeof(T) == typeof(Gpu.Yuv420))
					result = this.Copy() as T;
				else if (typeof(T) == typeof(Gpu.Bgra))
					result = new Bgra(this) as T;
				else if (typeof(T) == typeof(Gpu.Bgr))
					result = new Bgr(this) as T;
				else if (typeof(T) == typeof(Gpu.Monochrome))
					result = new Monochrome(this) as T;
			}
			return result;
		}
		public override Draw.Image Create(Geometry2D.Integer.Size size)
		{
			return null;
		}
		public override Draw.Image Copy()
		{
			return new Yuv420(this);
		}
		public override Draw.Image Copy(Geometry2D.Integer.Size size, Geometry2D.Single.Transform transform)
		{
			return null;
		}
		public override Draw.Image ResizeTo(Geometry2D.Integer.Size size)
		{
			return null;
		}
		public override Draw.Image Shift(Geometry2D.Integer.Size offset)
		{
			return null;
		}
		public override float Distance(Draw.Image other)
		{
			return float.NaN;
		}
		#endregion
	}
}
