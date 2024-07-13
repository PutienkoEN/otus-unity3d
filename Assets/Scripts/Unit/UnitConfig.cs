using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "UnitConfig",
        menuName = "Units/New Unit Config"
    )]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField] public int initialHealth;
        [SerializeField] public float speed;
        [SerializeField] public Team team;
    }
}