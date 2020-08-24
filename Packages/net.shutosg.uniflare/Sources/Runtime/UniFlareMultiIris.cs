using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace UniFlare
{
    public class UniFlareMultiIris<T, U> : UniFlareElementBase where T : UniFlareIris<U> where U : UniFlareElementBase
    {
        private const float SpreadUnit = 100f;
        [SerializeField] private float _spreadDistance = 0.75f * SpreadUnit;
        [SerializeField] private int _randomSeedDistance = 100000;
        [SerializeField] private float _spreadIntensity = 0.75f * SpreadUnit;
        [SerializeField] private int _randomSeedIntensity = 100000;
        [SerializeField] private float _spreadScale = 0.75f * SpreadUnit;
        [SerializeField] private int _randomSeedScale = 100000;
        [SerializeField] private Vector3 _spreadPositionOffset = Vector3.zero * SpreadUnit;
        [SerializeField] private int _randomSeedPositionOffset = 100000;
        [SerializeField] private Sprite _sprite = default;
        [SerializeField] private T[] _irises = default;

        private bool _initialized;

        public override Object[] GetRecordObjects() =>
            base.GetRecordObjects().Concat(_irises.SelectMany(i => i.GetRecordObjects())).ToArray();

        // TODO: 初期値をいい感じにセットするメソッドをclassごとに用意
        // public void Reset()
        // {
        //     _distance = 0;
        //     _spreadDistance = 0.75f * SpreadUnit;
        // }

        /// <summary>
        /// set iris's parameters with good randomness
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // distance
            // if _distance is 0 then, place irises at the same position of 'center'
            Random.InitState(_randomSeedDistance);
            _spreadDistance = Mathf.Max(0, _spreadDistance); // TODO: 0以上になることをEditor拡張で担保する
            foreach (var iris in _irises)
            {
                var distance = Percentage / 2 + _distance + Random.Range(-_spreadDistance, _spreadDistance) / 2;
                iris.InitializeDistance(distance);
            }

            // intensity
            Random.InitState(_randomSeedIntensity);
            _spreadIntensity = Mathf.Clamp(_spreadIntensity, 0f, SpreadUnit);
            foreach (var iris in _irises)
            {
                var intensity = Random.Range(_intensity * (SpreadUnit - _spreadIntensity) / SpreadUnit, _intensity);
                iris.InitializeIntensity(intensity);
            }

            // scale
            Random.InitState(_randomSeedScale);
            _spreadScale = Mathf.Clamp(_spreadScale, 0f, SpreadUnit);
            foreach (var iris in _irises)
            {
                var scaleFactor = 1 + Random.Range(-_spreadScale, _spreadScale) / SpreadUnit;
                iris.InitializeScale(_scale * scaleFactor);
            }

            // position offset
            Random.InitState(_randomSeedPositionOffset);
            _spreadPositionOffset = UniFlareVectorExtension.Clamp(_spreadPositionOffset, Vector3.zero, Vector3.one * SpreadUnit);
            foreach (var iris in _irises)
            {
                var x = _positionOffset.x + getRandomValue(_spreadPositionOffset.x);
                var y = _positionOffset.y + getRandomValue(_spreadPositionOffset.y);
                var z = _positionOffset.z + getRandomValue(_spreadPositionOffset.z);
                iris.InitializePositionOffset(new Vector3(x, y, z));
                float getRandomValue(float value) => Random.Range(-value, value);
            }

            // others
            foreach (var iris in _irises)
            {
                iris.InitializeColor(_color);
                iris.InitializeRenderer(_sprite);
                iris.InitializeTransition(_transition);
                iris.Initialize();
            }

            // reset random seed
            Random.InitState(System.Environment.TickCount);
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

        public override void UpdateOtherParams()
        {
            foreach (var iris in _irises)
            {
                iris.UpdateOtherParams();
            }
        }

        private void OnValidate()
        {
            Initialize();
        }
    }
}
