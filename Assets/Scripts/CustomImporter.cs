using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;

public class CustomImporter : AssetPostprocessor
{
    /* void OnPreprocessAudio()
    {

        if (assetPath.Contains(".mp3") || assetPath.Contains(".flac"))
        {
            Preset audioPreset = AssetDatabase.LoadAssetAtPath<Preset>("Assets/Audio/AudioImporter.preset");
            if(audioPreset != null)
            {
                AudioImporter audioImporter = (AudioImporter)assetImporter;
                Debug.Log(audioImporter);
                Debug.Log(audioPreset);
                audioPreset.ApplyTo(audioImporter);
                audioImporter.SaveAndReimport();
                Debug.Log("Automation Successful");
            }
        } 
    } */

    /* void OnPreprocessTexture()
    {
        if (assetPath.Contains(".jpg") || assetPath.Contains(".png"))
        {
            Preset audioPreset = AssetDatabase.LoadAssetAtPath<Preset>("Assets/Textures/TextureImporter.preset");
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            Debug.Log(textureImporter);
            Debug.Log(audioPreset);
            audioPreset.ApplyTo(textureImporter);
            textureImporter.SaveAndReimport();
            Debug.Log("Automation Successful");
        }
    } */
}
