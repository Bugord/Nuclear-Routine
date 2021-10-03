using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Modal Dialog", menuName = "Modal Dialog")]
    public class ModalContentSO : ScriptableObject
    {
        public string title;
        public Sprite sprite;
        [TextArea(4,6)] public string description;
        public ModalLayout modalLayout;
    }
}