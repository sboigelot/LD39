using System.Collections;
using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {        
        private bool IsGameOver;

        public bool IsGamePaused;

        public void Awake()
        {
            StartCoroutine(PrototypeManager.Instance.LoadPrototypes());

            //SaveManager.Instance.LoadProfiles();
            //DialogBoxManager.Instance.Show(typeof(MainMenuController));
        }

        public void NewGame(int level_index)
        {
            IsGameOver = false;
            
			//Hack: Don't want to store it for everyone
            //PrototypeManager.Instance.Levels[level_index].Index = level_index;

            //GameManager.Instance.NewGame((Level) PrototypeManager.Instance.Levels[level_index].Clone());
            //StartCoroutine(GameTick());
            //DialogBoxManager.Instance.Show(typeof(ObjectiveMenuController));
        }

        public IEnumerator GameTick()
        {
            while (!IsGameOver)
            {
                if (GameManager.Instance.CurrentLevel != null)
                {
                    if (IsGameWon())
                    {
                        GameOver(true);
                    }

                    if (!GameController.Instance.IsGamePaused)
                    {
                        //GameManager.Instance.CurrentLevel.Tick();
                    }
                    RebuildUi();
                }

                yield return new WaitForSeconds(1);
            }
        }

        private bool IsGameWon()
        {
			return false;
        }

        public void RebuildUi()
        {
            //GameHud.Instance.OnGameTick();
        }

        public void GameOver(bool victory)
        {
            IsGameOver = true;
            LevelController.Instance.StopLevel();
            //DialogBoxManager.Instance.Show(typeof(EndGameController), victory);
        }
    }
}