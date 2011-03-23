﻿using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kean.Core.Basis.Extension;

namespace Kean.Test.Math.Geometry2D.Abstract
{
    public abstract class Point<PointType, PointValue, R, V> : Vector<PointType, PointValue, R,V>
        where PointType : Kean.Math.Geometry2D.Abstract.Point<PointType, PointValue, R, V>, new()
        where PointValue : struct, Kean.Math.Geometry2D.Abstract.IPoint<V>, Kean.Math.Geometry2D.Abstract.IVector<V>
        where R : Kean.Math.Abstract<R, V>, new()
        where V : struct
    {
        [Test]
        public void GetValues()
        {
            Assert.That(this.Vector0.X.Value, Is.EqualTo(this.Cast(22.221)).Within(this.Precision));
            Assert.That(this.Vector0.Y.Value, Is.EqualTo(this.Cast(-3.1)).Within(this.Precision));
        }
        [Test]
        public void Swap()
        {
            PointType result = this.Vector0.Swap();
            Assert.That(result.X, Is.EqualTo(this.Vector0.Y));
            Assert.That(result.Y, Is.EqualTo(this.Vector0.X));
        }

        public void Run()
        {
            this.Run(
                this.Equality,
                this.Addition,
                this.Subtraction,
                this.ScalarMultitplication,
                this.GetValues,
                this.Swap
                );
        }
    }
}
