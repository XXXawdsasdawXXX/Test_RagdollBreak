using UnityEditor;
using UnityEngine;

namespace Code.Character.Editor
{
    [CustomEditor(typeof(MaterialController))]
    public class MaterialControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MaterialController controller = (MaterialController)target;

            if (GUILayout.Button("Find materials"))
            {
                controller.FindAllMaterial();
            }

            if (GUILayout.Button("Set disable mode"))
            {
                controller.SetEnable(false);
            }

            if (GUILayout.Button("Set enable mode"))
            {
                controller.SetEnable(true);
            }
            
        }
    }
}