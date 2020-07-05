using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *								Audio Manager File (AMF)
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 2.3.2
 *							  Last Updated: 20/06/2020
 * 
 * 
*/
namespace CarterGames.Assets.AudioManager
{
    [System.Serializable, CreateAssetMenu(fileName = "Audio Manager File", menuName = "Carter Games/Audio Manager File")]
    public class AudioManagerFile : ScriptableObject
    {
        public List<string> clipName;
        public List<AudioClip> audioClip;
        public GameObject soundPrefab;
        public bool isPopulated;
        public List<string> directory;
    }
}