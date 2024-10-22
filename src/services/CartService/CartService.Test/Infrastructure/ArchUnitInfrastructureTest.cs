using ArchUnitNET.Fluent;

namespace CartService.Test.Infrastructure
{
    public class ArchUnitInfrastructureTest : ArchUnitBaseTest
    {
        [Fact]
        public void InfrastructureLayerShouldNotHaveDependancyOnPresentation()
        {
            //Arrange
            ArchRuleDefinition
                .Types()
                .That().
                Are(InfrastructureLayer)
                //Act
                .Should()
                .NotDependOnAny(PresentationLayer)
                //Assert
                .Check(Architecture);
        }

        [Fact]
        public void InfrastructureLayerShouldNotHaveDependancyOnPersistance()
        {
            //Arrange
            ArchRuleDefinition
                .Types()
                .That().
                Are(InfrastructureLayer)
                //Act
                .Should()
                .NotDependOnAny(PersistanceLayer)
                //Assert
                .Check(Architecture);
        }

    }
}
