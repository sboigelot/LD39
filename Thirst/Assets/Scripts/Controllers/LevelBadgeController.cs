using Assets.Scripts.Managers;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class LevelBadgeController : MonoBehaviourSingleton<LevelBadgeController>
    {
        public Text[] Texts;

        public void Update()
        {
            foreach (var text in Texts)
            {
                text.text = "" + GameManager.Instance.Difficulty;
            }
        }
    }
}