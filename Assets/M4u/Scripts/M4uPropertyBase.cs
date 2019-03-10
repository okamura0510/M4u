//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// M4uPropertyBase. Data Binding to View
    /// </summary>
	public class M4uPropertyBase
    {
        List<M4uBinding> bindings = new List<M4uBinding>();

        public List<M4uBinding> Bindings
        {
            get { return bindings;  }
            set { bindings = value; }
        }
    }
}