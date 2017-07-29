using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerProfile Player { get; set; }

        public Level CurrentLevel { get; set; }

        public void NewGame(Level level)
        {
            Player = SaveManager.Instance.PlayerProfile;
            CurrentLevel = level;
            LevelController.Instance.StartLevel(CurrentLevel);
        }
    }
}