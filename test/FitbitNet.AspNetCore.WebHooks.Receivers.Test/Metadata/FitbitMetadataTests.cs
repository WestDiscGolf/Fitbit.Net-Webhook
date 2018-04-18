using FitbitNet.AspNetCore.WebHooks.Receivers.Metadata;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Xunit;

namespace FitbitNet.AspNetCore.WebHooks.Receivers.Test.Metadata
{
    public class FitbitMetadataTests
    {
        [Fact]
        public void CorrectRecieverName()
        {
            // Arrange
            var sut = new FitbitMetadata(null, null);

            // Act
            var result = sut.ReceiverName;

            // Assert
            Assert.Equal("fitbit", result);
        }

        [Fact]
        public void CorrectBodyType()
        {
            // Arrange
            var sut = new FitbitMetadata(null, null);

            // Act
            var result = sut.BodyType;

            // Assert
            Assert.Equal(WebHookBodyType.Json, result);
        }
    }
}
