//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;

namespace M4u
{
    /// <summary>
    /// M4uContextRoot. M4u core script
    /// </summary>
	[AddComponentMenu("M4u/ContextRoot")]
    public class M4uContextRoot : MonoBehaviour
    {
        [M4uDisable] public string ContextName;
        public M4uContextMonoBehaviour ContextMonoBehaviour;

        M4uContextInterface context;

        /// <summary>
        /// Context. Data Binding to View
        /// </summary>
		public M4uContextInterface Context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
                if(context != null) ContextName = context.ToString();
            }
        }

        void Awake()
        {
            if(ContextMonoBehaviour != null) Context = ContextMonoBehaviour;
        }
    }
}