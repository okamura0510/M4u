//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System.Reflection;

namespace M4u
{
    /// <summary>
	/// M4uSpecialBindings. Bind Anything
    /// </summary>
	[AddComponentMenu("M4u/SpecialBindings")]
    public class M4uSpecialBindings : M4uBindingMultiple
    {
        public string[] TargetPath;

        object[] obj;
        PropertyInfo[] pi;
        FieldInfo[] fi;

        public override void Start()
        {
            base.Start();

            if(TargetPath != null && TargetPath.Length > 0)
            {
                obj = new object[TargetPath.Length];
                pi  = new PropertyInfo[obj.Length];
                fi  = new FieldInfo[obj.Length];
                for(var i = 0; i < TargetPath.Length; i++)
                {
                    string[] names = TargetPath[i].Split('.');
                    object parent  = GetComponent(names[0]);
                    object value   = null;
                    for(int j = 1; j < names.Length; j++)
                    {
                        var isLast = (j == names.Length - 1);
                        var mname  = names[j];
                        ParseMember(isLast, ref mname, ref parent, ref value, ref obj[i], ref pi[i], ref fi[i]);
                    }
                }
            }
            OnChange();
        }

        public override void OnChange()
        {
            base.OnChange();

            if(obj != null)
            {
                for(var i = 0; i < obj.Length; i++)
                {
                    SetMember(obj[i], pi[i], fi[i], Values[i]);
                }
            }
        }

        public override string ToString()
        {
            var str = "";
            if(TargetPath != null)
            {
                var binds = GetBindStrs(Path);
                for(var i = 0; i < TargetPath.Length; i++)
                {
                    str += (str == "") ? "" : "/";
                    str += TargetPath[i] + "=";
                    str += (i < binds.Length) ? binds[i] : "";
                }
            }
            return str;
        }
    }
}