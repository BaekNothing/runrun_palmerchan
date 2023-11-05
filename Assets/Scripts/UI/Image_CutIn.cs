using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_CutIn : AbstractCutIn
{
    public int Index = 0;

    public List<Sprite> Sprites = new();
    [SerializeField] EffectController _effectController;
    public EffectController EffectController
    {
        get
        {
            if (_effectController == null)
                _effectController = GetComponent<EffectController>();

            return _effectController;
        }
    }

    [SerializeField] SpriteRenderer _spriteRenderer;
    SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            return _spriteRenderer;
        }
    }

    public override void PrevAction()
    {
        base.PrevAction();

        if (Sprites.Count == 0)
            return;
        SetSprite(Index);
        ShowEffect(Index);
    }

    void SetSprite(int index)
    {
        if (Sprites.Count == 0 || index >= Sprites.Count)
            return;

        SpriteRenderer.sprite = Sprites[index];
    }

    void ShowEffect(int index)
    {
        EffectController.PlayEffect(index);
    }
}
