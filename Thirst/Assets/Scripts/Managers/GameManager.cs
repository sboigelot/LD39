using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Level CurrentLevel { get; set; }

        public void NewGame(Level level)
        {
            CurrentLevel = level;
            LevelController.Instance.StartLevel(CurrentLevel);
        }
    }
}