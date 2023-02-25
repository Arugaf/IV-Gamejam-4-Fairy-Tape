using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Player {
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private float speed = 12f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float axisThreshold = 0.1f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;

        public bool IsMoving { get; private set; }

        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _isGrounded;

        private void Start() {
            _controller = GetComponent<CharacterController>();
            Assert.IsNotNull(_controller);
        }

        private void Update() {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0) {
                _velocity.y = -2f;
            }

            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            IsMoving = x > axisThreshold || z > axisThreshold;

            var localTransform = transform;
            var move = localTransform.right * x + localTransform.forward * z;

            _controller.Move(move * (speed * Time.deltaTime));

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        public void IncreaseSpeed(float amount) {
            speed += amount;
        }

        public void DecreaseSpeed(float amount) {
            speed -= amount;
        }
    }
}
