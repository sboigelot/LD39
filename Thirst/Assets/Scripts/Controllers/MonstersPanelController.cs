using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MonstersPanelController : MonoBehaviourSingleton<MonstersPanelController>
    {
        public MonsterPanelController[] MonsterPanels;

        public void RedrawMonstersSurroundingMermaid()
        {
            if (GameManager.Instance.Level == null)
            {
                return;
            }

            var mermaid = GameManager.Instance.Level.Mermaid;
            RedrawSurroundingMonsters(mermaid.X, mermaid.Y);
        }

        private void RedrawSurroundingMonsters(int centerX, int centerY)
        {
            var tileToScan = new List<Tile>();
            var addTile = new Action<int,int>((x, y) =>
            {
                if (GameManager.Instance.Level.TileExist(x, y))
                {
                    tileToScan.Add(GameManager.Instance.Level.Tiles[y, x]);
                }
            });

            addTile(centerX, centerY - 1);
            addTile(centerX, centerY + 1);
            addTile(centerX - 1, centerY);
            addTile(centerX + 1, centerY);

            var monsters = tileToScan.
                Where(t => t.Monster != null).
                Select(t => t.Monster).
                ToList();

            foreach (var monsterPanelController in MonsterPanels)
            {
                var monster = monsters.FirstOrDefault();
                monsterPanelController.Redraw(monster);
                var image = monsterPanelController.GetComponent<Image>();
                image.color = Color.white;
                if (monsters.Any())
                {
                    monsters.RemoveAt(0);
                }
            }
        }
    }
}