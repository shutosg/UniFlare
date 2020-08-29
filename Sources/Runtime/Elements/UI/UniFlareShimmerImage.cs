using shutosg.UniFlare.Tools;
using UnityEngine;

namespace shutosg.UniFlare.Elements.UI
{
    public class UniFlareShimmerImage : UniFlareImageElement
    {
        [SerializeField] private int _complexity = 100;
        [Range(0.01f, 4.0f)] [SerializeField] private float _sharpness = 1f;

        public override void UpdateOtherParams()
        {
            Image.SetParam(_complexity, 1);
            var normalized = UniFlareValueNormalizer.NormalizeLog(_sharpness, 0, -7, 2, 0, UniFlareImage.ParamPrecision);
            Image.SetParam(normalized, 2);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateOtherParams();
        }
#endif
    }
}
