using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FuelController : MonoBehaviour
{
    private Dictionary<SterjenGroup, List<SterjenController>> _sterjenControllers;
    [SerializeField] private List<SterjenController> allSterjenControllers;
    private Dictionary<SterjenGroup, float> _sterjenGroupDeep;

    [SerializeField] private float _deepChangeSpeed = 0.3f;

    private int _currentScalebarIndex;
    private SterjenGroup _currentSterjenGroup;

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

        
    }

    private void Start()
    {
        allSterjenControllers.ForEach(x => x.ChangeDeep(_sterjenGroupDeep[x.SterjenGroup]));
    }

    private void Update()
    {
        var y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectPrev();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNext();
        }

        if (y < 0)
        {
            FuelDown();
        }
        else if (y > 0)
        {
            FuelUp();
        }
    }

    public float GetTotalDeep()
    {
        return _sterjenGroupDeep.Sum(x => x.Value);
    }

    public void SelectNext()
    {
        _currentScalebarIndex++;
        if (_currentScalebarIndex >= 3)
        {
            _currentScalebarIndex = 0;
        }

        _currentSterjenGroup = (SterjenGroup) _currentScalebarIndex;
    }

    public void SelectPrev()
    {
        _currentScalebarIndex--;
        if (_currentScalebarIndex < 0)
        {
            _currentScalebarIndex = 2;
        }

        _currentSterjenGroup = (SterjenGroup) _currentScalebarIndex;
    }

    public void FuelDown()
    {
        _sterjenGroupDeep[_currentSterjenGroup] =
            Mathf.Clamp01(_sterjenGroupDeep[_currentSterjenGroup] + _deepChangeSpeed * Time.deltaTime);
        
        _sterjenControllers[_currentSterjenGroup]
            .ForEach(x => x.ChangeDeep(_sterjenGroupDeep[_currentSterjenGroup]));
    }

    public void FuelUp()
    {
        _sterjenGroupDeep[_currentSterjenGroup] =
            Mathf.Clamp01(_sterjenGroupDeep[_currentSterjenGroup] - _deepChangeSpeed * Time.deltaTime);
        
        _sterjenControllers[_currentSterjenGroup]
            .ForEach(x => x.ChangeDeep(_sterjenGroupDeep[_currentSterjenGroup]));
    }

    [ContextMenu("Fill Dictionary")]
    private void FillDictionary()
    {
       
        Debug.Log(_sterjenControllers[SterjenGroup.Red].Count);
        Debug.Log(_sterjenControllers[SterjenGroup.Green].Count);
        Debug.Log(_sterjenControllers[SterjenGroup.Blue].Count);
    }
}