using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class TeamComponent
    {
        [SerializeField] private Team team;

        public Team Team => team;
    }

    public enum Team
    {
        Player,
        Enemy
    }
}