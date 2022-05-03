using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
   public static string previousScene;

   public void goToScene(string scene)
   {
      SceneManager.LoadScene(scene);
      var currentScene = SceneManager.GetActiveScene();
      var currentSceneName = currentScene.name;
      previousScene = currentSceneName;
   }

   public void goPrev()
   {
      SceneManager.LoadScene(previousScene);
      var currentScene = SceneManager.GetActiveScene();
      var currentSceneName = currentScene.name;
      previousScene = currentSceneName;
   }

   public void Update()
   {
      var currentScene = SceneManager.GetActiveScene();
      var currentSceneName = currentScene.name;
      previousScene = currentSceneName;
   } 
   
   
    public void quitGame()
   {
      Application.Quit();
   }
}

