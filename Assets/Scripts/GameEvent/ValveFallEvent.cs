using System;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using DG.Tweening;
using ScriptableObjects;
using UnityEditor.UIElements;
using UnityEngine;

namespace GameEvent
{
    public class ValveFallEvent : BaseGameEvent
    {
        [SerializeField] private GameObject _valveGameObject;
        [SerializeField] private GameObject _valveDynamicGameObject;
        private Vector3 _valveInitialPosition;
        
        protected override void OnEventStart()
        {
            base.OnEventStart();
            _valveInitialPosition = _valveGameObject.transform.position;
            _valveDynamicGameObject.transform.GetChild(0).rotation = _valveGameObject.transform.GetChild(0).rotation;
            _valveGameObject.SetActive(false);
            _valveDynamicGameObject.SetActive(true);

            ScalebarManager.Instance.WaterScalebar.SetParameterId(1);
        }

        protected override void OnEventEnd()
        {
            // _valveDynamicGameObject.transform.DORotate(Vector3.zero, 0.5f);
            // _valveDynamicGameObject.transform.DOMove(_valveInitialPosition, 0.5f).OnComplete(() =>
            // {
            //     _valveGameObject.SetActive(true);
            //     _valveDynamicGameObject.SetActive(false);
            // });
            //
            // ScalebarManager.Instance.WaterScalebar.SetParameterId(0);

            base.OnEventEnd();
        }
    }
}