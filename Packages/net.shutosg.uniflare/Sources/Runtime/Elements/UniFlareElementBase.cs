using shutosg.UniFlare.Extensions;
using shutosg.UniFlare.Tools;
using UnityEngine;

namespace shutosg.UniFlare.Elements
{
    [ExecuteAlways]
    public abstract class UniFlareElementBase : MonoBehaviour, IUniFlareElement
    {
        protected const float Percentage = 100f;
        protected const int IntensityMagnification = 100;
        [Header("for Self Update")]
        [SerializeField] private bool _selfUpdate = default;
        [Range(0, 100)] [SerializeField] private float _flickerAmount = default;
        [Range(0, 20)] [SerializeField] private float _flickerSpeed = default;
        [SerializeField] private float _flickerTimeOffset = default;
        [Header("General Params")]
        [SerializeField] protected float _distance = Percentage;
        [SerializeField] protected Vector3 _positionOffset = Vector3.zero;
        [SerializeField] protected Vector3 _transition = Vector2.one * Percentage;
        [Range(0, 1000)] [SerializeField] protected int _intensity = IntensityMagnification;
        [SerializeField] protected Vector3 _scale = Vector3.one;
        [SerializeField] protected bool _useGlobalColor = true;
        [SerializeField] protected Color _color = Color.white;
        [SerializeField] protected Material _material;
        private UniFlareValueFlicker _flicker;
        private UniFlareValueFlicker Flicker => _flicker ?? (_flicker = new UniFlareValueFlicker());
        private bool _selfInitialized;

        public float FlickerAmount
        {
            get => Flicker.Max;
            set => Flicker.Max = Mathf.Max(0, value);
        }
        public float FlickerSpeed
        {
            get => Flicker.Speed;
            set => Flicker.Speed = Mathf.Max(0, value);
        }
        public float FlickerTimeOffset
        {
            get => Flicker.TimeOffset;
            set => Flicker.TimeOffset = value;
        }

        public virtual void SetMaterialIfNeeded(Material material)
        {
            if (_material != material) _material = material;
        }

        public void InitializeDistance(float distance) => _distance = distance;
        public void InitializeIntensity(int intensity) => _intensity = intensity;
        public void InitializeScale(Vector3 scale) => _scale = scale;
        public void InitializeTransition(Vector3 transition) => _transition = transition;
        public void InitializePositionOffset(Vector3 positionOffset) => _positionOffset = positionOffset;

        public void InitializeColor(Color color)
        {
            if (!_useGlobalColor) return;
            _color = color;
        }

        public virtual void Initialize()
        {
            SetMaterialIfNeeded(_material);
            if (_selfUpdate) SetFlickerValuesFromFields();
        }

        /// <summary>
        /// calculate and update localPosition using 'position' and 'center' and '_distance'
        /// if 'distance' is 0.0, then localPosition is the same as 'position'
        /// if 'distance' is 0.5, then localPosition is the same as 'center'
        /// if 'distance' is 1.0, then localPosition is the same as 'position' on the other side of 'center'
        /// </summary>
        /// <param name="position">flare position</param>
        /// <param name="center">center position</param>
        public virtual void UpdatePosition(Vector3 position, Vector3 center)
        {
            var newPosition = UniFlareVectorExtension.LerpUnclamped(position, center, _transition / Percentage * _distance / Percentage * 2);
            transform.localPosition = newPosition + _positionOffset;
        }

        public abstract void UpdateIntensity(float intensity);

        public virtual void UpdateScale(float scale)
        {
            transform.localScale = _scale * scale;
        }

        public virtual void ShiftColorHue(float hueOffset) => _color = _color.OffsetHue(hueOffset);

        public abstract void UpdateColor(Color color);
        public virtual void UpdateOtherParams() { }

        protected Color CalculateColor(Color color)
        {
            if (_useGlobalColor)
            {
                color.a = _color.a;
                return color;
            }
            return _color;
        }

        private void Update()
        {
            if (!_selfUpdate) return;
            if (!_selfInitialized)
            {
                Initialize();
                _selfInitialized = true;
            }
#if UNITY_EDITOR
            // update params for changing on inspector
            SetFlickerValuesFromFields();
#endif
            var localPosition = transform.localPosition;
            UpdatePosition(localPosition, localPosition);
            UpdateIntensity((_intensity + Flicker.Value) / 100);
            UpdateScale(1f);
            UpdateColor(Color.white);
            UpdateOtherParams();
        }

        private void SetFlickerValuesFromFields()
        {
            FlickerAmount = _flickerAmount;
            FlickerSpeed = _flickerSpeed;
            FlickerTimeOffset = _flickerTimeOffset;
        }

#if UNITY_EDITOR
        public virtual Object[] GetRecordObjects() => new[] { (Object)this };
        private void OnValidate()
        {
            SetFlickerValuesFromFields();
        }
#endif
    }
}
