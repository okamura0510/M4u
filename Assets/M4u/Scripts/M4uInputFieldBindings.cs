//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace M4u
{
    /// <summary>
    /// M4uInputFieldBindings. Bind InputField
    /// </summary>
	[AddComponentMenu("M4u/InputFieldBindings")]
    public class M4uInputFieldBindings : M4uBindingMultiple
    {
        public string Format = "";

        InputField ui;

        public override void Start()
        {
            base.Start();

            ui = GetComponent<InputField>();
            OnChange();
        }

        public override void OnChange()
        {
            base.OnChange();

            ui.text = string.Format(Format, Values);
        }

        public override string ToString()
        {
            var str = "InputField.text=";
            if(Path != null && Path.Length > 0)
            {
                str += string.Format(Format, GetBindStrs(Path));
            }
            return str;
        }
    }
}