using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public sealed class Moves : MonoBehaviour
{
    
    public static Moves Instance { get; private set; }
   
    public float _move = 0;
    
    
    public float MoveCount 
    {
        get => _move;
        set
        {
            if (_move == value) return;

            _move -= 1 ;

            moveCount.SetText($" {_move}");
        }

    }
    [SerializeField] private TextMeshProUGUI moveCount;
    private void Awake() => Instance = this;
    public void Start()
    {
        moveCount.SetText(_move.ToString());
    }
  
    public void Update()
    {
        if (_move == 0)
        {
            SceneManager.LoadScene("LoserScreen"); 

        }
    }

}
