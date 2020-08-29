using UnityEngine;

namespace shutosg.UniFlare.Elements.UI
{
    public class UniFlareShimmerImage : UniFlareImageElement
    {
        [SerializeField] private float _complexity = 100f;

        public override void UpdateOtherParams()
        {
            Image.FlareParam1 = _complexity / 50;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateOtherParams();
        }
#endif
    }
}
