using UnityEngine;

namespace TestWork.Characters
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public GameObject Prefab;
        public float SpawnRadius;
    }
}