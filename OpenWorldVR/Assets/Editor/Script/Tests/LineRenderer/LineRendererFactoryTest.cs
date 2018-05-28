using Assets.Script.Src.LineRenderer;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.LineRenderer
{
    public class LineRendererFactoryTest {
        private LineRendererFactory _lineRenderFactory;

        [SetUp]
        public void BeforeEveryTest()
        {
            _lineRenderFactory = new LineRendererFactory(); 
        }

        [Test]
        public void GivenANumberOfWantedLineRenderers_WhenCreatingLineRenderers_ThenWantedNumberOfLineRenders()
        {
            const int numberOfLineRenderers = 50;

            var lineRenderers = _lineRenderFactory.CreateUnityLineRenderers(numberOfLineRenderers, new GameObject().transform, null, 1);

            Assert.AreEqual(numberOfLineRenderers, lineRenderers.Length);
        }

        [Test]
        public void WhenCreatingLineRenderers_ThenLineRenderersAreCreated()
        {
            const int aNumberOfLineRenderers = 21;

            var lineRenderers = _lineRenderFactory.CreateUnityLineRenderers(aNumberOfLineRenderers, new GameObject().transform, null, 1);

            Assert.IsNotNull(lineRenderers[lineRenderers.Length - 1]);
        }
    }
}
