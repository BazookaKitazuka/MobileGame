using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void playGame()
   {
       //Change Game from title to game
        SceneManager.LoadScene("GameBoard");
   }

    public void quitGame()
   {
      Application.Quit();
   }
   public void GoBack()
   {
      //Change Game from game to title
      SceneManager.LoadScene("Main_Menu");
   }


}
