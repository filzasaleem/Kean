﻿// 
//  Image.cs
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
using GL = OpenTK.Graphics.OpenGL.GL;
using Error = Kean.Core.Error;
using Log = Kean.Extra.Log;
using Draw = Kean.Draw;
using Gpu = Kean.Draw.Gpu;
using Raster = Kean.Draw.Raster;
using Kean.Gui.OpenGL.Backend.Extension;

namespace Kean.Gui.OpenGL.Backend
{
	public abstract class Image :
		Gpu.Backend.IImage
	{

		protected Image(Factory factory, Gpu.Backend.ImageType type, Geometry2D.Integer.Size resolution, Draw.CoordinateSystem coordinateSystem) :
			this(factory, type, resolution, coordinateSystem, IntPtr.Zero)
		{ }
		protected Image(Factory factory, Gpu.Backend.ImageType type, Geometry2D.Integer.Size size, Draw.CoordinateSystem coordinateSystem, IntPtr data)
		{
			this.Factory = factory;
			this.Type = type;
			this.Size = size;
			this.Identifier = this.CreateIdentifier();
			this.Bind();
			this.Load(this.Type, this.Size, data);
			this.SetParameters();
		}

		#region Implementors interface
		public uint Identifier { get; private set; }
		protected virtual Geometry2D.Integer.Size AdaptResolution(Geometry2D.Integer.Size resolution)
		{
			int maximumTextureSize;
			GL.GetInteger(OpenTK.Graphics.OpenGL.GetPName.MaxTextureSize, out maximumTextureSize);
			if (resolution.Width > maximumTextureSize)
			{
				Log.Cache.Log(Error.Level.Warning, "Texture Width Exceeds Limit", string.Format("The requested texture size \"{0}\" is bigger than the maximum texture size \"{1}, {1}\". The textures width will be clamped.", resolution, maximumTextureSize));
				resolution = new Geometry2D.Integer.Size(maximumTextureSize, resolution.Height);
			}
			if (resolution.Height > maximumTextureSize)
			{
				Log.Cache.Log(Error.Level.Warning, "Texture Height Exceeds Limit", string.Format("The requested texture size \"{0}\" is bigger than the maximum texture size \"{1}, {1}\". The textures height will be clamped.", resolution, maximumTextureSize));
				resolution = new Geometry2D.Integer.Size(resolution.Width, maximumTextureSize);
			}
			return resolution;
		}
		protected virtual uint CreateIdentifier()
		{
			uint result;
			GL.GenTextures(1, out result);
			return result;
		}
		protected abstract Canvas CreateCanvas();
		protected virtual void Bind()
		{
			GL.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, this.Identifier);
		}
		protected virtual void Unbind()
		{
			GL.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0);
		}
		protected virtual void Load(Gpu.Backend.ImageType type, Geometry2D.Integer.Size size, IntPtr data)
		{
			GL.TexImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, type.PixelInternalFormat(), this.Size.Width, this.Size.Height, 0, type.PixelFormat(), OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, data);
		}
		protected virtual void SetParameters()
		{
			GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMinFilter, (int)OpenTK.Graphics.OpenGL.TextureMinFilter.Linear);
			GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMagFilter, (int)OpenTK.Graphics.OpenGL.TextureMagFilter.Linear);
			if (this.Wrap)
			{
				GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapS, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.MirroredRepeat);
				GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapT, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.MirroredRepeat);
			}
			else
			{
				GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapS, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.ClampToBorder);
				GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapT, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.ClampToBorder);
			}
		}
		protected virtual void Read(IntPtr pointer)
		{
			GL.GetTexImage(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, this.Type.PixelFormat(), OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, pointer); 
		}
		protected abstract Image Create();
		#endregion

		#region IDisposable Members
		public void Dispose()
		{
		}
		#endregion

		#region IImage Members
		public bool Wrap { get; set; }
		public Kean.Draw.Gpu.Backend.IFactory Factory { get; private set; }
		Kean.Draw.Gpu.Backend.ICanvas canvas;
		public Kean.Draw.Gpu.Backend.ICanvas Canvas
		{
			get 
			{
				if (this.canvas.IsNull())
					this.canvas = this.CreateCanvas();
				return this.canvas;
			}
		}
		public Draw.CoordinateSystem CoordinateSystem { get; set; }
		public Geometry2D.Integer.Size Size { get; private set; }
		public Gpu.Backend.ImageType Type { get; private set; }
		public void Load(Geometry2D.Integer.Point offset, Raster.Image image)
		{
		}
		public Raster.Image Read()
		{
			this.Bind();
			Raster.Image result;
			switch (this.Type)
			{
				case Gpu.Backend.ImageType.Bgra:
					result = new Raster.Bgra(this.Size);
					break;
				case Gpu.Backend.ImageType.Bgr:
					result = new Raster.Bgr(this.Size);
					break;
				case Gpu.Backend.ImageType.Monochrome:
					result = new Raster.Monochrome(this.Size);
					break;
				default:
					result = null;
					break;
			}
			if (result.NotNull())
				this.Read(result.Pointer);
			return result;
		}
		// TODO: Use GPU to copy instead.
		public Gpu.Backend.IImage Copy()
		{
			return Gpu.Backend.Factory.CreateImage(this.Read());
		}
		public void Render()
		{
			this.Render(new Geometry2D.Single.Box(0.0f, 0.0f, this.Size.Width, this.Size.Height), new Geometry2D.Single.Box(0.0f, 0.0f, this.Size.Width, this.Size.Height));
		}
		public void Render(Geometry2D.Single.Box source, Geometry2D.Single.Box destination)
		{
			this.Bind();
			Geometry2D.Single.PointValue leftTop = new Geometry2D.Single.PointValue(source.Left / this.Size.Width, source.Top / this.Size.Height);
			Geometry2D.Single.PointValue rightTop = new Geometry2D.Single.PointValue(source.Right / this.Size.Width, source.Top / this.Size.Height);
			Geometry2D.Single.PointValue leftBottom = new Geometry2D.Single.PointValue(source.Left / this.Size.Width, source.Bottom / this.Size.Height);
			Geometry2D.Single.PointValue rightBottom = new Geometry2D.Single.PointValue(source.Right / this.Size.Width, source.Bottom / this.Size.Height);
           	GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.Blend);
			GL.BlendFunc(OpenTK.Graphics.OpenGL.BlendingFactorSrc.One, OpenTK.Graphics.OpenGL.BlendingFactorDest.OneMinusSrcAlpha);
			GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Quads);
			GL.TexCoord2(leftTop.X, leftTop.Y); GL.Vertex2(destination.Left, destination.Top);
			GL.TexCoord2(rightTop.X, rightTop.Y); GL.Vertex2(destination.Right, destination.Top);
			GL.TexCoord2(rightBottom.X, rightBottom.Y); GL.Vertex2(destination.Right, destination.Bottom);
			GL.TexCoord2(leftBottom.X, leftBottom.Y); GL.Vertex2(destination.Left, destination.Bottom);
			GL.End();
			this.Unbind();
		}
		#endregion
	}
}