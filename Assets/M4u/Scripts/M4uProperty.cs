//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
namespace M4u
{
    /// <summary>
    /// M4uProperty. Data Binding to View
    /// </summary>
    /// <typeparam name="T">type</typeparam>
	public class M4uProperty<T> : M4uPropertyBase
    {
        T value;

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                // ViewModel->View
                for(var i = Bindings.Count - 1; i >= 0; i--)
                {
                    var binding = Bindings[i];
                    if(binding == null)
                    {
                        Bindings.RemoveAt(i);
                    }
                    else
                    {
                        binding.OnChange();
                    }
                }
            }
        }

        public M4uProperty() { }
        public M4uProperty(T value) { Value = value; }
    }
}