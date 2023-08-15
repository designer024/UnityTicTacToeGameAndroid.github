using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [Header("Mark Sprites ")] 
    [SerializeField] private Sprite _spriteX;
    [SerializeField] private Sprite _spriteO;
    [SerializeField] private Sprite _spriteNone;
    
    [Header("Mark Colors ")] 
    [SerializeField] private Color _colorX;
    [SerializeField] private Color _colorO;

    private Mark[] _marks;

    [SerializeField] private Camera _camera;

    private Mark _lastMark;
    private Mark _currentMark;

    public bool IsGameOver { private set; get; }
    [SerializeField] private GameObject _resultGameObject;
    [SerializeField] private Image _resultImage;
    [SerializeField] private Text _resultText;

    private int _currentStep = 0;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Init();
    }

    private void Update()
    {
        Debug.Log($"current: {_currentStep}");
        
        // check if anybody wins
        bool won = CheckIfWin();
        if (won)
        {
            IsGameOver = true;
            _resultGameObject.SetActive(IsGameOver);
            Debug.Log($"{_lastMark} Wins");
            _resultText.text = "Winner is    ";
            _resultImage.sprite = _lastMark == Mark.O ? _spriteO : _spriteX;
        }
        else if (_currentStep >= 9)
        {
            IsGameOver = true;
            Debug.Log($"平手");
            _resultGameObject.SetActive(IsGameOver);
            _resultText.text = "Tie";
            _resultImage.sprite = _spriteNone;
        }
    }

    private void Init()
    {
        _currentStep = 0;
        
        IsGameOver = false;
        _resultGameObject.SetActive(IsGameOver);
        _resultImage.sprite = _spriteNone;
        
        // X will start first
        _currentMark = Mark.X;
        
        _lastMark = Mark.None;

        if (_marks != null)
        {
            _marks = null;
        }
        _marks = new Mark[9];
    }

    public void HitBox(Box aBox)
    {
        // Debug.Log($"index: {aBox.index}");
        if (IsGameOver == false)
        {
            _currentStep += 1;
            
            if (!aBox.isMarked)
            {
                _marks[aBox.index] = _currentMark;
                aBox.SetAsMarked(GetSprite(), _currentMark);

                SwitchPlayer();
            }
        }
    }

    private bool CheckIfWin()
    {
        return 
            AreBoxesMatched(0, 1, 2) || AreBoxesMatched(3, 4, 5) || AreBoxesMatched(6, 7, 8) ||
            AreBoxesMatched(0, 3, 6) || AreBoxesMatched(1, 4, 7) || AreBoxesMatched(2, 5, 8) ||
            AreBoxesMatched(0, 4, 8) || AreBoxesMatched(2, 4, 6);
    }

    private bool AreBoxesMatched(int aIndexA, int aIndexB, int aIndexC)
    {
        _lastMark = _currentMark == Mark.X ? Mark.O : Mark.X;
        bool matched = _marks[aIndexA] == _lastMark && _marks[aIndexB] == _lastMark && _marks[aIndexC] == _lastMark;
        return matched;
    }

    private void SwitchPlayer()
    {
        _currentMark = _currentMark == Mark.X ? Mark.O : Mark.X;
    }

    private Color GetColor()
    {
        return _currentMark == Mark.X ? _colorX : _colorO;
    }

    private Sprite GetSprite()
    {
        return _currentMark == Mark.X ? _spriteX : _spriteO;
    }
}
