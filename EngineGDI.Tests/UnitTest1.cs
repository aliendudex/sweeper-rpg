using EngineGDI.Src.SweeperRpg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineGDI.Tests
{
    class DummyResettable : IResettable
    {
        public bool WasReset { get; private set; }

        public void Reset()
        {
            WasReset = true;
        }
    }

    [TestClass]
    public class PoolTests
    {
        [TestMethod]
        public void PoolReturnsPreviouslyAddedObject()
        {
            Pool<DummyResettable> pool = new Pool<DummyResettable>();
            DummyResettable dummy = new DummyResettable();

            pool.Add(dummy);

            DummyResettable result = pool.Get(() => new DummyResettable());

            Assert.AreSame(dummy, result);
            Assert.IsTrue(result.WasReset);
        }

        [TestMethod]
        public void PoolCreatesObjectWhenEmpty()
        {
            Pool<DummyResettable> pool = new Pool<DummyResettable>();

            DummyResettable result = pool.Get(() => new DummyResettable());

            Assert.IsNotNull(result);
        }
    }
}
