using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MapController : MonoBehaviourSingleton<MapController>
    {
        public List<TileContoller> TileControllers;

        public void Redraw()
        {
            TileControllers = new List<TileContoller>();
            this.transform.ClearChildren();

            var level = GameManager.Instance.Level;
            if (level == null)
            {
                return;
            }

            for (int y = 0; y < level.Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < level.Tiles.GetLength(1); x++)
                {
                    var tileController = AddTile(y, x, level.Tiles[y,x]);
                    TileControllers.Add(tileController);
                }
            }
        }

        private TileContoller AddTile(int y, int x, Tile tileContent)
        {
            var tile = new GameObject(string.Format("Tile[{0},{1}]", y, x));
            tile.transform.SetParent(this.transform);
            var tileController = tile.AddComponent<TileContoller>();
            tileController.X = x;
            tileController.Y = y;
            tileController.Tile = tileContent;
            tileController.Redraw();
            tile.GetComponent<DropZone>().StealDropParentality = false;
            return tileController;
        }

        public void RedrawAround(int xo, int yo)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    var tileController = TileControllers.FirstOrDefault(tc => tc.X == xo + x && tc.Y == yo + y);
                    if (tileController != null)
                    {
                        tileController.Redraw();
                    }
                }
            }
        }

        public void TestForTrappedState()
        {
            if (TileControllers.Any(tc => 
                tc.imageAnimationController.AnimationName == "0Anim" &&
                tc.image.color == Color.white))
            {
                return;
            }

            var tileControllerOfExit = TileControllers.FirstOrDefault(
                tc =>
                    tc.X == GameManager.Instance.Level.ExitLocationX &&
                    tc.Y == GameManager.Instance.Level.ExitLocationY);

            if (tileControllerOfExit == null)
            {
                return;
            }

            if (tileControllerOfExit.Tile == null)
            {
                GameController.Instance.GameOver(GameOverReason.Trap);
            }
        }
    }
}