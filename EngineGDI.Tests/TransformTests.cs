using System.Numerics;
using EngineGDI.Src.SweeperRpg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineGDI.Tests
{
    [TestClass]
    public class TransformTests
    {
        [TestMethod]
        public void TransformStoresValuesCorrectly()
        {
            Transform transform = new Transform();

            transform.Position = new Vector2(10, 20);
            transform.Rotation = new Vector2(90, 0);
            transform.Scale = new Vector2(2, 3);

            Assert.AreEqual(new Vector2(10, 20), transform.Position);
            Assert.AreEqual(new Vector2(90, 0), transform.Rotation);
            Assert.AreEqual(new Vector2(2, 3), transform.Scale);
        }
    }
}
