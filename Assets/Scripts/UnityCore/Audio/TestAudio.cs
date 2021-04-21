using UnityEngine;

namespace UnityCore
{
    namespace Audio
    {
        public class TestAudio : MonoBehaviour
        {
            public AudioController audioController;
            private void Update()
            {
                if (Input.GetKeyUp(KeyCode.F1))
                {
                    audioController.PlayAudio(AudioType.ST_01, true, 1.0f);
                }
                if (Input.GetKeyUp(KeyCode.F2))
                {
                    audioController.StopAudio(AudioType.ST_01);
                }
                if (Input.GetKeyUp(KeyCode.F3))
                {
                    audioController.RestartAudio(AudioType.ST_01);
                }
                if (Input.GetKeyUp(KeyCode.F4))
                {
                    audioController.PlayAudio(AudioType.SFX_01);
                }
                if (Input.GetKeyUp(KeyCode.F5))
                {
                    audioController.StopAudio(AudioType.SFX_01);
                }
                if (Input.GetKeyUp(KeyCode.F6))
                {
                    audioController.RestartAudio(AudioType.SFX_01);
                }
            }
        }
    }
}