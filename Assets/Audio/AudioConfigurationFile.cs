using UnityEngine;
using UnityEditor;

public class AudioConfigruationFile : ScriptableObject
{

    [MenuItem("Custom/Sound/Toggle audio compression/AAC")]
    static void ToggleCompression_AAC()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.AAC);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/ADPCM")]
    static void ToggleCompression_ADPCM()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.ADPCM);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/ATRAC9")]
    static void ToggleCompression_ATRAC9()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.ATRAC9);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/GCADPCM")]
    static void ToggleCompression_GCADPCM()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.GCADPCM);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/HEVAG")]
    static void ToggleCompression_HEVAG()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.HEVAG);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/MP3")]
    static void ToggleCompression_MP3()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.MP3);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/PCM")]
    static void ToggleCompression_PCM()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.PCM);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/VAG")]
    static void ToggleCompression_VAG()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.VAG);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/Vorbis")]
    static void ToggleCompression_Vorbis()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.Vorbis);
    }

    [MenuItem("Custom/Sound/Toggle audio compression/XMA")]
    static void ToggleCompression_XMA()
    {
        SelectedToggleCompressionSettings(AudioCompressionFormat.XMA);
    }
    // ----------------------------------------------------------------------------

    [MenuItem("Custom/Sound/load type/Stream from disc")]
    static void ToggleDecompressOnLoad_Disable()
    {
        SelectedToggleDecompressOnLoadSettings(AudioClipLoadType.Streaming);
    }

    [MenuItem("Custom/Sound/load type/Decompress on Load")]
    static void ToggleDecompressOnLoad_Enable()
    {
        SelectedToggleDecompressOnLoadSettings(AudioClipLoadType.DecompressOnLoad);
    }

    [MenuItem("Custom/Sound/load type/CompressedInMemory")]
    static void ToggleDecompressOnLoad_Enable2()
    {
        SelectedToggleDecompressOnLoadSettings(AudioClipLoadType.CompressedInMemory);
    }

    // ----------------------------------------------------------------------------

    static void SelectedToggleCompressionSettings(AudioCompressionFormat newFormat)
    {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips)
        {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            AudioImporterSampleSettings settings = audioImporter.defaultSampleSettings;
            settings.compressionFormat = newFormat;
            audioImporter.SetOverrideSampleSettings("Standalone", settings);
            AssetDatabase.ImportAsset(path);
        }
    }

    static void SelectedToggleDecompressOnLoadSettings(AudioClipLoadType loadType)
    {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips)
        {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            AudioImporterSampleSettings settings = audioImporter.defaultSampleSettings;
            settings.loadType = loadType;
            audioImporter.SetOverrideSampleSettings("Standalone", settings);
            AssetDatabase.ImportAsset(path);
        }
    }

    static Object[] GetSelectedAudioclips()
    {
        return Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
    }
}