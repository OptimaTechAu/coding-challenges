using NSubstitute;
using SupermarketCheckout.Lambda;
using SupermarketCheckout.Lambda.Interfaces;
using SupermarketCheckout.Lambda.Models;

namespace SupermarketCheckout.Tests;

public class FunctionTests
{
    private readonly ICheckout _mockCheckout;
    private readonly Function _function;
    private readonly TestLambdaContext _context;

    public FunctionTests()
    {
        _mockCheckout = Substitute.For<ICheckout>();
        _mockCheckout.SessionId.Returns("test-session-id");
        _mockCheckout.GetTotalPrice().Returns(0);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(new Dictionary<string, int>());

        _function = new Function((sessionId, logger) => _mockCheckout);
        _context = new TestLambdaContext();
    }

    [Fact]
    public async Task FunctionHandler_EmptyItems_ReturnsZeroTotalAndSessionId()
    {
        var request = new CheckoutRequest { Items = Array.Empty<string>() };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal(0, response.TotalPrice);
        Assert.Equal("test-session-id", response.SessionId);
    }

    [Fact]
    public async Task FunctionHandler_SingleItem_CallsScanAndReturnsTotal()
    {
        _mockCheckout.GetTotalPrice().Returns(50);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(new Dictionary<string, int> { { "A", 1 } });
        var request = new CheckoutRequest { Items = ["A"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal(50, response.TotalPrice);
        _mockCheckout.Received(1).Scan("A");
    }

    [Fact]
    public async Task FunctionHandler_MultipleItems_ScansEachItem()
    {
        _mockCheckout.GetTotalPrice().Returns(115);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(new Dictionary<string, int>
        {
            { "A", 1 }, { "B", 1 }, { "C", 1 }, { "D", 1 }
        });
        var request = new CheckoutRequest { Items = ["A", "B", "C", "D"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal(115, response.TotalPrice);
        _mockCheckout.Received(1).Scan("A");
        _mockCheckout.Received(1).Scan("B");
        _mockCheckout.Received(1).Scan("C");
        _mockCheckout.Received(1).Scan("D");
    }

    [Fact]
    public async Task FunctionHandler_WithSpecialOffers_ReturnsDiscountedTotal()
    {
        _mockCheckout.GetTotalPrice().Returns(175);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(new Dictionary<string, int>
        {
            { "A", 3 }, { "B", 2 }
        });
        var request = new CheckoutRequest { Items = ["A", "A", "A", "B", "B"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal(175, response.TotalPrice); // 130 + 45
    }

    [Fact]
    public async Task FunctionHandler_InvalidItem_ReturnsError()
    {
        _mockCheckout.When(x => x.Scan("X")).Throw(new ArgumentException("Unknown item: X"));
        var request = new CheckoutRequest { Items = ["X"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.False(response.Success);
        Assert.Contains("Unknown item", response.Error);
    }

    [Fact]
    public async Task FunctionHandler_ReturnsItemBreakdown()
    {
        var breakdown = new Dictionary<string, int> { { "A", 2 }, { "B", 1 } };
        _mockCheckout.GetTotalPrice().Returns(130);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(breakdown);
        var request = new CheckoutRequest { Items = ["A", "B", "A"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.NotNull(response.ItemBreakdown);
        Assert.Equal(2, response.ItemBreakdown["A"]);
        Assert.Equal(1, response.ItemBreakdown["B"]);
    }

    [Fact]
    public async Task FunctionHandler_NullItems_ReturnsZeroTotal()
    {
        var request = new CheckoutRequest { Items = null! };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal(0, response.TotalPrice);
        Assert.Equal("test-session-id", response.SessionId);
        _mockCheckout.DidNotReceive().Scan(Arg.Any<string>());
    }

    [Fact]
    public async Task FunctionHandler_WithProvidedSessionId_UsesProvidedSessionId()
    {
        var sessionId = "custom-session-123";
        _mockCheckout.SessionId.Returns(sessionId);
        _mockCheckout.GetTotalPrice().Returns(50);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(new Dictionary<string, int> { { "A", 1 } });
        var request = new CheckoutRequest { SessionId = sessionId, Items = ["A"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal(sessionId, response.SessionId);
    }

    [Fact]
    public async Task FunctionHandler_WithoutSessionId_UsesCheckoutSessionId()
    {
        _mockCheckout.GetTotalPrice().Returns(50);
        _mockCheckout.GetAllSkusAndCountsAsync().Returns(new Dictionary<string, int> { { "A", 1 } });
        var request = new CheckoutRequest { Items = ["A"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.True(response.Success);
        Assert.Equal("test-session-id", response.SessionId);
    }

    [Fact]
    public async Task FunctionHandler_InvalidItemWithSessionId_ReturnsSessionIdInError()
    {
        var sessionId = "error-session-456";
        _mockCheckout.When(x => x.Scan("X")).Throw(new ArgumentException("Unknown item: X"));
        var request = new CheckoutRequest { SessionId = sessionId, Items = ["X"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.False(response.Success);
        Assert.Equal(sessionId, response.SessionId);
    }

    [Fact]
    public async Task FunctionHandler_UnexpectedException_ReturnsGenericError()
    {
        _mockCheckout.When(x => x.Scan("A")).Throw(new InvalidOperationException("Something broke"));
        var request = new CheckoutRequest { Items = ["A"] };
        var response = await _function.FunctionHandler(request, _context);

        Assert.False(response.Success);
        Assert.Equal("An error occurred processing the checkout", response.Error);
    }
}
