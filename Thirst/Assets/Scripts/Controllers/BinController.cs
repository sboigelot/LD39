using Assets.Scripts.Managers;
using Assets.Scripts.UI;

namespace Assets.Scripts.Controllers
{
    public class BinController : DropZone
    {
        public override void OnDrop(Draggable draggable)
        {
            base.OnDrop(draggable);
            var card = draggable as CardController;
            if (card == null)
            {
                return;
            }

            GameManager.Instance.Level.Mermaid.BinCard(card.TileProto);
            HandPanelController.Instance.Redraw();
        }
    }
}