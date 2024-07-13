using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class TeamComponent
    {
        [SerializeField] private Team team;

        [Inject]
        public TeamComponent(Team team)
        {
            this.team = team;
        }

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