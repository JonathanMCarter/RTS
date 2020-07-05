using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/*
 * 
 *								Audio Manager Script
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 2.3.2
 *							  Last Updated: 20/06/2020
 * 
 * 
*/

namespace CarterGames.Assets.AudioManager
{
    [System.Serializable]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private bool hasScannedOnce;
        [SerializeField] private bool shouldShowDir;
        [SerializeField] private bool shouldShowClips;

        [Tooltip("This should be the Audio Manager File you are working with, if it is not then please change it to the correct one.")]
        public AudioManagerFile audioManagerFile;

        public AudioManagerFile lastAudioManagerFile;

        [Tooltip("This should be set when you have a audioManagerFile in use, if not please select it and put it here.")]
        public GameObject soundPrefab = null;       // Holds the prefab that plays the sound requested

        public List<string> directory;

        public Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();       // Dictionary that holds the audio names and clips



        private void Start()
        {
            UpdateLibrary();

            if (soundPrefab == null)
            {
                Debug.LogWarning("(*Audio Manager*): Prefab has not been assigned! Please assign a prefab in the inspector before using the manager.");
            }

            GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;

            for (int i = 0; i < audioManagerFile.clipName.Count; i++)         // For loop that populates the dictionary with all the sound assets in the lists
            {
                soundLibrary.Add(audioManagerFile.clipName[i], audioManagerFile.audioClip[i]);
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound that is scanned into the audio manager 
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void Play(string request, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))                                              // If the sound is in the library
            {
                GameObject _clip = Instantiate(soundPrefab);                                    // Instantiate a Sound prefab
                _clip.GetComponent<AudioSource>().clip = soundLibrary[request];                     // Get the prefab and add the requested clip to it
                _clip.GetComponent<AudioSource>().volume = volume;  // changes the volume if a it is inputted
                _clip.GetComponent<AudioSource>().pitch = pitch;      // changes the pitch if a change is inputted
                _clip.GetComponent<AudioSource>().Play();                                       // play the audio from the prefab
                Destroy(_clip, _clip.GetComponent<AudioSource>().clip.length);                  // Destroy the prefab once the clip has finished playing
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound from a different gameobject instead of the default prefab object
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void Play(string request, GameObject gameObject, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                if (!_clip.GetComponent<AudioSource>()) { _clip.AddComponent<AudioSource>(); }
                _clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                _clip.GetComponent<AudioSource>().volume = volume;
                _clip.GetComponent<AudioSource>().pitch = pitch;
                _clip.GetComponent<AudioSource>().Play();
                Destroy(_clip, _clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void Play(string request, Vector3 position, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.gameObject.transform.position = position;
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().Play();
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound from a particular timecode on the audio clip audioManagerFile
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayFromTime(string request, float time, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().time = time;
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().Play();
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound from a particular timecode on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayFromTime(string request, float time, GameObject gameObject, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = gameObject;
                if (!clip.GetComponent<AudioSource>()) { clip.AddComponent<AudioSource>(); }
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().time = time;
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().Play();
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound from a particular timecode at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayFromTime(string request, float time, Vector3 position, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.gameObject.transform.position = position;
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().time = time;
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().Play();
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound after a defined amount of time
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayWithDelay(string request, float delay, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().PlayDelayed(delay);                            // Only difference, played with a delay rather that right away
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound after a defined amount of time on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayWithDelay(string request, float delay, GameObject gameObject, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = gameObject;
                if (!clip.GetComponent<AudioSource>()) { clip.AddComponent<AudioSource>(); }
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().PlayDelayed(delay);                            // Only difference, played with a delay rather that right away
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a sound after a defined amount of time at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayWithDelay(string request, float delay, Vector3 position, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(request))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.gameObject.transform.position = position;
                clip.GetComponent<AudioSource>().clip = soundLibrary[request];
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().PlayDelayed(delay);
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayRandom(float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(GetRandomSound().name))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.GetComponent<AudioSource>().clip = soundLibrary[GetRandomSound().name];
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().Play();                            // Only difference, played with a delay rather that right away
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }


        /// <summary>
        /// (Audio Manager) Play a random sound that has been scanned by this manager, from a particular time
        /// </summary>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayRandomFromTime(float time, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(GetRandomSound().name))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.GetComponent<AudioSource>().clip = soundLibrary[GetRandomSound().name];
                clip.GetComponent<AudioSource>().time = time;
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().Play();                            // Only difference, played with a delay rather that right away
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }



        /// <summary>
        /// (Audio Manager) Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        public void PlayRandomWithDelay(float delay, float volume = 1, float pitch = 1)
        {
            if (soundLibrary.ContainsKey(GetRandomSound().name))
            {
                GameObject clip = Instantiate(soundPrefab);
                clip.GetComponent<AudioSource>().clip = soundLibrary[GetRandomSound().name];
                clip.GetComponent<AudioSource>().volume = volume;
                clip.GetComponent<AudioSource>().pitch = pitch;
                clip.GetComponent<AudioSource>().PlayDelayed(delay);                            // Only difference, played with a delay rather that right away
                Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
            }
            else
            {
                Debug.LogWarning("(*Audio Manager*): Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
            }
        }



        // Useful if you need to check the number of clips in the selected list
        public int GetNumberOfClips()
        {
            return soundLibrary.Count;
        }

        // Picks a random sound from the current selection
        public AudioClip GetRandomSound()
        {
            return soundLibrary.Values.ElementAt(Random.Range(0, soundLibrary.Count - 1));
        }

        // Changes the Audio Manager File to what is inputted
        public void ChangeAudioManagerFile(AudioManagerFile newFile)
        {
            audioManagerFile = newFile;
            UpdateLibrary();
        }

        // Returns the Audio Manager File that is currently in use
        public AudioManagerFile GetAudioManagerFile()
        {
            return audioManagerFile;
        }

        // Used in the editor script, to update the library with a fresh input, don't call this, it doesn't play audio
        public void UpdateLibrary()
        {
            soundLibrary.Clear();

            if (audioManagerFile.clipName.Count > 0)
            {
                for (int i = 0; i < audioManagerFile.clipName.Count; i++)         // For loop that populates the dictionary with all the sound assets in the lists
                {
                    soundLibrary.Add(audioManagerFile.clipName[i], audioManagerFile.audioClip[i]);
                }
            }
        }
    }
}