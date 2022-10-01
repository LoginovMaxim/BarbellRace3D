namespace UI.Commands
{
    public sealed class ShowIconPopupCommand
    {
        /*private readonly IPopupService _popupService;
        private readonly IDiscoveryInfoProvider _discoveryInfo;

        public ShowIconPopupCommand(
            IPopupService popupService, 
            IDiscoveryInfoProvider discoveryInfo, 
            SignalBus signalBus)
        {
            _popupService = popupService;
            _discoveryInfo = discoveryInfo;
            
            signalBus.Subscribe<ShowIconPopupSignal>(x => ShowEntityDiscoveryPopup(x.DiscoveryId));
        }
        
        private void ShowEntityDiscoveryPopup(DiscoveryId discoveryId)
        {
            var perkInfo = _discoveryInfo.GetEntityInfo(discoveryId);
            var iconPopupData = new IconPopupData
            {
                Label = perkInfo.Label,
                Description = perkInfo.Description,
                Sprite = perkInfo.Sprite
            };
            
            _popupService.ShowPopup(iconPopupData);
        }*/
    }
}