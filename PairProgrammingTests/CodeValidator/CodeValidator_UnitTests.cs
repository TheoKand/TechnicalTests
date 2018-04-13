using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeValidator
{

    [TestFixture]
    class CodeValidator_UnitTests
    {

        [TestCase("{ ( [ code ] ) }", true)]
        [TestCase("{ ( [ code ] )", false)]
        [TestCase("{ [ ( code ] ) }", false)]
        [TestCase("{ code )", false)]
        [TestCase("( code { code } [ code ] {{code}} [[[code]]] )", true)]
        [TestCase("", true)]
        [TestCase("[]{}()({})", true)]
        [TestCase("code", true)]
        public void CodeValidator_CheckCode_IsValid(string input,bool expectedResult)
        {
            CodeValidator c = new CodeValidator();
            bool result = c.Validate(input);
            Assert.AreEqual(result, expectedResult);

        }

    }
}
