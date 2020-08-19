using UnityEngine;

namespace UniFlare
{
    public class UniFlareGlowImage : UniFlareImageElement
    {
        [SerializeField] private float _size = 100f;

        private void OnValidate()
        {
            Image.Size = _size / 50;
        }
    }
}
