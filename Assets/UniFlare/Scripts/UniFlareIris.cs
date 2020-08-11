using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
{
    [RequireComponent(typeof(UniFlareElementBase))]
    public abstract class UniFlareIris<T> : MonoBehaviour, IUniFlareElement where T : UniFlareElementBase
    {
        protected T _element => _elementCache != null ? _elementCache : _elementCache = GetComponent<T>();
        private T _elementCache;
        public void InitializeDistance(float distance) => _element.InitializeDistance(distance);
        public void InitializeIntensity(float intensity) => _element.InitializeIntensity(intensity);
        public void InitializeScale(Vector3 scale) => _element.InitializeScale(scale);
        public void InitializeColor(Color color) => _element.InitializeColor(color);

        public abstract void InitializeRenderer(Sprite sprite);
        public void UpdatePosition(Vector3 position, Vector3 center) => _element.UpdatePosition(position, center);

        public void UpdateIntensity(float intensity) => _element.UpdateIntensity(intensity);

        public void UpdateScale(Vector3 scale) => _element.UpdateScale(scale);

        public void UpdateColor(Color color) => _element.UpdateColor(color);
    }
}
