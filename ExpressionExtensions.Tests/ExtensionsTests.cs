using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace TripleG3.ExpressionExtensions.Tests;

[TestClass]
public class ExtensionsTests
{
    [TestMethod]
    public void GetMemberNameTest()
    {
        //Arrange
        const string expected = nameof(Person.FirstName);
        var person = new Person("Test", "User", 50);

        static string ScopedMethod(Expression<Func<Person, string>> expression)
        {
            return expression.GetMemberName();
        }

        //Act
        var actual = ScopedMethod(person => person.FirstName);

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JoinTest()
    {
        //Arrange
        var people = new List<Person>
        {
            new("A", "Z", default),
            new("B", "Z", default),
            new("C", "ZZ", default),
            new("D", "ZZ", default),
            new("E", "Z", default),
            new("F", "ZZ", default)
        };

        var employees = new List<Employee>
        {
            new("A", "Z", default, "Cashier"),
            new("B", "Z", default, "Cashier"),
            new("C", "ZZ", default, "Bagger"),
            new("D", "ZZ", default, "Bagger"),
            new("E", "Z", default, "Manager"),
            new("F", "ZZ", default, "Stocker")
        };

        //Act
        var result = people.Join(employees,
                                   person => person.LastName,
                                   employee => employee.LastName,
                                   (Person, Employee) => new { Person, Employee },
                                   (a, b) => a == b);

        //Assert
        CollectionAssert.AreEquivalent(result.Select(x => x.Person.LastName).ToList(), result.Select(x => x.Employee.LastName).ToList());
    }
}