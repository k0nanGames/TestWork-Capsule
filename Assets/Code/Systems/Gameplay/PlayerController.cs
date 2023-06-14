using System.Collections;
using System.Collections.Generic;
using TestWork.Core;
using UnityEngine;
using TestWork.Input;
using TestWork.SceneManagement;
using TestWork.UI;

namespace TestWork.Characters
{
    public class PlayerController : MonoBehaviour, IGameSystem
    {
        private const string PLAYER_CONFIG_PATH = "Player/PlayerConfig";

        [SerializeField] private PlayerConfig _config;

        private GameObject _spawnedPlayer;
        private GameObject _spawnedCamera;
        private Rigidbody _playerBody;

        private InputController _inputController;

        private bool _loadConfigFromFile;

        public void Init()
        {
            _inputController = CoreGameManager.GetInstance<InputController>();

            if (_loadConfigFromFile)
                _config = Resources.Load<PlayerConfig>(PLAYER_CONFIG_PATH);
        }

        public void SpawnPlayer()
        {
            if (_spawnedPlayer == null)
            {
                _spawnedPlayer = Instantiate(_config.PlayerPrefab, transform);
                _spawnedCamera = Instantiate(_config.CameraPrefab, _spawnedPlayer.transform);

                _spawnedPlayer.transform.position = _config.SpawnPosition;
                _spawnedCamera.transform.position = _spawnedPlayer.transform.position + _config.CameraOffset;
                _spawnedCamera.transform.eulerAngles = _config.CameraAngle;

                _playerBody = _spawnedPlayer.GetComponent<Rigidbody>();
            }
        }

        public void Die()
        {
            Destroy(_spawnedCamera);
            Destroy(_spawnedPlayer);

            _spawnedCamera = null;
            _spawnedPlayer = null;

            CoreGameManager.GetInstance<EnemyController>().ResetEnemies();
            CoreGameManager.GetInstance<InputController>().ClearTouches();
            CoreGameManager.GetInstance<UIManager>().Show(UIViewType.PlayerDead);
        }

        public Vector3 GetPlayerPosition()
        {
            return _spawnedPlayer.transform.position;
        }

        private void FixedUpdate()
        {
            if (_spawnedPlayer == null)
                return;

            _spawnedCamera.transform.LookAt(_spawnedPlayer.transform);

            Vector2 moving = _inputController.GetAxis(InputStickType.Walk) * (_config.InverseMovement ? 1 : -1);
            Vector2 rotation = _inputController.GetAxis(InputStickType.Rotation) * -1;

            Vector3 lookTarget = (_spawnedPlayer.transform.position - _spawnedCamera.transform.position);
            lookTarget.y = 0;

            lookTarget = lookTarget.normalized;

            if (_config.InverseRotationX)
                rotation.x *= -1;

            if (_config.InverseRotationY)
                rotation.y *= -1;

            Vector3 moveTarget = (lookTarget * moving.y * _config.PlayerSpeed) //Move forward/backward
                + (Vector3.Cross(lookTarget, Vector3.up) * moving.x * _config.PlayerSpeed * -1); //Move left/right

            _playerBody.AddForce(moveTarget, ForceMode.VelocityChange);

            if(_spawnedCamera.transform.eulerAngles.x < _config.CameraMinY && rotation.y < 0)
                rotation.y = 0;
            
            if(_spawnedCamera.transform.eulerAngles.x > _config.CameraMaxY && rotation.y > 0)
                rotation.y = 0;

            _spawnedCamera.transform.Translate(rotation * _config.CameraSpeed);

            if(moving != Vector2.zero)
            {
                _spawnedCamera.transform.parent = null;

                if(!_config.RotatePlayerToTarget)
                    _spawnedPlayer.transform.LookAt(_spawnedPlayer.transform.position + lookTarget * 100);
                else
                    _spawnedPlayer.transform.LookAt(_spawnedPlayer.transform.position + moveTarget * 100);

                _spawnedCamera.transform.parent = _spawnedPlayer.transform;
            }
        }
    }
}