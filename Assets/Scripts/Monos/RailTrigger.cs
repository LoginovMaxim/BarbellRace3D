using UnityEngine;
using Views;

namespace Monos
{
    public sealed class RailTrigger : MonoBehaviour
    {
        public RailView RailView => railView;
        
        [SerializeField] private RailView railView;
    }
}