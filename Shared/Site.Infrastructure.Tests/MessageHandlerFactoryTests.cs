using System;
using NUnit.Framework;
using Site.Application.Interfaces.Messaging;

namespace Site.Infrastructure.Tests
{
    public class MessageHandlerFactoryTests
    {
        private IMessageHandlerFactory _factory;
        private IServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void Setup()
        {

        }
        

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}