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

        public void Awake()
        {
            var lg = GetComponent<HorizontalLayoutGroup>();
            lg.childControlHeight = true;
            lg.childControlWidth = true;
            lg.childForceExpandHeight = true;
            lg.childForceExpandWidth = true;
        }

        public void Redraw()
        {
            
        }
    }
}