using NAudio.Wave;
using UnityEngine;

namespace VoidLib2
{
    public static class AudioImporter
    {
        public readonly struct ImportResult
        {
            public readonly int Value;
            public readonly bool WasAttempted;
            public bool Success => WasAttempted && Value == 0;

            public static ImportResult Ok => new(0, true);
            public static ImportResult NotAttempted => new(0, false);

            public ImportResult(int value, bool attemped = false)
            {
                Value = value;
                WasAttempted = attemped;
            }
        }

        public static AudioClip? Load(string filePath, out ImportResult bassImportResult, out ImportResult naudioImportResult)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Could not find file '{filePath}'.", filePath);

            bassImportResult = ImportResult.NotAttempted;
            try
            {

                AudioClip? clip = Bass.LoadAudio(filePath, out int bassCode);
                if (bassCode == 0)
                {
                    bassImportResult = ImportResult.Ok;
                    naudioImportResult = ImportResult.NotAttempted;
                    return clip;
                }
            }
            catch (DllNotFoundException ex)
            {
                bassImportResult = new ImportResult(ex.HResult, true);
            }
            catch (EntryPointNotFoundException ex)
            {
                bassImportResult = new ImportResult(ex.HResult, true);
            }

            if (!bassImportResult.WasAttempted) throw new Exception("unreachable");

            try
            {
                AudioClip? naudioClip = NAudio.Load(filePath, out int naudioHResult);
                if (naudioHResult == 0)
                {
                    naudioImportResult = ImportResult.Ok;
                    return naudioClip;
                }
                naudioImportResult = new ImportResult(naudioHResult);
                return null;
            }
            catch (DllNotFoundException ex)
            {
                naudioImportResult = new ImportResult(ex.HResult);
                return null;
            }
        }

        /// <summary>
        /// Requires NAudio.dll, NAudio.Core.dll, NAudio.Wasapi.dll, NAudio.WinMN.dll
        /// </summary>
        public static class NAudio
        {
            public static AudioClip? Load(string filePath, out int HResult)
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException(filePath);

                try
                {
                    using AudioFileReader reader = new(filePath);
                    int channels = reader.WaveFormat.Channels;
                    int sampleRate = reader.WaveFormat.SampleRate;

                    float[] samples = ReadAllSamples(reader);

                    AudioClip clip = AudioClip.Create(
                        Path.GetFileNameWithoutExtension(filePath),
                        samples.Length / channels,
                        channels,
                        sampleRate,
                        false
                    );
                    clip.SetData(samples, 0);
                    HResult = 0;
                    return clip;
                }
                catch (Exception ex)
                {
                    HResult = ex.HResult;
                    return null;
                }
            }

            private static float[] ReadAllSamples(AudioFileReader reader)
            {
                List<float> sampleList = new((int)(reader.Length / 4));

                float[] buffer = new float[4096];
                int read;

                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < read; i++)
                        sampleList.Add(buffer[i]);
                }

                return sampleList.ToArray();
            }
        }
    }

    /// <summary>
    /// Requires bass.dll
    /// </summary>
    public static class Bass
    {
        static Bass()
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

        public static AudioClip? LoadAudio(string filePath, out int code)
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
                code = NativeBass.BASS_ErrorGetCode();
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
            code = 0;
            return clip;
        }
    }
}
