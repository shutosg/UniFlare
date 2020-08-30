using UnityEngine;

namespace shutosg.UniFlare.Elements.UI
{
    public class UniFlareGlowImage : UniFlareImageElement
    {
        private const int SizeOffset = 5000;
        [Header("Glow Param")]
        [Range(-100, 1000)] [SerializeField] private int _size = 100;

        public override void UpdateOtherParams()
        {
            Image.SetParam(Mathf.Clamp(_size + SizeOffset, 0, UniFlareImage.ParamPrecision), 1);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateOtherParams();
        }
#endif
    }
}
