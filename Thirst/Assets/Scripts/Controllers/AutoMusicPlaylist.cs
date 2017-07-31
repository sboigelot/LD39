using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class AutoMusicPlaylist : MonoBehaviourSingleton<AutoMusicPlaylist>
    {
        public AudioClip[] audioClips; // Array for the audio clips
        public AudioSource controlledAudioSource; // The audio source that will play the audio clips
        private float currentAudioClipLength; // Length of the active audio clip in seconds
        public int iterator; // The id of the active audio clip
        public bool loop = true; // Indicates whether the play list should loop when finished (start from beginning)
        private bool playlistEnded; // Whether or not the playlist has ended
        private float timer; // Timer that keeps track of how long the audio clip has been playing
        public float Volume = 0.25f;

        public void SetVolume(float volume)
        {
            Volume = volume;
            controlledAudioSource.volume = volume;
        }

        private void Start()
        {
            // If atleast one audio clip exists, start playing:
            if (audioClips.Length > 0)
            {
                PlayCurrentClip();
            }
        }

        private void Update()
        {
            // If atleast one audio clip exists and the play list has ended, update:
            if (audioClips.Length > 0 && !playlistEnded)
            {
                // Increase timer with the time difference between this and the previous frame:
                timer += Time.deltaTime;

                // Check whether the timer has exceeded the length of the audio clip:
                if (timer > currentAudioClipLength)
                {
                    // Either start from the beginning if the last clip is played
                    // or go to next audio clip:
                    if (iterator + 1 == audioClips.Length)
                    {
                        if (loop)
                        {
                            // Set it to the first audio clip:
                            iterator = 0;
                        }
                        else
                        {
                            // Stop the active audio clip:
                            controlledAudioSource.Stop();

                            // Set the playlist as ended:
                            playlistEnded = true;

                            // No more playing, so return:
                            return;
                        }
                    }
                    else
                    {
                        iterator++;
                    }

                    // Play the next audio clip:
                    PlayCurrentClip();
                }
            }
        }

        /// <summary>
        /// This function plays the current clip. It does not take
        /// any parameters, as it accesses the global variables.
        /// </summary>
        public void PlayCurrentClip()
        {
            controlledAudioSource.volume = Volume;
            // Stop the active clip:
            controlledAudioSource.Stop();

            var currentClip = audioClips[Random.Range(0, audioClips.Length - 1)];
            // Set the current clip as active audio clip:
            controlledAudioSource.clip = currentClip;

            // Set the length (in seconds) of the audio clip:
            currentAudioClipLength = currentClip.length;

            timer = 0;
            controlledAudioSource.Play();
        }

        public void PlayClip(AudioClip clip)
        {
            controlledAudioSource.volume = Volume;
            // Stop the active clip:
            controlledAudioSource.Stop();

            // Set the current clip as active audio clip:
            controlledAudioSource.clip = clip;

            // Set the length (in seconds) of the audio clip:
            currentAudioClipLength = clip.length;

            timer = 0;
            controlledAudioSource.Play();
        }

        /// <summary>
        /// This function start the playlist from an specific id.
        /// </summary>
        /// <param name="index"></param>
        public void PlayFromIndex(int index)
        {
            if (iterator + 1 <= audioClips.Length)
            {
                // Set the new start iterator:
                iterator = index;

                // Play the audio clip:
                PlayCurrentClip();

                // Start the playlist again:
                playlistEnded = false;
            }
            else
            {
                // This is not allowed:
                Debug.Log("Index " + index + " is out of the audio clip range.");
            }
        }
    }
}
