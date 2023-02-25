using Player;
using UnityEngine;
using UnityEngine.Assertions;

namespace UI {
    public class InventoryTabScreen : MonoBehaviour {
        [SerializeField] private Inventory.Inventory inventory;

        private void Start() {
            Assert.IsNotNull(inventory);

            PlayerInput.GotTabButtonDown += OnTabButtonHold;
            PlayerInput.GotTabButtonUp += () => PlayerInput.GotTabButtonDown += OnTabButtonHold;
        }

        private void OnTabButtonHold() {
            PlayerInput.GotTabButtonDown -= OnTabButtonHold;

            Assert.IsNotNull(inventory);

            if (!inventory) return;

            var inventoryContent = inventory.Tapes;
            var inventorySize = inventoryContent.Count;
        }
    }
}
