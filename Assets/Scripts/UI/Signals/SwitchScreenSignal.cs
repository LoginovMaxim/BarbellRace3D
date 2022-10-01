using UI.Screens;

namespace UI.Signals
{
    public class SwitchScreenSignal
    {
        public readonly ScreenId ScreenId;

        public SwitchScreenSignal(ScreenId screenId)
        {
            ScreenId = screenId;
        }
    }
}