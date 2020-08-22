using UnityEngine;

namespace UniFlare
{
    public static class UniFlareVectorExtension
    {
        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, Vector3 factor)
            => new Vector3(
                Mathf.LerpUnclamped(a.x, b.x, factor.x),
                Mathf.LerpUnclamped(a.y, b.y, factor.y),
                Mathf.LerpUnclamped(a.z, b.z, factor.z)
            );

        public static Vector3 Clamp(Vector3 a, Vector3 min, Vector3 max)
            => new Vector3(
                Mathf.Clamp(a.x, min.x, max.x),
                Mathf.Clamp(a.y, min.y, max.y),
                Mathf.Clamp(a.z, min.z, max.z)
            );
    }
}
