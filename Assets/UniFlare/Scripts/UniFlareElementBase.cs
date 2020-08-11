using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
{
    public abstract class UniFlareElementBase : MonoBehaviour, IUniFlareElement
    {
        protected const float DistanceMagnification = 50f;
        protected const float IntensityMagnification = 100f;
        [SerializeField] protected float _distance = DistanceMagnification * 2;
        [SerializeField] protected float _intensity = IntensityMagnification;
        [SerializeField] protected Vector3 _scale = Vector3.one;
        [SerializeField] protected Color _color = Color.white;

        public void InitializeDistance(float distance) => _distance = distance;
        public void InitializeIntensity(float intensity) => _intensity = intensity;
        public void InitializeScale(Vector3 scale) => _scale = scale;
        public void InitializeColor(Color color) => _color = color;

        public virtual void UpdatePosition(Vector3 position, Vector3 center)
        {
            transform.localPosition = Vector3.LerpUnclamped(position, center, _distance / DistanceMagnification);
        }

        public abstract void UpdateIntensity(float intensity);

        public virtual void UpdateScale(Vector3 scale)
        {
            scale.Scale(_scale);
            transform.localScale = scale;
        }

        public abstract void UpdateColor(Color color);
    }
}
