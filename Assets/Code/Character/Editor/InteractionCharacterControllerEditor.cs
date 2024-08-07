using UnityEditor;
using UnityEngine;

namespace Code.Character.Editor
{
    [CustomEditor(typeof(InteractionCharacterController))]
    public class InteractionCharacterControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            InteractionCharacterController controller = (InteractionCharacterController)target;

            if (GUILayout.Button("Find joints"))
            {
                controller.FindInteractionJoints();
            }
            
            if (GUILayout.Button("Find foots"))
            {
                controller.FindFoots();
            }
        }
    }
}