using System;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ScanerController : MonoBehaviour
    {
        public RectTransform Needle;

        public float destinationRotationZ;
        public float timeSinceLastTest;
        public float NeedleSpeed = 1f;
        public Vector3 rotationEuler;

        public void Scan()
        {
            var level = GameManager.Instance.Level;
            if (level == null)
            {
                return;
            }

            var mermaid = level.Mermaid;
            if (mermaid == null)
            {
                return;
            }

            if (!mermaid.ConsumeWater(10))
            {
                gameObject.SetActive(false);
                return;
            }

            var distance = Vector2.Distance(
                new Vector2(level.ExitLocationX, level.ExitLocationY),
                new Vector2(mermaid.X, mermaid.Y));

            var maxDistance = Vector2.Distance(
                Vector2.zero,
                new Vector2(level.Tiles.GetLength(1), level.Tiles.GetLength(0)) 
            ) - 1;

            var distancePercent = distance / maxDistance;
            var actualDegree = distancePercent * 184 - 92;
            destinationRotationZ = actualDegree;
            timeSinceLastTest = 0;

            GameController.Instance.PlaySound(GameController.Instance.Scan);
        }

        public void Update()
        {
            var difference = (rotationEuler.z + 92) - (destinationRotationZ + 92);

            if (Math.Abs(difference) < NeedleSpeed * 2f)
            {
                return;
            }

            var directionalSpeed = difference < 0 ? NeedleSpeed : -NeedleSpeed;
            rotationEuler += Vector3.forward * directionalSpeed;
            Needle.rotation = Quaternion.Euler(rotationEuler);
        }
    }
}