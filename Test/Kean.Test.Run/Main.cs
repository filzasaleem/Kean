// 
//  Main.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2009-2013 Simon Mika
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
			Core.Error.Log.CatchErrors = false;
			Html.Dom.Test.All.Test();

			#region Core
			Core.Test.All.Test();
			Core.Collection.Test.All.Test();
			Core.Error.Test.All.Test();
			Core.Reflect.Test.All.Test();
			Core.Uri.Test.All.Test();
			#endregion
			#region Math
			Math.Test.All.Test();
			Math.Complex.Test.All.Test();
			Math.Geometry2D.Test.All.Test();
			Math.Geometry3D.Test.All.Test();
			Math.Matrix.Test.All.Test();
			// Math.Random.Test.All.Test();
			Math.Regression.Test.All.Test();
			#endregion
			#region Draw
			Draw.Test.All.Test();
			Draw.Cairo.Test.All.Test();
			Draw.Raster.Test.All.Test();
			//Draw.Vector.Test.All.Test();
			//Draw.Gpu.Test.All.Test();
			//Draw.Net.Test.All.Test();
			//Draw.Jpeg.Test.All.Test();
			#endregion
			#region Gui
			Gui.OpenGL.Test.Window.Test();
			//new Gui.OpenGL.Test.Window().Create();
			#endregion
			#region Xml
			Xml.Serialize.Test.All.Test();
			Xml.Dom.Test.All.Test();
			#endregion
			#region Json
			//Json.Dom.Test.All.Test();			
			//Json.Serialize.Test.All.Test();
			#endregion
			Console.WriteLine();
			Console.WriteLine("Press any key to continue");
			Console.ReadKey(true);
		}
	}
}