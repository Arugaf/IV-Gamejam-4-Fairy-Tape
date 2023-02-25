using Inventory;
using UnityEngine;

namespace Player {
    public class Picker : MonoBehaviour {
        [SerializeField] private float maxDistance = 5f;
        [SerializeField] private int layerMask = ~0;

        private Ray _ray;
        private RaycastHit _hit;

        private Pickable _currentPickable;

        private void Start() {
            PlayerInput.GotEButtonDown += TryPick;
            PlayerInput.GotEButtonUp += () => PlayerInput.GotEButtonDown += TryPick;
        }

        private void Update() {
            var item = CheckPickable();
            if (!item || item != _currentPickable) {
                if (_currentPickable) {
                    _currentPickable.Highlight(false);
                }
            }

            _currentPickable = item;
            if (_currentPickable) {
                _currentPickable.Highlight(true);
            }
        }

        private void TryPick() {
            PlayerInput.GotEButtonDown -= TryPick;

            // Debug.DrawRay(_ray.origin, ((Func<Vector3>)(() => {
            //     var heading = _hit.point - _ray.origin;
            //     return (heading / heading.magnitude) * heading.magnitude;
            // }))(), Color.cyan);

            if (!_currentPickable) return;
            
            _currentPickable.Pick();
            Debug.Log("Picked item");
        }

        private Pickable CheckPickable() {
            var localTransform = transform;
            _ray = new Ray(localTransform.position, localTransform.forward);

            // todo return old instance
            if (Physics.Raycast(_ray, out _hit, maxDistance, layerMask) && _hit.transform.CompareTag("Pickable")) {
                return _hit.transform.GetComponent<Pickable>();
            }

            return null;
        }
    }
}
