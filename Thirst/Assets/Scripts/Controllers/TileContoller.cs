using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(ImageAnimationController))]
    public class TileContoller : DropZone
    {
        public int Y;
        public int X;
        public Tile Tile;

        public ImageAnimationController OnTileDisplay;

        public Image image;
        public ImageAnimationController imageAnimationController;

        public void Awake()
        {
            image = GetComponent<Image>();
            imageAnimationController = GetComponent<ImageAnimationController>();

            Redraw();
        }

        public void Redraw()
        {
            if (GameManager.Instance.Level == null)
            {
                return;
            }

            if (false)
            {
                //Cheat mode: see the exit
                if (X == GameManager.Instance.Level.ExitLocationX &&
                    Y == GameManager.Instance.Level.ExitLocationY)
                {
                    image.color = Color.white;
                    imageAnimationController.AnimationName = "GridAnim";
                    return;
                }
            }

            if (Tile == null)
            {
                if (X == GameManager.Instance.Level.ExitLocationX &&
                    Y == GameManager.Instance.Level.ExitLocationY)
                {
                    image.color = TileHasOtherTileNext() ? Color.white : new Color(0, 0, 0, 0);
                    imageAnimationController.AnimationName = "GridAnim";
                }
                else
                {
                    image.color = TileIsAccessible() ? Color.white : new Color(0, 0, 0, 0);
                    imageAnimationController.AnimationName = "0Anim";
                }
                return;
            }

            image.color = Color.white;
            imageAnimationController.AnimationName = Tile.AnimationName;

            OnTileDisplay = OnTileDisplay ?? CreateOnTileDisplay();

            if (X == GameManager.Instance.Level.Mermaid.X &&
                Y == GameManager.Instance.Level.Mermaid.Y)
            {
                OnTileDisplay.AnimationName = GameManager.Instance.Level.Mermaid.CurrentAnimation;
                return;
            }

            if (Tile.Monster != null)
            {
                OnTileDisplay.AnimationName = Tile.Monster.AnimationName;
                return;
            }

            if (X == GameManager.Instance.Level.ExitLocationX &&
                Y == GameManager.Instance.Level.ExitLocationY)
            {
                OnTileDisplay.AnimationName = "SewerAnim";
                return;
            }

            if (!string.IsNullOrEmpty(Tile.Weapon))
            {
                var prototype = PrototypeManager.FindWeaponPrototype(Tile.Weapon);
                if (prototype != null)
                {
                    OnTileDisplay.AnimationName = prototype.AnimationName;
                }
                return;
            }


            if (!string.IsNullOrEmpty(Tile.Item))
            {
                var prototype = PrototypeManager.FindItemPrototype(Tile.Item);
                if (prototype != null)
                {
                    OnTileDisplay.AnimationName = prototype.AnimationName;
                }
                return;
            }

            if (OnTileDisplay != null)
            {
                OnTileDisplay.AnimationName = "";
            }
        }

        private bool TileHasOtherTileNext()
        {
            if (Tile != null)
            {
                return true;
            }

            var level = GameManager.Instance.Level;
            if (level == null || level.Tiles == null)
            {
                return false;
            }

            var tileConnected = level.TileExist(X - 1, Y) ||
                                level.TileExist(X + 1, Y) ||
                                level.TileExist(X, Y - 1) ||
                                level.TileExist(X, Y + 1);

            return tileConnected;
        }

        private bool TileIsAccessible()
        {
            if (Tile != null)
            {
                return true;
            }
            
            var level = GameManager.Instance.Level;
            if (level == null || level.Tiles == null)
            {
                return false;
            }
            
            var tileConnected = level.TileExist(X - 1, Y) && !level.TileHasWallTo(X - 1, Y, X, Y) ||
                                level.TileExist(X + 1, Y) && !level.TileHasWallTo(X + 1, Y, X, Y) ||
                                level.TileExist(X, Y - 1) && !level.TileHasWallTo(X, Y - 1, X, Y) ||
                                level.TileExist(X, Y + 1) && !level.TileHasWallTo(X, Y + 1, X, Y);

            return tileConnected;
        }

        private ImageAnimationController CreateOnTileDisplay()
        {
            var onTileDisplay = new GameObject("onTileDisplay");
            onTileDisplay.transform.SetParent(this.transform, false);

            var imageController = onTileDisplay.AddComponent<ImageAnimationController>();
            imageController.GetComponent<Image>().raycastTarget = false;
            var rectTransform = onTileDisplay.GetComponent<RectTransform>() ?? onTileDisplay.AddComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(.5f, 0);
            rectTransform.anchorMax = new Vector2(.5f, 0);
            rectTransform.pivot = new Vector2(.5f, 0);
            rectTransform.anchoredPosition = new Vector2(0, 0);

            return imageController;
        }

        public override void OnDrop(Draggable draggable)
        {
            var card = draggable as CardController;
            if (card == null)
            {
                return;
            }

            if (Tile != null)
            {
                return;
            }

            if (!TileIsAccessible())
            {
                return;
            }

            Tile = GameManager.Instance.Level.Mermaid.UseCard(card.TileProto);
            GameManager.Instance.Level.Tiles[Y, X] = Tile;
            MapController.Instance.RedrawAround(X, Y);
            MapController.Instance.TestForTrappedState();
            MonstersPanelController.Instance.RedrawMonstersSurroundingMermaid();
        }
    }
}