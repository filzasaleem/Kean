// 
//   Size.cs (generated by template)
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
using NUnit.Framework;
using Target = Kean.Math.Geometry3D;


namespace Kean.Math.Geometry3D.Test.Single
{
    [TestFixture]
	public class Size :
		Kean.Test.Fixture<Size>
    {
		float Precision { get { return 1e-4f; } }
       Target.Single.Size CastFromString(string value)
        {
            return value;
        }
         string CastToString(Target.Single.Size value)
        {
            return value;
        }
           Target.Single.Size Vector0 = new Target.Single.Size(22, -3, 10);
           Target.Single.Size Vector1 = new Target.Single.Size(12, 13, 20);
           Target.Single.Size Vector2 = new Target.Single.Size(34, 10, 30);
		   Target.Single.Size Vector3 = new Target.Single.Size(10, 20, 30);
        
        protected override void Run()
        {
            this.Run(
                
                this.ValueStringCasts,
				this.Equality,
                this.Addition,
                this.Subtraction,
                this.VectorProduct,
                this.Casting,
                this.Hash,
				this.IntegerCast,
				this.SingleCast,
				this.DoubleCast
                );
        }
		[Test]
		public void Norm()
		{
			Verify(this.Vector0.Norm, Is.EqualTo(593).Within(this.Precision));
			Verify(this.Vector0.Norm, Is.EqualTo(this.Vector0.ScalarProduct(this.Vector0)).Within(this.Precision));
		}
		[Test]
		public void Volume()
		{
			Verify(this.Vector3.Volume, Is.EqualTo(6000).Within(this.Precision));
		}
		[Test]
		public void ScalarProduct()
		{
			Target.Single.Size point = new Target.Single.Size();
			Verify(this.Vector0.ScalarProduct(point), Is.EqualTo(0).Within(this.Precision));
			Verify(this.Vector0.ScalarProduct(this.Vector1), Is.EqualTo(425).Within(this.Precision));
		}
		[Test]
		public void VectorProduct()
		{
			Verify(this.Vector0.VectorProduct(this.Vector1), Is.EqualTo(-this.Vector1.VectorProduct(this.Vector0)));
			Verify((this.Vector0.VectorProduct(this.Vector1)).Width, Is.EqualTo((-190)).Within(this.Precision));
			Verify((this.Vector0.VectorProduct(this.Vector1)).Height, Is.EqualTo((-320)).Within(this.Precision));
			Verify((this.Vector0.VectorProduct(this.Vector1)).Depth, Is.EqualTo((322)).Within(this.Precision));
		}
		#region Equality
		[Test]
		public void Equality()
		{
			Target.Single.Size point = null;
			Verify(this.Vector0, Is.EqualTo(this.Vector0));
			Verify(this.Vector0, Is.EqualTo(this.Vector0));
			Verify(this.Vector0.Equals(this.Vector0), Is.True);
			Verify(this.Vector0.Equals(this.Vector0 as object), Is.True);
			Verify(this.Vector0 == this.Vector0, Is.True);
			Verify(this.Vector0 != this.Vector1, Is.True);
			Verify(this.Vector0 == point, Is.False);
			Verify(point == point, Is.True);
			Verify(point == this.Vector0, Is.False);
		}
		#endregion
		#region Arithmetic
		[Test]
		public void Addition()
		{
			Verify((this.Vector0.Width + this.Vector1.Width), Is.EqualTo(this.Vector2.Width).Within(this.Precision));
			Verify((this.Vector0.Height + this.Vector1.Height), Is.EqualTo(this.Vector2.Height).Within(this.Precision));
			Verify((this.Vector0.Depth + this.Vector1.Depth), Is.EqualTo(this.Vector2.Depth).Within(this.Precision));
		}
		[Test]
		public void Subtraction()
		{
			Target.Single.Size size = null;
			Verify(this.Vector0 - this.Vector0, Is.EqualTo(size));
		}
		
		#endregion
		#region Hash Code
		[Test]
		public void Hash()
		{
			Verify(this.Vector0.GetHashCode(), Is.Not.EqualTo(0));
		}
		#endregion
		#region Casts
		[Test]
		public void Casting()
		{
			string value = "10, 20, 30";
			Verify(this.CastToString(this.Vector3), Is.EqualTo(value));
			Verify(this.CastFromString(value), Is.EqualTo(this.Vector3));
		}
        [Test]
        public void ValueStringCasts()
        {
            string textFromValue = new Target.Single.Size(10, 20, 30);
            Verify(textFromValue, Is.EqualTo("10, 20, 30"));
            Target.Single.Size @integerFromText = "10 20 30";
            Verify(@integerFromText.Width, Is.EqualTo(10));
            Verify(@integerFromText.Height, Is.EqualTo(20));
            Verify(@integerFromText.Depth, Is.EqualTo(30));
        }
		[Test]
        public void IntegerCast()
        {
            // Integer - Single
            Target.Integer.Size correct = new Target.Integer.Size(10, 20, 30);
            Target.Single.Size Single = correct;
            Verify(Single.Width, Is.EqualTo(10));
            Verify(Single.Height, Is.EqualTo(20));
            Verify(Single.Depth, Is.EqualTo(30));
            Verify((Target.Integer.Size)Single, Is.EqualTo(correct));
        }
		[Test]
        public void SingleCast()
        {
            // Single - Single
            Target.Single.Size correct = new Target.Single.Size(10, 20, 30);
            Target.Single.Size Single = (Target.Single.Size)correct;
            Verify(Single.Width, Is.EqualTo(10));
            Verify(Single.Height, Is.EqualTo(20));
            Verify(Single.Depth, Is.EqualTo(30));
            Verify((Target.Single.Size)Single, Is.EqualTo(correct));
        }
		[Test]
        public void DoubleCast()
        {
            // Double - Single
            Target.Double.Size correct = new Target.Double.Size(10, 20, 30);
            Target.Single.Size Single = (Target.Single.Size)correct;
            Verify(Single.Width, Is.EqualTo(10));
            Verify(Single.Height, Is.EqualTo(20));
            Verify(Single.Depth, Is.EqualTo(30));
            Verify((Target.Double.Size)Single, Is.EqualTo(correct));
        }
		#endregion
    }
}
