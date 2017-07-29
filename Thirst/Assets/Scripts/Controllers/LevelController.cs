using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class LevelController : MonoBehaviourSingleton<LevelController>
    {
        public Level Level { get; private set; }
        
        public void StartLevel(Level level)
        {
            Level = level;
            RebuildChildren();
        }

        public void StopLevel()
        {
            Level = null;
        }

        public void RebuildChildren()
        {
         
        }

        public void FixedUpdate()
        {
            if (GameController.Instance.IsGamePaused)
                return;

            if (Level != null)
            {
               
            }
        }
    }
}