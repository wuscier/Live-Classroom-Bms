using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool {
    class TabContentManager {

        static TabContentManager _instance = new TabContentManager();

        public static TabContentManager Instance { get { return _instance; } }

        public void AddTabPage(TabPage tabPage, TabControl tb) {
            if (!IsAddTabpages(tabPage, tb))
                tb.TabPages.Add(tabPage);
            foreach (TabPage tp in tb.Controls) {
                if (tp.Name != tabPage.Name)
                    tb.TabPages.Remove(tp);
            }
        }

        public bool IsAddTabpages(TabPage tabPage, TabControl tb) {
            foreach (TabPage tp in tb.Controls) {
                if (tp.Name == tabPage.Name)
                    return true;
            }
            return false;
        }
    }
}
