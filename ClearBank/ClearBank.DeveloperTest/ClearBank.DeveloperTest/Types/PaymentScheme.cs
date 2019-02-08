using System;

namespace ClearBank.DeveloperTest.Types
{
    [Flags]
    public enum PaymentScheme
    {
        FasterPayments=1,
        Bacs=2,
        Chaps=4
    }
}
