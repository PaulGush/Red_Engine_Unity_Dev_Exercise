using UnityEngine;

namespace RedEngine
{
    public class Connection : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        
        public void SetPositions(Transform origin, Transform target)
        {
            transform.position = origin.position;
            m_target = target;
        }
    }
}
