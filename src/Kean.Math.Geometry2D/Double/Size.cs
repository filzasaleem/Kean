﻿// 
//  Size.cs
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
using System;
using Kean.Core.Basis.Extension;

namespace Kean.Math.Geometry2D.Double
{
    public class Size : Abstract.Size<Transform, TransformValue, Size, SizeValue, Kean.Math.Double, double>
    {
        public override SizeValue Value { get { return new SizeValue(this.Width, this.Height); } }
        public Size() { }
        public Size(double x, double y) : base(x, y) { }
        #region Casts
        public static implicit operator Size(Kean.Math.Geometry2D.Single.Size value)
        {
            return new Size(value.Width, value.Height);
        }
        public static implicit operator Size(Kean.Math.Geometry2D.Integer.Size value)
        {
            return new Size(value.Width, value.Height);
        }
        public static explicit operator Kean.Math.Geometry2D.Single.Size(Size value)
        {
            return new Kean.Math.Geometry2D.Single.Size((Kean.Math.Single)(value.Width), (Kean.Math.Single)(value.Height));
        }
        public static explicit operator Kean.Math.Geometry2D.Integer.Size(Size value)
        {
            return new Kean.Math.Geometry2D.Integer.Size((Kean.Math.Integer)(value.Width), (Kean.Math.Integer)(value.Height));
        }
        public static explicit operator SizeValue(Size value)
        {
            return new SizeValue(value.Width, value.Height);
        }
        public static implicit operator string(Size value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator Size(string value)
        {
            Size result = null;
            try
            {
                string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 2)
                    result = new Size(Kean.Math.Double.Parse(values[0]), Kean.Math.Double.Parse(values[1]));
            }
            catch
            {
                result = null;
            }
            return result;
        }
        #endregion

    }
}
