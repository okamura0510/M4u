using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace M4u.Demo
{
    public class Demo : MonoBehaviour
    {
        public enum HeartAbility { Sun, Moon }

        public Sprite[] SpriteData;
        public Texture[] TextureData;
        public Texture MonsterTextureData;

        M4uProperty<string> message         = new M4uProperty<string>("");
        M4uProperty<Sprite> sprite          = new M4uProperty<Sprite>();
        M4uProperty<Texture> texture        = new M4uProperty<Texture>();
        M4uProperty<bool> isActive          = new M4uProperty<bool>(true);
        M4uProperty<HeartAbility> ability   = new M4uProperty<HeartAbility>();
        M4uProperty<float> x                = new M4uProperty<float>();
        M4uProperty<Vector3> scale          = new M4uProperty<Vector3>(Vector3.one);
        M4uProperty<Color> color            = new M4uProperty<Color>(Color.white);
        M4uProperty<List<Monster>> monsters = new M4uProperty<List<Monster>>(new List<Monster>());
        List<GameObject> monsterList        = new List<GameObject>();
        M4uProperty<float> progress         = new M4uProperty<float>();
        M4uProperty<Vector2> size           = new M4uProperty<Vector2>(Vector2.one);
        M4uProperty<Monster> masterMonster  = new M4uProperty<Monster>(new Monster());
        int spriteIdx;
        int textureIdx;

        public string Message               { get { return message.Value;       } set { message.Value       = value; } }
        public Sprite Sprite                { get { return sprite.Value;        } set { sprite.Value        = value; } }
        public Texture Texture              { get { return texture.Value;       } set { texture.Value       = value; } }
        public bool IsActive                { get { return isActive.Value;      } set { isActive.Value      = value; } }
        public HeartAbility Ability         { get { return ability.Value;       } set { ability.Value       = value; } }
        public float X                      { get { return x.Value;             } set { x.Value             = value; } }
        public Vector3 Scale                { get { return scale.Value;         } set { scale.Value         = value; } }
        public Color Color                  { get { return color.Value;         } set { color.Value         = value; } }
        public List<Monster> Monsters       { get { return monsters.Value;      } set { monsters.Value      = value; } }
        public List<GameObject> MonsterList { get { return monsterList;         } set { monsterList         = value; } }
        public float Progress               { get { return progress.Value;      } set { progress.Value      = value; } }
        public Vector2 Size                 { get { return size.Value;          } set { size.Value          = value; } }
        public Monster MasterMonster        { get { return masterMonster.Value; } set { masterMonster.Value = value; } }

        void Awake()
        {
            App.Instance.Demo = this;
            GetComponent<M4uContextRoot>().Context = App.Instance;
        }

        public void OnUpdate()
        {
            Message  = Random.Range(1, 100).ToString();
            Sprite   = SpriteData[spriteIdx++ % SpriteData.Length];
            Texture  = TextureData[textureIdx++ % TextureData.Length];
            IsActive = !IsActive;
            Ability  = (Ability == HeartAbility.Sun) ? HeartAbility.Moon : HeartAbility.Sun;
            X        = Random.Range(-200f, 200f);
            Scale    = new Vector3(Random.Range(0.3f, 1f), Random.Range(0.3f, 1f), 0);
            Color    = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Progress = Random.Range(0f, 1f);
            Size     = new Vector2(Random.Range(64, 128), Random.Range(64, 128));

            Monsters.Clear();
            var count = Random.Range(0, 6);
            for(var i = 0; i < count; i++)
            {
                var monster     = new Monster();
                monster.Name    = "Aruka" + Random.Range(1, 100);
                monster.Texture = MonsterTextureData;
                Monsters.Add(monster);
            }

            MasterMonster.Name    = "Amanuma" + Random.Range(1, 100);
            MasterMonster.Texture = MonsterTextureData;
        }

        public void OnNext()
        {
            var scene        = SceneManager.GetActiveScene();
            var sceneIdx     = scene.buildIndex;
            var sceneCount   = SceneManager.sceneCountInBuildSettings;
            var nextSceneIdx = (sceneIdx < sceneCount - 1) ? scene.buildIndex + 1 : 0;
            SceneManager.LoadScene(nextSceneIdx);
        }

        void OnClickEvent()
        {
            Debug.Log("OnClickEvent");
        }

        void OnChangedMonsterList()
        {
            Debug.Log("MonsterCount = " + MonsterList.Count);
        }
    }
}