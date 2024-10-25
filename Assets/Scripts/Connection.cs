using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace RedEngine
{
    public class Connection : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private VisualEffect m_electricArc;
        [SerializeField] private Transform m_target;
        [SerializeField] private Transform[] m_positions;
        public void SetPositions(Transform origin, Transform target)
        {
            transform.position = origin.position;
            m_target = target;
            m_positions[3].position = target.position;
            m_positions[3].SetParent(target, true);
        }

        public void SetArcColor(Color newColor)
        {
            m_electricArc.SetVector4("BaseColor", newColor);
        }

        private void Update()
        {
            transform.LookAt(m_target);
        }
    }
}
