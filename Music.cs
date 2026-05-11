using UnityEngine;

namespace VoidLib2
{
    public static class Music
    {
        public static bool SetMusicMuted(bool muted)
        {
            GameObject obj = GameObject.Find("World/Ambience/Ambient Music");
            if (obj == null) return false;
            obj.GetComponent<AudioSource>().mute = muted;
            return true;
        }
    }
}
