using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace OrderService.Test.Persistance
{
  public class ArchUnitPersistanceTest : ArchUnitBaseTest
  {
    [Fact]
    public void PersistanceLayerShouldNotHaveDependancyOnPresentation()
    {
      //Arrange
      ArchRuleDefinition
          .Types()
          .That().
          Are(PersistanceLayer)
          //Act
          .Should()
          .NotDependOnAny(PresentationLayer)
          //Assert
          .Check(Architecture);
    }
  }
}
