﻿// 
//  Keyboard.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2009-2011 Simon Mika
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
using Collection = Kean.Core.Collection;
using Kean.Core.Collection.Extension;
using Geometry2D = Kean.Math.Geometry2D;
using Kean.Core.Extension;

namespace Kean.Gui.Backend
{
    public abstract class Keyboard
    {
        public abstract KeyboardButtons Buttons { get; }

        internal event Action<Input.Key> Pressed;
        internal event Action<Input.Key> Released;
        internal event Action<char> Character;

        protected void OnPress(Input.Key key)
        {
            if (this.Pressed.NotNull())
                this.Pressed(key);
        }
        protected void OnRelease(Input.Key key)
        {
            if (this.Released.NotNull())
                this.Released(key);
        }
        protected void OnCharacter(char character)
        {
            if (this.Character.NotNull())
                this.Character(character);
        }
    }
}
