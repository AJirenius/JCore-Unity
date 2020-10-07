using System.Collections.Generic;

namespace JCore.Tracking
{
    public class Tags
    {
        private List<string> _tags;
        public List<string> AllTags => _tags;
        
        public Tags()
        {
            _tags = new List<string>();
        }

        public void AddTag(string tagName)
        {
            if (!string.IsNullOrEmpty(tagName) && !_tags.Contains(tagName))
            {
                _tags.Add(tagName);
            }
        }

        public void RemoveTag(string tagName)
        {
            if (!string.IsNullOrEmpty(tagName) && _tags.Contains(tagName))
            {
                _tags.Remove(tagName);
            }
        }
    }
}