using UnityEngine;

namespace Views
{
    public sealed class Disk : MonoBehaviour
    {
        [SerializeField] private int _diskCount;

        public int DiskCount => _diskCount;
    }
}