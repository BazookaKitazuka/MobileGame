using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    private int _score;

    public int ScoreCount
    {
        get => _score;
        set
        {
            if (_score == value) return;

            _score = value;

            scoreBoard.SetText($"Score = {_score}");
        }
       
    }
    [SerializeField] private TextMeshProUGUI scoreBoard;
    private void Awake() => Instance = this;
    

}
