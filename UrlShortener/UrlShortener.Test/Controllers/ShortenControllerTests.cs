using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using UrlShortener.Controllers;
using UrlShortener.DTO;
using UrlShortener.EntityFramework;

namespace UrlShortener.Test.Controllers
{
    [TestFixture]
    public class ShortenControllerTests
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
        public void ShortenController_PostUrlForShortening_ReturnOkAndShortUrl()
        {
            // Arrange
            var shortenController = new ShortenController(dbContext);
            var originalUrl = "http://www.google.com";
            var shortenUrlDto = new ShortenUrlDTO { Url = originalUrl };

            // Act
            var actionResult = shortenController.Post(shortenUrlDto);

            // Assert
            actionResult.Should().NotBeNull();
            var okResult = actionResult as OkObjectResult;
            okResult.Should().NotBeNull();
            var shortenedUrlDto = okResult.Value as ShortenedUrlDTO;
            shortenedUrlDto.Should().NotBeNull();
            shortenedUrlDto.Original_link.Should().Be(originalUrl);
            dbContext.ShortUrls.Any(url => url.OriginalUrl == originalUrl).Should().BeTrue();
        }
    }
}
