using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RedEngine
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private SceneManager m_sceneManager;
        [SerializeField] private GameObject m_connectionPrefab;

        [SerializeField, ColorUsage(false, true)] private Color m_blueColour;
        [SerializeField, ColorUsage(false, true)] private Color m_pinkColour;
        
        [SerializeField] private int m_blueConnectionCount = 0;
        [SerializeField] private int m_pinkConnectionCount = 0;
        
        private List<Connection> m_connections = new List<Connection>();

        private void OnEnable()
        {
            Puck.OnAnyTeamColourChanged += Puck_OnAnyTeamColourChanged;
            Puck.OnAnyStatusChanged += Puck_OnAnyStatusChanged;
            m_sceneManager.OnPucksInitialized += SceneManager_OnPucksInitialized;
        }
        
        private void OnDisable()
        {
            Puck.OnAnyTeamColourChanged -= Puck_OnAnyTeamColourChanged;
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
            m_sceneManager.OnPucksInitialized -= SceneManager_OnPucksInitialized;
        }

        private void OnDestroy()
        {
            Puck.OnAnyTeamColourChanged -= Puck_OnAnyTeamColourChanged;
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
            m_sceneManager.OnPucksInitialized -= SceneManager_OnPucksInitialized;
        }
        
        private void CheckConnections()
        {
            #region BluePucks

            if (TeamManager.Instance.BluePucks.Count <= 1)
            {
                m_blueConnectionCount = 0;
            }
            else if (TeamManager.Instance.BluePucks.Count == 2)
            {
                m_blueConnectionCount = 1;
            }
            else if (TeamManager.Instance.BluePucks.Count > 2)
            {
                m_blueConnectionCount = TeamManager.Instance.BluePucks.Count;
            }

            #endregion
            #region PinkPucks
            
            if (TeamManager.Instance.PinkPucks.Count <= 1)
            {
                m_pinkConnectionCount = 0;
            }
            else if (TeamManager.Instance.PinkPucks.Count == 2)
            {
                m_pinkConnectionCount = 1;
            }
            else if (TeamManager.Instance.PinkPucks.Count > 2)
            {
                m_pinkConnectionCount = TeamManager.Instance.PinkPucks.Count;
            }
            
            #endregion
        }

        private void SpawnConnections(List<Puck> puckList, int connectionCount, Color targetColor)
        {
            if (connectionCount == 0)
            {
                return;
            }

            for (int i = 0; i < connectionCount; i++)
            {
                if (puckList[i].GetComponentInChildren<Connection>())
                {
                    continue;
                }
                
                var temp = i + 1;
                
                if (i + 1 == connectionCount)
                {
                    temp = 0;
                }
                
                SpawnConnection(puckList[temp].transform, puckList[i].transform, targetColor);
            }
        }

        private void SpawnConnection(Transform target, Transform parent, Color targetColor)
        {
            var clone = Instantiate(m_connectionPrefab, parent);

            var connection = clone.GetComponent<Connection>();
            
            connection.SetPositions(clone.transform, target);
            connection.SetArcColor(targetColor);
            
            m_connections.Add(connection);
        }

        private void Puck_OnAnyTeamColourChanged(Puck puck)
        {
            CheckConnections();
        }

        private void Puck_OnAnyStatusChanged(Puck puck)
        {
            CheckConnections();
        }

        private void SceneManager_OnPucksInitialized()
        {
            SpawnConnections(TeamManager.Instance.BluePucks, m_blueConnectionCount, m_blueColour);
            SpawnConnections(TeamManager.Instance.PinkPucks, m_pinkConnectionCount, m_pinkColour);
        }
    }
}