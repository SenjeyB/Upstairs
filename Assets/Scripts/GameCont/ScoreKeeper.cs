using System;
using System.Collections.Generic;
using System.IO;
using MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCont
{
    public class ScoreKeeper : MonoBehaviour
    {
        private Dictionary<string, int> _scores = new Dictionary<string, int>();
        private Dictionary<string, bool> _settings = new Dictionary<string, bool>();
        private int _difficulty;
        private int _lastScore;
        private string _filePath;
        private string _filePathSettings;
        private string _lastSceneName;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if(GameObject.FindWithTag("ScoreKeeper") != gameObject)
            {
                Destroy(gameObject);
            }
            _filePath = Path.Combine(Application.persistentDataPath, "scores.dat");
            _filePathSettings = Path.Combine(Application.persistentDataPath, "settings.dat");
            LoadScores();
            LoadSettings();
            _settings.TryAdd("Music", false);
            _settings.TryAdd("Sound", false);
            //SaveSettings();
            //Debug.Log(_settings["Sound"]);
            //Debug.Log(_settings["Music"]);
        }

        public string GetLastSceneName()
        {
            return _lastSceneName;
        }
        public void SaveScore(int amount)
        { 
            amount = Mathf.FloorToInt(((_difficulty - 1) * 0.25f + 0.75f) * amount);
            _lastScore = amount;
            _lastSceneName = SceneManager.GetActiveScene().name;
            if (_scores.ContainsKey(_lastSceneName))
            {
                _scores[_lastSceneName] = Math.Max(amount, _scores[_lastSceneName]);
            }
            else
            {
                _scores.Add(_lastSceneName, amount);
            }
            SaveScores();
        }

        private void SaveScores()
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(_filePath, FileMode.Create));
            writer.Write(_scores.Count);
            foreach (var score in _scores)
            { 
                writer.Write(score.Key);
                writer.Write(score.Value);
            }
        }
        
        private void SaveSettings()
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(_filePathSettings, FileMode.Create));
            writer.Write(_settings.Count);
            foreach (var score in _settings)
            { 
                writer.Write(score.Key);
                writer.Write(score.Value);
            }
        }

        private void LoadScores()
        {
            if (!File.Exists(_filePath)) return;
            using BinaryReader reader = new BinaryReader(File.Open(_filePath, FileMode.Open));
            int count = reader.ReadInt32();
            _scores = new Dictionary<string, int>(count);
            for (int i = 0; i < count; i++)
            { 
                string key = reader.ReadString();
                int value = reader.ReadInt32();
                _scores.Add(key, value);
            }
        }

        private void LoadSettings()
        {
            if (!File.Exists(_filePathSettings)) return;
            using BinaryReader reader = new BinaryReader(File.Open(_filePathSettings, FileMode.Open));
            int count = reader.ReadInt32();
            _settings = new Dictionary<string, bool>(count);
            for (int i = 0; i < count; i++)
            { 
                string key = reader.ReadString();
                bool value = reader.ReadBoolean();
                _settings.Add(key, value);
            }
        }
        
        public int GetScore(string sceneName)
        {
            _scores.TryAdd(sceneName, 0);
            return _scores[sceneName];
        }
        
        public int GetLastScore()
        {
            return _lastScore;
        }
        
        public void SetDifficulty(int difficulty)
        {
            _difficulty = difficulty;
        }
        
        public int GetDifficulty()
        {
            return _difficulty;
        }

        public void ToggleSetting(string setting, bool value)
        {
            _settings[setting] = value;
            if(setting == "Music")
            {
                GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MenuMusic>()._audioSource.volume = _settings[setting] ? 1.0f : 0.0f; 
            }
            SaveSettings();
        }
        
        public bool IsToggle(string setting)
        {
            return _settings[setting];
        }
    }
}