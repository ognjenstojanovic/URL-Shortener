using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using UrlShortener.Controllers;
using UrlShortener.EntityFramework;

namespace UrlShortener.Test.Controllers
{
    [TestFixture]
    public class RedirectControllerTests
    {
        private UrlShortenerDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<UrlShortenerDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            dbContext = new UrlShortenerDbContext(options);
        }

        [Test]
        public void RedirectController_GetWithAnExistingAlias_RedirectsToAUrl()
        {
            // Arrange
            var alias = "123abc";
            var redirectUrl = "http://www.google.com";
            dbContext.ShortUrls.Add(new Models.ShortUrl { Id = Guid.NewGuid(), Alias = alias, OriginalUrl = redirectUrl });
            dbContext.SaveChanges();
            var controller = new RedirectController(dbContext);

            // Act
            var actionResult = controller.Get(alias);

            // Assert
            actionResult.Should().NotBeNull();
            var redirectResult = actionResult as RedirectResult;
            redirectResult.Should().NotBeNull();
            redirectResult.Url.Should().Be(redirectUrl);
        }

        [Test]
        public void RedirectController_GetWithANonExistingAlias_ReturnsBadRequest()
        {
            // Arrange
            var controller = new RedirectController(dbContext);

            // Act
            var actionResult = controller.Get("unknownAlias");

            // Assert
            actionResult.Should().NotBeNull();
            var badRequestResult = actionResult as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be("Url does not exist!");
        }
    }
}
