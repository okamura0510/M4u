//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace M4u
{
    /// <summary>
    /// M4uToggleBinding. Bind Toggle
    /// </summary>
	[AddComponentMenu("M4u/ToggleBinding")]
    public class M4uToggleBinding : M4uBindingBool
    {
        Toggle ui;

        public override void Start()
        {
            base.Start();

            ui = GetComponent<Toggle>();
            OnChange();
        }

        public override void OnChange()
        {
            base.OnChange();

            ui.isOn = IsCheck();
        }

        public override string ToString()
        {
            return "Toggle.isOn=" + base.ToString();
        }
    }
}