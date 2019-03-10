//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
namespace M4u
{
    /// <summary>
    /// M4uBindingMultiple. Bind multiple Path
    /// </summary>
	public class M4uBindingMultiple : M4uBinding
    {
        public string[] Path;

        public override void Awake()
        {
            base.Awake();

            Paths  = Path;
            Values = new object[Paths.Length];
        }
    }
}