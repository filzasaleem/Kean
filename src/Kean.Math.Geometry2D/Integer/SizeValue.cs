﻿// 
//  SizeValue.cs
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
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;
namespace Kean.Math.Geometry2D.Integer
{
	public struct SizeValue :
		Abstract.ISize<int>, Abstract.IVector<int>
	{
		int width;
		int height;
        #region ISize<int>
        public int Width
		{
			get { return this.width; }
			set { this.width = value; }
		}
		public int Height
		{
			get { return this.height; }
			set { this.height = value; }
        }
        #endregion
        #region IVector<float> Members
        int Abstract.IVector<int>.X { get { return this.width; } }
        int Abstract.IVector<int>.Y { get { return this.height; } }
        #endregion
        public SizeValue(int width, int height)
		{
			this.width = width;
			this.height = height;
		}
		public static implicit operator Size(SizeValue value)
		{
			return new Size(value.Width, value.Height);
		}
		public static explicit operator SizeValue(Size value)
		{
			return new SizeValue(value.Width, value.Height);
		}
	}
}