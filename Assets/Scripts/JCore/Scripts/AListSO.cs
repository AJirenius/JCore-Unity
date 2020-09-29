using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore
{

    /// <summary>
    /// Base class for ScriptableObjects that are just a global directory of a certain type of things (Screens, Itemsdefinitions etc).
    /// Can be extended with validation + editor features
    /// </summary>
      public abstract class AListSO<T> : AListSOBase where T : UnityEngine.Object, IListSOEntry
    {

        public T[] entries;
        private Dictionary<string, T> entriesByID = new Dictionary<string, T>();

        public T GetEntry(string id)
        {
            if (string.IsNullOrEmpty(id)) return default(T);
            if (entriesByID.ContainsKey(id)) return entriesByID[id];
            for (int i = 0; i < entries.Length; i++)
            {
                T entry = entries[i];
                if (entry != null)
                {
                    if (entry.GetEntryID() == id)
                    {
                        entriesByID[id] = entry;
                        return entry;
                    }
                }
                else
                {
                    Debug.LogWarning("Null entry in " + GetListName());
                }
            }
            return default(T);
        }

        public Dictionary<string, T> GetEntriesByID()
        {
            Dictionary<string, T> result = new Dictionary<string, T>();
            for (int i = 0; i < entries.Length; i++)
            {
                T entry = entries[i];
                string key = entry.GetEntryID();
                entriesByID[key] = entry;
                result[key] = entry;
            }
            return result;
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>(entries);
            return result;
        }

        public string[] GetAllIDs()
        {
            string[] result = new string[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i] == null) continue;
                result[i] = entries[i].GetEntryID();
            }
            return result;
        }

        public override void Validate()
        {
            for (int i = 0; i < entries.Length; i++)
            {
                T entry = entries[i];
                if (entry == null)
                {
                    Debug.LogWarning("Null entry in " + GetListName());
                }
                else
                {
                    string entryID = entry.GetEntryID();
                    if (string.IsNullOrEmpty(entryID))
                    {
                        Debug.LogWarning("Entry with no ID in " + GetListName());
                    }
                    else
                    {
                        if (entriesByID.ContainsKey(entryID) && entriesByID[entryID] != entry)
                        {
                            string fileNames = entry.name + "," + entriesByID[entryID].name;
                            Debug.LogWarning("Duplicate id [" + entryID + "] for [" + fileNames + "] in " + GetListName());
                        }
                        else
                        {
                            entriesByID[entryID] = entry;
                        }
                    }
                }
            }
        }

        public override void SetEntries(UnityEngine.Object[] newEntries)
        {
            entriesByID = new Dictionary<string, T>();
            entries = new T[newEntries.Length];
            for (int i = 0; i < newEntries.Length; i++)
            {
                entries[i] = newEntries[i] as T;
            }
        }

        public override string GetEntryTypeName()
        {
            return typeof(T).ToString();
        }

        public override Type GetEntryType()
        {
            return typeof(T);
        }

        public override string GetListName()
        {
            return "AListSO<" + typeof(T) + "> " + this;
        }
    }

    // Base class without generics to be able to have a custom Editor
    public abstract class AListSOBase : ScriptableObject
    {
        public string SearchFolder;

        public abstract void SetEntries(UnityEngine.Object[] entries);
        public abstract void Validate();
        public abstract string GetEntryTypeName();
        public abstract Type GetEntryType();
        public abstract string GetListName();
    }

    public interface IListSOEntry
    {
        string GetEntryID();
    }
}