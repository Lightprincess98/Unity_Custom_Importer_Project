using UnityEngine;
using UnityEditor;

public class AudioConfigruationFile : ScriptableObject
{
    public static uint clipSampleRateSetting { get; set; } = 44100;
    public static AudioCompressionFormat clipCompressionFormatSetting { get; set; } = AudioCompressionFormat.Vorbis;
    public static AudioClipLoadType clipLoadTypeSetting { get; set; } = AudioClipLoadType.DecompressOnLoad;
    public static string platformOverrideSetting { get; set; } = "Standalone";

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

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/Web Player")]
    static void TogglePlatformOverride_WebPlayer()
    {
        platformOverrideSetting = "WebPlayer";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/iOS")]
    static void TogglePlatformOverride_iOS()
    {
        platformOverrideSetting = "iOS";
    }

    [MenuItem("Manual Audio Controls/Set Platform Override Setting/Web GL")]
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
    static public void ToggleApplySettings()
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

public class TextureConfigurationFile : ScriptableObject
{
    public static int textureMaxSizeSetting { get; set; } = 2048;
    public static int textureAnisoLevel { get; set; } = 1;
    public static string platformOverrideSetting { get; set; } = "Standalone";

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/32px")]
    static void ToggleTextureSize_32()
    {
        textureMaxSizeSetting = 32;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/64px")]
    static void ToggleTextureSize_64()
    {
        textureMaxSizeSetting = 64;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/128px")]
    static void ToggleTextureSize_128()
    {
        textureMaxSizeSetting = 128;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/256px")]
    static void ToggleTextureSize_256()
    {
        textureMaxSizeSetting = 256;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/512px")]
    static void ToggleTextureSize_512()
    {
        textureMaxSizeSetting = 512;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/1024px")]
    static void ToggleTextureSize_1024()
    {
        textureMaxSizeSetting = 1024;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/2048px")]
    static void ToggleTextureSize_2048()
    {
        textureMaxSizeSetting = 2048;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/4096px")]
    static void ToggleTextureSize_4096()
    {
        textureMaxSizeSetting = 4096;
    }

    [MenuItem("Manual Texture Controls/Set Texture Size Setting/8192px")]
    static void ToggleTextureSize_8192()
    {
        textureMaxSizeSetting = 8192;
    }

    // ----------------------------------------------------------------------------

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 1")]
    static void ToggleTextureAnisoLevel_1()
    {
        textureAnisoLevel = 1;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level ")]
    static void ToggleTextureAnisoLevel_2()
    {
        textureAnisoLevel = 2;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 3")]
    static void ToggleTextureAnisoLevel_3()
    {
        textureAnisoLevel = 3;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 4")]
    static void ToggleTextureAnisoLevel_4()
    {
        textureAnisoLevel = 4;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 5")]
    static void ToggleTextureAnisoLevel_5()
    {
        textureAnisoLevel = 5;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 6")]
    static void ToggleTextureAnisoLevel_6()
    {
        textureAnisoLevel = 6;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 7")]
    static void ToggleTextureAnisoLevel_7()
    {
        textureAnisoLevel = 7;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 8")]
    static void ToggleTextureAnisoLevel_8()
    {
        textureAnisoLevel = 8;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 9")]
    static void ToggleTextureAnisoLevel_9()
    {
        textureAnisoLevel = 9;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 10")]
    static void ToggleTextureAnisoLevel_10()
    {
        textureAnisoLevel = 10;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 11")]
    static void ToggleTextureAnisoLevel_11()
    {
        textureAnisoLevel = 11;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 12")]
    static void ToggleTextureAnisoLevel_12()
    {
        textureAnisoLevel = 12;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 13")]
    static void ToggleTextureAnisoLevel_13()
    {
        textureAnisoLevel = 13;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 14")]
    static void ToggleTextureAnisoLevel_14()
    {
        textureAnisoLevel = 14;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 15")]
    static void ToggleTextureAnisoLevel_15()
    {
        textureAnisoLevel = 15;
    }

    [MenuItem("Manual Texture Controls/Set Texture Aniso Level/Level 16")]
    static void ToggleTextureAnisoLevel_16()
    {
        textureAnisoLevel = 16;
    }

    // ----------------------------------------------------------------------------

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/Standalone")]
    static void TogglePlatformOverride_Standalone()
    {
        platformOverrideSetting = "Standalone";
    }

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/Android")]
    static void TogglePlatformOverride_Android()
    {
        platformOverrideSetting = "Android";
    }

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/Web Player")]
    static void TogglePlatformOverride_WebPlayer()
    {
        platformOverrideSetting = "WebPlayer";
    }

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/iOS")]
    static void TogglePlatformOverride_iOS()
    {
        platformOverrideSetting = "iOS";
    }

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/Web GL")]
    static void TogglePlatformOverride_WebGL()
    {
        platformOverrideSetting = "WegGL";
    }

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/PS4")]
    static void TogglePlatformOverride_PS4()
    {
        platformOverrideSetting = "PS4";
    }

    [MenuItem("Manual Texture Controls/Set Platform Override Setting/Xbox One")]
    static void TogglePlatformOverride_XBoxOne()
    {
        platformOverrideSetting = "XboxOne";
    }

    // ----------------------------------------------------------------------------

    [MenuItem("Manual Texture Controls/Apply Selected Settings")]
    static public void ToggleApplySettings()
    {
        ApplySettings(textureMaxSizeSetting, textureAnisoLevel, platformOverrideSetting);
    }

    static void ApplySettings(int maxSize, int anisoLevel,string platform)
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            TextureImporterPlatformSettings texturesettings = textureImporter.GetDefaultPlatformTextureSettings();
            texturesettings.maxTextureSize = maxSize;
            texturesettings.name = platform;
            texturesettings.overridden = true;
            textureImporter.anisoLevel = anisoLevel;
            textureImporter.SetPlatformTextureSettings(texturesettings);
            AssetDatabase.ImportAsset(path);
        }
    }

    //Method which searches and adds all textures to an array of objects, including searching all child folders for textures.
    static Object[] GetSelectedTextures()
    {
        return Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
    }
}

//Class for handling Automatic importing for Audio and Textures
public class AutomaticImport : AssetPostprocessor
{   

    //Method that automatically runs when a new audio file is added to the Unity Project. Will always run when a new Audio File is added to the process.
   void OnPreprocessAudio()
    {
        //First checks if the asset is going into any folder labeled "Audio", otherwise it will not run the rest of the code at all.
        if (assetPath.Contains("Audio"))
        {
            //Settings are obtained from the Audio Configuration Class to allow for easy access to the already selected choices.
            //Audio importer must save and Reimport for the changes to take effect, otherwise the changes will not apply to the file.
            AudioImporter audioImporter = (AudioImporter)assetImporter;
            AudioImporterSampleSettings settings = audioImporter.defaultSampleSettings;
            settings.compressionFormat = AudioConfigruationFile.clipCompressionFormatSetting;
            settings.sampleRateSetting = AudioSampleRateSetting.OverrideSampleRate;
            settings.sampleRateOverride = AudioConfigruationFile.clipSampleRateSetting;
            settings.loadType = AudioConfigruationFile.clipLoadTypeSetting;
            audioImporter.SetOverrideSampleSettings(AudioConfigruationFile.platformOverrideSetting, settings);
            audioImporter.SaveAndReimport();
        }
    }

    void OnPreprocessTexture()
    {
        //First checks if the asset is going into any folder labeled "Audio", otherwise it will not run the rest of the code at all.
        if (assetPath.Contains("Textures"))
        {
            //Settings are obtained from the Audio Configuration Class to allow for easy access to the already selected choices.
            //Audio importer must save and Reimport for the changes to take effect, otherwise the changes will not apply to the file.
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            TextureImporterPlatformSettings texturesettings = textureImporter.GetDefaultPlatformTextureSettings();
            texturesettings.maxTextureSize = TextureConfigurationFile.textureMaxSizeSetting;
            texturesettings.name = TextureConfigurationFile.platformOverrideSetting;
            texturesettings.overridden = true;
            textureImporter.anisoLevel = TextureConfigurationFile.textureAnisoLevel;
            textureImporter.SetPlatformTextureSettings(texturesettings);
            textureImporter.SaveAndReimport();
        }
    }
}
