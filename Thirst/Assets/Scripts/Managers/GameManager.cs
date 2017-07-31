using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Level Level { get; private set; }

        public int Difficulty = 1;

        public float DifficultySpawnMonsterModifier = .025f;

        public void NewGame(Level level)
        {
            Level = level;
        }

        public void StopLevel()
        {
            Level = null;
        }

        public void GameOver(GameOverReason reason)
        {
            if (reason == GameOverReason.Victory)
            {
                Difficulty++;
            }
            else
            {
                Difficulty = 1;
            }

            StopLevel();
        }
    }
}