using System.Collections.Generic;
using UnityEngine;

namespace RedEngine
{
    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance;
        
        [SerializeField] private SceneManager m_sceneManager;
        [SerializeField] private GameObject m_connectionPrefab;

        [SerializeField, ColorUsage(false, true)] private Color m_blueColour;
        [SerializeField, ColorUsage(false, true)] private Color m_pinkColour;
        
        [SerializeField] private int m_blueConnectionCount = 0;
        [SerializeField] private int m_pinkConnectionCount = 0;

        private readonly Dictionary<Connection, float> m_blueConnectionDistances = new();
        private readonly Dictionary<Connection, float> m_pinkConnectionDistances = new();

        private void Awake()
        {
            #region Singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion
        }

        private void OnEnable()
        {
            Puck.OnAnyStatusChanged += Puck_OnAnyStatusChanged;
            m_sceneManager.OnPucksInitialized += SceneManager_OnPucksInitialized;
        }

        private void Update()
        {
            UpdateConnectionArcIntensity(m_blueConnectionDistances);
            UpdateConnectionArcIntensity(m_pinkConnectionDistances);
        }

        private void OnDisable()
        {
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
            m_sceneManager.OnPucksInitialized -= SceneManager_OnPucksInitialized;
        }

        private void OnDestroy()
        {
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
            m_sceneManager.OnPucksInitialized -= SceneManager_OnPucksInitialized;
        }
        
        private int CheckConnectionCount(List<Puck> puckList)
        {
            if (puckList.Count == 2)
            {
                return 1;
            }
            else if (puckList.Count > 2)
            {
                return puckList.Count;
            }
            
            return 0;
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
                
                SpawnConnection(puckList[temp].InboundArcTargetSocket, 
                    puckList[i].OutboundArcTargetSocket, 
                    puckList[temp].InboundArcTargetSocket.transform, 
                    puckList[i].OutboundArcTargetSocket.transform, 
                    targetColor,
                    puckList[i].TeamColour);
            }
        }

        private void SpawnConnection(LookAtTarget inbound, LookAtTarget outbound, Transform target, Transform parent, Color targetColor, TeamColour teamColour)
        {
            inbound.SetTarget(parent);
            outbound.SetTarget(target);
            
            var clone = Instantiate(m_connectionPrefab, parent);
            
            clone.transform.Translate(0,0,1);
            
            var connection = clone.GetComponent<Connection>();
            
            connection.SetPositions(clone.transform, target);
            connection.SetArcColor(targetColor);
            connection.SetTeamColour(teamColour);
            
            if (teamColour == TeamColour.Blue)
            {
                m_blueConnectionDistances.Add(connection, Vector3.Distance(target.position, parent.position));
            }
            else if (teamColour == TeamColour.Pink)
            {
                m_pinkConnectionDistances.Add(connection, Vector3.Distance(target.position, parent.position));
            }
        }

        public void UpdateConnectionDistance(Connection connection, float distance)
        {
            if (connection.GetTeamColour() == TeamColour.Blue)
            {
                if (m_blueConnectionDistances.ContainsKey(connection))
                {
                    m_blueConnectionDistances[connection] = distance;
                }
                else
                {
                    Debug.LogError("Key Value Pair not initialized.");
                }
            }
            else
            {
                if (m_pinkConnectionDistances.ContainsKey(connection))
                {
                    m_pinkConnectionDistances[connection] = distance;
                }
                else
                {
                    Debug.LogError("Key Value Pair not initialized.");
                }
            }
        }

        private void UpdateConnectionArcIntensity(Dictionary<Connection, float> connectionDistances)
        {
            float biggestDistance = 0;
            Connection connectionToUpdate = null;
            
            foreach (var connection in connectionDistances)
            {
                connection.Key.SetArcColourIntensity(0f);
                
                if (biggestDistance < connection.Value)
                {
                    biggestDistance = connection.Value;
                    connectionToUpdate = connection.Key;
                }
            }
            
            connectionToUpdate?.SetArcColourIntensity(10f);
        }

        private void Puck_OnAnyStatusChanged(Puck puck)
        {
            m_blueConnectionCount = CheckConnectionCount(TeamManager.Instance.BluePucks);
            m_pinkConnectionCount = CheckConnectionCount(TeamManager.Instance.PinkPucks);
        }

        private void SceneManager_OnPucksInitialized()
        {
            SpawnConnections(TeamManager.Instance.BluePucks, m_blueConnectionCount, m_blueColour);
            SpawnConnections(TeamManager.Instance.PinkPucks, m_pinkConnectionCount, m_pinkColour);
        }
    }
}