using System.Collections;
using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class PageController : MonoBehaviour
        {
            #pragma warning disable 0649
            public bool debug;
            public PageType entryPage;
            public Page[] pages;
            #pragma warning restore 0649

            private Hashtable menu_Pages;

            #region Unity Functions
            private void Awake()
            {
                 menu_Pages = new Hashtable();
                 RegisterAllPages();

                 if(entryPage != PageType.None) TurnPageOn(entryPage);
            }
            #endregion

            #region Public Functions
            public void TurnPageOn(PageType type)
            {
                if (type == PageType.None) return;

                if (!PageExists(type))
                {
                    LogWarning("You're trying to turn on unregistred [" + type + "] page!");
                    return;
                }

                Page page = GetPage(type);
                page.gameObject.SetActive(true);
                page.Animate(true);
            }

            public void TurnPageOff(PageType off, PageType on = PageType.None)
            {
                if (off == PageType.None) return;
                if (!PageExists(off))
                {
                    LogWarning("You're trying to turn a page off [" + off + "] that has not been registered");
                    return;               
                }

                Page offPage = GetPage(off);
                if (offPage.gameObject.activeSelf)
                {
                    offPage.Animate(false);
                }

                if (on != PageType.None)
                {
                    Page onPage = GetPage(on);
                    TurnPageOn(onPage.type);
                }
               
            }
            #endregion

            #region Private Functions

            private void RegisterAllPages()
            {
                foreach (Page page in pages)
                {
                    RegisterPage(page);
                }
            }

            private void RegisterPage(Page page)
            {
                if (PageExists(page.type))
                {
                    LogWarning("Page ["+page.type+"] already exists! GameObject:" +page.gameObject.name);
                    return;
                }
                menu_Pages.Add(page.type, page);
                Log("Registered new page [" + page.type + "]");
            }

            private Page GetPage(PageType type)
            {
                if (!PageExists(type))
                {
                    LogWarning("You're trying to get a page [" + type + "] that has not been registered");
                    return null;
                }
                return (Page)menu_Pages[type];
            }

            private bool PageExists(PageType type) => menu_Pages.ContainsKey(type);

            private void Log(string msg)
            {
                if (!debug) return;
                Debug.Log("[PageController]: " + msg);
            }

            private void LogWarning(string msg)
            {
                if (!debug) return;
                Debug.LogWarning("[PageController]: " + msg);
            }
            #endregion
        }
    }
}

