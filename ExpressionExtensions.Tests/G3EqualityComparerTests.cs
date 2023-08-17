using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Text;

namespace TripleG3.ExpressionExtensions.Tests;

[TestClass()]
public class G3EqualityComparerTests
{
    [TestMethod()]
    public void EqualsTest()
    {
        //Arrange
        var personA = new Person("A", "Person", default);
        var actor = new G3EqualityComparer<Person>((a, b) => a == b);

        //Act
        var actual = actor.Equals(personA, personA);

        //Assert
        Assert.IsTrue(actual);
    }

    [TestMethod()]
    public void NotEqualsTest()
    {
        //Arrange
        var personA = new Person("A", "Person", default);
        var personB = new Person("B", "Person", default);
        var actor = new G3EqualityComparer<Person>((a, b) => a == b);

        //Act
        var actual = actor.Equals(personA, personB);

        //Assert
        Assert.IsFalse(actual);
    }

    [TestMethod()]
    public void GetHashCodeTest()
    {
        //Arrange
        var personA = new Person("A", "Person", default);
        var expected = personA.GetHashCode();
        var actor = new G3EqualityComparer<Person>((a, b) => a == b, personA.GetHashCode);

        //Act
        var actual = actor.GetHashCode(personA);

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void NewInstanceTest()
    {
        //Arrange
        var personA = new Person("A", "Person", default);
        var personB = new Person("B", "Person", default);
        var expected = new Person[] { personA, personB };
        var people = expected.Append(personA);

        var actor = new G3EqualityComparer<Person>((a, b) => a == b);

        //Act
        var actual = people.Distinct(actor);

        //Assert
        CollectionAssert.AreEquivalent(expected, actual.ToArray());
    }

    [TestMethod]
    public void ExplicitFuncTest()
    {
        //Arrange
        var personA = new Person("A", "Person", default);
        var personB = new Person("B", "Person", default);
        var expected = new Person[] { personA, personB };
        var people = expected.Append(personA);

        var actor = (Person? personA, Person? personB) => personA == personB;

        //Act
        var actual = people.Distinct((G3EqualityComparer<Person>)actor);

        //Assert
        CollectionAssert.AreEquivalent(expected, actual.ToArray());
    }

    [TestMethod]
    public void ImplicitFuncTest()
    {
        //Arrange
        var personA = new Person("A", "Person", default);
        var personB = new Person("B", "Person", default);
        var expected = new Person[] { personA, personB };
        var people = expected.Append(personA);

        IEnumerable<Person> distinct(IEnumerable<Person> values, G3EqualityComparer<Person> comparer)
            => values.Distinct(comparer);

        var actor = (Person? personA, Person? personB) => personA == personB;

        //Act
        var actual = distinct(people, actor);

        //Assert
        CollectionAssert.AreEquivalent(expected, actual.ToArray());
    }

    [TestMethod]
    public void ExplicitLambdaCompareLastNameTest()
    {
        //Arrange
        var personA = new Person("A", "Z", default);
        var personB = new Person("B", "Z", default);
        var personC = new Person("C", "ZZ", default);
        var actor = (G3EqualityComparer<Person>)(Expression<Func<Person?, Person?, bool>>)((a, b) => a!.LastName == b!.LastName);

        //Act
        var actualTrue = actor.Equals(personA, personB);
        var actualFalse = actor.Equals(personA, personC);

        //Assert
        Assert.IsTrue(actualTrue);
        Assert.IsFalse(actualFalse);
    }

    [TestMethod]
    public void ImplicitLambdaCompareLastNameTest()
    {
        //Arrange
        var personA = new Person("A", "Z", default);
        var personB = new Person("B", "Z", default);
        var personC = new Person("C", "ZZ", default);
        bool isEqual(G3EqualityComparer<Person> comparer, Person a, Person b) => comparer.Equals(a, b);
        bool lambdaIsEqual(Expression<Func<Person?, Person?, bool>> lambda, Person a, Person b) => isEqual(lambda, a, b);

        //Act
        var actualTrue = lambdaIsEqual((a, b) => a!.LastName == b!.LastName, personA, personB);
        var actualFalse = lambdaIsEqual((a, b) => a!.LastName == b!.LastName, personA, personC);

        //Assert
        Assert.IsTrue(actualTrue);
        Assert.IsFalse(actualFalse);
    }
}