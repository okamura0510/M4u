//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;

namespace M4u
{
    /// <summary>
    /// M4uMasterPath. It's the component from which a path can be omitted.
    /// </summary>
    [AddComponentMenu("M4u/MasterPath")]
    public class M4uMasterPath : M4uBinding
    {
        public string Path;

        public override void Awake()
        {
            // Not call base.Awake(). Because MasterPath changes.
        }

        public override string ToString()
        {
            return "Path=" + Path;
        }
    }
}