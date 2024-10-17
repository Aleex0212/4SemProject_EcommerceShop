using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using ProductService.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Test.Infrastructure
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
