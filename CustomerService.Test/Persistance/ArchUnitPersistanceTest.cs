using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using CustomerService.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Test.Persistance
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

        [Fact]
        public void PersistanceLayerShouldNotHaveDependancyOnInfrastructure()
        {
            //Arrange
            ArchRuleDefinition
                .Types()
                .That().
                Are(PersistanceLayer)
                //Act
                .Should()
                .NotDependOnAny(InfrastructureLayer)
                //Assert
                .Check(Architecture);
        }
    }
}
