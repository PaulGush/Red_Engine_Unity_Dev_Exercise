using System;
using UnityEngine;

namespace RedEngine
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private PuckData[] puckData = Array.Empty<PuckData>();
        [SerializeField] private Puck puckPrefab;
        [SerializeField] private Transform puckParent;
        
        public event Action OnPucksInitialized;
        
        void Start()
        {
            InitialisePucks();
        }

        private void InitialisePucks()
        {
            foreach (var puckData in puckData)
            {
                SpawnPuck(puckData);
            }
            
            OnPucksInitialized?.Invoke();
        }

        private void SpawnPuck(PuckData puckData)
        {
            var puck = Instantiate(puckPrefab, puckData.position, Quaternion.identity, puckParent);
            puck.SetTeamColour(puckData.puckColour);
        }
    }
}
