using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public class EnemyAttackHandler : IGameFixedUpdateListener
    {
        private readonly Settings settings;
        private readonly List<AttackCommand> attackCommands = new();

        [Inject]
        public EnemyAttackHandler(Settings settings)
        {
            this.settings = settings;
        }

        public void Attack(Unit unit, Unit target)
        {
            attackCommands.Add(new AttackCommand(unit, target));
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            attackCommands
                .ForEach(attackCommand => Attack(attackCommand, fixedDeltaTime));
        }

        private void Attack(AttackCommand attackCommand, float fixedDeltaTime)
        {
            attackCommand.CurrentTime -= fixedDeltaTime;
            if (!(attackCommand.CurrentTime <= 0))
            {
                return;
            }

            attackCommand.Unit.Attack(attackCommand.Target.transform);
            attackCommand.CurrentTime += settings.attackInterval;
        }

        private class AttackCommand
        {
            public readonly Unit Unit;
            public readonly Unit Target;
            public float CurrentTime;

            public AttackCommand(Unit unit, Unit target)
            {
                Unit = unit;
                Target = target;
            }
        }

        [Serializable]
        public class Settings
        {
            public float attackInterval;
        }
    }
}