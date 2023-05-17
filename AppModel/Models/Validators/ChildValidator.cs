using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MPPCSharp.Models.Validators
{
    internal class ChildValidator : ChildValidatorInterface
    {
        public void validate(string firstName, string lastName, string age)
        {
            string digitsPattern = ".*[0-9].*";
            string lettersPattern = ".*[a-zA-Z].*";
            Regex digitsRegex = new Regex(digitsPattern);
            Regex lettersRegex = new Regex(lettersPattern);

            if(digitsRegex.IsMatch(firstName) || digitsRegex.IsMatch(lastName))
            {
                throw new ValidationException("There can't be any numbers in someone's name!");
            }

            if (lettersRegex.IsMatch(age)) 
            {
                throw new ValidationException("There can't be any letters in someone's age!");
            }
        }
    }
}
