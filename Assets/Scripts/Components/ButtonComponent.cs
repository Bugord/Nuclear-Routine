using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ButtonComponent : MonoBehaviour
{
    [SerializeField] private ButtonPaletteSo palette;
    [SerializeField] private SoundController soundController;
    [SerializeField] private ButtonSoundPaletteSo soundPalette;
    
    private SpriteRenderer _renderer;
    private bool _isClamped;
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = palette.released;
    }

    protected virtual void OnMouseDown()
    {
        SetSpritePressed();
        soundController.Play(soundPalette.pressed);
    }

    protected virtual void OnMouseDrag()
    {
        if (!_isClamped)
        {
            _isClamped = true;
        }

        soundController.Play(soundPalette.clamped, !_isClamped);
    }

    protected virtual void OnMouseUp()
    {
        SetSpriteReleased();
        _isClamped = false;
        soundController.Play(soundPalette.released, true);
    }

    protected void SetSpritePressed()
    {
        _renderer.sprite = palette.pressed;
    }
    
    protected void SetSpriteReleased()
    {
        _renderer.sprite = palette.released;
    }
}
