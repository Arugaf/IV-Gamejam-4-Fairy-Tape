using System.Collections;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Stamina))]
    public class Sprinter : MonoBehaviour {
        [SerializeField] private uint sprintCost = 10;
        [SerializeField] private float staminaSubtractionDelay = 0.5f;
        [SerializeField] private float speedBoost = 5f;

        private Stamina _stamina;
        private PlayerMovement _playerMovement;

        private bool _sprinting;

        private void Start() {
            _stamina = GetComponent<Stamina>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update() {
            if (_sprinting) {
                StartCoroutine(Sprint());
            }
        }

        public void StartSprint() {
            _sprinting = true;
            _playerMovement.IncreaseSpeed(speedBoost);
        }

        public void EndSprint() {
            _sprinting = false;
            _playerMovement.DecreaseSpeed(speedBoost);
        }

        private IEnumerator Sprint() {
            while (_sprinting) {
                if (!_playerMovement.IsMoving) {
                    yield return null;
                }

                if (_stamina.RemoveStamina(sprintCost)) {
                    yield return new WaitForSeconds(staminaSubtractionDelay);
                }

                EndSprint();
                yield break;
            }

            EndSprint();
        }
    }
}
