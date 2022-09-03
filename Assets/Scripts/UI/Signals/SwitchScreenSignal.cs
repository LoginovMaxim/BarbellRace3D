using App.UI.Screens;

namespace App.UI.Signals
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