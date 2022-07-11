using UnityEngine;

public class SoundController : MonoBehaviour
{
   public AudioClip[] audioClips;

   private void Start()
   {
      audioClips = Resources.LoadAll<AudioClip>("Sounds");
   }
}
