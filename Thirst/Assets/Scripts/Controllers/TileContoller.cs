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

        public void Awake()
        {
            var lg = GetComponent<HorizontalLayoutGroup>();
            lg.childControlHeight = true;
            lg.childControlWidth = true;
            lg.childForceExpandHeight = true;
            lg.childForceExpandWidth = true;
            Redraw();
        }

        public void Redraw()
        {
            if (Tile == null)
            {
                GetComponent<Image>().color = new Color(0, 0, 0, 0);
                GetComponent<ImageAnimationController>().AnimationName = "0Anim";
                return;
            }

            GetComponent<Image>().color = Color.white;
            GetComponent<ImageAnimationController>().AnimationName = Tile.AnimationName;
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

            if (!CanUse(card.TileProto))
            {
                return;
            }

            Tile = GameManager.Instance.Level.Mermaid.UseCard(card.TileProto);
            GameManager.Instance.Level.Tiles[Y, X] = Tile;

            Redraw();
        }

        private bool CanUse(string cardTileProto)
        {
            return true; //TODO implement tile connexion check
        }
    }
}