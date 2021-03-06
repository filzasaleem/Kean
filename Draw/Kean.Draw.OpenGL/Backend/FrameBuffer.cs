﻿//
//  FrameBuffer.cs
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

using Kean.Core.Collection.Extension;
using System;
using Collection = Kean.Core.Collection;
using Error = Kean.Core.Error;
using GL = OpenTK.Graphics.OpenGL.GL;
using Kean.Core.Extension;

namespace Kean.Draw.OpenGL.Backend
{
	public abstract class FrameBuffer :
		Resource
	{
		internal int Identifier { get; private set; }
		protected FrameBuffer(Context context) :
			base(context)
		{
			int identifier;
			GL.Ext.GenFramebuffers(1, out identifier);
			this.Identifier = identifier;
		}
		protected FrameBuffer(FrameBuffer original) :
			base(original)
		{
			this.Identifier = original.Identifier;
			original.Identifier = 0;
		}
		protected override void Dispose(bool disposing)
		{
			if (this.Context.NotNull())
				this.Context.Recycle(this.Refurbish());
		}
		#region Implementors Interface
		public abstract void Use();
		public abstract void UnUse();
		public abstract void Create(Texture texture, Depth depth);
		protected internal abstract FrameBuffer Refurbish();
		protected internal override void Delete()
		{
			this.Identifier = 0;
			base.Delete();
		}
		#endregion
		public override string ToString()
		{
			return this.Identifier.ToString();
		}
	}
}
