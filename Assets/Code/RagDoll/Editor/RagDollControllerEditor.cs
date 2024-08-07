using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace Code.Character.Editor
{
    [CustomEditor(typeof(RagDollController))]
    public class RagDollControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RagDollController controller = (RagDollController)target;

            if (GUILayout.Button("Find bodies"))
            {
                controller.FindAllRigidBodies();
            }
            
            if (GUILayout.Button("Find colliders"))
            {
                controller.FindColliders();
            }

            if (GUILayout.Button("On"))
            {
                controller.SetEnable(true);
            }

            if (GUILayout.Button("Off"))
            {
                controller.SetEnable(false);
            }
        }
    }
}

#endif
