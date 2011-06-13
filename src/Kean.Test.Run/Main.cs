// 
//  Main.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2009 Simon Mika
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
//  You should have received data copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

namespace Kean.Test.Run
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Started");
            Kean.Test.Math.Matrix.Algorithms.Double.Test();

            Kean.Test.Math.Single.Test();
            Kean.Test.Math.Double.Test();
            Kean.Test.Math.Complex.Single.Test();
            Kean.Test.Math.Complex.Double.Test();
            Kean.Test.Math.Complex.Fourier.Single.Test();
            Kean.Test.Math.Complex.Fourier.Double.Test();

            Kean.Test.Math.Geometry2D.Integer.Point.Test();
            Kean.Test.Math.Geometry2D.Single.Point.Test();
            Kean.Test.Math.Geometry2D.Double.Point.Test();

            Kean.Test.Math.Geometry2D.Integer.Size.Test();
            Kean.Test.Math.Geometry2D.Single.Size.Test();
            Kean.Test.Math.Geometry2D.Double.Size.Test();
            
            Kean.Test.Math.Geometry2D.Integer.Box.Test();
            Kean.Test.Math.Geometry2D.Single.Box.Test();
            Kean.Test.Math.Geometry2D.Double.Box.Test();
            
            Kean.Test.Math.Geometry2D.Integer.Transform.Test();
            Kean.Test.Math.Geometry2D.Single.Transform.Test();
            Kean.Test.Math.Geometry2D.Double.Transform.Test();

            Kean.Test.Math.Geometry3D.Integer.Point.Test();
            Kean.Test.Math.Geometry3D.Single.Point.Test();
            Kean.Test.Math.Geometry3D.Double.Point.Test();

            Kean.Test.Math.Geometry3D.Integer.Size.Test();
            Kean.Test.Math.Geometry3D.Single.Size.Test();
            Kean.Test.Math.Geometry3D.Double.Size.Test();
          
            Kean.Test.Math.Geometry3D.Integer.Box.Test();
            Kean.Test.Math.Geometry3D.Single.Box.Test();
            Kean.Test.Math.Geometry3D.Double.Box.Test();

            Kean.Test.Math.Matrix.Algorithms.Double.Test();

            
            Kean.Test.Math.Geometry3D.Single.Transform.Test();
            Kean.Test.Math.Geometry3D.Double.Transform.Test();
           
            Kean.Test.Math.Geometry3D.Double.Quaternion.Test();

            Kean.Test.Core.Collection.Vector.Test();
			Kean.Test.Core.Collection.List.Test();
			Kean.Test.Core.Collection.Queue.Test();
			Kean.Test.Core.Collection.Stack.Test();
			Kean.Test.Core.Collection.Dictionary.Test();
			Kean.Test.Core.Collection.Linked.List.Test();
			Kean.Test.Core.Collection.Linked.Queue.Test();
			Kean.Test.Core.Collection.Linked.Stack.Test();
			Kean.Test.Core.Collection.Array.Vector.Test();
			Kean.Test.Core.Collection.Array.List.Test();
			Kean.Test.Core.Collection.Array.Queue.Test();
			Kean.Test.Core.Collection.Array.Stack.Test();
			Kean.Test.Core.Collection.Sorted.List.Test();

			//Kean.Test.Core.Error.Error.Test();
			Console.WriteLine("Done");
		}
	}
}