using NUnit.Framework;

using System;

namespace SedolValidatorTests
{
    [TestFixture]
    public class SedolValidator_UnitTests
    {

        [TestCase(null, false, false, "Input string was not 7-characters long")]
        [TestCase("", false, false, "Input string was not 7-characters long")]
        [TestCase("12", false, false, "Input string was not 7-characters long")]
        [TestCase("123456789", false, false, "Input string was not 7-characters long")]
        public void SedolValidator_InvalidInput(string input, bool expected_isValidSedol, bool expected_isUserDefined, string expected_validationDetails)
        {
            //**Scenario:**Null, empty string or string other than 7 characters long
            //InputString Test Value| IsValidSedol | IsUserDefined | ValidationDetails
            //-- -| --| --| --|
            //Null | False | False | Input string was not 7 - characters long
            //"" | False | False | Input string was not 7 - characters long
            //12 | False | False | Input string was not 7 - characters long
            //123456789 | False | False | Input string was not 7 - characters long

            ISedolValidator validator = new SedolValidator();
            ISedolValidationResult actualResult = validator.ValidateSedol(input);

            Assert.IsTrue(actualResult.InputString == input && actualResult.IsUserDefined == expected_isUserDefined && actualResult.IsValidSedol == expected_isValidSedol && actualResult.ValidationDetails == expected_validationDetails);

        }

        [TestCase("1234567", false, false, "Checksum digit does not agree with the rest of the input")]
        public void SedolValidator_InvalidNonUserDefined(string input, bool expected_isValidSedol, bool expected_isUserDefined, string expected_validationDetails)
        {
            //**Scenario:**Invalid non user define SEDOL
            //InputString Test Value | IsValidSedol | IsUserDefined | ValidationDetails
            //-- -| --| --| --|
            //1234567 | False | False | Checksum digit does not agree with the rest of the input

            ISedolValidator validator = new SedolValidator();
            ISedolValidationResult actualResult = validator.ValidateSedol(input);

            Assert.IsTrue(actualResult.InputString == input && actualResult.IsUserDefined == expected_isUserDefined && actualResult.IsValidSedol == expected_isValidSedol && actualResult.ValidationDetails == expected_validationDetails);

        }

        [TestCase("0709954", true, false, null)]
        [TestCase("B0YBKJ7", true, false, null)]
        public void SedolValidator_ValidNonUserDefined(string input, bool expected_isValidSedol, bool expected_isUserDefined, string expected_validationDetails)
        {
            //**Scenario:**Valid non user define SEDOL
            //InputString Test Value | IsValidSedol | IsUserDefined | ValidationDetails
            //-- -| --| --| --|
            //0709954 | True | False | Null
            //B0YBKJ7 | True | False | Null

            ISedolValidator validator = new SedolValidator();
            ISedolValidationResult actualResult = validator.ValidateSedol(input);

            Assert.IsTrue(actualResult.InputString == input && actualResult.IsUserDefined == expected_isUserDefined && actualResult.IsValidSedol == expected_isValidSedol && actualResult.ValidationDetails == expected_validationDetails);

        }

        [TestCase("9123451", false, true, "Checksum digit does not agree with the rest of the input")]
        [TestCase("9ABCDE8", false, true, "Checksum digit does not agree with the rest of the input")]
        public void SedolValidator_InvalidUserDefined(string input, bool expected_isValidSedol, bool expected_isUserDefined, string expected_validationDetails)
        {
            //**Scenario * *Invalid user defined SEDOL
            //InputString Test Value| IsValidSedol | IsUserDefined | ValidationDetails
            //-- -| --| --| --|
            //9123451 | False | True | Checksum digit does not agree with the rest of the input
            //9ABCDE8 | False | True | Checksum digit does not agree with the rest of the input

            ISedolValidator validator = new SedolValidator();
            ISedolValidationResult actualResult = validator.ValidateSedol(input);

            Assert.IsTrue(actualResult.InputString == input && actualResult.IsUserDefined == expected_isUserDefined && actualResult.IsValidSedol == expected_isValidSedol && actualResult.ValidationDetails == expected_validationDetails);

        }


        [TestCase("9123458", true, true, null)]
        [TestCase("9ABCDE1", true, true, null)]
        public void SedolValidator_ValidUserDefined(string input, bool expected_isValidSedol, bool expected_isUserDefined, string expected_validationDetails)
        {
            //InputString Test Value | IsValidSedol | IsUserDefined | ValidationDetails
            //-- -| --| --| --|
            //9123458 | True | True | Null
            //9ABCDE1 | True | True | Null

            ISedolValidator validator = new SedolValidator();
            ISedolValidationResult actualResult = validator.ValidateSedol(input);

            Assert.IsTrue(actualResult.InputString == input && actualResult.IsUserDefined == expected_isUserDefined && actualResult.IsValidSedol == expected_isValidSedol && actualResult.ValidationDetails == expected_validationDetails);

        }



    }
}
