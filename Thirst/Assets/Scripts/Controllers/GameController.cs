using System.Collections;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {        
        public void Awake()
        {
            StartCoroutine(
                PrototypeManager.Instance.LoadPrototypes(OnPrototypeLoaded)
                );
        }

        private void OnPrototypeLoaded()
        {
            GameManager.Instance.NewGame(new Level());
            HandPanelController.Instance.Redraw();
            //DialogBoxManager.Instance.Show(typeof(MainMenuController));
            //DialogBoxManager.Instance.Show(typeof(ObjectiveMenuController));
        }
        
        public void GameOver(bool victory)
        {
            GameManager.Instance.StopLevel();
            //DialogBoxManager.Instance.Show(typeof(EndGameController), victory);
        }
    }
}