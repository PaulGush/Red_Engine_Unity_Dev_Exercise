using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedEngine
{
    public class TeamManager : MonoBehaviour
    {
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
            Puck.OnAnyTeamColourChanged += Puck_OnAnyTeamColourChanged;
            Puck.OnAnyStatusChanged += Puck_OnAnyStatusChanged;
        }
        
        private void OnDisable()
        {
            Puck.OnAnyTeamColourChanged -= Puck_OnAnyTeamColourChanged;
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
        }

        private void OnDestroy()
        {
            Puck.OnAnyTeamColourChanged -= Puck_OnAnyTeamColourChanged;
            Puck.OnAnyStatusChanged -= Puck_OnAnyStatusChanged;
        }

        private void Puck_OnAnyTeamColourChanged(Puck puck)
        {
            if (puck.TeamColour == TeamColour.Blue)
            {
                if (PinkPucks.Contains(puck))
                {
                    PinkPucks.Remove(puck);
                }

                BluePucks.Add(puck);
            }
            else
            {
                if (BluePucks.Contains(puck))
                {
                    BluePucks.Remove(puck);
                }
                
                PinkPucks.Add(puck);
            }
        }

        private void Puck_OnAnyStatusChanged(Puck puck)
        {
            if (puck.TeamColour == TeamColour.Blue)
            {
                BluePucks.Remove(puck);
            }
            else
            {
                PinkPucks.Remove(puck);
            }
        }
    }
}
