using UnityEngine;

namespace M4u.Demo
{
    public class Monster : M4uContext
    {
        M4uProperty<string> name     = new M4uProperty<string>("");
        M4uProperty<Texture> texture = new M4uProperty<Texture>();

        public string Name     { get { return name.Value;    } set { name.Value    = value; } }
        public Texture Texture { get { return texture.Value; } set { texture.Value = value; } }
    }
}