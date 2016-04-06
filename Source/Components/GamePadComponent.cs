using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using GameEngine.InputDefs;

namespace GameEngine
{
    public class GamePadComponent :IComponent
    {
        public PlayerIndex PlayerIndex { get; set; }
        public Dictionary<string, List<Buttons>> Actions{ get; set; }
        public Dictionary<string, BUTTON_STATE> ActionStates { get; set; }
        public GamePadState NewState { get; set; }
        public GamePadState OldState { get; set; }
        public IInputScript Script { get; set; }

        public GamePadComponent()
        {
            PlayerIndex = PlayerIndex.One;
            Script = null;
        }

        public GamePadComponent(PlayerIndex playerIndex, IInputScript script)
        {
            ActionStates = new Dictionary<string, BUTTON_STATE>();
            Actions = new Dictionary<string, List<Buttons>>();
            PlayerIndex = playerIndex;
            Script = script;
        }


        public void AddButtonToAction(string action, Buttons button)
        {
            if (!Actions.ContainsKey(action))
            {
                Actions[action] = new List<Buttons>();
                ActionStates[action] = BUTTON_STATE.NOT_PRESSED;
            }
            Actions[action].Add(button);
        }


        public void RemoveButtonFromAction(string action, Buttons button)
        {
            if (Actions.ContainsKey(action))
            {
                Actions[action].Remove(button);
            }
        }

    }
}
