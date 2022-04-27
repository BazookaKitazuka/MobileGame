using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public sealed class Moves : MonoBehaviour
{
    
    public static Moves Instance { get; private set; }
    public GameObject Boards;

    public float _move = 0;
    
    
    public float MoveCount 
    {
        get => _move;
        set
        {
            if (_move == value) return;

            _move -= 1 ;

            moveCount.SetText($"Moves = {_move}");
        }

    }
    [SerializeField] private TextMeshProUGUI moveCount;
    private void Awake() => Instance = this;



}
