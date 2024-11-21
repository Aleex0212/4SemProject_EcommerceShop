using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace OrderService.Test.Application
{
  public class ArchUnitApplicationTest : ArchUnitBaseTest
  {
    [Fact]
    public void ApplicationLayerShouldNotHaveDependancyOnPersistance()
    {
      //Arrange
      ArchRuleDefinition
          .Types()
          .That().
          Are(ApplicationLayer)
          //Act
          .Should()
          .NotDependOnAny(PersistanceLayer)
          //Assert
          .Check(Architecture);
    }

    [Fact]
    public void ApplicationLayerShouldNotHaveDependancyOnPresentation()
    {
      //Arrange
      ArchRuleDefinition
          .Types()
          .That().
          Are(ApplicationLayer)
          //Act
          .Should()
          .NotDependOnAny(PresentationLayer)
          //Assert
          .Check(Architecture);
    }
  }
}
