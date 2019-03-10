//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;

namespace M4u
{
    /// <summary>
    /// M4uActiveBinding. Bind GameObject.activeSelf
    /// </summary>
	[AddComponentMenu("M4u/ActiveBinding")]
	public class M4uActiveBinding : M4uBindingBool
	{
		GameObject ui;

        public override void Start()
        {
            base.Start();

            ui = gameObject;
            OnChange();
        }

        public override void OnChange()
        {
            base.OnChange();

            ui.SetActive(IsCheck());
        }

        public override string ToString()
        {
            return "GameObject.active=" + base.ToString();
        }
	}
}