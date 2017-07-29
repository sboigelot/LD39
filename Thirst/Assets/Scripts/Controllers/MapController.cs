using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class MapController : MonoBehaviourSingleton<MapController>
    {
        public void Redraw()
        {
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
                    AddTile(y, x, level.Tiles[y,x]);
                }
            }
        }

        private void AddTile(int y, int x, Tile tileContent)
        {
            var tile = new GameObject(string.Format("Tile[{0},{1}]", y, x));
            tile.transform.SetParent(this.transform);
            var tileController = tile.AddComponent<TileContoller>();
            tileController.Tile = tileContent;
            tileController.Redraw();
        }
    }
}