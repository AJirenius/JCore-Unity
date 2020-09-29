using UnityEngine;

namespace JCore.Sound
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Sound/SoundSO")]
    public class SoundSO : ScriptableObject, IListSOEntry
    {
        public string id;

        public bool looping;

        [Tooltip("How many of this same sound can be played at the same time? 0 = unlimited")]
        public int maxPlaying = 0;

        [Tooltip("If several sounds, it will randomize between them")]
        public AudioClip[] sound = new AudioClip[1];

        public float volume = 1.0f;

        public string GetEntryID()
        {
            return id;
        }

        public override string ToString()
        {
            return "SoundSO(" + id + ")";
        }

        public AudioClip GetSound()
        {
            if (sound.Length > 1)
            {
                return sound[Random.Range(0, sound.Length)];
            }
            return sound[0];
        }
    }
}
