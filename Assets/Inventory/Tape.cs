using UnityEngine;
using UnityEngine.Events;

namespace Inventory {
    public class Tape : Pickable {
        public enum TapeType {
            Default,
            Stub1,
            Stub2
        }

        [SerializeField] private TapeType tapeType = TapeType.Default;

        public static event UnityAction<TapeType> GotTapePick;

        public override void Pick() {
            GotTapePick?.Invoke(tapeType);
            Destroy(gameObject);
        }
    }
}
