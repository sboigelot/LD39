using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(ImageAnimationController))]
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class TileContoller : DropZone
    {
        public int Y;
        public int X;
        public Tile Tile;

        public ImageAnimationController OnTileDisplay;

        private Image image;
        private ImageAnimationController imageAnimationController;

        public void Awake()
        {
            var lg = GetComponent<HorizontalLayoutGroup>();
            lg.childControlHeight = true;
            lg.childControlWidth = true;
            lg.childForceExpandHeight = true;
            lg.childForceExpandWidth = true;

            image = GetComponent<Image>();
            imageAnimationController = GetComponent<ImageAnimationController>();

            Redraw();
        }

        public void Redraw()
        {
            if (Tile == null)
            {
                image.color = TileIsAccessible() ? Color.white : new Color(0, 0, 0, 0);
                imageAnimationController.AnimationName = "0Anim";
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
            onTileDisplay.transform.SetParent(this.transform);
            return onTileDisplay.AddComponent<ImageAnimationController>();
        }

        public override void OnDrop(Draggable draggable)
        {
            var card = draggable as CardController;
            if (card == null)
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
        }
    }
}