using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        static private int currentPlayingId = 1;

        // Unique playing sound id to be able to stop or tweak specific sounds even if they have the same audio clip.
        private int playingId;
        public int PlayingId { get { return playingId; } }

        private float timeStart;
        public float TimeStart { get { return timeStart; } }

        private float timeEnd;
        public float TimeEnd { get { return timeEnd; } }

        public SoundSO sound;

        public AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public int Play(SoundSO sound)
        {
            this.sound = sound;
            audioSource.clip = sound.GetSound();
            audioSource.volume = sound.volume;
            audioSource.loop = sound.looping;
            audioSource.Play();
            timeStart = Time.time;
            timeEnd = Time.time + audioSource.clip.length;
            playingId = currentPlayingId++;
            return playingId;
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}
