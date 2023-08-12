using ExpressionExtensionsTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionExtensions.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void GetMemberNameTest()
        {
            //Arrange
            const string expected = nameof(Person.FirstName);
            var person = new Person("Test", "User", 50);

            string ScopedMethod(Expression<Func<Person, string>> expression)
            {
                return expression.GetMemberName();
            }

            //Act
            var actual = ScopedMethod(person => person.FirstName);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}