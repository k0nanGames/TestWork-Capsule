using UnityEngine;

namespace TestWork.Characters
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Prefabs")]
        public GameObject PlayerPrefab;
        public GameObject CameraPrefab;

        [Header("Parameters")]
        public float PlayerSpeed;
        public float CameraSpeed;
        public float CameraMinY;
        public float CameraMaxY;
        public Vector3 CameraOffset;
        public Vector3 CameraAngle;
        public Vector3 SpawnPosition;

        [Header("Input")]
        public bool InverseMovement;
        public bool InverseRotationX;
        public bool InverseRotationY;
    }
}