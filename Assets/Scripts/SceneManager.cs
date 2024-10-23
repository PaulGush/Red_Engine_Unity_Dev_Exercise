using System;
using UnityEngine;

namespace RedEngine
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private PuckData[] puckData = Array.Empty<PuckData>();
        [SerializeField] private Puck puckPrefab;
        [SerializeField] private Transform puckParent;
        
        void Start()
        {
            foreach (var puckData in puckData)
            {
	            var puck = Instantiate(puckPrefab, puckData.position, Quaternion.identity, puckParent);
				puck.SetTeamColour(puckData.puckColour);
			}
        }
    }
}
