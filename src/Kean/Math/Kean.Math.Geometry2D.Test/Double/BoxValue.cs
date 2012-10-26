// 
//  BoxValue.cs
//  
//  Author:
//       Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2012 Anders Frisk
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
using NUnit.Framework.SyntaxHelpers;
using Target = Kean.Math.Geometry2D;
using Kean.Core.Extension;

namespace Kean.Math.Geometry2D.Test.Double
{
    [TestFixture]
    public class BoxValue : 
        Kean.Test.Fixture<BoxValue>
    {
        string prefix = "Kean.Math.Geometry2D.Test.Double.BoxValue.";
        protected override void Run()
        {
            this.Run(
                this.Equality,
                this.LeftTop,
                this.Size,
                this.Addition,
                this.Subtraction,
                this.ScalarMultitplication,
                this.Hash,
                this.Casts,
                this.StringCasts);
        }
       Target.Double.BoxValue box0 = new Target.Double.BoxValue(1, 2, 3, 4);
       Target.Double.BoxValue box1 = new Target.Double.BoxValue(4, 3, 2, 1);
       Target.Double.BoxValue box2 = new Target.Double.BoxValue(2, 1, 4, 3);
       #region Equality
       [Test]
       public void Equality()
       {
          Target.Double.BoxValue box = new  Target.Double.BoxValue();
           Expect(this.box0, Is.EqualTo(this.box0));
           Expect(this.box0.Equals(this.box0 as object), Is.True);
           Expect(this.box0 == this.box0, Is.True);
           Expect(this.box0 != this.box1, Is.True);
           Expect(this.box0 == box, Is.False);
           Expect(box == box, Is.True);
           Expect(box == this.box0, Is.False);
       }
       #endregion
       [Test]
       public void LeftTop()
       {
           Target.Double.PointValue leftTop = this.box0.LeftTop;
           Expect(leftTop.X, Is.EqualTo(1), this.prefix + "LeftTop.0");
           Expect(leftTop.Y, Is.EqualTo(2), this.prefix + "LeftTop.1");
       }
       [Test]
       public void Size()
       {
           Target.Double.SizeValue size = this.box0.Size;
           Expect(size.Width, Is.EqualTo(3), this.prefix + "Size.0");
           Expect(size.Height, Is.EqualTo(4), this.prefix + "Size.1");
       }
       #region Arithmetic
       [Test]
       public void Addition()
       {
       }
       [Test]
       public void Subtraction()
       {
       }
       [Test]
       public void ScalarMultitplication()
       {
       }
       #endregion
       #region Hash Code
       [Test]
       public void Hash()
       {
           Expect(this.box0.Hash(), Is.Not.EqualTo(0));
       }
       #endregion
       [Test]
       public void Casts()
       {
           // integer - double
           {
               Target.Integer.BoxValue integer = new Target.Integer.BoxValue(10, 20, 30, 40);
               Target.Double.BoxValue @double = integer;
               Expect(@double.Left, Is.EqualTo(10));
               Expect(@double.Top, Is.EqualTo(20));
               Expect(@double.Width, Is.EqualTo(30));
               Expect(@double.Height, Is.EqualTo(40));
               Expect((Target.Integer.BoxValue)@double, Is.EqualTo(integer));
           }
           // single - double
           {
               Target.Single.BoxValue single = new Target.Single.BoxValue(10, 20, 30, 40);
               Target.Double.BoxValue @double = single;
               Expect(@double.Left, Is.EqualTo(10));
               Expect(@double.Top, Is.EqualTo(20));
               Expect(@double.Width, Is.EqualTo(30));
               Expect(@double.Height, Is.EqualTo(40));
               Expect((Target.Single.BoxValue)@double, Is.EqualTo(single));
           }
       }
       [Test]
       public void StringCasts()
       {
           string textFromValue = new Target.Double.BoxValue(10, 20, 30, 40);
           Expect(textFromValue, Is.EqualTo("10, 20, 30, 40"));
           Target.Double.BoxValue @doubleFromText = "10 20 30 40";
           Expect(@doubleFromText.Left, Is.EqualTo(10));
           Expect(@doubleFromText.Top, Is.EqualTo(20));
           Expect(@doubleFromText.Width, Is.EqualTo(30));
           Expect(@doubleFromText.Height, Is.EqualTo(40));
       }
    }
}
