using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Character
{
    public class CharacterPhysicsDistributor : MonoBehaviour
    {
        [Serializable]
        public class Data
        {
            public string Name;
            public Rigidbody Body;
            public Character CharacterJointData;
            public Spring SprintJointData;
            public Data(string name, Rigidbody body)
            {
                Name = name;
                Body = body;

                if (body.TryGetComponent(out CharacterJoint characterJoint) && characterJoint != null)
                {
                    CharacterJointData = new Character(characterJoint);
                }

                if (body.TryGetComponent(out SpringJoint springJoint) && springJoint != null)
                {
                    SprintJointData = new Spring(springJoint);
                }
            }

            [Serializable]
            public class Character
            {
                public CharacterJoint Joint;
                public Vector3 Axis;
                public float Spring;
                public float Damper;
                public float BreakForce;
                public float MassScale;
                public float ConnectedMassScale;
                public float LowLimit;
                public float MaxLimit;

                public Character(CharacterJoint joint)
                {
                    Joint = joint;
                    Spring = joint.twistLimitSpring.spring;
                    Damper = joint.twistLimitSpring.damper;
                    MassScale = joint.massScale;
                    ConnectedMassScale = joint.connectedMassScale;
                    LowLimit = joint.lowTwistLimit.limit;
                    MaxLimit = joint.highTwistLimit.limit;
                    Axis = joint.axis;
                    BreakForce = joint.breakForce;
                }
            }

            [Serializable]
            public class Spring
            {
                public SpringJoint Joint;
                public float SpringValue;
                public float Damper;
                public float MassScale;
                public float ConnectedMassScale;
                public float MinDistance;
                public float MaxDistance;

                public Spring(SpringJoint joint)
                {
                    Joint = joint;
                    SpringValue = joint.spring;
                    Damper = joint.damper;
                    MassScale = joint.massScale;
                    ConnectedMassScale = joint.connectedMassScale;
                    MinDistance = joint.minDistance;
                    MaxDistance = joint.maxDistance;
                }
            }
        }

        [SerializeField] private float _totalMass = 70f;
        [SerializeField] private float _brakeMultiplier = 1f;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private List<Data> _data;

        public void DistributeMass()
        {
            float totalVolume = 0f;
            foreach (var rb in _rigidbodies)
            {
                totalVolume += GetVolume(rb);
            }

            foreach (var rb in _rigidbodies)
            {
                float volume = GetVolume(rb);
                rb.mass = _totalMass * (volume / totalVolume);
            }
        }

        private float GetVolume(Rigidbody rb)
        {
            var colliders = rb.GetComponents<Collider>();

            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                {
                    continue;
                }

                if (collider is BoxCollider box)
                {
                    return box.size.x * box.size.y * box.size.z;
                }
                else if (collider is SphereCollider sphere)
                {
                    return (4f / 3f) * Mathf.PI * Mathf.Pow(sphere.radius, 3);
                }
                else if (collider is CapsuleCollider capsule)
                {
                    float cylinderHeight = capsule.height - (2f * capsule.radius);
                    float cylinderVolume = Mathf.PI * Mathf.Pow(capsule.radius, 2) * cylinderHeight;
                    float sphereVolume = (4f / 3f) * Mathf.PI * Mathf.Pow(capsule.radius, 3);
                    return cylinderVolume + 2f * sphereVolume;
                }
            }

            return 0f;
        }

        public void MultiplyBreakForce()
        {
            foreach (var data in _data)
            {
                var cData = data.CharacterJointData;
                var cJoint = data.CharacterJointData.Joint;
                if (cData != null && cJoint != null)
                {
                    cData.BreakForce *= _brakeMultiplier;
                }
            }
        }

        public void LoadData()
        {
            _data.Clear();
            foreach (var body in _rigidbodies)
            {
                _data.Add(new Data(body.name, body));
            }
        }

        public void SetData()
        {
            foreach (var data in _data)
            {
                var cData = data.CharacterJointData;
                var cJoint = data.CharacterJointData.Joint;
                if (cData != null && cJoint != null)
                {

                    Debug.Log($"{data.Name} {cJoint != null} {cData != null}");
                    cJoint.breakForce = cData.BreakForce;
                    cJoint.axis = cData.Axis;
                    cJoint.twistLimitSpring = new SoftJointLimitSpring()
                    {
                        spring = cData.Spring,
                        damper = cData.Damper
                    };
                    cJoint.massScale = cData.MassScale;
                    cJoint.connectedMassScale = cData.ConnectedMassScale;
                    var lowLimit = cJoint.lowTwistLimit;
                    lowLimit.limit = cData.LowLimit;
                    cJoint.lowTwistLimit = lowLimit;

                    var maxLimit = cJoint.highTwistLimit;
                    maxLimit.limit = cData.MaxLimit;
                    cJoint.highTwistLimit = maxLimit;
                }
            }
        }

        private void OnValidate()
        {
            if (_rigidbodies == null)
            {
                _rigidbodies = GetComponentsInChildren<Rigidbody>();
            }
        }
    }
}