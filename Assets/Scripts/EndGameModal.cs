using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public enum LostReason
    {
        Heat,
        Steam,
        Power
    }
    
    public class EndGameModal : MonoBehaviour
    {
        [SerializeField] private ModalContentSO heatLostContent;
        [SerializeField] private ModalContentSO steamLostContent;
        [SerializeField] private ModalContentSO powerLostContent;
        [SerializeField] private ModalContentSO winContent;

        public void OpenEndDialog(bool isLost, LostReason lostReason = default)
        {
            ScalebarManager.Instance.isFreezed = true;
            
            if (!isLost)
            {
                GetComponent<ModalDialog>().SetContent(winContent);
            }
            else
            {
                switch (lostReason)
                {
                    case LostReason.Heat:
                        GetComponent<ModalDialog>().SetContent(heatLostContent);
                        break;
                    case LostReason.Steam:
                        GetComponent<ModalDialog>().SetContent(steamLostContent);
                        break;
                    case LostReason.Power:
                        GetComponent<ModalDialog>().SetContent(powerLostContent);
                        break;
                }
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}