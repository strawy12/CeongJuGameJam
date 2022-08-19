using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class EditorSceneMenu : MonoBehaviour
{
    [MenuItem("/Scene/Main")]
    static void EditorMenu_LoadInGameScene()
    {
        EditorSceneManager.OpenScene("Assets/01.Scenes/Main.unity");
    }

    [MenuItem("/Scene/OIF")]
    static void EditorMenu_LoadInSecondScene()
    {
        EditorSceneManager.OpenScene("Assets/01.Scenes/OIF.unity");
    }
}
