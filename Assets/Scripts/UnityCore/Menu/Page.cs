using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class Page : MonoBehaviour
        {
            public PageType type;

            public void Animate(bool on)
            {
                if (!on) gameObject.SetActive(false);
            }
        }
    }
}

