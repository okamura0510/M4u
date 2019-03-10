//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using System;
using System.Reflection;

namespace M4u
{
    /// <summary>
    /// M4uBinding. Bind core script
    /// </summary>
	public class M4uBinding : MonoBehaviour
    {
        [M4uDisable] public string MasterPath = "";

        M4uContextRoot root;
        string[] paths;
        object[] values;
        object[] objs;
        PropertyInfo[] pis;
        FieldInfo[] fis;

        public M4uContextRoot Root { get { return root;   } set { root   = value; } }
        public string[] Paths      { get { return paths;  } set { paths  = value; } }
        public object[] Values     { get { return values; } set { values = value; } }

        public virtual void Awake()
        {
            root = GetRoot(transform, true);
        }

        public virtual void Start()
        {
            var isEventBinding  = (this is M4uEventBinding);
            var isEventBindings = (this is M4uEventBindings);
            var isMasterPath    = (this is M4uMasterPath);
            if(!isEventBinding && !isEventBindings     && !isMasterPath &&
                root != null   && root.Context != null && paths[0] != "")
            {
                objs = new object[values.Length];
                pis  = new PropertyInfo[values.Length];
                fis  = new FieldInfo[values.Length];
                for(var i = 0; i < paths.Length; i++)
                {
                    var path = MasterPath + paths[i];
                    try
                    {
                        object parent = root.Context;
                        var names     = path.Split('.');
                        for(var j = 0; j < names.Length; j++)
                        {
                            var isLast = (j == names.Length - 1);
                            var name   = names[j];
                            ParseMember(isLast, ref name, ref parent, ref values[i], ref objs[i], ref pis[i], ref fis[i]);
                            if(isLast)
                            {
                                var pname = name[0].ToString().ToLower() + name.Substring(1);
                                var pfi   = parent.GetType().GetField(pname, M4uConst.BindingAttr);
                                var p     = (M4uPropertyBase)pfi.GetValue(parent);
                                if(!p.Bindings.Contains(this)) p.Bindings.Add(this);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.LogError(gameObject.name + ":" + path);
                        throw e;
                    }
                }
            }
        }

        public virtual void Update() { }

        public virtual void OnChange()
        {
            if(objs == null) return;

            for(var i = 0; i < objs.Length; i++)
            {
                values[i] = GetMember(objs[i], pis[i], fis[i]);
            }
        }

        public M4uContextRoot GetRoot()
        {
            return GetRoot(transform);
        }

        public M4uContextRoot GetRoot(Transform t, bool isMasterCheck = false)
        {
            if(t == null) return null;

            if(isMasterCheck)
            {
                var mp = t.GetComponent<M4uMasterPath>();
                if(mp != null) MasterPath = mp.Path + "." + MasterPath;
            }

            var root = t.GetComponent<M4uContextRoot>();
            return root ?? GetRoot(t.parent, isMasterCheck);
        }

        public void ParseMember(bool isLast, ref string name, ref object parent, ref object lastValue, ref object lastObj, ref PropertyInfo lastPi, ref FieldInfo lastFi)
        {
            PropertyInfo pi = null;
            FieldInfo fi    = null;
            object value    = null;
            GetMemberInfo(ref name, ref parent, ref pi, ref fi, ref value);
            if(isLast)
            {
                lastValue = value;
                lastObj   = parent;
                lastPi    = pi;
                lastFi    = fi;
            }
            else
            {
                parent = value;
            }
        }

        public void GetMemberInfo(ref string name, ref object parent, ref PropertyInfo pi, ref FieldInfo fi, ref object value)
        {
            Type type = parent.GetType();
            {
                pi = type.GetProperty(name, M4uConst.BindingAttr);
                if(pi != null) value = pi.GetValue(parent, null);
            }
            if(pi == null)
            {
                fi = type.GetField(name, M4uConst.BindingAttr);
                if(fi != null) value = fi.GetValue(parent);
            }
        }

        public void SetMember(object obj, PropertyInfo pi, FieldInfo fi, object value)
        {
            if(pi != null)
            {
                pi.SetValue(obj, value, null);
            }
            else if(fi != null)
            {
                fi.SetValue(obj, value);
            }
        }

        public object GetMember(object obj, PropertyInfo pi, FieldInfo fi)
        {
            return GetMember<object>(obj, pi, fi);
        }

        public T GetMember<T>(object obj, PropertyInfo pi, FieldInfo fi) where T : class
        {
            if(pi != null)
            {
                return pi.GetValue(obj, null) as T;
            }
            else if(fi != null)
            {
                return fi.GetValue(obj) as T;
            }
            return null;
        }

        public string[] GetBindStrs(string[] path)
        {
            if(path == null || path.Length == 0) return null;

            var parent = (root != null && root.Context != null) ? (root.Context.GetType().Name + ".") : "";
            var strs   = new string[path.Length];
            for(var i = 0; i < strs.Length; i++)
            {
                strs[i] = "[" + parent + MasterPath + path[i] + "]";
            }
            return strs;
        }

        public string GetBindStr(string path)
        {
            var parent = (root != null && root.Context != null) ? (root.Context.GetType().Name + ".") : "";
            return "[" + parent + MasterPath + path + "]";
        }
    }
}