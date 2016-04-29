namespace GameEngine
{
    public class TriggerComponent : IComponent
    {
        public IScript Script { get; set; }

        public TriggerComponent(IScript script)
        {
            Script = script;
        }
    }
}
