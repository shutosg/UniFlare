using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        [SerializeField] private float _flickerTimeOffset = default;
        [SerializeField] private UniFlareElementBase[] elements = default;
        private readonly List<IUniFlareElement> _elements = new List<IUniFlareElement>();

        public Transform Position => _position;
        public Transform Center => _center;
        public float Intensity
        {
            get => _intensity;
            set => _intensity = value;
        }
        public float Scale
        {
            get => _scale;
            set => _scale = value;
        }
        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            UpdateFlare();
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

        private void UpdateFlare()
        {
            if (_elements.Count == 0) return;
            // flick intensity
            var noise = 2 * (0.5f - Mathf.PerlinNoise((Time.time + _flickerTimeOffset) * _flickerSpeed, 0)) * _flickerAmount;
            foreach (var element in _elements)
            {
                element.UpdatePosition(_position.localPosition, _center.localPosition);
                element.UpdateIntensity((_intensity + noise) / 100);
                element.UpdateScale(_scale / 100);
                element.UpdateColor(_color);
                element.UpdateOtherParams();
            }
        }

        public void SetColorHue(float targetHue)
        {
            var currentHue = _color.ToHSV().x;
            var hueOffset = targetHue - currentHue;
            // Debug.Log($"targetHue: {targetHue}, currentHue: {currentHue}, offset: {hueOffset}");
            _color = _color.OffsetHue(hueOffset);
            foreach (var element in _elements)
            {
                element.ShiftColorHue(hueOffset);
            }
        }

#if UNITY_EDITOR
        public Object[] GetTransforms() =>
            elements.Select(e => (Object)e.transform).ToArray();

        public Object[] GetRecordObjects() => elements
            .SelectMany(e => e.GetRecordObjects())
            .Append(this)
            .ToArray();
#endif
    }
}
