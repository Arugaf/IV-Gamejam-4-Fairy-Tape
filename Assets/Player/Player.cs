using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Player {
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Inventory.Inventory))]
    [RequireComponent(typeof(Picker))]
    [RequireComponent(typeof(Sprinter))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Stamina))]
    public class Player : MonoBehaviour {
        public static event UnityAction GotPlayerDeath;
        
        [SerializeField] private Weapon weapon;

        private Sprinter _sprinter;
        
        private void Start() {
            _sprinter = GetComponent<Sprinter>();

            Weapon.GotWeaponPick += newWeapon => weapon = newWeapon;

            PlayerInput.GotPrimaryMouseButtonDown += TryHit;
            PlayerInput.GotPrimaryMouseButtonUp += () => PlayerInput.GotPrimaryMouseButtonDown += TryHit;

            PlayerInput.GotShiftButtonDown += OnSprintStart;
            PlayerInput.GotShiftButtonUp += OnSprintEnd;
        }

        private void TryHit() {
            PlayerInput.GotPrimaryMouseButtonDown -= TryHit;

            if (!weapon) return;

            weapon.Hit();
            Debug.Log("Successful hit");
        }

        private void OnSprintStart() {
            PlayerInput.GotShiftButtonDown -= OnSprintStart;
            _sprinter.StartSprint();
        }

        private void OnSprintEnd() {
            _sprinter.EndSprint();
            PlayerInput.GotShiftButtonDown += OnSprintStart;
        }

        public void OnPlayerDeath() {
            GotPlayerDeath?.Invoke();
        }
    }
}
