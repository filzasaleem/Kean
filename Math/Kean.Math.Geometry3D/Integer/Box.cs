// 
//  Box.cs (generated by template)
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2011-2013 Simon Mika
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
using Kean.Core.Extension;

namespace Kean.Math.Geometry3D.Integer
{
    public struct Box:
	IEquatable<Box>
    {
		public Point LeftTopFront;
		public Size Size;
		#region Sizes
		public int Width { get { return this.Size.Width; } }
		public int Height { get { return this.Size.Height; } }
		public int Depth { get { return this.Size.Depth; } }
		#endregion
		#region All sides
		public int Left { get { return this.LeftTopFront.X; } }
		public int Right { get { return this.LeftTopFront.X + this.Size.Width; } }
		public int Top { get { return this.LeftTopFront.Y; } }
		public int Bottom { get { return this.LeftTopFront.Y + this.Size.Height; } }
		public int Front { get { return this.LeftTopFront.Z; } }
		public int Back { get { return this.LeftTopFront.Z + this.Size.Depth; } }
		#endregion
	    public bool Empty { get { return this.Size.Empty; } }
		public Box(Point leftTopFront, Size size)
		{
			this.LeftTopFront = leftTopFront;
			this.Size = size;
		}
        public Box(int left, int top, int front, int width, int height, int depth) : 
			this(new Point(left, top, front), new Size(width, height, depth)) 
		{ }
        public Box(Size size) :
			this(new Geometry3D.Integer.Point(), size) 
		{ }
        
        public  Box Pad(int left, int right, int top, int bottom, int front, int back)
        {
            return new Box(new Point(this.Left - left, this.Top - top, this.Front - front), new Size(this.Size.Width + left + right, this.Size.Height + top + bottom, this.Size.Depth + front + back));
        }
        public  Box Intersection(Box other)
        {
            int left = this.Left > other.Left ? this.Left : other.Left;
            int top = this.Top > other.Top ? this.Top : other.Top;
            int front = this.Front > other.Front ? this.Front: other.Front;
            int width = Kean.Math.Integer.Maximum((this.Right < other.Right ? this.Right : other.Right) - left, 0);
            int height = Kean.Math.Integer.Maximum((this.Bottom < other.Bottom ? this.Bottom : other.Bottom) - top, 0);
            int depth = Kean.Math.Integer.Maximum((this.Back < other.Back ? this.Back : other.Back) - front, 0);
            return new Box(left, top, width, height, front, depth);
        }
		#region Arithmetic operators
		public static Box operator +(Box left, Box right)
		{
			Box result;
			if (left.Empty)
				result = right;
			else if (right.Empty)
				result = left;
			else
				result = new Box(Kean.Math.Integer.Minimum(left.Left, right.Left), Kean.Math.Integer.Minimum(left.Top, right.Top), Kean.Math.Integer.Minimum(left.Front, right.Front), Kean.Math.Integer.Maximum(left.Right, right.Right) - Kean.Math.Integer.Minimum(left.Left, right.Left), Kean.Math.Integer.Maximum(left.Bottom, right.Bottom) - Kean.Math.Integer.Minimum(left.Top, right.Top), Kean.Math.Integer.Maximum(left.Back, right.Back) - Kean.Math.Integer.Minimum(left.Front, right.Front));
			return result;
		}
		public static Box operator -(Box left, Box right)
		{
			Box result;
			if (!left.Empty && !right.Empty)
			{
				 int l = Kean.Math.Integer.Maximum(left.Left, right.Left);
				 int r = Kean.Math.Integer.Minimum(left.Right, right.Right);
				 int t = Kean.Math.Integer.Maximum(left.Top, right.Top);
				 int b = Kean.Math.Integer.Minimum(left.Bottom, right.Bottom);
				 int u = Kean.Math.Integer.Maximum(left.Front, right.Front);
				 int v = Kean.Math.Integer.Minimum(left.Back, right.Back);
				if (l < r && t < b && u < v)
				{
					result = new Box(l, t, u, r - l, b - t, v - u);
				}
				else
					result = new Box();
			}
			else
				result = new Box();
			return result;
		}
		public static Box operator +(Box left, Point right)
		{
			return new Box(left.LeftTopFront + right, left.Size);
		}
		public static Box operator -(Box left, Point right)
		{
			return new Box(left.LeftTopFront - right, left.Size);
		}
		public static Box operator +(Box left, Size right)
		{
			return new Box(left.LeftTopFront, left.Size + right);
		}
		public static Box operator -(Box left, Size right)
		{
			return new Box(left.LeftTopFront, left.Size - right);
		}
		public static Box operator *(Transform left, Box right)
		{
			return new Box(left * right.LeftTopFront, left * right.Size);
		}
		#endregion
		#region Comparison Operators
		public static bool operator ==(Box left, Box  right)
		{
			return object.ReferenceEquals(left, right) ||
				!object.ReferenceEquals(left, null) && !object.ReferenceEquals(right, null) &&
				left.LeftTopFront == right.LeftTopFront &&
				left.Size == right.Size;
		}
		public static bool operator !=(Box left, Box right)
		{
			return !(left == right);
		}
		#endregion
		#region Object Overrides
		public override bool Equals(object other)
		{
			return (other is Box) && this.Equals((Box)other);
		}
		public bool Equals(Box other)
		{
			return this == other;
		}
		public override string ToString()
		{
			return this.ToString("{0}, {1}, {2}, {3}, {4}, {5}");
		}
		public string ToString(string format)
		{
			return string.Format(format, (this.LeftTopFront.X).ToString(), (this.LeftTopFront.Y).ToString(), (this.LeftTopFront.Z).ToString(), (this.Size.Width).ToString(), (this.Size.Height).ToString(), (this.Size.Depth).ToString());
		}
		public override int GetHashCode()
		{
			return 33 * this.LeftTopFront.GetHashCode() ^ this.Size.GetHashCode();
		}
		#endregion
		#region Static Methods
		public static Box Create(Point leftTopFront, Size size)
		{
			Box result = new Box();
			result.LeftTopFront = leftTopFront;
			result.Size = size;
			return result;
		}
		public static Box Bounds(int left, int right, int top, int bottom, int front, int back)
		{
			return new Box(left, top, front,right - left, bottom - top, back - front);
		}
		public static Box Bounds(params Point[] points)
		{
			Box result = new Box();
			if (points.Length > 0)
			{
				int xMinimum = 0;
				int xMaximum = xMinimum;
				int yMinimum = xMinimum;
				int yMaximum = xMinimum;
				int zMinimum = xMinimum;
				int zMaximum = xMinimum;
				bool initilized = false;
				foreach (Point point in points)
				{
						if (!initilized)
						{
							initilized = true;
							xMinimum = point.X;
							xMaximum = point.X;
							yMinimum = point.Y;
							yMaximum = point.Y;
							zMinimum = point.Z;
							zMaximum = point.Z;
						}
						else
						{
							if (point.X < xMinimum)
								xMinimum = point.X;
							else if (point.X > xMaximum)
								xMaximum = point.X;
							if (point.Y < yMinimum)
								yMinimum = point.Y;
							else if (point.Y > yMaximum)
								yMaximum = point.Y;
							if (point.Z < zMinimum)
								zMinimum = point.Z;
							else if (point.Z > zMaximum)
								zMaximum = point.Z;
						}
					
				}
				result = new Box(xMinimum, yMinimum, zMinimum, xMaximum - xMinimum, yMaximum - yMinimum, zMaximum - zMinimum);
			}
			return result;
		}
		#endregion
		#region Casts
		 public static implicit operator Box(string value)
        {
            Box result = new Box();
            if (value.NotEmpty())
            {

                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 6)
                        result = new Box((Point)(values[0] + " " + values[1] + " " + values[2]), (Size)(values[3] + " " + values[4] + " " + values[5]));
                }
                catch
                {
                }
            }
            return result;
        }
		public static implicit operator string(Box value)
        {
            return value.NotNull() ? value.ToString() : null;
        }


		
		
		
		
        #endregion
    }
}
