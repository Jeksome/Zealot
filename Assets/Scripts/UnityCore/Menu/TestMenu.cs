using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class TestMenu : MonoBehaviour
        {
#if UNITY_EDITOR
            public PageController pageController;
            private void Update()
            {
                if(Input.GetKeyDown(KeyCode.J))
                {
                    pageController.TurnPageOn(PageType.Controls);
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    pageController.TurnPageOff(PageType.Controls);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    pageController.TurnPageOff(PageType.Controls, PageType.MainMenu);
                }
            }
#endif
        }
    }
}

