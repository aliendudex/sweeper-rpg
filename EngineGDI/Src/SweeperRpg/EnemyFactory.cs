namespace EngineGDI.Src.SweeperRpg
{
    public static class EnemyFactory
    {
        private static readonly Pool<Enemy> pool = new Pool<Enemy>();

        public static Enemy Create(int x, int y, Enemy.EnemyKind kind)
        {
            return pool.Get(() => new Enemy(x, y, kind));
        }

        public static void Return(Enemy enemy)
        {
            pool.Return(enemy);
        }
    }
}
