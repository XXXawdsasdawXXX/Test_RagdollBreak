using System.Collections.Generic;
using UnityEngine;

namespace Code.Character
{
    public class MaterialController : MonoBehaviour
    {
        [SerializeField] private Color _disabledColor = Color.gray;
        [SerializeField] private Material[] _materials;

        private readonly Dictionary<Material, Color> _defaultColors = new();

        private void Awake()
        {
            foreach (var material in _materials)
            {
                _defaultColors.Add(material, material.color);
            }
        }

        private void OnDisable()
        {
            SetEnable(true);
        }

        public void SetEnable(bool enable)
        {
            foreach (var material in _materials)
            {
                material.color = enable ? _defaultColors[material] : _disabledColor;
            }
        }
        
        #region Editor
#if UNITY_EDITOR
        
        public void FindAllMaterial()
        {
            var materials = new List<Material>();
            var renderers = GetComponentsInChildren<MeshRenderer>();

            foreach (var meshRenderer in renderers)
            {
                var rendersMaterials = new List<Material>();
                meshRenderer.GetMaterials(rendersMaterials );
                foreach (var material in rendersMaterials)
                {
                    if (materials.Contains(material))
                    {
                        continue;
                    }

                    materials.Add(material);
                }
            }

            _materials = materials.ToArray();
        }
#endif
        #endregion
    }
}