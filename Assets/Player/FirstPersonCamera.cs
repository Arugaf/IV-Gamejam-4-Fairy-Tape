using UnityEngine;

namespace Player {
    public class FirstPersonCamera : MonoBehaviour {
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private Transform playerBody;
        private float _xRotation;

        private bool _focused;

        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() {
            if (!Application.isFocused) {
                return;
            }

            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
