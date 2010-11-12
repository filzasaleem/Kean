﻿//
//  Queue.cs
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
using Kean.Core.Basis.Extension;

namespace Kean.Core.Collection.Hooked
{
	public class Queue<T> :
		IQueue<T>
	{
		IQueue<T> data;
		public event Func<T, bool> OnEnqueue;
		public event Action<T> Enqueued;
		public event Func<T, bool> OnDequeue;
		public event Action<T> Dequeued;
		public Queue(IQueue<T> data)
		{
			this.data = data;
		}
		#region IQueue<T> Members
		public bool Empty
		{
			get { return this.data.Empty; }
		}
		public void Enqueue(T item)
		{
			bool enqueue = true;
			if (this.OnEnqueue.NotNull ()) {
				Delegate[] onEnqueue = this.OnEnqueue.GetInvocationList ();
				for (int i = 0; enqueue && i < onEnqueue.Length; i++)
					enqueue &= (onEnqueue[i] as Func<T, bool>) (item);
			}
			if (enqueue)
			{
				this.data.Enqueue(item);
				this.Enqueued.Call(item);
			}
		}
		public T Peek()
		{
			return this.data.Peek();
		}
		public T Dequeue()
		{
			T result;
			if (this.OnDequeue.IsNull() || this.OnDequeue(this.Peek()))
			{
				result = this.data.Dequeue();
				this.Dequeued.Call(result);
			}
			else
				result = default(T);
			return result;
		}
		#endregion
	}
}
