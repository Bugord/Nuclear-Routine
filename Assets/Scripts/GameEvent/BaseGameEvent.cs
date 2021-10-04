using System;
using System.Collections;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace GameEvent
{
    public abstract class BaseGameEvent : MonoBehaviour
    {
        [SerializeField] protected float EventDuration = 5f;
        [SerializeField] protected ModalContentSO eventModalContent;
        private Action _callback;
        public float cooldown;
        
        public void StartEvent(Action callback = null)
        {
            _callback = callback;
            OnEventStart();
        }

        protected virtual void OnEventStart()
        {
            if (eventModalContent)
            {
                ModalDialogManager.Instance.OpenModal(eventModalContent, () => { StartCoroutine(EventTimer()); });
            }
            else
            {
                StartCoroutine(EventTimer());
            }
        }

        protected virtual void OnEventEnd()
        {
            _callback?.Invoke();
        }


        public IEnumerator EventTimer()
        {
            yield return new WaitForSeconds(EventDuration);
            yield return new WaitUntil(() => ModalDialogManager.Instance.isTutorialPassed);
            OnEventEnd();
        }
    }
}