using UnityEngine;

namespace VoidLib2
{
    public static class AudioImporter
    {
        public class BassError
        {
            public readonly int ErrorCode;
            public readonly string Message;
            public readonly BassErrorType Type;
            public override string ToString() => $"[{Type}] {Message} (code {ErrorCode})";
            public BassError(int errorCode, string message, BassErrorType type)
            {
                ErrorCode = errorCode;
                Message = message;
                Type = type;
            }
        }
        public enum BassErrorType
        {
            Init,
            Stream,
            FileNotFound
        }

        static AudioImporter()
        {
            bool inited = NativeBass.BASS_Init(-1, 44100, 0, IntPtr.Zero, IntPtr.Zero);
            if (!inited)
            {
                int code = NativeBass.BASS_ErrorGetCode();
                throw new Exception($"BASS init failed with error {code}");
            }
        }

        public static AudioClip? LoadAudio(string filePath, out BassError? error)
        {
            if (!File.Exists(filePath))
            {
                error = new BassError(0, $"{filePath}: no such file", BassErrorType.FileNotFound);
                return null;
            }
            // Flags: BASS_SAMPLE_FLOAT (256) | BASS_STREAM_DECODE (2097152) | BASS_UNICODE (0x80000000)
            uint flags = 256 | 2097152 | 0x80000000;

            int handle = NativeBass.BASS_StreamCreateFile(false, filePath, 0, 0, flags);
            if (handle == 0)
            {
                int code = NativeBass.BASS_ErrorGetCode();
                error = new BassError(code, $"stream creation error {code}", BassErrorType.Stream);
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
            error = null;
            return clip;
        }
    }
}
