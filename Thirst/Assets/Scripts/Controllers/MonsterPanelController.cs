using System.Collections;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MonsterPanelController : MonoBehaviour
    {
        public ImageAnimationController Portrait;
        public Slider HealthSlider;
        public Text AttackText;
        public Monster Monster;

        public void Redraw(Monster monster)
        {
            Monster = monster;

            if (monster == null)
            {
                gameObject.SetActive(false);
                return;
            }

            var proto = PrototypeManager.FindMonsterPrototype(monster.PrototypeName);
            if (proto == null)
            {
                gameObject.SetActive(false);
                return;
            }

            Portrait.AnimationName = monster.PortraitAnimationName;
            AttackText.text = "" + monster.Attack;
            HealthSlider.maxValue = proto.BaseHealth;
            HealthSlider.value = proto.BaseHealth - monster.Health;
            gameObject.SetActive(true);
        }

        public void StartBlink()
        {
            StartCoroutine(Blink());
        }

        private IEnumerator Blink()
        {
            var image = GetComponent<Image>();
            image.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            image.color = Color.white;
        }
    }
}