using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        public const string TileFolder = "Images/Tiles";

        public const string WeaponFolder = "Images/Weapon";

        public const string ItemFolder = "Images/Items";

        public const string MonsterFolder = "Images/Monsters";

        private readonly Sprite notFoundSprite;

        private readonly int pixelsPerUnit = 100;

        private readonly Dictionary<string, Sprite> sprites;

        public SpriteManager()
        {
            if (notFoundSprite == null)
            {
                // Generate a 32x32 magenta image
                var notFoundTexture2D = new Texture2D(32, 32, TextureFormat.ARGB32, false);
                var pixels = notFoundTexture2D.GetPixels32();
                for (var i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = new Color32(255, 0, 255, 255);
                }

                notFoundTexture2D.SetPixels32(pixels);
                notFoundTexture2D.Apply();
                notFoundSprite =
                    Sprite.Create(notFoundTexture2D,
                        new Rect(0, 0, notFoundTexture2D.width, notFoundTexture2D.height),
                        new Vector2(0.5f, 0.5f),
                        pixelsPerUnit);
            }

            sprites = new Dictionary<string, Sprite>();
        }

        public static IEnumerator Set(Action<Sprite> onLoaded, string folder, string key)
        {
            var fullpath = Path.Combine(Path.Combine(Application.streamingAssetsPath, folder), key + ".png");

            if (!Instance.sprites.ContainsKey(fullpath))
            {
                var sub = Instance.LoadImage(fullpath);
                foreach (var s in sub)
                {
                    yield return s;
                }
            }

            if (Instance.sprites.ContainsKey(fullpath))
            {
                var sprite = Instance.sprites[fullpath];
                onLoaded(sprite);
            }
            else
            {
                onLoaded(Instance.notFoundSprite);
            }
        }

        public static IEnumerator Set(Image image, string folder, string key)
        {
            return Set(sprite =>
            {
                image.sprite = sprite;
                image.enabled = true;
            }, folder, key);
        }

        public static IEnumerator Set(SpriteRenderer renderer, string folder, string key)
        {
            return Set(sprite =>
            {
                renderer.sprite = sprite;
                renderer.enabled = true;
            }, folder, key);
        }

        private IEnumerable LoadImage(string fullpath)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var protocol = "";
#else
            var protocol = "file:///";
#endif

            var www = new WWW(protocol + fullpath);
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogWarning("Un-able to load file: " + protocol + fullpath);
                Debug.LogError(www.error);
                yield break;
            }

            var imageBytes = www.bytes;

            var imageTexture = new Texture2D(2, 2);
            // LoadImage will correctly resize the texture based on the image file
            if (imageTexture.LoadImage(imageBytes))
            {
                imageTexture.filterMode = FilterMode.Point;
                var s = Sprite.Create(
                    imageTexture,
                    new Rect(0, 0, imageTexture.width, imageTexture.height),
                    new Vector2(0.5f, 0.5f),
                    pixelsPerUnit);
                sprites[fullpath] = s;
            }
        }
    }
}