using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Monsters {
    public class TargetChecker : MonoBehaviour {
        [SerializeField] private float radius = 10f;
        [Range(0f, 360f)] [SerializeField] private float angle;

        [SerializeField] private Transform target;

        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstructionMask;

        private bool _canSeeTarget;

        private readonly Collider[] _targetInSphere = new Collider[1];

        [SerializeField] private UnityEvent<int> gotTargetVisibilityStatusChanged;

        private readonly bool _coroutineStub = false;

        private void Start() {
            Assert.IsNotNull(target);
            StartCoroutine(FOVRoutine());
        }

        public float GetRadius() {
            return radius;
        }

        public float GetAngle() {
            return angle;
        }

        public bool IsTargetSeen() {
            return _canSeeTarget;
        }

        public Transform GetTargetRef() {
            return target;
        }

        private IEnumerator FOVRoutine() {
            var wait = new WaitForSeconds(0.2f);

            while (true) {
                yield return wait;

                if (CheckForTarget() == _canSeeTarget) continue;

                _canSeeTarget = !_canSeeTarget;
                gotTargetVisibilityStatusChanged.Invoke(_canSeeTarget ? 1 : 0);

                if (_coroutineStub) {
                    yield break;
                }
            }
        }

        private bool CheckForTarget() {
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, _targetInSphere, targetMask) <= 0) {
                return false;
            }

            var position = transform.position;
            var targetPosition = target.transform.position;
            var direction = (targetPosition - position).normalized;

            if (Vector3.Angle(transform.forward, direction) * 2 < angle) {
                return !Physics.Raycast(
                    targetPosition,
                    direction,
                    Vector3.Distance(
                        position,
                        targetPosition),
                    obstructionMask);
            }

            return false;
        }
    }
}
