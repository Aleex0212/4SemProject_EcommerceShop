using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using ProductService.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Test.Application
{
    public class ArchUnitApplicationTest : ArchUnitBaseTest
    {
        [Fact]
        public void ApplicationLayerShouldNotHaveDependancyOnInfrastructure()
        {
            //Arrange
            ArchRuleDefinition
                .Types()
                .That().
                Are(ApplicationLayer)
                //Act
                .Should()
                .NotDependOnAny(InfrastructureLayer)
                //Assert
                .Check(Architecture);
        }

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
