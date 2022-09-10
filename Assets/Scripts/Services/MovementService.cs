using App.Monos;
using Views;

namespace App.Services
{
    public abstract class MovementService : UpdatableService, IMovement
    {
        protected abstract MovementType MovementType { get; }
        
        protected MovementService(
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
        }
        
        #region IMovement

        MovementType IMovement.MovementType => MovementType;

        void IMovement.Pause()
        {
            Pause();
        }

        void IMovement.UnPause()
        {
            UnPause();
        }

        #endregion
    }
}