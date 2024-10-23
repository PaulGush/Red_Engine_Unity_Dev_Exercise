using UnityEngine;

namespace RedEngine
{
    [CreateAssetMenu(fileName = "PuckData", menuName = "Red Engine/PuckData", order = 0)]
    public class PuckData : ScriptableObject
    {
        public Vector3 position;
        public TeamColour puckColour;
    }
}