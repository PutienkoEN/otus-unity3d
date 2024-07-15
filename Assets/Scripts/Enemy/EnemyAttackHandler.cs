using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyAttackHandler :
        IFixedTickable,
        IGamePauseListener,
        IGameFinishListener
    {
        private readonly Settings settings;
        private readonly List<AttackCommand> attackCommands = new();

        // Dynamic
        private bool attackEnabled = true;

        [Inject]
        public EnemyAttackHandler(
            Settings settings,
            IGameStateObserver<IGamePauseListener> pauseObserver,
            IGameStateObserver<IGameFinishListener> finishObserver)
        {
            this.settings = settings;

            pauseObserver.Observe(this);
            finishObserver.Observe(this);
        }

        public void Attack(Unit unit, Unit target)
        {
            attackCommands.Add(new AttackCommand(unit, target));
        }

        public void FixedTick()
        {
            if (!attackEnabled)
            {
                return;
            }

            var fixedDeltaTime = Time.fixedDeltaTime;

            attackCommands
                .ForEach(attackCommand => Attack(attackCommand, fixedDeltaTime));
        }

        public void OnGamePause()
        {
            attackEnabled = false;
        }

        public void OnGameResume()
        {
            attackEnabled = true;
        }

        public void OnGameFinish()
        {
            OnGamePause();
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