using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets : MonoBehaviour
{

    // Internal instance reference
    private static Assets _i;

    // Instance reference
    public static Assets Instance
    {
        get
        {
            if (_i == null)
                _i = Instantiate(Resources.Load<Assets>("LofiAssets"));
            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

}

