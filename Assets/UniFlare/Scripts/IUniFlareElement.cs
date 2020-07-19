using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
{
    public interface IUniFlareElement
    {
        void UpdatePosition(Vector3 position, Vector3 center);
        void UpdateIntensity(float intensity);
        void UpdateScale(Vector3 scale);
        void UpdateColor(Color color);
    }
}
