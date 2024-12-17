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
        //parses the input string DOB into a Datetime object
        DateTime dob = DateTime.Parse(dateOfBirth);

        //calls CalculatedAge method and stores that result in int age
        int age = BusinessRules.CalculateAge(dob);

        //verifies that the calculated age matches the expected age
        Assert.AreEqual(expectedAge, age);
    }

    //test cases
    [TestCase("2010-01-01", true, false)] //Minor, no authorization so result: false
    [TestCase("2010-01-01", false, false)] //Minor, no authorization so result: false
    [TestCase("2007-01-01", true, true)] //Minor with authorization so result: true
    [TestCase("2000-01-01", false, true)] //Adult, so result: true
    public void IsRegistrationAllowed_ValidCases_CorrectResult(string dateOfBirth, bool parentalAuthorization, bool expectedResult)
    {
        //creates a person object with the given DOB
        var person = new Person { DateOfBirth = DateTime.Parse(dateOfBirth) };

        // Act - calls the method under test
        bool result = BusinessRules.IsRegistrationAllowed(person, parentalAuthorization);

        // Asserttion - verifies that the result matches the expected output
        Assert.AreEqual(expectedResult, result);
    }
}
