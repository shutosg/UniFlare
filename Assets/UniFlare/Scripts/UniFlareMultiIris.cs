using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace UniFlare
{
    public class UniFlareMultiIris<T, U> : UniFlareElementBase where T : UniFlareIris<U> where U : UniFlareElementBase
    {
        private const float SpreadUnit = 100f;
        [SerializeField] private float _spreadDistance = DistanceMagnification;
        [SerializeField] private int _randomSeedDistance = 100000;
        [SerializeField] private float _spreadIntensity = 0.75f * SpreadUnit;
        [SerializeField] private int _randomSeedIntensity = 100000;
        [SerializeField] private float _spreadScale = 0.75f * SpreadUnit;
        [SerializeField] private int _randomSeedScale = 100000;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private T[] _irises;

        private bool _initialized;

        public override Object[] GetRecordObjects() =>
            base.GetRecordObjects().Concat(_irises.SelectMany(i => i.GetRecordObjects())).ToArray();

        public override void Initialize()
        {
            base.Initialize();
            Random.InitState(_randomSeedDistance);
            _spreadDistance = Mathf.Max(0, _spreadDistance);
            foreach (var iris in _irises)
            {
                var distance = _distance + Random.Range(0, _spreadDistance);
                iris.InitializeDistance(distance);
            }

            Random.InitState(_randomSeedIntensity);
            _spreadIntensity = Mathf.Clamp(_spreadIntensity, 0f, SpreadUnit);
            foreach (var iris in _irises)
            {
                var intensity = Random.Range(_intensity * (SpreadUnit - _spreadIntensity) / SpreadUnit, _intensity);
                iris.InitializeIntensity(intensity);
            }

            Random.InitState(_randomSeedScale);
            _spreadScale = Mathf.Clamp(_spreadScale, 0f, SpreadUnit);
            foreach (var iris in _irises)
            {
                var scaleFactor = Random.Range((SpreadUnit - _spreadScale) / SpreadUnit, SpreadUnit) / SpreadUnit;
                iris.InitializeScale(_scale * scaleFactor);
            }

            foreach (var iris in _irises)
            {
                iris.InitializeColor(_color);
                iris.InitializeRenderer(_sprite);
                iris.Initialize();
            }
        }

        public override void SetMaterialIfNeeded(Material material)
        {
            base.SetMaterialIfNeeded(material);
            foreach (var iris in _irises)
            {
                iris.SetMaterialIfNeeded(_material);
            }
        }

        public override void ShiftColorHue(float hueOffset)
        {
            base.ShiftColorHue(hueOffset);
            foreach (var iris in _irises)
            {
                iris.ShiftColorHue(hueOffset);
            }
        }

        private void initializeIfNeeded()
        {
            if (_initialized) return;
            Initialize();
            _initialized = true;
        }

        public override void UpdatePosition(Vector3 position, Vector3 center)
        {
            initializeIfNeeded();
            foreach (var iris in _irises)
            {
                iris.UpdatePosition(position, center);
            }
        }

        public override void UpdateIntensity(float intensity)
        {
            initializeIfNeeded();
            var masterIntensity = _intensity / 100;
            foreach (var iris in _irises)
            {
                iris.UpdateIntensity(intensity * masterIntensity);
            }
        }

        public override void UpdateScale(float scale)
        {
            initializeIfNeeded();
            foreach (var iris in _irises)
            {
                iris.UpdateScale(scale);
            }
        }

        public override void UpdateColor(Color color)
        {
            initializeIfNeeded();
            color = CalculateColor(color);
            foreach (var iris in _irises)
            {
                iris.UpdateColor(color);
            }
        }

        private void OnValidate()
        {
            Initialize();
        }
    }
}
