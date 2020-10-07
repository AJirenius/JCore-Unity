using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.Sound
{
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [Tooltip("How many sounds will be able to play at once at any time?")]
        [SerializeField]
        private int maxPlayingSound = 10;

        [SerializeField]
        private SoundPlayer prefabSoundPlayer = null;

        [Tooltip("If you want to fetch sounds via id you will need to fill in with a SoundListSO")]
        [SerializeField]
        private SoundListSO soundList = null;        

        private Queue<SoundPlayer> availablePlayers;
        private List<SoundPlayer> playingSounds;

        void Awake()
        {
            availablePlayers = new Queue<SoundPlayer>();
            for (int i = 0; i < maxPlayingSound; i++)
            {
                SoundPlayer soundPlayer = Instantiate(prefabSoundPlayer);
                soundPlayer.transform.SetParent(this.transform);
                availablePlayers.Enqueue(soundPlayer);
            }
            playingSounds = new List<SoundPlayer>();
        }

        void Update()
        {
            // Remove players that has finished playing sound
            if (playingSounds.Count > 0)
            {
                SoundPlayer soundPlayer;
                float t = Time.time;
                for (int i = playingSounds.Count - 1; i >= 0; i--)
                {
                    soundPlayer = playingSounds[i];
                    if (!soundPlayer.sound.looping && soundPlayer.TimeEnd < t)
                    {
                        playingSounds.RemoveAt(i);
                        availablePlayers.Enqueue(soundPlayer);
                    }
                }
            }
        }

        public int PlaySound(string id)
        {
            if (id == null || id == "")
            {
                Debug.LogWarning("Sound id is null or empty");
                return 0;
            }
            if (!soundList)
            {
                Debug.LogWarning("No SoundListSO is attached to SoundManager. Cannot play sound using SoundSO id");
                return 0;
            }
            return PlaySound(soundList.GetEntry(id));
        }

        // returning a unique sound id for stopping that specific instance. If return 0, the sound couldn't be played.
        public int PlaySound(SoundSO sound)
        {
            if (sound.maxPlaying > 0 && HasMaxPlaying(sound)) return 0;

            SoundPlayer soundPlayer;
            // if none left, take first in play queue (been playing for longest)
            if (availablePlayers.Count == 0)
            {
                soundPlayer = ReuseEarliest();
                if (soundPlayer == null)
                {
                    Debug.LogWarning("Couldn't play sounds as all SoundPlayers were looping. Consider increasing the sound buffer.");
                    return 0;
                }
            }
            else
            {
                soundPlayer = availablePlayers.Dequeue();
            }
            playingSounds.Add(soundPlayer);
            return soundPlayer.Play(sound);
        }
        
        public void StopSound(string id)
        {
            if (!soundList)
            {
                Debug.LogWarning("No SoundListSO is attached to SoundManager. Cannot stop sound using SoundSO id");
                return;
            }
            StopSound(soundList.GetEntry(id));
        }

        public void StopSound(SoundSO sound)
        {
            SoundPlayer soundPlayer;
            for (int i = playingSounds.Count - 1; i >= 0; i--)
            {
                soundPlayer = playingSounds[i];
                if (soundPlayer.sound == sound)
                {
                    soundPlayer.Stop();
                    playingSounds.RemoveAt(i);
                    availablePlayers.Enqueue(soundPlayer);
                }
            }
        }

        // Stop with unique playid
        public void StopSound(int id)
        {
            for (int i = playingSounds.Count - 1; i >= 0; i--)
            {
                SoundPlayer soundPlayer;
                soundPlayer = playingSounds[i];
                if (soundPlayer.PlayingId == id)
                {
                    soundPlayer.Stop();
                    playingSounds.RemoveAt(i);
                    availablePlayers.Enqueue(soundPlayer);
                    return;
                }
            }
        }

        private SoundPlayer ReuseEarliest()
        {
            foreach (SoundPlayer sp in playingSounds)
            {
                // Get earliest that is not a looping sound
                if (!sp.sound.looping)
                {
                    playingSounds.Remove(sp);
                    return sp;
                }
            }
            return null;
        }

        private bool HasMaxPlaying(SoundSO sound)
        {
            int count = 0;
            foreach (SoundPlayer playingSP in playingSounds)
            {
                if (playingSP.sound == sound) count++;
            }
            return (count >= sound.maxPlaying);
        }
    }
}
