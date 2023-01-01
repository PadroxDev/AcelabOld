using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Acelab {
    public static class Utilities
    {
        private static Camera _camera;
        private static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary =
            new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;
            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        private static readonly Dictionary<string, CultureInfo> CultureDictionary =
            new Dictionary<string, CultureInfo>();
        public static CultureInfo GetCulture(string cultureName)
        {
            if (CultureDictionary.TryGetValue(cultureName, out var culture)) return culture;
            CultureDictionary[cultureName] = new CultureInfo(cultureName);
            return CultureDictionary[cultureName];
        }

        public static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;
        public static bool IsOverUI()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
            return _results.Count > 0;
        }

        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
            return result;
        }

        public static void DeleteChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }

        public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
        {
            float t = Mathf.InverseLerp(iMin, iMax, value);
            return Mathf.Lerp(oMin, oMax, t);
        }

        public static Color Remap(float iMin, float iMax, Color c1, Color c2, float value)
        {
            float t = Mathf.InverseLerp(iMin, iMax, Mathf.Clamp(value, iMin, iMax));
            return Color.Lerp(c1, c2, t);
        }

        public static bool IsEmpty<T>(this IList<T> source)
            => source.Count == 0;

        public static IEnumerable<T> GetValues<T>() =>
            System.Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static class EnumUtilities
    {
        public static IEnumerable<T> GetValues<T>() =>
            System.Enum.GetValues(typeof(T)).Cast<T>();
    }
}