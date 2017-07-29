using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Managers.DialogBoxes
{
    public class DialogBoxManager : Singleton<DialogBoxManager>
    {
        private readonly Dictionary<Type, IDialogBox> screens;

        public DialogBoxManager()
        {
            this.screens = new Dictionary<Type, IDialogBox>();
        }

        public bool AnyActiveModal
        {
            get
            {
                var activeModal = screens.Values.Any(s => s.IsModal && s.IsOpen);
                return activeModal;
            }
        }

        public void Register(Type screenType, IDialogBox dialogBox)
        {
            screens[screenType] = dialogBox;
        }

        public IDialogBox Show(Type screenType)
        {
            return Show(screenType, null);
        }

        public IDialogBox Show(Type screenType, object context)
        {
            if (screens.ContainsKey(screenType))
            {
                var dialogBox = screens[screenType];
                dialogBox.OpenDialog(context);

                foreach (var otherDialogBox in screens.Values)
                {
                    if(otherDialogBox != dialogBox)
                        otherDialogBox.CloseDialog();
                }

                return dialogBox;
            }

            return null;
        }

        public void Close(Type screenType)
        {
            if (screens.ContainsKey(screenType))
            {
                var dialogBox = screens[screenType];
                dialogBox.CloseDialog();
            }
        }
    }
}
