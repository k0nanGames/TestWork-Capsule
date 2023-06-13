using TestWork.Core;
using UnityEngine;

namespace TestWork.Characters
{
    public class SphereEnemyAI : MonoBehaviour, IGameEnemy
    {
        public void Die()
        {
            CoreGameManager.GetInstance<EnemyController>().RemoveEnemy(this);
            Destroy(gameObject);
        }

        public void Init()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Player")
            {
                CoreGameManager.GetInstance<PlayerController>().Die();
            }
        }
    }
}