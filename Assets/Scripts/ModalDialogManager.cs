using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class ModalDialogManager : MonoBehaviour
    {
        [SerializeField] private ModalContentSO modalContent;
        [SerializeField] private ModalDialog modalDialogPrefab;
        
        [ContextMenu("Test")]
        private void Test()
        {
            Instantiate(modalDialogPrefab, new Vector3(100, 100), Quaternion.identity, transform).SetContent(modalContent);
        }
    }
}