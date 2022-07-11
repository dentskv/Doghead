namespace Core.Scripts.StateMachine
{
    public class StateParams
    {
        public bool Modal { get; }

        public StateParams(bool modal)
        {
            Modal = modal;
        }
    }
}