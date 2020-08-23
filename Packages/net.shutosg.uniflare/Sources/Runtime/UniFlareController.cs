using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UEObject = UnityEngine.Object;

namespace UniFlare
{
    [ExecuteAlways]
    public class UniFlareController : MonoBehaviour
    {
        [SerializeField] private Transform _position = default;
        [SerializeField] private Transform _center = default;
        [SerializeField] private float _intensity = 100f;
        [SerializeField] private float _scale = 100f;
        [SerializeField] private Color _color = Color.white;
        [Range(0, 30)] [SerializeField] private float _flickerAmount = default;
        [Range(0, 20)] [SerializeField] private float _flickerSpeed = default;
        [SerializeField] private UniFlareElementBase[] elements = default;
        private readonly List<IUniFlareElement> _elements = new List<IUniFlareElement>();

        public UEObject[] GetTransforms() =>
            elements.Select(e => (UEObject)e.transform).ToArray();

        public UEObject[] GetRecordObjects() => elements
            .SelectMany(e => e.GetRecordObjects())
            .Concat(new[] { (UEObject)this })
            .ToArray();

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            ResetElementList();
            _elements.ForEach(e => e.Initialize());
        }

        private void ResetElementList()
        {
            _elements.Clear();
            foreach (var element in elements)
            {
                _elements.Add(element);
            }
        }

        public void UpdateFlare()
        {
            if (_elements.Count == 0) return;
            // flick intensity
            var noise = 2 * (0.5f - Mathf.PerlinNoise(Time.time * _flickerSpeed, 0)) * _flickerAmount;
            foreach (var element in _elements)
            {
                element.UpdatePosition(_position.localPosition, _center.localPosition);
                element.UpdateIntensity((_intensity + noise) / 100);
                element.UpdateScale(_scale / 100);
                element.UpdateColor(_color);
            }
        }

        public void ShiftColorHue(float hueOffset)
        {
            _color = _color.OffsetHue(hueOffset);
            foreach (var element in _elements)
            {
                element.ShiftColorHue(hueOffset);
            }
        }

        private void Update()
        {
            UpdateFlare();
        }
    }
}
