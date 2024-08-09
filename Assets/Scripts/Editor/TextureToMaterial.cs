using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class TextureToMaterial : EditorWindow
{
    [MenuItem("Tools/Generate Materials")]
    public static void ShowWindow()
    {
        GetWindow<TextureToMaterial>("Generate Materials");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Generate Materials"))
        {
            GenerateMaterials();
        }
    }

    private void GenerateMaterials()
    {
        string textureFolderPath = "Assets/Texture";
        string materialFolderPath = "Assets/Resources/Materials";
        if (!Directory.Exists(materialFolderPath))
        {
            Directory.CreateDirectory(materialFolderPath);
        }
        else
        {
            string[] existingMaterialPaths = Directory.GetFiles(materialFolderPath, "*.mat", SearchOption.TopDirectoryOnly);
            foreach (string file in existingMaterialPaths)
            {
                File.Delete(file);
            }
        }
        string[] texturePaths = Directory.GetFiles(textureFolderPath, "*.jpg", SearchOption.AllDirectories);
        foreach (string texturePath in texturePaths)
        {
            string assetPath = texturePath.Replace("\\", "/");
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

            if (texture != null)
            {
                Material newMaterial = new Material(Shader.Find("Standard"));
                newMaterial.mainTexture = texture;
                string materialPath = Path.Combine(materialFolderPath, texture.name + ".mat");
                materialPath = AssetDatabase.GenerateUniqueAssetPath(materialPath);
                AssetDatabase.CreateAsset(newMaterial, materialPath);
            }
        }

        AssetDatabase.Refresh();
    }
}
