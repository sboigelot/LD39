using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Networking;
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
        }

        public override void OnDrop(Draggable draggable)
        {
            var card = draggable as CardController;
            if (card == null)
            {
                return;
            }
            Tile = GameManager.Instance.Level.Mermaid.UseCard(card.TileProto);
            
            Redraw();
        }
    }
}