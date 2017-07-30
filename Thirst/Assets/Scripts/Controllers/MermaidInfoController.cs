using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Managers;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MermaidInfoController : MonoBehaviourSingleton<MermaidInfoController>
    {
        public Slider WaterSlider;
        public Slider HealhSlider;
        public ImageAnimationController WeaponDisplay;
        public Text AttacText;
        
        public void FixedUpdate()
        {
            if (GameManager.Instance.Level == null)
            {
                return;
            }

            var mermaid = GameManager.Instance.Level.Mermaid;

            WaterSlider.maxValue = PrototypeManager.Instance.MermaidPrototype.MaxWater;
            WaterSlider.value = WaterSlider.maxValue - mermaid.WaterLevel;

            HealhSlider.maxValue = PrototypeManager.Instance.MermaidPrototype.MaxHealth;
            HealhSlider.value = HealhSlider.maxValue - mermaid.Health;

            AttacText.text = "" + mermaid.Attack;
            if (string.IsNullOrEmpty(mermaid.WeaponName))
            {
                WeaponDisplay.gameObject.SetActive(false);
            }
            else
            {
                var proto = PrototypeManager.FindWeaponPrototype(mermaid.WeaponName);
                if (proto != null)
                {
                    WeaponDisplay.AnimationName = proto.AnimationName;
                    WeaponDisplay.gameObject.SetActive(true);
                    AttacText.text = "" + (mermaid.Attack + proto.GainAttack);
                }
            }
        }
    }
}
