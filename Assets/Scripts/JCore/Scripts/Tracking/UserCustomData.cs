using System.Collections.Generic;
using UnityEngine;

namespace JCore.Tracking
{
    public class UserCustomData
    {
        private Dictionary<string, string> _data;
        public Dictionary<string, string> Data => _data;
        
        public UserCustomData()
        {
            _data = new Dictionary<string, string>();
        }

        public void Add(string key, string value, bool overwrite = true)
        {
            if (overwrite)
            {
                _data[key] = value;
            }
            else if(!_data.ContainsKey(key))
            {
                _data.Add(key, value);
            }
            else
            {
                Debug.LogWarning("Key: " + key + " has already been added to UserCustomData while overwrite is set to false.");
            }
        }
        
        public void Remove(string key)
        {
            if (!string.IsNullOrEmpty(key) && _data.ContainsKey(key))
            {
                _data.Remove(key);
            }
        }
    }
}