using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationController : MonoBehaviour
    {
        public string AnimationName;

        public ushort FrameIndex;

        public float LastFrameChange;

        private SpriteRenderer spriteRenderer;

        public void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void FixedUpdate()
        {
            if (string.IsNullOrEmpty(AnimationName))
            {
                return;
            }

            var animationPrototype = PrototypeManager.FindAnimationPrototype(AnimationName);
            if (animationPrototype == null)
            {
                return;
            }

            if (LastFrameChange + animationPrototype.FrameDelay > Time.time)
            {
                return;
            }

            LastFrameChange = Time.time;
            FrameIndex++;
            if (FrameIndex >= animationPrototype.Frames.Count)
            {
                FrameIndex = 0;
            }

            var frame = animationPrototype.Frames[FrameIndex];
            StartCoroutine(SpriteManager.Set(spriteRenderer, "Images", frame));
        }
    }
}