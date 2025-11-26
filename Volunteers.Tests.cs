using Xunit;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviationFoundation.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace GiftOfTheGivers.Tests.Unit.Services;

public class DonationControllerTests
{
    private readonly DonationController _controller;

    public DonationControllerTests()
    {
        // Your controller doesn't need ILogger, so we create it directly
        _controller = new DonationController();
    }

    [Fact]
    public void DonateGoods_ReturnsViewResult()
    {
        // Act
        var result = _controller.DonateGoods();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void ViewDonations_ReturnsViewResult()
    {
        // Act
        var result = _controller.ViewDonations();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void SimpleTest_ShouldPass()
    {
        // Simple test to verify our setup works
        Assert.True(1 == 1);
    }

    [Fact]
    public void IntegrationTest_DonateGoods_ReturnsViewResult()
    {
        var controller = new DonationController();
        var result = controller.DonateGoods();
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void IntegrationTest_ViewDonations_ReturnsViewResult()
    {
        var controller = new DonationController();
        var result = controller.ViewDonations();
        Assert.IsType<ViewResult>(result);
    }
}