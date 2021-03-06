﻿// 
//  Variable.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2010 Simon Mika
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
using Kean.Core.Notify.Extension;

namespace Kean.Core.Notify
{
	public class Variable<T> :
		Abstract<T>
	{
		T value;
		event Action<T> changed;
		event OnChange<T> onChange;
		public override bool Connected { get { return true; } }
		public override T Value
		{
			get { return this.value; }
			set
			{
				if (!value.SameOrEquals(this.value) && this.onChange.Call(value))
				{
					this.value = value;
					this.changed.Call(this.value);
				}
			}
		}
		public override event Action<T> Changed
		{
			add { this.changed += value; }
			remove { this.changed -= value; }
		}
		public override event OnChange<T> OnChange
		{
			add { this.onChange += value; }
			remove { this.onChange -= value; }
		}

		#region Constructors
		public Variable()
		{ }
		public Variable(T value)
		{
			this.value = value;
		}
		public Variable(Action<T> changed)
		{
			this.changed = changed;
		}
		public Variable(T value, Action<T> changed) :
			this(value)
		{
			this.changed = changed;
		}
		public Variable(T value, Action<T> changed, OnChange<T> onChange) :
			this(value, changed)
		{
			this.onChange = onChange;
		}
		#endregion

	}
}