using UnityEngine;

namespace VoidLib2
{
    public static class AudioImporter
    {
        static AudioImporter()
        {
            // device: -1 = default device
            // freq: 44100 = sample rate in Hz
            // flags: 0 = no special flags
            // win: IntPtr.Zero = console/no window handle
            // clsid: IntPtr.Zero = use default device class
            bool inited = NativeBass.BASS_Init(-1, 44100, 0, IntPtr.Zero, IntPtr.Zero);
            if (!inited)
            {
                int code = NativeBass.BASS_ErrorGetCode();
                throw new Exception($"BASS init failed with error {code}");
            }
        }

        public static AudioClip? LoadAudio(string filePath, out int errorCode)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file '{filePath}'.", filePath);
            }
            // Flags: BASS_SAMPLE_FLOAT (256) | BASS_STREAM_DECODE (2097152) | BASS_UNICODE (0x80000000)
            uint flags = 256 | 2097152 | 0x80000000;

            int handle = NativeBass.BASS_StreamCreateFile(false, filePath, 0, 0, flags);
            if (handle == 0)
            {
                errorCode = NativeBass.BASS_ErrorGetCode();
                return null;
            }
            NativeBass.BASS_ChannelGetInfo(handle, out NativeBass.BASS_CHANNELINFO info);
            long lengthBytes = NativeBass.BASS_ChannelGetLength(handle, 0 /* BASS_POS_BYTE */);
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
            errorCode = 0;
            return clip;
        }
    }
}
