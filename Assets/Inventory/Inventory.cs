using System.Collections.Generic;
using UnityEngine;

namespace Inventory {
    public class Inventory : MonoBehaviour {
        public HashSet<Tape.TapeType> Tapes { get; private set; }

        private void Start() {
            Tapes = new HashSet<Tape.TapeType>();
            Tape.GotTapePick += type => Tapes.Add(type);
        }
    }
}
