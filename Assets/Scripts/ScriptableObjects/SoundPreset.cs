using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Sound", fileName = "Sound")]
    
    public class SoundPreset : ScriptableObject
    {
        public AudioClip[] audioClips;
    }
}
