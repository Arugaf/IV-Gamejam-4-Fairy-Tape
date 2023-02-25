using System.Collections;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Stamina))]
    [RequireComponent(typeof(PlayerMovement))]
    public class Sprinter : MonoBehaviour {
        [SerializeField] private uint sprintCost = 10;
        [SerializeField] private float staminaSubtractionDelay = 0.5f;
        [SerializeField] private float speedBoost = 5f;

        private Stamina _stamina;
        private PlayerMovement _playerMovement;

        private Coroutine _sprintCoroutine;
        private IEnumerator _sprintEnumerator;

        private bool _sprinting;

        private void Start() {
            _sprintEnumerator = Sprint();
            _stamina = GetComponent<Stamina>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public void StartSprint() {
            if (_sprintCoroutine != null || _stamina.GetCurrentStamina() <= 0f) return;

            _sprinting = true;
            _playerMovement.IncreaseSpeed(speedBoost);
            _sprintCoroutine = StartCoroutine(_sprintEnumerator);
        }

        public void EndSprint() {
            if (_sprintCoroutine == null) return;

            _sprinting = false;
            _playerMovement.DecreaseSpeed(speedBoost);
            StopCoroutine(_sprintCoroutine);
            _sprintCoroutine = null;
        }

        private IEnumerator Sprint() {
            while (_sprinting) {
                if (_playerMovement.IsMoving) {
                    if (!_stamina.RemoveStamina(sprintCost)) {
                        EndSprint();
                    }
                }

                yield return new WaitForSeconds(staminaSubtractionDelay);
            }
        }
    }
}
