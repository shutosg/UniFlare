using UnityEngine;

namespace UniFlare
{
    public class UniFlareGlowImage : UniFlareImageElement
    {
        [SerializeField] private float _size = 100f;

        private void OnValidate()
        {
            UpdateOtherParams();
        }

        public override void UpdateOtherParams()
        {
            Image.FlareParam1 = _size / 50;
        }
    }
}
