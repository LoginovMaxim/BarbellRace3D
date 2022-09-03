using System;
using UnityEngine;

namespace App.Data
{
    [Serializable] public class PlayerCurrency : IData
    {
        public event Action DataChanged;

        public int Soft
        {
            get
            {
                return _soft;
            }
            set
            {
                if (_soft == value)
                    return;

                _soft = value;
                DataChanged?.Invoke();
            }
        }
        
        public int Hard
        {
            get
            {
                return _hard;
            }
            set
            {
                if (_hard == value)
                    return;

                _hard = value;
                DataChanged?.Invoke();
            }
        }

        [SerializeField] private int _soft;
        [SerializeField] private int _hard;
    }
}