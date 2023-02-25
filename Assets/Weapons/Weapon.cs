using Inventory;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons {
    public class Weapon : Pickable {
        public enum WeaponType {
            None,
            Default
        }

        public static event UnityAction<Weapon> GotWeaponPick;

        public WeaponType WeaponTypeValue { get; } = WeaponType.None;

        [SerializeField] private uint damageValue = 20;
        [SerializeField] private float damageDistance = 3f;

        public override void Pick() {
            GotWeaponPick?.Invoke(this);
        }

        public void Hit() {
            // pass health
            var localTransform = transform;
            var ray = new Ray(localTransform.position, localTransform.forward);

            // todo return old instance
            if (Physics.Raycast(ray, out var hit, damageDistance)) {
                hit.transform.GetComponent<Health>()?.DoDamage(damageValue);
            }
        }
    }
}
