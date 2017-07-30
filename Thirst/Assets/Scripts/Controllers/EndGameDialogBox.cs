using Assets.Scripts.Managers;
using Assets.Scripts.Managers.DialogBoxes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class EndGameDialogBox : ContextualDialogBoxBase<EndGameDialogBox, GameOverReason>
    {
        public Image VictoryImage;
        public Text VictoryFlavorText;

        public Sprite ThirthPicture;
        public Sprite WoundPicture;
        public Sprite EscapePicture;

        public void RestartGame()
        {
            GameController.Instance.RestartGame();
            CloseDialog();
        }

        protected override void OnScreenOpen(GameOverReason reason)
        {
            switch (reason)
            {
                case GameOverReason.Thirst:
                    VictoryImage.sprite = ThirthPicture;
                    VictoryFlavorText.text = "You ran out of water and lost all your magic power...";
                    break;
                case GameOverReason.Wound:
                    VictoryImage.sprite = WoundPicture;
                    VictoryFlavorText.text = "Lurking monsters in the sand got the last of you...";
                    break;
                case GameOverReason.Victory:
                    VictoryImage.sprite = EscapePicture;
                    VictoryFlavorText.text = "You managed to escape the beach and swim fee in the see!";
                    break;
            }
        }
    }
}