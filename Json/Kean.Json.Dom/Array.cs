//
//  Array.cs
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

using System;
using Kean.Core;
using Kean.Core.Extension;
using Collection = Kean.Core.Collection;
using Kean.Core.Collection;
using Uri = Kean.Core.Uri;

namespace Kean.Json.Dom
{
	public class Array :
		Collection,
		Core.Collection.IList<Item>
	{
		Core.Collection.IList<Item> backend;
		public Array ()
		{ }
		public Array (Uri.Region region) :
			base(region)
		{
			Core.Collection.Hooked.List<Item> list = new Core.Collection.Hooked.List<Item>();
			list.Added += (index, item) =>
			{
				if (item.NotNull())
					item.Parent = this;
			};
			list.Removed += (index, item) =>
			{
				if (item.NotNull())
					item.Parent = null;
			};
			list.Replaced += (index, old, @new) =>
			{
				if (old.NotNull())
					old.Parent = null;
				if (@new.NotNull())
					@new.Parent = this;
			};
			this.backend = list;
		}
		internal override Collection Add(string label, Uri.Region region, Item item)
		{
			this.Add(item);
			return this;
		}
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return this.backend.GetEnumerator();
		}
		#endregion
		#region IEnumerable implementation
		public System.Collections.Generic.IEnumerator<Item> GetEnumerator ()
		{
			return this.GetEnumerator();
		}
		#endregion
        #region IEquatable implementation
        public override int GetHashCode()
        {
            return this.backend.GetHashCode();
        }
        public override bool Equals(object other)
        {
            return this.Equals(other as Array);
        }
        public override bool Equals(Item other)
        {
            return other is Array && this.backend.Equals(other);
        }
		public bool Equals (IVector<Item> other)
		{
			return this.backend.Equals(other);
		}
		#endregion
		#region IVector implementation
		public int Count { get { return this.backend.Count; } }
		public Item this[int index] 
		{
			get { return this.backend[index]; }
			set { this.backend[index] = value; }
		}
		#endregion
		#region IList implementation
		public IList<Item> Add (Item item)
		{
			this.backend.Add(item);
			return this;
		}
		public Item Remove ()
		{
			return this.backend.Remove();
		}
		public IList<Item> Insert (int index, Item item)
		{
			this.backend.Insert(index, item);
			return this;
		}
		public Item Remove (int index)
		{
			return this.backend.Remove(index);
		}
		#endregion
	}
}

