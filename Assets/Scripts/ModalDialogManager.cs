using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ModalDialogManager : MonoBehaviour
    {
        [SerializeField] private ModalContentSO modalContent;
        [SerializeField] private ModalDialog modalDialogPrefab;
        [SerializeField] private ModalDialog modalDialog;

        [SerializeField] private List<ModalContentSO> tutorialDialogs;
        private int _currentTutorialId;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = FindObjectOfType<Canvas>();
            modalDialog.Closed += OnModalDialogClosed;
        }

        private void OnModalDialogClosed()
        {
            LoadNextDialog();
        }

        [ContextMenu("Test")]
        private void Test()
        {
            LaunchTutorial();
        }

        public void LaunchTutorial()
        {
            ScalebarManager.Instance.isFreezed = true;
            _currentTutorialId = -1;
            LoadNextDialog();
        }

        private void LoadNextDialog()
        {
            _currentTutorialId++;
            if (_currentTutorialId == tutorialDialogs.Count)
            {
                ScalebarManager.Instance.isFreezed = false;
                return;
            }
            modalDialog.SetContent(tutorialDialogs[_currentTutorialId]);
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
    }
}