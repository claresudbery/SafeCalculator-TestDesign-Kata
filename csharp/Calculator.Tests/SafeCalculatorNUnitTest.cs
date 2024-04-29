using Calculator;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

using Moq;

namespace TestCalculator;

[TestFixture]
public class SafeCalculatorNUnitTest
{
    [Test]
    public void DoNotThrowWhenAuthorized()
    {
        // Arrange
        var authorizer = new Mock<IAuthorizer>();
        authorizer.Setup(m => m.Authorize()).Returns(true);
        var safeCalculator = new SafeCalculator(authorizer.Object);
        
        // Act
        var result = safeCalculator.Add(1, 2);
        
        // Assert
        Assert.Equals(3, result);
    }
    
    [Test]
    public void ThrowWhenNotAuthorized()
    {
        // Arrange
        var authorizer = new Mock<IAuthorizer>();
        authorizer.Setup(m => m.Authorize()).Returns(false);
        var safeCalculator = new SafeCalculator(authorizer.Object);
        
        // Act & Assert
        Assert.Throws<Exception>(() => safeCalculator.Add(1, 2));
    }
}