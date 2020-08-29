using UnityEngine;

namespace shutosg.UniFlare.Elements.UI
{
    public class UniFlareGlowImage : UniFlareImageElement
    {
        [SerializeField] private int _size = 100;

        public override void UpdateOtherParams()
        {
            Image.SetParam(_size, 1);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateOtherParams();
        }
#endif
    }
}
