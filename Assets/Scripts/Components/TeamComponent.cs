using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class TeamComponent
    {
        [SerializeField] private Team team;

        public bool SameTeam(Team otherTeam)
        {
            return team == otherTeam;
        }
    }

    public enum Team
    {
        Player,
        Enemy
    }
}