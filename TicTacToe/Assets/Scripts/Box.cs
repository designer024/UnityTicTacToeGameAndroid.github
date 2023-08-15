using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] private Board _board;

    private Button _button;
    public Button GetButton => _button;
    private Image _image;
    
    public int index;
    public Mark mark;
    public bool isMarked;

    
    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        
        index = transform.GetSiblingIndex();
        mark = Mark.None;
        isMarked = false;
        
        _button.onClick.AddListener(HitBox);
    }

    private void HitBox() => _board.HitBox(this);

    public void SetAsMarked(Sprite aSprite, Mark aMark)
    {
        isMarked = true;
        mark = aMark;
        
        _image.sprite = aSprite;
    }
}
