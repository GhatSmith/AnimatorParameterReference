using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;


[ClassSummary("Add context menu item for Animator assets")]
public static class AnimatorContextMenu
{
    [MenuItem("Assets/Odd/Animator/Generate Parameters References")]
    private static void GenerateAllReferences()
    {
        AnimatorController animatorController = Selection.activeObject as AnimatorController;

        string folderPath = AssetDatabase.GetAssetPath(animatorController);
        folderPath = folderPath.Substring(0, folderPath.LastIndexOf('.')) + "Parameters";
        if (Directory.Exists(folderPath) && Directory.GetFileSystemEntries(folderPath).Length > 0)
        {
            Debug.LogWarningFormat("Animator parameters generations aborted. Folder {0} exists and is not empty", folderPath);
            return;
        }
        Directory.CreateDirectory(folderPath);

        for (int i = 0; i < animatorController.parameters.Length; i++)
        {
            string parameterName = animatorController.parameters[i].name;
            EditorUtility.DisplayProgressBar("Generating Animator Parameters References", parameterName + " (" + i + "/" + animatorController.parameters.Length + ")", (float)i / (float)animatorController.parameters.Length);

            AnimatorParameterReference animatorParameterReference = ScriptableObject.CreateInstance<AnimatorParameterReference>();
            animatorParameterReference.ParameterName = parameterName;

            string fileName = animatorController.name + "_Parameter_" + parameterName.Replace('/', '_');

            if (parameterName.Contains("/"))
            {
                string directoryPath = folderPath + "/" + parameterName;
                directoryPath = directoryPath.Substring(0, directoryPath.LastIndexOf("/"));
                DirectoryInfo dir = new DirectoryInfo(directoryPath);
                if (!dir.Exists) dir.Create();
                AssetDatabase.CreateAsset(animatorParameterReference, directoryPath + "/" + fileName + ".asset");
            }
            else AssetDatabase.CreateAsset(animatorParameterReference, folderPath + "/" + fileName + ".asset");
        }

        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }


    [MenuItem("Assets/Odd/Animator/Generate Parameters References", true)]
    private static bool SelectionIsAnimator()
    {
        return Selection.activeObject is AnimatorController;
    }
}