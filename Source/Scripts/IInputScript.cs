using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameEngine.InputDefs;

namespace GameEngine
{
    public interface IInputScript
    {
        void Update(GameTime gameTime, Entity inputOwner, Dictionary<string, BUTTON_STATE> actionStates);
    }
}
