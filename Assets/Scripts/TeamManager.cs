using System.Collections.Generic;
using UnityEngine;

namespace RedEngine
{
    public class TeamManager : MonoBehaviour
    {
        /// <summary>
        /// Manages the teams that pucks are on
        /// </summary>
        public static TeamManager Instance;
        
        public List<Puck> BluePucks = new List<Puck>();
        public List<Puck> PinkPucks = new List<Puck>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            Puck.OnAnyStatusChanged += Puck_OnAnyStatusChanged;
        }
        
        private void OnDisable()
        {
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
        }

        private void OnDestroy()
        {
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
        }

        private void Puck_OnAnyStatusChanged(Puck puck)
        {
            if (puck.enabled)
            {
                if (puck.TeamColour == TeamColour.Blue)
                {
                    if (!BluePucks.Contains(puck))
                    {
                        BluePucks.Add(puck);
                    }
                }
                else
                {
                    if (!PinkPucks.Contains(puck))
                    {
                        PinkPucks.Add(puck);
                    }
                }
            }
            else
            {
                if (puck.TeamColour == TeamColour.Blue)
                {
                    if (BluePucks.Contains(puck))
                    {
                        BluePucks.Remove(puck);
                    }
                }
                else
                {
                    if (PinkPucks.Contains(puck))
                    {
                        PinkPucks.Remove(puck); 
                    }
                }
            }
        }
    }
}
