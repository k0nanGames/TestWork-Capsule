using System.Collections;
using System.Collections.Generic;
using TestWork.Core;
using UnityEngine;
using TestWork.Input;

namespace TestWork.Characters
{
    public class EnemyController : MonoBehaviour, IGameSystem
    {
        [SerializeField] private List<EnemyConfig> _enemies;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _maxEnemies;

        private List<IGameEnemy> _spawnedEnemies;

        private Coroutine _spawnRoutine;
        private PlayerController _playerController;

        private bool _timerStarted;
        private float _timeAlive;

        public float GetTimeAlive() => _timeAlive;

        public void Init()
        {
            _spawnedEnemies = new List<IGameEnemy>();
            _playerController = CoreGameManager.GetInstance<PlayerController>();
        }

        private void Update()
        {
            if(_timerStarted)
                _timeAlive += Time.deltaTime;
        }

        public void StartSpawning()
        {
            if(_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }

            _spawnRoutine = StartCoroutine(SpawnRoutine());

            _timeAlive = 0;
            _timerStarted = true;
        }

        public void RemoveEnemy(IGameEnemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
        }

        public void ResetEnemies()
        {
            _timerStarted = false;
            
            DataManager data = CoreGameManager.GetInstance<DataManager>();
            
            if (_timeAlive > data.GetBestTime())
                data.SetBestTime(_timeAlive);

            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }

            IGameEnemy[] gameEnemies = _spawnedEnemies.ToArray();
            foreach (IGameEnemy gameEnemy in gameEnemies)
            {
                gameEnemy.Die();
            }

            _spawnedEnemies.Clear();
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(_spawnDelay);

            while (true)
            {
                if (_spawnedEnemies.Count < _maxEnemies)
                {
                    Vector3 playerPos = _playerController.GetPlayerPosition();

                    EnemyConfig enemy = _enemies[Random.Range(0, _enemies.Count)];

                    Vector3 spawnPos = playerPos;
                    while(Vector3.Distance(spawnPos, playerPos) < enemy.SpawnRadius / 2f)
                    {
                        spawnPos = playerPos + enemy.SpawnRadius * Random.insideUnitSphere;
                        spawnPos.y = playerPos.y;
                    }

                    GameObject spawned = Instantiate(enemy.Prefab, transform);
                    spawned.transform.position = spawnPos;

                    if(spawned.TryGetComponent(out IGameEnemy gameEnemy))
                    {
                        gameEnemy.Init();
                        _spawnedEnemies.Add(gameEnemy);
                    }
                    else
                    {
                        Destroy(spawned);
                    }

                }

                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}