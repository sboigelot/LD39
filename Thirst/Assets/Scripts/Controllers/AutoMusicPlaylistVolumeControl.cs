using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class AutoMusicPlaylistVolumeControl : MonoBehaviour
    {
        public Slider Slider;

        public void Update()
        {
            AutoMusicPlaylist.Instance.SetVolume(Slider.value);
        }
    }
}