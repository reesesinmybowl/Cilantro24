using UnityEngine;
using UnityEditor;
using System.IO;

public class CardMaterialGenerator
{
    [MenuItem("Tools/Generate Card Materials")]
    static void GenerateMaterials()
    {
        string textureFolder = "Assets/Cards"; // Change this to your PNG folder
        string[] pngFiles = Directory.GetFiles(textureFolder, "*.png");

        foreach (string file in pngFiles)
        {
            string assetPath = file.Replace(Application.dataPath, "Assets");
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

            if (texture != null)
            {
                string materialPath = Path.ChangeExtension(assetPath, ".mat");

                Material mat = new Material(Shader.Find("Standard"));
                mat.mainTexture = texture;

                AssetDatabase.CreateAsset(mat, materialPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("✅ Card materials created!");
    }
}
