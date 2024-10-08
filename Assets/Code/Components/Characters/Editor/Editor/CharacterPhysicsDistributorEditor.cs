﻿using UnityEditor;
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

            if (GUILayout.Button("Rigidbody: Distribute Mass"))
            {
                distributor.DistributeMass();
            }
            
            
            if (GUILayout.Button("Rigidbody: Multiply  Mass"))
            {
                distributor.MultiplyMass();
            }

            
            if (GUILayout.Button("Character Joint: multiply brask force"))
            {
                distributor.MultiplyBreakForce();
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