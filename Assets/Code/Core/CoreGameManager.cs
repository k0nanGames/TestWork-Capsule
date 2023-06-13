using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestWork.Core
{
    public class CoreGameManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _systemObjects;

        #region Instances

        private static Dictionary<string, IGameSystem> _instances = new Dictionary<string, IGameSystem>();

        public static void Register(string key, IGameSystem instance)
        {
            if (_instances.ContainsKey(key))
            {
                Debug.LogError("This is already registered!");
            }

            _instances[key] = instance;
        }

        public static T GetInstance<T>() where T : class, IGameSystem
        {
            string key = typeof(T).ToString();
            if (_instances.ContainsKey(key))
                return _instances[key] as T;

            return null;
        }

        public static void UnRegister(string key)
        {
            if (_instances.ContainsKey(key))
            {
                _instances.Remove(key);
            }
        }

        #endregion

        void Start()
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);

            foreach (GameObject system in _systemObjects)
            {
                if (system.TryGetComponent(out IGameSystem gameSystem))
                {
                    Register(gameSystem.GetType().ToString(), gameSystem);
                }
            }

            foreach(KeyValuePair<string, IGameSystem> system in _instances)
            {
                system.Value.Init();
            }
        }

        private void OnDestroy()
        {
            foreach (GameObject system in _systemObjects)
            {
                if (system.TryGetComponent(out IGameSystem gameSystem))
                {
                    UnRegister(gameSystem.GetType().ToString());
                }
            }
        }
    }
}
