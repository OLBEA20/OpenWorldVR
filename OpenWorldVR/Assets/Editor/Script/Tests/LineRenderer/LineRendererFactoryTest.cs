using UnityEngine;
using NUnit.Framework;
using NSubstitute;

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
        int numberOfLineRenderers = 50;

        var lineRenderers = _lineRenderFactory.CreateLineRenderers(numberOfLineRenderers);

        Assert.AreEqual(numberOfLineRenderers, lineRenderers.Length);
    }

    [Test]
    public void WhenCreatingLineRenderers_ThenLineRenderersAreCreated()
    {
        int aNumberOfLineRenderers = 21;

        var lineRenderers = _lineRenderFactory.CreateLineRenderers(aNumberOfLineRenderers);

        Assert.IsNotNull(lineRenderers[lineRenderers.Length - 1]);
    }

    [Test]
    public void GivenNoParameters_WhenCreatingLineRenderer_ThenDefaultValuesAreUsed()
    {
        float defaultStartWidth = 0.51f;
        float defaultEndWWidth = 0.31f;
        bool enabled = true;

        var lineRenderer = _lineRenderFactory.CreateLineRenderer(null, defaultStartWidth, defaultEndWWidth, enabled);

        Assert.AreEqual(defaultStartWidth, lineRenderer.startWidth);
        Assert.AreEqual(defaultEndWWidth, lineRenderer.endWidth);
        Assert.AreEqual(enabled, lineRenderer.enabled);
    }

}
