using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Bson;
using UnityEngine;

namespace DefaultNamespace
{
    public class FuelController : MonoBehaviour
    {
        [SerializeField] private float _deepChangeSpeed = 0.3f;
        
        private List<FuelScalebar> _fuelScalebars;
        private int _currentScalebarIndex;
        
        private void Awake()
        {
            _fuelScalebars = ScalebarManager.Instance.FuelScalebars;
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

        public void SelectNext()
        {
            _currentScalebarIndex++;
            if (_currentScalebarIndex >= _fuelScalebars.Count)
            {
                _currentScalebarIndex = 0;
            }
        }

        public void SelectPrev()
        {
            _currentScalebarIndex--;
            if (_currentScalebarIndex < 0)
            {
                _currentScalebarIndex = _fuelScalebars.Count - 1;
            }
        }

        public void FuelDown()
        {
            _fuelScalebars[_currentScalebarIndex].ChangeDeep(_deepChangeSpeed * Time.deltaTime);
        }

        public void FuelUp()
        {
            _fuelScalebars[_currentScalebarIndex].ChangeDeep(-_deepChangeSpeed * Time.deltaTime);
        }
    }
}