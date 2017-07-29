using UnityEngine;

namespace Assets.Scripts.Managers.DialogBoxes
{
    public abstract class ContextualDialogBoxBase<T, CT> : MonoBehaviour, IDialogBox where T: new() where CT: new()
    {
        protected ContextualDialogBoxBase()
        {
            IsModal = true;
            DialogBoxManager.Instance.Register(typeof(T), this);
        }

        public bool IsModal { get; protected set; }

        public bool IsOpen { get; set; }

        public void OpenDialog(object context)
        {
            gameObject.SetActive(true);
            OnScreenOpen((CT) context);
            IsOpen = true;
        }

        protected abstract void OnScreenOpen(CT context);

        public void CloseDialog()
        {
            gameObject.SetActive(false);
            IsOpen = false;
        }
    }
}