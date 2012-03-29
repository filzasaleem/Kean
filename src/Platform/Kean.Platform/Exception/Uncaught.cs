﻿// 
//  Uncaught.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2012 Simon Mika
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

using Error = Kean.Core.Error;
using Kean.Core.Reflect.Extension;

namespace Kean.Platform.Exception
{
    public class Uncaught : 
        Abstract
    {
		internal Uncaught(System.Exception innerException) :
			base(innerException, Error.Level.Critical, "Uncaught Error.", "An exception of type \"{0}\" with message \"{1}\" was not caught.", innerException.Type().Name, innerException.Message) { }
    }
}
