using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Monsters {
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIMovement : MonoBehaviour {
        private NavMeshAgent _agent;

        [SerializeField] private Transform[] patrolPoints;
        private Transform _currentTarget;
        [SerializeField] private float sufficientDistanceToTargetEpsilon = 0.5f;
        private uint _currentPatrolPointIndex;
        private bool _patrolling = true;

        [SerializeField] private float switchTargetDelaySeconds = 5f;

        // [SerializeField] private bool isRandomPath = false;

        private delegate void GotDestinationReached();

        private GotDestinationReached _gotDestinationReached;

        private Coroutine _movementCoroutine;
        private IEnumerator _movementEnumerator;
        private bool _restartCoroutine = false;

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _movementEnumerator = MoveToPosition();
            _gotDestinationReached += OnDestinationReached;

            StartPatrolling();
        }

        private void Update() {
            if (!_restartCoroutine) return;

            _restartCoroutine = false;

            if (_movementCoroutine != null) {
                StopCoroutine(_movementCoroutine);
            }

            _movementEnumerator = MoveToPosition();
            _movementCoroutine = StartCoroutine(_movementEnumerator);
        }

        public void InterruptPatrollingAndMoveToTarget(Transform target) {
            if (_movementCoroutine != null) {
                StopCoroutine(_movementCoroutine);
            }

            _patrolling = false;
            if (target) {
                _currentTarget = target;
            }

            _movementEnumerator = MoveToPosition();
            _movementCoroutine = StartCoroutine(_movementEnumerator);

            Debug.Log("InterruptPatrollingAndMoveToTarget");
        }

        public void StopMovingToTargetAndStartPatrolling() {
            Debug.Log("StopMovingToTargetAndStartPatrolling");
            StartPatrolling();
        }

        // private void InterruptPatrollingAndMoveToTarget

        private void SwitchCurrentTargetPoint() {
            Debug.Log("SwitchCurrentTargetPoint");
            _currentPatrolPointIndex =
                ++_currentPatrolPointIndex >= patrolPoints.Length ? 0u : _currentPatrolPointIndex;
            _currentTarget = patrolPoints.Length > 0 ? patrolPoints[_currentPatrolPointIndex] : null;
        }

        private void StartPatrolling() {
            Debug.Log("StartPatrolling");
            if (patrolPoints.Length > 0) {
                _currentTarget = patrolPoints[_currentPatrolPointIndex];
            }

            _patrolling = true;

            if (_movementCoroutine != null) {
                StopCoroutine(_movementCoroutine);
            }

            _movementEnumerator = MoveToPosition();
            _movementCoroutine = StartCoroutine(_movementEnumerator);
        }

        private void OnDestinationReached() {
            Debug.Log("OnDestinationReached");
            if (_patrolling) {
                SwitchCurrentTargetPoint();
            }

            _restartCoroutine = true;
        }

        private IEnumerator MoveToPosition() {
            Debug.Log("MoveToPosition");
            if (!_currentTarget) yield break;

            Debug.Log(_currentTarget.gameObject.name);
            _agent.SetDestination(_currentTarget.position);

            while (Vector3.Distance(transform.position, _currentTarget.position) > sufficientDistanceToTargetEpsilon) {
                _agent.SetDestination(_currentTarget.position);
                yield return new WaitForSeconds(1f);
            }

            _agent.SetDestination(transform.position);

            if (_patrolling) {
                yield return new WaitForSeconds(switchTargetDelaySeconds);
            }

            _gotDestinationReached?.Invoke();
        }
    }
}
