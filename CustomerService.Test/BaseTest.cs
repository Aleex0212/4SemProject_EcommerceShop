﻿using System.Reflection;

namespace CustomerService.Test.BaseTest
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(BoardMember).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(BoardMemberCommandService).Assembly;
        protected static readonly Assembly InfrastructureAssembly = typeof(AddressValidationInf).Assembly;
        protected static readonly Assembly PersistanceAssembly = typeof(BoardMemberCommandRepo).Assembly;
        protected static readonly Assembly PresentationAssembly = typeof(BoardMemberController).Assembly;
    }
}
