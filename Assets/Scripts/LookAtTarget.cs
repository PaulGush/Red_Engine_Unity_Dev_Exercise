using System;
using UnityEngine;

namespace RedEngine
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        
        private void Update()
        {
            if (m_target != null)
            {
                transform.LookAt(m_target);
            }
        }

        public void SetTarget(Transform target)
        {
            m_target = target;
        }
    }
}
