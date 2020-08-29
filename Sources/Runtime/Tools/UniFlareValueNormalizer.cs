using UnityEngine;

namespace shutosg.UniFlare.Tools
{
    public static class UniFlareValueNormalizer
    {
        /// <summary>
        /// take log2 for value and then normalize minNorm ~ maxNorm
        /// </summary>
        /// <param name="value">original value</param>
        /// <param name="epsilon">small num to be added to value to avoid -Infinity when value is 0</param>
        /// <param name="minLog"></param>
        /// <param name="maxLog"></param>
        /// <param name="minNorm"></param>
        /// <param name="maxNorm"></param>
        /// <returns></returns>
        public static int NormalizeLog(float value, float epsilon, float minLog, float maxLog, int minNorm, int maxNorm)
        {
            var log = Mathf.Log(value + epsilon, 2);
            return (int)Mathf.Clamp((log - minLog) / (maxLog - minLog) * (maxNorm - minNorm), minNorm, maxNorm);
        }
    }
}
