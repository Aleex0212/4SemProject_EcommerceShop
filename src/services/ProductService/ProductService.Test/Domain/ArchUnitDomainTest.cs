using ArchUnitNET.Fluent;

namespace ProductService.Test.Domain
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
        public void DomainLayerShouldNotHaveDependancyOnInfrastructure()
        {
            //Arrange
            ArchRuleDefinition
                .Types()
                .That().
                Are(DomainLayer)
                //Act
                .Should()
                .NotDependOnAny(InfrastructureLayer)
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
        ///// <summary>
        ///// test EF
        ///// </summary>
        //[Fact]
        //public void EntitiesShouldHavePrivateConstructorWithoutParameters()
        //{
        //    //Arrange
        //    IEnumerable<Class> entityTypes = ArchRuleDefinition
        //        .Classes()
        //        .That()
        //        .AreAssignableTo(typeof(Entity))
        //        .GetObjects(Architecture);

        //    var failingTypes = new List<Class>();
        //    //Act
        //    foreach (Class entityType in entityTypes)
        //    {
        //        IEnumerable<MethodMember> construtors = entityType.GetConstructors();
        //        if (!construtors.Any(c => c.Visibility == Visibility.Internal && !c.Parameters.Any()))
        //        {
        //            failingTypes.Add(entityType);
        //        }
        //    }
        //    //Assert
        //    Assert.True(failingTypes.Count() == 0, "Not all entities in domain layer have a private and" +
        //        "parameterless constructor");
        //}
    }
}
