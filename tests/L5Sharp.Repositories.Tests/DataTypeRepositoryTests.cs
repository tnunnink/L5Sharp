using System;
using System.IO;
using NUnit.Framework;

namespace L5Sharp.Repositories.Tests
{
    [TestFixture]
    public class DataTypeRepositoryTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private LogixContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new LogixContext(_fileName);
        }
        
        
        /*[Test]
        public void GetInclude_WhenCalled_ShouldReturnExpected()
        {
            var repo = new DataTypeRepository(_context);

            var dataTypes = repo.GetAllInclude(d => d.Members);
        }*/
    }
}