using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types.DataTransfer;
using ClearBank.DeveloperTest.Types.Model;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private IAccountRepository accountRepository;

        public PaymentService(IAccountRepository accountRepository)
        {
            if (accountRepository == null) throw new ArgumentNullException(nameof(accountRepository));

            this.accountRepository = accountRepository;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            try
            {
                IAccountDataStore dataStore = accountRepository.GetDataStore();

                Account debtorAccount = dataStore.GetAccount(request.DebtorAccountNumber);
                Account creditorAccount = dataStore.GetAccount(request.CreditorAccountNumber);

                if (debtorAccount == null)
                {
                    throw new ArgumentException("Debtor account not found.");
                } else if (creditorAccount==null)
                {
                    throw new ArgumentException("Creditor account not found.");
                }

                debtorAccount.Debit(request.Amount, request.PaymentScheme);
                creditorAccount.Credit(request.Amount, request.PaymentScheme);

                //Normally these two operations would be part of a transaction, e.g. the repository would implement the UnitOfWork pattern
                dataStore.UpdateAccount(debtorAccount);
                dataStore.UpdateAccount(creditorAccount);

            } catch(Exception ex)
            {
                //Normall we would the log error here with an abstraction of a logging component (ILogger etc) injected in this service

                return new MakePaymentResult { Success = false };
            }

            return new MakePaymentResult { Success = true };

        }
    }
}
