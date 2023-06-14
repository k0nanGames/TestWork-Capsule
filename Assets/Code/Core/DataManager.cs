using System.Collections;
using System.Collections.Generic;
using TestWork.Core;
using UnityEngine;
using TestWork.Input;

namespace TestWork.Core
{
    public class DataManager : MonoBehaviour, IGameSystem
    {
        private const string SAVED_DATA_PATH = "com.testwork.capsule.saveddata";

        private SavedData _savedData;

        public float GetBestTime() => _savedData.BestTime;

        public void SetBestTime(float time)
        {
            _savedData.BestTime = time;
            Save();
        }

        public void Init()
        {
            string json = PlayerPrefs.GetString(SAVED_DATA_PATH);

            if (string.IsNullOrEmpty(json))
                _savedData = new SavedData();
            else
                _savedData = JsonUtility.FromJson<SavedData>(json);

        }

        private void Save()
        {
            if (_savedData == null)
                return;

            string json = JsonUtility.ToJson(_savedData);
            PlayerPrefs.SetString(SAVED_DATA_PATH, json);
        }

        private void Update()
        {

        }

        
    }
}