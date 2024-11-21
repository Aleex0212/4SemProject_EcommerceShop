using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace OrderService.Test.Domain
{
  public class ArchUnitDomainTest : ArchUnitBaseTest
  {
    [Fact]
    public void DomainLayerShouldNotHaveDependancyOnApplication()
    {
      //Arrange
      ArchRuleDefinition
          .Types()
          .That().
          Are(DomainLayer)
          //Act
          .Should()
          .NotDependOnAny(ApplicationLayer)
          //Assert
          .Check(Architecture);
    }

    [Fact]
    public void DomainLayerShouldNotHaveDependancyOnPersistance()
    {
      //Arrange
      ArchRuleDefinition
          .Types()
          .That().
          Are(DomainLayer)
          //Act
          .Should()
          .NotDependOnAny(PersistanceLayer)
          //Assert
          .Check(Architecture);
    }

    [Fact]
    public void DomainLayerShouldNotHaveDependancyOnPresentation()
    {
      //Arrange
      ArchRuleDefinition
          .Types()
          .That().
          Are(DomainLayer)
          //Act
          .Should()
          .NotDependOnAny(PresentationLayer)
          //Assert
          .Check(Architecture);
    }
  }
}
