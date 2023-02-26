using Common;
using UnityEngine;

namespace Monsters {
    [RequireComponent(typeof(AIMovement))]
    [RequireComponent(typeof(TargetChecker))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Weapon))]
    public class MonsterBrain : MonoBehaviour {
        private AIMovement _movement;
        private TargetChecker _targetChecker;

        private void Start() {
            _movement = GetComponent<AIMovement>();
            _targetChecker = GetComponent<TargetChecker>();
        }

        public void OnTargetVisibilityStatusChanged(int status) {
            Debug.Log($"OnTargetVisibilityStatusChanged {status}");
            switch (status) {
                case 1:
                    _movement.InterruptPatrollingAndMoveToTarget(_targetChecker.GetTargetRef());
                    break;
                case 0:
                    _movement.StopMovingToTargetAndStartPatrolling();
                    break;
            }
        }
    }
}
