using UnityEngine;
using FMODUnity;

public class AudioSystem : MonoBehaviour
{
    public StudioEventEmitter musicEmitter;
        public static bool musicStarted;
        public static AudioSystem instance;
        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (musicStarted)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            musicEmitter.Play();
            musicStarted = true;
            instance = this;
        }
}


