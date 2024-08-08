using UnityEditor;
using UnityEngine;

namespace Code.Character.Editor
{
    [CustomEditor(typeof(CharacterPhysicsDistributor))]
    public class CharacterPhysicsDistributorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            CharacterPhysicsDistributor distributor = (CharacterPhysicsDistributor)target;

            if (GUILayout.Button("Distribute Mass"))
            {
                distributor.DistributeMass();
            }
            
            
            if (GUILayout.Button("Load"))
            {
                distributor.LoadData();
            }
            
                
            if (GUILayout.Button("Set"))
            {
                distributor.SetData();
            }
        }
    }
}