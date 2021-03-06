using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FuelController : MonoBehaviour
{
    private Dictionary<SterjenGroup, List<SterjenController>> _sterjenControllers;
    [SerializeField] private List<SterjenController> allSterjenControllers;
    private Dictionary<SterjenGroup, float> _sterjenGroupDeep;

    [SerializeField] private float _deepChangeSpeed = 0.5f;

    private HeatScalebar _heatScalebar;
    [SerializeField] private float maxIntensity, minIntensity;
    [SerializeField] private SoundController radiationSoundController;
    [SerializeField] private RadiationSoundPaletteSo radiationPalette;

    private bool _isStuck;
    private SterjenGroup _stuckGroup;
    private void Awake()
    {
        _sterjenGroupDeep = new Dictionary<SterjenGroup, float>
        {
            {SterjenGroup.Red, Random.Range(.3f, .6f)},
            {SterjenGroup.Green, Random.Range(.3f, .6f)},
            {SterjenGroup.Blue, Random.Range(.3f, .6f)}
        };

        allSterjenControllers = GetComponentsInChildren<SterjenController>().ToList();

        _sterjenControllers = new Dictionary<SterjenGroup, List<SterjenController>>
        {
            {
                SterjenGroup.Red,
                GetComponentsInChildren<SterjenController>().Where(x => x.SterjenGroup == SterjenGroup.Red).ToList()
            },
            {
                SterjenGroup.Green,
                GetComponentsInChildren<SterjenController>().Where(x => x.SterjenGroup == SterjenGroup.Green).ToList()
            },
            {
                SterjenGroup.Blue,
                GetComponentsInChildren<SterjenController>().Where(x => x.SterjenGroup == SterjenGroup.Blue).ToList()
            }
        };

        _heatScalebar = ScalebarManager.Instance.HeatScalebar;
    }

    private void Start()
    {
        allSterjenControllers.ForEach(x => x.ChangeDeep(_sterjenGroupDeep[x.SterjenGroup]));
    }

    private void Update()
    {
        if (_heatScalebar.Value >= 0.7f)
        {
            var heatLevel = (_heatScalebar.Value - 0.7f) / 0.3f;
            var intensity = Mathf.Lerp(minIntensity, maxIntensity, heatLevel);
            radiationSoundController.Play(radiationPalette.GetRadiationClip(heatLevel), 1, false, true);
            foreach (var sterjenController in allSterjenControllers)
            {
                sterjenController.DoPerlin(intensity, heatLevel);
            }
        }
    }

    public float GetTotalDeep()
    {
        return 3 - _sterjenGroupDeep.Sum(x => x.Value);
    }

    public void FuelDown(SterjenGroup sterjenGroup)
    {

        _sterjenGroupDeep[sterjenGroup] =
            Mathf.Clamp01(_sterjenGroupDeep[sterjenGroup] + _deepChangeSpeed * Time.deltaTime);

        _sterjenControllers[sterjenGroup].ForEach(x => x.ChangeDeep(_sterjenGroupDeep[sterjenGroup]));
    }

    public void FuelUp(SterjenGroup sterjenGroup)
    {
        if(_isStuck && sterjenGroup == _stuckGroup)
            return;
        
        _sterjenGroupDeep[sterjenGroup] =
            Mathf.Clamp01(_sterjenGroupDeep[sterjenGroup] - _deepChangeSpeed * Time.deltaTime);

        _sterjenControllers[sterjenGroup].ForEach(x => x.ChangeDeep(_sterjenGroupDeep[sterjenGroup]));
    }

    public void StuckRods(SterjenGroup sterjenGroup)
    {
        _isStuck = true;
        _stuckGroup = sterjenGroup;
    }
}