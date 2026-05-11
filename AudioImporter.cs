using UnityEngine;

namespace VoidLib2
{
    public static class AudioImporter
    {
        public static AudioClip? LoadAudio(string filePath)
        {
            // Flags: BASS_SAMPLE_FLOAT (256) | BASS_STREAM_DECODE (2097152)
            uint flags = 256 | 2097152;

            NativeBass.BASS_Init(-1, 44100, 0, IntPtr.Zero, IntPtr.Zero);

            int handle = NativeBass.BASS_StreamCreateFile(false, filePath, 0, 0, flags);
            if (handle == 0) return null;
            NativeBass.BASS_ChannelGetInfo(handle, out NativeBass.BASS_CHANNELINFO info);
            long lengthBytes = NativeBass.BASS_ChannelGetLength(handle, 0); // BASS_POS_BYTE
            int totalSamples = (int)(lengthBytes / 4);
            float[] sampleBuffer = new float[totalSamples];
            NativeBass.BASS_ChannelGetData(handle, sampleBuffer, (int)lengthBytes);
            AudioClip clip = AudioClip.Create(
                Path.GetFileName(filePath),
                totalSamples / info.chans,
                info.chans,
                info.freq,
                false
            );
            clip.SetData(sampleBuffer, 0);
            NativeBass.BASS_StreamFree(handle);
            return clip;
        }
    }
}
