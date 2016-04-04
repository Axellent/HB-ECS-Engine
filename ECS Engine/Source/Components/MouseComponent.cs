using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using GameEngine.InputDefs;

namespace GameEngine
{
    public class MouseComponent : IComponent
    {
        public MouseState NewState { get; set; }
        public MouseState OldState { get; set; }
        public IInputScript Script { get; set; }

        public Dictionary<string, BUTTON_STATE> actionStates = new Dictionary<string, BUTTON_STATE>();

        public int GetX()
        {
            return NewState.X;
        }

        public int GetY()
        {
            return NewState.Y;
        }

        public int GetScrollValue()
        {
            return NewState.ScrollWheelValue;
        }
    }
}
