﻿// 
//  Fixture.cs
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

namespace Kean.Draw.Gpu.Test
{
	public abstract class Fixture<T> :
		Raster.Test.Fixture<T>
		where T : Fixture<T>, new()
	{
		protected Fixture() :
			this(0.2f)
		{ }
		protected Fixture(float tolerance) : 
			base(tolerance)
		{ }
		Gui.Backend.IWindow window;
		public override void Setup()
		{
			base.Setup();
			this.window = new Gui.OpenGL.Window(new Geometry2D.Integer.Size(100, 100), "");
		}
		public override void TearDown()
		{
			this.window.Dispose();
			base.TearDown();
		}
	}
}
