using UnityEngine;
using UnityEngine.Events;

namespace Common {
    public class Health : MonoBehaviour {
        [SerializeField] private UnityEvent<uint> gotDamage;
        [SerializeField] private UnityEvent gotLethalDamage;

        [SerializeField] private long hp = 100;

        public void DoDamage(uint amount) {
            hp -= amount;
            gotDamage.Invoke(amount);

            if (hp < 0) {
                gotLethalDamage.Invoke();
            }
        }
    }
}
