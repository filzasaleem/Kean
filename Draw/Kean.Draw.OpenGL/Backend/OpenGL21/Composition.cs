﻿//
//  Composition.cs
//
//  Author:
//       Simon Mika <smika@hx.se>
//
//  Copyright (c) 2013 Simon Mika
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
using Kean.Draw.OpenGL.Backend.Extension;
using Kean.Core.Extension;
using Collection = Kean.Core.Collection;
using Color = Kean.Draw.Color;
using Error = Kean.Core.Error;
using Geometry2D = Kean.Math.Geometry2D;
using GL = OpenTK.Graphics.OpenGL.GL;

namespace Kean.Draw.OpenGL.Backend.OpenGL21
{
	public class Composition :
		Backend.Composition
	{
		protected internal Composition(Context context) :
			base(context, new Texture(context), new Depth(context), new FrameBuffer(context))
		{ }
		protected Composition(Composition original) :
			base(original)
		{ }

		public override void Setup()
		{
			GL.PushAttrib(OpenTK.Graphics.OpenGL.AttribMask.AllAttribBits);
			this.FrameBuffer.Use();
			Exception.Framebuffer.Check();
			GL.Viewport(0, 0, this.Size.Width, this.Size.Height);
			GL.Ortho(0.0, 0.0, 1.0, 1.0, 0.0, 0.0);
			GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
			GL.PushMatrix();
			new Geometry2D.Single.Transform(2.0f / this.Size.Width, 0.0f, 0.0f, 2.0f / this.Size.Height, -1.0f, -1.0f).Load();
			GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
			GL.PushMatrix();
			GL.LoadIdentity();
			GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.Blend);
			GL.BlendFunc(OpenTK.Graphics.OpenGL.BlendingFactorSrc.SrcAlpha, OpenTK.Graphics.OpenGL.BlendingFactorDest.OneMinusSrcAlpha);
		}

		public override void Teardown()
		{
			GL.Ext.BindFramebuffer(OpenTK.Graphics.OpenGL.FramebufferTarget.FramebufferExt, 0);
			Exception.Framebuffer.Check();
			GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
			GL.PopMatrix();
			GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
			GL.PopMatrix();
			GL.PopAttrib();
		}
		public override void SetClip(Geometry2D.Single.Box region)
		{
			if (!region.Empty)
			{
				double[] left = new double[] { 1.0, 0.0, 0.0, -region.Left };
				GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane0);
				GL.ClipPlane(OpenTK.Graphics.OpenGL.ClipPlaneName.ClipPlane0, ref left[0]);
				double[] right = new double[] { -1.0, 0.0, 0.0, region.Right };
				GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane1);
				GL.ClipPlane(OpenTK.Graphics.OpenGL.ClipPlaneName.ClipPlane1, ref right[0]);
				double[] top = new double[] { 0.0, 1.0, 0.0, -region.Top };
				GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane2);
				GL.ClipPlane(OpenTK.Graphics.OpenGL.ClipPlaneName.ClipPlane2, ref top[0]);
				double[] bottom = new double[] { 0.0, -1.0, 0.0, region.Bottom };
				GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane3);
				GL.ClipPlane(OpenTK.Graphics.OpenGL.ClipPlaneName.ClipPlane3, ref bottom[0]);
			}
			else
			{
				GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane0);
				GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane1);
				GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane2);
				GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.ClipPlane3);
			}
		}
		public override void CopyToTexture()
		{
			GL.RasterPos2(0, 0);
			GL.CopyTexImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0,
				OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba,
				0,
				0,
				this.Size.Width,
				this.Size.Height, 0);
		}
		public override void CopyToTexture(Geometry2D.Integer.Size offset)
		{
			GL.RasterPos2(0, 0);
			GL.CopyTexSubImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0,
				(offset.Width < 0) ? 0 : offset.Width,
				(offset.Height < 0) ? 0 : offset.Height,
				(offset.Width < 0) ? (-offset.Width) : 0,
				(offset.Height < 0) ? (-offset.Height) : 0,
				this.Size.Width - Kean.Math.Integer.Absolute(offset.Width),
				this.Size.Height - Kean.Math.Integer.Absolute(offset.Height));
		}
		public override void Read(IntPtr pointer, Geometry2D.Integer.Box region)
		{
			switch (this.Type)
			{
				default:
				case TextureType.Argb:
					GL.ReadPixels(region.Left, region.Top, region.Width, region.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, pointer);
					break;
				case TextureType.Rgb:
					GL.ReadPixels(region.Left, region.Top, region.Width, region.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, pointer);
					break;
				case TextureType.Monochrome:
					GL.ReadPixels(region.Left, region.Top, region.Width, region.Height, OpenTK.Graphics.OpenGL.PixelFormat.Red, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, pointer);
					break;
			}
		}
		public override void Clear()
		{
			GL.ClearColor(0f, 0f, 0f, 0f);
			GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit | OpenTK.Graphics.OpenGL.ClearBufferMask.StencilBufferBit);
		}

		public override void Clear(Kean.Math.Geometry2D.Single.Box region)
		{
			GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.Texture2D);
			GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.Blend);
			GL.BlendFunc(OpenTK.Graphics.OpenGL.BlendingFactorSrc.Zero, OpenTK.Graphics.OpenGL.BlendingFactorDest.Zero);
			this.CreateRectangle(region);
		}
		public override void Blend(float factor)
		{
			GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.Texture2D);
			GL.BlendFunc(OpenTK.Graphics.OpenGL.BlendingFactorSrc.One, OpenTK.Graphics.OpenGL.BlendingFactorDest.One);
			GL.BlendEquation(OpenTK.Graphics.OpenGL.BlendEquationMode.FuncReverseSubtract);
			GL.Color4(factor, factor, factor, factor * 3);
			this.CreateRectangle();
		}
		public override void Draw(Color.Bgra color, Geometry2D.Single.Box region)
		{
			GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.Texture2D);
			GL.Color4(color.Red, color.Green, color.Blue, color.Alpha);
			GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Quads);
			GL.Vertex2(region.Left, region.Top);
			GL.Vertex2(region.Right, region.Top);
			GL.Vertex2(region.Right, region.Bottom);
			GL.Vertex2(region.Left, region.Bottom);
			GL.End();
		}	 
		protected void CreateRectangle()
		{
			this.CreateRectangle(new Geometry2D.Single.Box(new Geometry2D.Single.Point(), this.Size));
		}
		protected void CreateRectangle(Geometry2D.Single.Box region)
		{
			GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Quads);
			GL.Vertex2(region.Left, region.Top);
			GL.Vertex2(region.Right, region.Top);
			GL.Vertex2(region.Right, region.Bottom);
			GL.Vertex2(region.Left, region.Bottom);
			GL.End();
		}
		protected override Backend.Composition Refurbish()
		{
			return new Composition(this);
		}
	}
}
