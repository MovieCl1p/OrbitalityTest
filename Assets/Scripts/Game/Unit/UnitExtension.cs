using UnityEngine;

namespace Game.Unit
{
    public static class UnitExtension
    {
        public static Vector2 EvaluateEllipse(float t, float xAxis, float yAxis)
        {
            float angle = Mathf.Deg2Rad * 360 * t;
            float x = Mathf.Sin(angle) * xAxis;
            float y = Mathf.Cos(angle) * yAxis;
            return new Vector2(x, y);
        }
    }
}