using Monsters;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(TargetChecker))]
    public class TargetCheckerEditor : UnityEditor.Editor {
        private void OnSceneGUI() {
            var targetChecker = (TargetChecker)target;
            var position = targetChecker.transform.position;
            var angle = targetChecker.GetAngle();
            var radius = targetChecker.GetRadius();

            Handles.color = Color.white;
            Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360f, radius);

            var eulerAngles = targetChecker.transform.eulerAngles;
            var viewAngle01 = DirectionFromAngle(eulerAngles.y, -angle / 2);
            var viewAngle02 = DirectionFromAngle(eulerAngles.y, angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(position, position + viewAngle01 * radius);
            Handles.DrawLine(position, position + viewAngle02 * radius);

            if (!targetChecker.IsTargetSeen()) return;

            Handles.color = Color.green;
            Handles.DrawLine(position, targetChecker.GetTargetRef().position);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
