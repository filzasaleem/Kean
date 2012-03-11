﻿using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kean.Math.Test
{
    public class Fraction : 
        Kean.Test.Fixture<Fraction>
    {
		string prefix = "Kean.Math.Test.";
        protected override void Run()
        {
			this.Run(
                this.Constructors, 
                this.Casts, 
                this.GCD,
                this.Equality,
                this.Arithmetics);
        }
	    [Test]
        public void Constructors()
        {
            Expect(new Kean.Math.Fraction("3:5").Nominator, Is.EqualTo(3), this.prefix + "Constructors.0");
			Expect(new Kean.Math.Fraction("3:5").Denominator, Is.EqualTo(5), this.prefix + "Constructors.1");
			Expect((float)new Kean.Math.Fraction(3.245f), Is.EqualTo(3.245f), this.prefix + "Constructors.2");
			Expect((double)new Kean.Math.Fraction(3.245), Is.EqualTo(3.245), this.prefix + "Constructors.3");
			Expect((string)new Kean.Math.Fraction(3.245), Is.EqualTo("649:200"), this.prefix + "Constructors.4");
			Expect(new Kean.Math.Fraction("-3:5").Nominator, Is.EqualTo(-3), this.prefix + "Constructors.5");
			Expect(new Kean.Math.Fraction("3:-5").Denominator, Is.EqualTo(-5), this.prefix + "Constructors.6");
			Expect((float)new Kean.Math.Fraction(-3.245f), Is.EqualTo(-3.245f), this.prefix + "Constructors.7");
			Expect((double)new Kean.Math.Fraction(-3.245), Is.EqualTo(-3.245), this.prefix + "Constructors.8");
			Expect((string)new Kean.Math.Fraction(-3.245), Is.EqualTo("-649:200"), this.prefix + "Constructors.9");
            Expect(new Kean.Math.Fraction(3, 5).Nominator, Is.EqualTo(3), this.prefix + "Constructors.10");
            Expect(new Kean.Math.Fraction(3, 5).Denominator, Is.EqualTo(5), this.prefix + "Constructors.11");
        }
		[Test]
		public void Casts()
		{
			Expect(((Kean.Math.Fraction)("-3:5")).Nominator, Is.EqualTo(-3), this.prefix + "Casts.0");
			Expect(((Kean.Math.Fraction)("3:-5")).Denominator, Is.EqualTo(-5), this.prefix + "Casts.1");
			Expect((string)((Kean.Math.Fraction)("-3/5")), Is.EqualTo("-3:5"), this.prefix + "Casts.2");
			Expect((float)(Kean.Math.Fraction)(-3.245f), Is.EqualTo(-3.245f), this.prefix + "Casts.3");
			Expect((double)(Kean.Math.Fraction)(-3.245), Is.EqualTo(-3.245), this.prefix + "Casts.4");
			Expect((string)(Kean.Math.Fraction)(-3.245), Is.EqualTo("-649:200"), this.prefix + "Casts.5");
			Expect(((Kean.Math.Fraction)("-1989/867")).GCD, Is.EqualTo(51), this.prefix + "Casts.6");
			Expect(((Kean.Math.Fraction)("-1989/867")).ContinousFraction, Is.EqualTo("[-2;3,2,2]"), this.prefix + "Casts.7");
			Expect(((Kean.Math.Fraction)(Kean.Math.Double.SquareRoot(14))).ContinousFraction, Is.EqualTo("[3;1,2,1,6,1,2,1,7]"), this.prefix + "Casts.8");
			Expect((float)(Kean.Math.Fraction)("3.5"), Is.EqualTo(3.5f), this.prefix + "Casts.9");
			Expect((float)(Kean.Math.Fraction)("3,5"), Is.EqualTo(3.5), this.prefix + "Casts.10");
			Expect(((Kean.Math.Fraction)("3:5")).Nominator, Is.EqualTo(3), this.prefix + "Casts.11");
			Expect(((Kean.Math.Fraction)("3:5")).Denominator, Is.EqualTo(5), this.prefix + "Casts.12");
			Expect((string)((Kean.Math.Fraction)("3/5")), Is.EqualTo("3:5"), this.prefix + "Casts.13");
			Expect((float)(Kean.Math.Fraction)(3.245f), Is.EqualTo(3.245f), this.prefix + "Casts.14");
			Expect((double)(Kean.Math.Fraction)(3.245), Is.EqualTo(3.245), this.prefix + "Casts.15");
			Expect((string)(Kean.Math.Fraction)(3.245), Is.EqualTo("649:200"), this.prefix + "Casts.16");
			Expect(((Kean.Math.Fraction)("1989/867")).GCD, Is.EqualTo(51), this.prefix + "Casts.17");
			Expect(((Kean.Math.Fraction)("1989/867")).ContinousFraction, Is.EqualTo("[2;3,2,2]"), this.prefix + "Casts.18");
		}
		[Test]
		public void Equality()
		{
			Expect((Kean.Math.Fraction)("3:5") == (Kean.Math.Fraction)("6:10"), Is.True, this.prefix + "Equality.0");
			Expect((Kean.Math.Fraction)("3:5") != (Kean.Math.Fraction)("5:10"), Is.True, this.prefix + "Equality.1");
			Expect(((Kean.Math.Fraction)("3:5")).Equals((Kean.Math.Fraction)("6:10")), Is.True, this.prefix + "Equality.2");
		}
        [Test]
        public void GCD()
        {
            Expect(new Kean.Math.Fraction().GCD, Is.EqualTo(1), this.prefix + "GCD.0");
            Expect(new Kean.Math.Fraction(3,5).GCD, Is.EqualTo(1), this.prefix + "GCD.1");
            Expect(new Kean.Math.Fraction(-3, 5).GCD, Is.EqualTo(1), this.prefix + "GCD.2");
            Expect(new Kean.Math.Fraction(-3, 15).GCD, Is.EqualTo(3), this.prefix + "GCD.3");
            Expect(((Kean.Math.Fraction)("3:5") / 3).Nominator, Is.EqualTo(1), this.prefix + "GCD.4");
            Expect(((Kean.Math.Fraction)("3:5") / 3).Denominator, Is.EqualTo(5), this.prefix + "GCD.5");
        }
        [Test]
        public void Arithmetics()
        {
            Expect((Kean.Math.Fraction)("3:5") * (Kean.Math.Fraction)("2:7"), Is.EqualTo((Kean.Math.Fraction)("6:35")), this.prefix + "Arithmetics.0");
            Expect((Kean.Math.Fraction)("3:5") + (Kean.Math.Fraction)("2:7"), Is.EqualTo((Kean.Math.Fraction)("31:35")), this.prefix + "Arithmetics.1");
            Expect((Kean.Math.Fraction)("3:5") - (Kean.Math.Fraction)("2:7"), Is.EqualTo((Kean.Math.Fraction)("11:35")), this.prefix + "Arithmetics.2");
            Expect(-(Kean.Math.Fraction)("3:5"), Is.EqualTo((Kean.Math.Fraction)("-3:5")), this.prefix + "Arithmetics.3");
            Expect(7 *(Kean.Math.Fraction)("3:5"), Is.EqualTo((Kean.Math.Fraction)("21:5")), this.prefix + "Arithmetics.4");
            Expect((Kean.Math.Fraction)("3:5") * 7, Is.EqualTo((Kean.Math.Fraction)("21:5")), this.prefix + "Arithmetics.5");
            Expect((Kean.Math.Fraction)("3:5") / 3, Is.EqualTo((Kean.Math.Fraction)("1:5")), this.prefix + "Arithmetics.6");
        }
    }
}