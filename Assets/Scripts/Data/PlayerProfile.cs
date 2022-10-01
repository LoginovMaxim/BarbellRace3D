namespace Data
{
    public class PlayerProfile : IPlayerProfile
    {
        private readonly IDataStorage<PlayerData> _playerData;
        private readonly IDataStorage<PlayerCurrency> _playerCurrency;
        
        public PlayerProfile(
            IDataStorage<PlayerData> playerData,
            IDataStorage<PlayerCurrency> playerCurrency)
        {
            _playerData = playerData;
            _playerCurrency = playerCurrency;
        }

        #region IPlayerProfile

        string IPlayerProfile.Id => _playerData.Data.Id;

        #endregion
    }
}