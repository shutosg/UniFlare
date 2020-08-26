using UnityEngine;

namespace UniFlare
{
    public class UniFlareGlowImage : UniFlareImageElement
    {
        [SerializeField] private float _size = 100f;

        public override void UpdateOtherParams()
        {
            Image.FlareParam1 = _size / 50;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateOtherParams();
        }
#endif
    }
}
