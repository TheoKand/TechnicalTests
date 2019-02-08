using System;

namespace ClearBank.DeveloperTest.Types.Model
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public PaymentScheme AllowedPaymentSchemes { get; set; }

        protected void ValidatePaymentType(PaymentScheme scheme)
        {
            //To my knowledge this rule applies to all types of payments. Therefore it belongs here because in the spirit of 
            //DDD and DRY it's part of the validation of the Account domain entity. 
            //Another approach is to implement all the rules in a separate service e.g. IPaymentValidationService that is injected in Payment Service.
            //I didn't do that because logically these checks are common for all payment schemes. In a real world scenario it
            //would be clear from the bussiness requirements that it's beneficial to decouple the validation logic to separate
            //classes per payment scheme. 
            if (!this.AllowedPaymentSchemes.HasFlag(scheme))
            {
                //normally there would be a custom exception like DomainArgumentException etc
                throw new InvalidOperationException($"The account does not support {scheme.ToString()} payments.");
            }
        }

        public void Credit(decimal amount, PaymentScheme scheme)
        {
            ValidatePaymentType(scheme);

            //same comment as above applies
            if (this.Status != AccountStatus.Live && this.Status != AccountStatus.InboundPaymentsOnly)
            {
                throw new InvalidOperationException("Incorrect account status.");
            }

            this.Balance += amount;
        }

        public void Debit(decimal amount, PaymentScheme scheme)
        {
            ValidatePaymentType(scheme);

            //same comment as above applies
            if (this.Status != AccountStatus.Live)
            {
                throw new InvalidOperationException("Incorrect account status.");
            } else if (this.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            this.Balance -= amount;
        }
    }
}
