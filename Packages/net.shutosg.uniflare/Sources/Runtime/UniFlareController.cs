using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using shutosg.UniFlare.Elements;
using shutosg.UniFlare.Extensions;
using shutosg.UniFlare.Tools;

namespace shutosg.UniFlare
{
    [ExecuteAlways]
    public class UniFlareController : MonoBehaviour
    {
        [SerializeField] private Transform _position = default;
        [SerializeField] private Transform _center = default;
        [Range(0, 1000)] [SerializeField] private int _intensity = 100;
        [SerializeField] private float _scale = 100f;
        [SerializeField] private Color _color = Color.white;
        [Range(0, 100)] [SerializeField] private float _flickerAmount = default;
        [Range(0, 20)] [SerializeField] private float _flickerSpeed = default;
        [Range(-100, 100)] [SerializeField] private float _flickerTimeOffset = default;
        [SerializeField] private UniFlareElementBase[] elements = default;
        private readonly List<IUniFlareElement> _elements = new List<IUniFlareElement>();
        private readonly UniFlareValueFlicker _flicker = new UniFlareValueFlicker();

        public Transform Position => _position;
        public Transform Center => _center;
        public int Intensity
        {
            get => _intensity;
            set => _intensity = Mathf.Max(0, value);
        }
        public float Scale
        {
            get => _scale;
            set => _scale = Mathf.Max(0, value);
        }
        public Color Color
        {
            get => _color;
            set => _color = value;
        }
        public float FlickerAmount
        {
            get => _flicker.Max;
            set => _flicker.Max = Mathf.Max(0, value);
        }
        public float FlickerSpeed
        {
            get => _flicker.Speed;
            set => _flicker.Speed = Mathf.Max(0, value);
        }
        public float FlickerTimeOffset
        {
            get => _flicker.TimeOffset;
            set => _flicker.TimeOffset = value;
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
            SetFlickerValuesFromInspector();
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
#if UNITY_EDITOR
            // update params for changing on inspector
            SetFlickerValuesFromInspector();
#endif
            var noise = _flicker.Value;
            foreach (var element in _elements)
            {
                element.UpdatePosition(_position.localPosition, _center.localPosition);
                element.UpdateIntensity((_intensity + noise) / 100f);
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

        private void SetFlickerValuesFromInspector()
        {
            FlickerAmount = _flickerAmount;
            FlickerSpeed = _flickerSpeed;
            FlickerTimeOffset = _flickerTimeOffset;
        }

#if UNITY_EDITOR
        public Object[] GetTransforms() =>
            elements.Select(e => (Object)e.transform).ToArray();

        public Object[] GetRecordObjects() => elements
            .SelectMany(e => e.GetRecordObjects())
            .Append(this)
            .ToArray();

        private void OnValidate()
        {
            SetFlickerValuesFromInspector();
        }
#endif
    }
}
