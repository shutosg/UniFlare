using UnityEngine;

namespace UniFlare
{
    public abstract class UniFlareElementBase : MonoBehaviour, IUniFlareElement
    {
        protected const float DistanceMagnification = 50f;
        protected const float IntensityMagnification = 100f;
        [SerializeField] protected float _distance = DistanceMagnification * 2;
        [SerializeField] protected Vector3 _positionOffset = Vector3.zero;
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

        public void InitializeColor(Color color)
        {
            if (!_useGlobalColor) return;
            _color = color;
        }

        public virtual void Initialize()
        {
            SetMaterialIfNeeded(_material);
        }

        public virtual void UpdatePosition(Vector3 position, Vector3 center)
        {
            transform.localPosition = Vector3.LerpUnclamped(position, center, _distance / DistanceMagnification) + _positionOffset;
        }

        public abstract void UpdateIntensity(float intensity);

        public virtual void UpdateScale(float scale)
        {
            transform.localScale = _scale * scale;
        }

        public virtual void ShiftColorHue(float hueOffset) => _color = _color.OffsetHue(hueOffset);

        public abstract void UpdateColor(Color color);

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
