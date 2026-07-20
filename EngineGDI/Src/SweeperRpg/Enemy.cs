using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EngineGDI.Src.SweeperRpg
{
    public class Enemy : Node, IResettable, IDrawableEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EnemyKind
        {
            [EnumMember(Value = "G_R")]
            GOBLIN_ROGUE,

            [EnumMember(Value = "G_A")]
            GOBLIN_ARCHER,

            [EnumMember(Value = "G_M")]
            GOBLIN_MAGE,

            [EnumMember(Value = "G_C")]
            GOBLIN_BARBARIC,

            [EnumMember(Value = "G_B1")]
            GOBLIN_BOSS_1,

            [EnumMember(Value = "G_B2")]
            GOBLIN_BOSS_2,

            [EnumMember(Value = "G_B3")]
            GOBLIN_BOSS_3,
        }

        private enum State
        {
            ALIVE,
            DEAD,
        }

        private class EnemyData
        {
            public Point point;
            public int damage;
        }

        private Point position;

        private readonly Transform transform = new Transform();
        public Transform Transform
        {
            get { return transform; }
        }

        private readonly Image tile;
        private readonly Renderer renderer;
        private readonly Collisioner collisioner;

        private static readonly Dictionary<EnemyKind, EnemyData> enemyData =
            JsonConvert.DeserializeObject<Dictionary<EnemyKind, EnemyData>>(
                File.ReadAllText("Assets/32rogues/monsters.json")
            );

        private readonly int damage;
        public int Damage
        {
            get { return damage; }
        }

        public event Action<Enemy> OnDefeated;

        private State state = State.ALIVE;

        public Collisioner Collisioner
        {
            get { return collisioner; }
        }

        public Enemy(int x, int y, EnemyKind kind)
        {
            enemyData.TryGetValue(kind, out EnemyData data);

            damage = data.damage;

            tile = TileMap.LoadSprite(
                path: "Assets/32rogues/monsters.png",
                row: data.point.X,
                column: data.point.Y
            );

            renderer = new Renderer(texture: tile, baseSize: new Size(32, 32));

            position = new Point(x, y);

            transform.Position = new Vector2(position.X * 32, position.Y * 32);

            collisioner = new Collisioner(
                transform: transform,
                size: new Size(32, 32),
                brushColor: Color.BlanchedAlmond
            );
        }

        public void Defeat()
        {
            state = State.DEAD;
            OnDefeated?.Invoke(this);
        }

        public bool IsAlive()
        {
            return state == State.ALIVE;
        }

        public void Reset()
        {
            state = State.ALIVE;
            collisioner.Reset();
        }

        public override void Draw()
        {
            renderer.Draw(transform);
        }
    }
}
