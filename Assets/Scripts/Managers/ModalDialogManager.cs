using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameEvent;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace
{
    public class ModalDialogManager : MonoBehaviour
    {
        public static ModalDialogManager Instance;
        [SerializeField] private ModalContentSO modalContent;
        [SerializeField] private ModalDialog modalDialogPrefab;
        [SerializeField] private ModalDialog modalDialog;

        [SerializeField] private List<ModalContentSO> tutorialDialogs;
        private int _currentTutorialId;
        private Canvas _canvas;
        private bool _isTutorial;
        private Action _closeCallback;

        public bool isTutorialPassed;

        [SerializeField] private GameObject tutorialButton;
        [SerializeField] private GameObject skipTutorialButton;
        
        private void Awake()
        {
            Instance = this;
            _canvas = FindObjectOfType<Canvas>();
            modalDialog.Closed += OnModalDialogClosed;

            if (!Preferences.TutorialWasShown)
            {
                StartCoroutine(StartTutorial());
            }
        }

        public void OpenModal(ModalContentSO content, Action callback = null)
        {
            if(GameOverController.Instance.isLost)
                return;
            
            ScalebarManager.Instance.isFreezed = true;
            tutorialButton.SetActive(false);
            modalDialog.SetContent(content);
            _closeCallback = callback;
        }

        private void OnModalDialogClosed()
        {
            _closeCallback?.Invoke();
            
            LoadNextDialog();
            
            if (isTutorialPassed)
            {
                tutorialButton.SetActive(true);
                ScalebarManager.Instance.isFreezed = false;
            }
        }

        [ContextMenu("Test")]
        private void Test()
        {
            LaunchTutorial();
        }

        public void LaunchTutorial()
        {
            skipTutorialButton.SetActive(true);
            tutorialButton.SetActive(false);
            _closeCallback = null;
            ScalebarManager.Instance.isFreezed = true;
            _isTutorial = true;
            isTutorialPassed = false;
            _currentTutorialId = -1;
            LoadNextDialog();
        }

        private void LoadNextDialog()
        {
            if (_isTutorial)
            {
                _currentTutorialId++;
                if (_currentTutorialId == tutorialDialogs.Count)
                {
                    _isTutorial = false;
                    isTutorialPassed = true;
                    Preferences.TutorialWasShown = true;
                    skipTutorialButton.SetActive(false);
                    tutorialButton.SetActive(true);
                    return;
                }

                modalDialog.SetContent(tutorialDialogs[_currentTutorialId]);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                modalDialog.SetContent(modalContent);
                // Canvas.ForceUpdateCanvases();
                // StartCoroutine(Kostyl());
                // Instantiate(modalDialogPrefab, new Vector3(100, 100), Quaternion.identity, _canvas.transform).SetContent(modalContent);
            }
        }
        
        

        IEnumerator Kostyl()
        {
            yield return new WaitForEndOfFrame();
            modalDialog.transform.Translate(new Vector3(10, 10));
        }

        private IEnumerator StartTutorial()
        {
            yield return new WaitForSeconds(1);
            LaunchTutorial();
        }

        public void SkipTutorial()
        {
            isTutorialPassed = true;
            _isTutorial = false;
            Preferences.TutorialWasShown = true;
            modalDialog.CloseDialog();
            skipTutorialButton.SetActive(false);
            tutorialButton.SetActive(true);
        }
    }
}