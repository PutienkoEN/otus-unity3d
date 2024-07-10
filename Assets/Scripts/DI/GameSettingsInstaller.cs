using ShootEmUp;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public EnemyTimedSpawner.Settings enemySpawnerSettings;
    public EnemyMoveHandler.Settings enemyMoveSettings;
    public EnemyAttackHandler.Settings enemyAttackSettings;


    public override void InstallBindings()
    {
        Container.BindInstance(enemySpawnerSettings);
        Container.BindInstance(enemyMoveSettings);
        Container.BindInstance(enemyAttackSettings);
    }
}