using UnityEngine;

namespace UniFlare
{
    public abstract class UniFlareElementBase : MonoBehaviour, IUniFlareElement
    {
        protected const float Percentage = 100f;
        protected const float IntensityMagnification = 100f;
        [SerializeField] protected float _distance = Percentage;
        [SerializeField] protected Vector3 _positionOffset = Vector3.zero;
        [SerializeField] protected Vector3 _transition = Vector2.one * Percentage;
        [SerializeField] protected float _intensity = IntensityMagnification;
        [SerializeField] protected Vector3 _scale = Vector3.one;
        [SerializeField] protected bool _useGlobalColor = true;
        [SerializeField] protected Color _color = Color.white;
        [SerializeField] protected Material _material;

        public virtual Object[] GetRecordObjects() => new[] { (Object)this };

        public virtual void SetMaterialIfNeeded(Material material)
        {
            if (_material != material) _material = material;
        }

        public void InitializeDistance(float distance) => _distance = distance;
        public void InitializeIntensity(float intensity) => _intensity = intensity;
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
    }
}
