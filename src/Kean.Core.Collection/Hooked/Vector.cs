//
//  Array.cs
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
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Kean.Core.Basis.Extension;

namespace Kean.Core.Collection.Hooked
{
	public class Vector<T> :
		Abstract.Vector<T>,
		IVector<T>
	{
		IVector<T> data;
		public event Action<int, T, T> Replaced;
		public event Func<int, T, T, bool> OnReplace;
		public override T this[int index] {
			get { return this.data[index]; }
			set {
				if (this.data[index] != value) {
					T oldValue = this.data[index];
					bool replace = true;
					if (this.OnReplace.NotNull ()) {
						Delegate[] onReplace = this.OnReplace.GetInvocationList ();
						for (int i = 0; replace && i < onReplace.Length; i++)
							replace &= (onReplace[i] as Func<int, T, T, bool>) (index, oldValue, value);
					}
					if (replace) {
						this.data[index] = value;
						this.Replaced.Call (index, oldValue, this.data[index]);
					}
				}
			}
		}
		public override int Count {
			get { return this.data.Count; }
		}
		public Vector (Interface.IVector<T> data)
		{
			this.data = data;
		}
	}
}
