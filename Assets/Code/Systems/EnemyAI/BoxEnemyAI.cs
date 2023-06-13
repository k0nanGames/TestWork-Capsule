using TestWork.Core;
using UnityEngine;

namespace TestWork.Characters
{
    public class BoxEnemyAI : MonoBehaviour, IGameEnemy
    {
        private PlayerController _player;

        [SerializeField] private Rigidbody _body;
        [SerializeField] private float _speed;

        public void Die()
        {
            CoreGameManager.GetInstance<EnemyController>().RemoveEnemy(this);
            Destroy(gameObject);
        }

        public void Init()
        {
            _player = CoreGameManager.GetInstance<PlayerController>();
        }

        private void FixedUpdate()
        {
            if (_player == null)
                Init();

            Vector3 direction = (_player.GetPlayerPosition() - transform.position).normalized;
            direction.y = 0;

            _body.AddForce(direction * _speed, ForceMode.VelocityChange);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag == "Player")
            {
                _player.Die();
            }
        }
    }
}