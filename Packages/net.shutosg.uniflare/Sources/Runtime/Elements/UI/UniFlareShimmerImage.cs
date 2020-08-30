using shutosg.UniFlare.Tools;
using UnityEngine;

namespace shutosg.UniFlare.Elements.UI
{
    public class UniFlareShimmerImage : UniFlareImageElement
    {
        private const int AnimationSpeedMultiplier = 2000;
        [Header("Shimmer Param")]
        [Range(0, 500)] [SerializeField] private int _complexity = 100;
        [Range(0.01f, 4.0f)] [SerializeField] private float _sharpness = 1f;
        [Range(0, 3f)] [SerializeField] private float _animationSpeed = 0.25f;

        public override void UpdateOtherParams()
        {
            Image.SetParam(_complexity, 1);
            var normalized = UniFlareValueNormalizer.NormalizeLog(_sharpness, 0, -7, 3, 0, UniFlareImage.ParamPrecision);
            Image.SetParam(normalized, 2);
            Image.SetParam((int)(_animationSpeed * AnimationSpeedMultiplier + 0.5f), 3);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateOtherParams();
        }
#endif
    }
}
