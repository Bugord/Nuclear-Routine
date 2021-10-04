using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameEvent
{
    public class GameEventManager : MonoBehaviour
    {
        [SerializeField] private List<BaseGameEvent> _gameEvents;

        [SerializeField] private int _currentEventId;

        [SerializeField] private List<BaseGameEvent> shuffledGameEvents;

        private void Start()
        {
            StartCoroutine(StartEventsCoroutine());
            shuffledGameEvents = _gameEvents.OrderBy(x => GUID.Generate()).ToList();
        }

        [ContextMenu("Start Events")]
        private void StartEvents()
        {
            shuffledGameEvents[_currentEventId].StartEvent(() => StartCoroutine(LoadNextEvent()));
        }

        private IEnumerator LoadNextEvent()
        {
            yield return new WaitForSeconds(shuffledGameEvents[_currentEventId].cooldown);
            _currentEventId++;
            if (_currentEventId < _gameEvents.Count)
            {
                shuffledGameEvents[_currentEventId].StartEvent(() => StartCoroutine(LoadNextEvent()));
            }
        }

        private IEnumerator StartEventsCoroutine()
        {
            yield return new WaitUntil(() => ModalDialogManager.Instance.isTutorialPassed);
            yield return new WaitForSeconds(20);
            yield return new WaitUntil(() => ModalDialogManager.Instance.isTutorialPassed);
            StartEvents();
        }
    }
}