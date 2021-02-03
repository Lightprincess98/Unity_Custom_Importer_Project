using UnityEngine;
using UnityEditor;

public class AudioConfigruationFile : ScriptableObject
{
    static private uint clipSampleRateSetting = 44100;
    static private AudioCompressionFormat clipCompressionFormatSetting = AudioCompressionFormat.Vorbis;
    static private AudioClipLoadType clipLoadTypeSetting = AudioClipLoadType.DecompressOnLoad;
    static private string platformOverrideSetting = "Standalone";

    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/8,000 Hz")]
    static void ToggleSampleRate_8000()
    {
        clipSampleRateSetting = 8000;
    }

    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/11,025 Hz")]
    static void ToggleSampleRate_11025()
    {
        clipSampleRateSetting = 11025;
    }

    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/22,050 Hz")]
    static void ToggleSampleRate_22050()
    {
        clipSampleRateSetting = 22050;
    }
    
    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/44,100 Hz")]
    static void ToggleSampleRate_44100()
    {
        clipSampleRateSetting = 44100;
    }

    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/48,000 Hz")]
    static void ToggleSampleRate_48000()
    {
        clipSampleRateSetting = 48000;
    }

    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/96,000 Hz")]
    static void ToggleSampleRate_96000()
    {
        clipSampleRateSetting = 96000;
    }

    [MenuItem("Manual Audio Controls/Set Sample Rate Setting/192,000 Hz")]
    static void ToggleSampleRate_192000()
    {
        clipSampleRateSetting = 192000;
    }

    // ----------------------------------------------------------------------------

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/AAC")]
    static void ToggleCompression_AAC()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.AAC;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/ADPCM")]
    static void ToggleCompression_ADPCM()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.ADPCM;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/ATRAC9")]
    static void ToggleCompression_ATRAC9()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.ATRAC9;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/GCADPCM")]
    static void ToggleCompression_GCADPCM()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.GCADPCM;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/HEVAG")]
    static void ToggleCompression_HEVAG()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.HEVAG;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/MP3")]
    static void ToggleCompression_MP3()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.MP3;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/PCM")]
    static void ToggleCompression_PCM()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.PCM;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/VAG")]
    static void ToggleCompression_VAG()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.PCM;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/Vorbis")]
    static void ToggleCompression_Vorbis()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.Vorbis;
    }

    [MenuItem("Manual Audio Controls/Set Audio Compression Setting/XMA")]
    static void ToggleCompression_XMA()
    {
        clipCompressionFormatSetting = AudioCompressionFormat.XMA;
    }
    // ----------------------------------------------------------------------------

    [MenuItem("Manual Audio Controls/Set Load Type Setting/Streaming")]
    static void ToggleDecompressOnLoad_Streaming()
    {
        clipLoadTypeSetting = AudioClipLoadType.Streaming;
    }

    [MenuItem("Manual Audio Controls/Set Load Type Setting/Decompress on Load")]
    static void ToggleDecompressOnLoad_Decompressed()
    {
        clipLoadTypeSetting = AudioClipLoadType.DecompressOnLoad;
    }

    [MenuItem("Manual Audio Controls/Set Load Type Setting/CompressedInMemory")]
    static void ToggleDecompressOnLoad_Compressed()
    {
        clipLoadTypeSetting = AudioClipLoadType.CompressedInMemory;
    }

    // ----------------------------------------------------------------------------

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/Standalone")]
    static void TogglePlatformOverride_Standalone()
    {
        platformOverrideSetting = "Standalone";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/Android")]
    static void TogglePlatformOverride_Android()
    {
        platformOverrideSetting = "Android";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/WebPlayer")]
    static void TogglePlatformOverride_WebPlayer()
    {
        platformOverrideSetting = "WebPlayer";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/iOS")]
    static void TogglePlatformOverride_iOS()
    {
        platformOverrideSetting = "iOS";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/WebGL")]
    static void TogglePlatformOverride_WebGL()
    {
        platformOverrideSetting = "WegGL";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/PS4")]
    static void TogglePlatformOverride_PS4()
    {
        platformOverrideSetting = "PS4";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/Xbox One")]
    static void TogglePlatformOverride_XBoxOne()
    {
        platformOverrideSetting = "XboxOne";
    }

    // ----------------------------------------------------------------------------

    [MenuItem("Manual Audio Controls/Apply Selected Settings")]
    static void ToggleApplySettings()
    {
        ApplySettings(clipSampleRateSetting, clipCompressionFormatSetting, clipLoadTypeSetting, platformOverrideSetting);
    }

    static void ApplySettings(uint sampleRate, AudioCompressionFormat format, AudioClipLoadType loadType, string platform)
    {
        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips)
        {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            AudioImporterSampleSettings settings = audioImporter.defaultSampleSettings;
            settings.compressionFormat = format;
            settings.sampleRateSetting = AudioSampleRateSetting.OverrideSampleRate;
            settings.sampleRateOverride = sampleRate;
            settings.loadType = loadType;
            audioImporter.SetOverrideSampleSettings(platform, settings);
            AssetDatabase.ImportAsset(path);
        }
    }

    //Method which searches and adds all audioclips to an array of objects, including searching all child folders for audio clips.
    static Object[] GetSelectedAudioclips()
    {
        return Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
    }
}