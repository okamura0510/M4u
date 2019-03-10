//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace M4u
{
    /// <summary>
    /// M4uHierarchyIcon. Show/Hide Hierarchy Icon
    /// </summary>
    [InitializeOnLoad]
    public class M4uHierarchyIcon
    {
        static readonly string IconSearchName = "icon_b_0";
        static readonly int IconCount         = 26;
        static readonly string IconPrefsKey   = "m4u_show_hierarchy_icon";

        static string resPath;
        static string[] iconPaths = { "{0}/icon_r_{1}.png", "{0}/icon_b_{1}.png" };
        static Texture[,] icons   = new Texture[2, IconCount];

        static bool IsShowIcon
        {
            get
            {
                return (PlayerPrefs.GetInt(IconPrefsKey, 0) == 1);
            }
            set
            {
                PlayerPrefs.SetInt(IconPrefsKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        static M4uHierarchyIcon()
        {
            ShowHierarchyIcon(IsShowIcon);
        }

        [MenuItem("Tools/M4u/Show Hierarchy Icon")]
        public static void ShowHierarchyIcon()
        {
            ShowHierarchyIcon(true);
        }

        [MenuItem("Tools/M4u/Hide Hierarchy Icon")]
        public static void HideHierarchyIcon()
        {
            ShowHierarchyIcon(false);
        }

        static void ShowHierarchyIcon(bool isShow)
        {
            resPath = GetResPath(new DirectoryInfo(Application.dataPath));

            IsShowIcon = isShow;
            if(isShow)
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
            }
            else
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
            }
        }

        static string GetResPath(DirectoryInfo current)
        {
            var fi = current.GetFiles().FirstOrDefault(f => f.Name.Contains(IconSearchName));
            if(fi != null) return current.FullName;

            foreach(var di in current.GetDirectories())
            {
                var path = GetResPath(di);
                if(path != null) return path;
            }
            return null;
        }

        static void OnHierarchyWindowItemOnGUI(int id, Rect rect)
        {
            var go = EditorUtility.InstanceIDToObject(id) as GameObject;
            if(go == null) return;

            var idx = 0;
            foreach(var c in go.GetComponents<Component>())
            {
                var isRoot = (c is M4uContextRoot);
                var isBind = (c is M4uBinding);
                if(isRoot || isBind)
                {
                    idx++;

                    var rootId = id;
                    if(isBind)
                    {
                        var bind = c as M4uBinding;
                        var root = bind.GetRoot();
                        rootId   = (root != null) ? root.gameObject.GetInstanceID() : 0;
                    }
                    rootId = Mathf.Abs(rootId);

                    var iconType = (isRoot) ? 0 : 1;
                    var colorIdx = rootId % IconCount;
                    if(icons[iconType, colorIdx] == null)
                    {
                        var path = string.Format(iconPaths[iconType], resPath, colorIdx);
                        icons[iconType, colorIdx] = M4uUtil.CreateTexture2D(File.ReadAllBytes(path));
                    }

                    var icon     = icons[iconType, colorIdx];
                    var irect    = new Rect(rect);
                    irect.x     += rect.width - icon.width * idx;
                    irect.width  = icon.width;
                    irect.height = icon.height;

                    GUI.DrawTexture(irect, icon);
                }
            }
        }
    }
}