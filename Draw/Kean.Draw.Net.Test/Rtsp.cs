﻿// 
//  Single.cs
//  
//  Author:
//      Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2011 Anders Frisk
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
using Target = Kean.Draw.Net.Rtsp;

namespace Kean.Draw.Net.Test
{
	[TestFixture]
	public class Rtsp :
		Kean.Test.Fixture<Rtsp>
	{
		protected override void Run()
		{
			this.Run(
				this.Open
				);
		}
		[Test]
		public void Open()
		{
		  //  Target.Jpeg jpeg = new Target.Jpeg("rtsp://192.168.1.21:554/axis-media/media.amp?videocodec=jpeg");
			
			Target.Client jpeg = new Target.Client();

			//bool opened = jpeg.Open("rtsp://81.227.126.88:554/axis-media/media.amp?videocodec=jpeg");
				bool opened = jpeg.Open("rtsp://elmer.tapir.caltech.edu/Lecture6_tape1_web.mov");
			if (opened)
			{
				int counter = 0;
				jpeg.NewFrame += image =>
				{
				//    image.Save("frames/test" + (counter++).ToString("0000") + ".png");
					Console.Write(".");
				};
				jpeg.Play();
				DateTime now = DateTime.Now;
				while (counter < 20 && (DateTime.Now - now).Duration().TotalSeconds < 10)
					System.Threading.Thread.Sleep(10);
				jpeg.Close();
			}
		}
	}
}
