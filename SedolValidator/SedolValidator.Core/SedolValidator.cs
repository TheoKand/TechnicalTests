
using System;


public class SedolValidator : ISedolValidator
{
    public ISedolValidationResult ValidateSedol(string input)
    {
        SedolValidationResult result = new SedolValidationResult()
        {
            InputString = input,
            IsValidSedol = false,
            IsUserDefined = false,
            ValidationDetails = null
        };

        if (String.IsNullOrEmpty(input) || (input.Length != 7))
        {
            result.ValidationDetails = "Input string was not 7-characters long";
        }
        else
        {
            #region calculate checkdigit
            int[] weights = new int[] { 1, 3, 1, 7, 3, 9, 1 };
            int weightedCheckSum = 0;

            for (int i = 0; i < 6; i++)
            {
                char letterOrDigit = input[i];
                int letterOrDigitValue = 0;

                if (letterOrDigit >= 'A' && letterOrDigit <= 'Z')
                {
                    letterOrDigitValue = GetLetterValue(letterOrDigit);
                }
                else
                {
                    int digitValue = 0;
                    bool parseOk = int.TryParse(letterOrDigit.ToString(), out digitValue);
                    if (parseOk) letterOrDigitValue = digitValue;
                }

                weightedCheckSum += letterOrDigitValue * weights[i];

            }

            int checkDigit = (10 - (weightedCheckSum % 10)) % 10;
            #endregion

            int inputCheckDigit = 0;
            bool inputCheckDigitParseOk = int.TryParse(input[6].ToString(), out inputCheckDigit);
            if (inputCheckDigitParseOk && checkDigit != inputCheckDigit)
            {
                result.ValidationDetails = "Checksum digit does not agree with the rest of the input";
                result.IsValidSedol = false;
            } else
            {
                result.IsValidSedol = true;
            }

            //There's no information in the readme file about what "IsUserDefined" means.
            //From the scenarios we can assume that if the SEDOL starts with 9 it's user defined ;)
            result.IsUserDefined = input[0] == '9';

        }

        return result;
    }

    /// <summary>
    /// Letters have the value of 9 plus their alphabet position, such that B = 11 and Z = 35. 
    /// </summary>
    private int GetLetterValue(char letter)
    {
        return 9 + letter - (int)'A'+1;
    }

}




