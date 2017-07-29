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

        public string LastFrameSet;

        public void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        //TODO this is super dirty, change if time during LD39 - it will keep looping on still frame, 
        //we don't want to use animation for still images
        public void FixedUpdate()
        {
            if (string.IsNullOrEmpty(AnimationName))
            {
                spriteRenderer.enabled = false;
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
            if (frame != LastFrameSet)
            {
                StartCoroutine(SpriteManager.Set(spriteRenderer, "Images", frame));
                LastFrameSet = frame;
            }
        }
    }
}