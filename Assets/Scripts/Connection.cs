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
            m_positions[3].Translate(0,0,1);
        }

        public void SetArcColor(Color newColor)
        {
            m_electricArc.SetVector4("BaseColor", newColor);
        }

        private void Update()
        {
            transform.LookAt(m_target);
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            m_positions[1].transform.position = GetWorldPositionFromDistance(m_positions[0], m_positions[3], 0.33f);
            m_positions[2].transform.position = GetWorldPositionFromDistance(m_positions[0], m_positions[3], 0.66f);
        }

        private Vector3 GetWorldPositionFromDistance(Transform origin, Transform target, float percentage)
        {
            return Vector3.Lerp(origin.position, target.position, percentage);
        }
    }
}
