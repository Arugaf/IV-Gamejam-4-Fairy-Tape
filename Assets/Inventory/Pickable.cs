using UnityEngine;
using UnityEngine.Assertions;

namespace Inventory {
    public abstract class Pickable : MonoBehaviour {
        [SerializeField] private Color highlightColor = new(227, 223, 204);

        private Color _originalColor;
        private Renderer _renderer;

        private bool _active;

        private void Start() {
            gameObject.tag = "Pickable";
            _renderer = GetComponent<Renderer>();
            Assert.IsNotNull(_renderer);
        }

        public abstract void Pick();

        public void Highlight(bool enable) {
            if (enable) {
                if (_active) return;

                _active = true;
                var material = _renderer.material;
                _originalColor = material.color;
                material.color = highlightColor;
            } else {
                _active = false;
                _renderer.material.color = _originalColor;
            }
        }
    }
}
