using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using OrderService.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Test.Infrastructure
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
