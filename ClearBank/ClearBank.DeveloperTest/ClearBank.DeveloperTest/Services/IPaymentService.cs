using ClearBank.DeveloperTest.Types.DataTransfer;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
