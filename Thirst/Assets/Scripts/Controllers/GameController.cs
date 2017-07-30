using System.Collections;
using System.Linq;
using System.Xml;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.DialogBoxes;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        public GameObject StartGamePanel;

        public void Awake()
        {
            StartGamePanel.SetActive(true);
            StartCoroutine(
                    PrototypeManager.Instance.LoadPrototypes(OnPrototypeLoaded)
                );
        }

        private void OnPrototypeLoaded()
        {
            RestartGame();
            //DialogBoxManager.Instance.Show(typeof(MainMenuController));
        }

        public void RestartGame()
        {
            GameManager.Instance.NewGame(new Level());
            HandPanelController.Instance.Redraw();
            MapController.Instance.Redraw();
        }

        public void GameOver(GameOverReason reason)
        {
            GameManager.Instance.GameOver(reason);
            DialogBoxManager.Instance.Show(typeof(EndGameDialogBox), reason);
        }

        public void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            if (GameManager.Instance.Level == null)
            {
                return;
            }

            int my = 0;
            int mx = 0;

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                my--;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SUAnim";
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                my++;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SDAnim";
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                mx--;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SLAnim";
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                mx++;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SRAnim";
            }

            if (my != 0 || mx != 0)
            {
                GameManager.Instance.Level.Mermaid.Move(mx, my);
                MonstersPanelController.Instance.RedrawMonstersSurroundingMermaid();
            }
        }
    }
}