using System;
using UnityEngine;

namespace Player {
    public class Stamina : MonoBehaviour {
        [SerializeField] private uint maxAmount = 100u;
        private uint _currentAmount;

        private void Start() {
            _currentAmount = maxAmount;
        }

        public bool RemoveStamina(uint amount) {
            return (_currentAmount = Math.Clamp(_currentAmount - amount, 0u, maxAmount)) > 0;
        }

        public uint GetCurrentStamina() {
            return _currentAmount;
        }
    }
}
