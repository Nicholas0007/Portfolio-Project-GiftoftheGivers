using Xunit;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviationFoundation.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace GiftOfTheGivers.Tests.Integration.TestingControllers;

public class DonationControllerIntegrationTests
{
    [Fact]
    public void DonateGoods_ReturnsViewResult()
    {
        // Arrange
        var controller = new DonationController();

        // Act
        var result = controller.DonateGoods();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void ViewDonations_ReturnsViewResult()
    {
        // Arrange
        var controller = new DonationController();

        // Act
        var result = controller.ViewDonations();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
}