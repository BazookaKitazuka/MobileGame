using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class Moves : MonoBehaviour
{
public static Moves Instance { get; private set; }

    private int _move;
    private int movesLeft;
    public int MoveCount
    {
        get => _move;
        set
        {
            if (_move == movesLeft) return;

            _move = value;

            moveCount.SetText($"Score = {_move}");
        }

    }
    [SerializeField] private TextMeshProUGUI moveCount;
    private void Awake() => Instance = this;

}
