namespace UI.Signals
{
    public class ClosePopupSignal
    {
        public readonly int PopupIndex;

        public ClosePopupSignal(int popupIndex)
        {
            PopupIndex = popupIndex;
        }
    }
}