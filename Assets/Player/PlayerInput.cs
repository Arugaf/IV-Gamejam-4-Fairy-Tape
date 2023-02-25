using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerInput : MonoBehaviour {
        public static event UnityAction GotEButtonDown;
        public static event UnityAction GotEButtonUp;

        public static event UnityAction GotPrimaryMouseButtonDown;
        public static event UnityAction GotPrimaryMouseButtonUp;

        public static event UnityAction GotTabButtonDown;
        public static event UnityAction GotTabButtonUp;

        public static event UnityAction GotShiftButtonDown;
        public static event UnityAction GotShiftButtonUp;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.E)) {
                GotEButtonDown?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.E)) {
                GotEButtonUp?.Invoke();
            }

            if (Input.GetMouseButtonDown(PrimaryButton)) {
                GotPrimaryMouseButtonDown?.Invoke();
            }

            if (Input.GetMouseButtonUp(PrimaryButton)) {
                GotPrimaryMouseButtonUp?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Tab)) {
                GotTabButtonDown?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Tab)) {
                GotTabButtonUp?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                GotShiftButtonDown?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                GotShiftButtonUp?.Invoke();
            }
        }

        private const int PrimaryButton = 0;
    }
}
