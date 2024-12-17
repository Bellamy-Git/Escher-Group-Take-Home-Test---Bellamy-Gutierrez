using NUnit.Framework;
using System;
using BellamyGutierrezEscher;

[TestFixture]
public class BusinessRulesTests
{
    [TestCase("2005-01-01", 2024, 19)] //Age 19
    [TestCase("2010-12-31", 2024, 13)] //Age 13
    [TestCase("2024-01-01", 2024, 0)]  //Age 0
    public void CalculateAge_ValidDates_CorrectAge(string dateOfBirth, int currentYear, int expectedAge)
    {
        // Arrange
        DateTime dob = DateTime.Parse(dateOfBirth);

        // Act
        int age = BusinessRules.CalculateAge(dob);

        // Assert
        Assert.AreEqual(expectedAge, age);
    }

    [TestCase("2010-01-01", true, false)] //Minor, no authorization
    [TestCase("2010-01-01", false, false)] //Minor, no authorization
    [TestCase("2007-01-01", true, true)] //Minor with authorization
    [TestCase("2000-01-01", false, true)] //Adult
    public void IsRegistrationAllowed_ValidCases_CorrectResult(string dateOfBirth, bool parentalAuthorization, bool expectedResult)
    {
        // Arrange
        var person = new Person { DateOfBirth = DateTime.Parse(dateOfBirth) };

        // Act
        bool result = BusinessRules.IsRegistrationAllowed(person, parentalAuthorization);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
