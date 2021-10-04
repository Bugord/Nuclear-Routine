using ScriptableObjects;
using UnityEngine;

public enum SterjenGroup
{
    Red,
    Green,
    Blue
}

public class SterjenController : MonoBehaviour
{
    public SterjenGroup SterjenGroup => _sterjenGroup;
    [SerializeField] private SterjenPaletteSo _sterjenPaletteSo;
    [SerializeField] private SterjenGroup _sterjenGroup;
    [SerializeField] private float deepDistance;
    [Header("Perlin")] 
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    private float _maxDeepPosY;
    private float _minDeepPosY;

    private float _currentDeep;
    private float _initialY;
    private float _noizeOffset;

    private void Awake()
    {
        _initialY = transform.position.y;
        _maxDeepPosY = transform.position.y;
        _minDeepPosY = transform.position.y + deepDistance;
        _noizeOffset = ((float) _sterjenGroup + 1) * 0.33f;
    }

    public void ChangeDeep(float deepValue)
    {
        _currentDeep = deepValue;
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(_minDeepPosY, _maxDeepPosY, deepValue));
    }

    public void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        switch (SterjenGroup)
        {
            case SterjenGroup.Red:
                spriteRenderer.sprite = _sterjenPaletteSo.redSterjen;
                break;
            case SterjenGroup.Blue:
                spriteRenderer.sprite = _sterjenPaletteSo.blueSterjen;
                break;
            case SterjenGroup.Green:
                spriteRenderer.sprite = _sterjenPaletteSo.greenSterjen;
                break;
        }
    }


    public void DoPerlin(float intensity, float blend)
    {
        _noizeOffset += Time.deltaTime * intensity;
        var perlinHeight = Mathf.PerlinNoise(transform.position.x + _noizeOffset, _initialY + _noizeOffset) * 1.2f - 0.08f;
        var blendedHeight = Mathf.Lerp(_currentDeep, perlinHeight, blend * 0.5f);
        var maxDeepPos = Mathf.Lerp(_maxDeepPosY, _maxDeepPosY - 0.16f, blend * 0.5f);
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(_minDeepPosY, maxDeepPos, blendedHeight));
    }
}