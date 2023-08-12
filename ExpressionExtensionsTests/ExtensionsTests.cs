using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using TripleG3.ExpressionExtensions;

namespace TripleG3.ExpressionExtensionsTests
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