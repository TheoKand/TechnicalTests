
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types.DataTransfer;
using ClearBank.DeveloperTest.Types.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Configuration;

namespace ClearBank.DeveloperTest.Tests.UnitTests.Payment
{

    [TestFixture]
    public class PaymentService_MakePayment
    {

        private Mock<IAccountRepository> accountRepositoryMock;
        private Mock<IAccountDataStore> accountDataStoreMock;
        private Account creditorAccount, debtorAccount;
        private string debtorAccountNumber = "1111";
        private string creditorAccountNumber = "2222";

        [SetUp]
        public void Setup()
        {
            accountDataStoreMock = new Mock<IAccountDataStore>();

            accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock
                .Setup(o => o.GetDataStore())
                .Returns(accountDataStoreMock.Object);
        }

        [Test]
        [Category("PositiveCases")]
        public void MakePayment_PaymentIsOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            int creditorAccountBalance = 0;
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.Live
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = creditorAccountBalance,
                Status = AccountStatus.Live
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(debtorAccount.Balance, DebtorAccountBalance - request.Amount);
            Assert.AreEqual(creditorAccount.Balance, creditorAccountBalance + request.Amount);
            accountDataStoreMock.Verify(o => o.UpdateAccount(debtorAccount));
            accountDataStoreMock.Verify(o => o.UpdateAccount(creditorAccount));
        }

        #region Creditor account
        [Test]
        [Category("CreditorAccount")]
        public void MakePayment_CreditorAccountNotFound_PaymentNotOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.Live
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
            //If we had an ILogger we could also have custom Exceptions for each bussiness case e.g. AccountNotFoundException etc
            //and we could verify that the logger method has been called with the correct exception for each case.

        }

        [Test]
        [Category("CreditorAccount")]
        public void MakePayment_CreditorAccountPaymentSchemeNotSupported_PaymentNotOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            int creditorAccountBalance = 0;
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.Live
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Chaps | Types.PaymentScheme.FasterPayments,
                Balance = creditorAccountBalance,
                Status = AccountStatus.Live
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        [Category("CreditorAccount")]
        public void MakePayment_CreditorAccountIncorrectStatus_PaymentNotOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            int creditorAccountBalance = 0;
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.Live
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = creditorAccountBalance,
                Status = AccountStatus.Disabled
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
        }
        #endregion

        #region debtor account
        [Test]
        [Category("DebtorAccount")]
        public void MakePayment_DebtorAccountNotFound_PaymentNotOK()
        {
            //Arrange
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = 0,
                Status = AccountStatus.Live
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        [Category("DebtorAccount")]
        public void MakePayment_DebtorAccountPaymentSchemeNotSupported_PaymentNotOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            int creditorAccountBalance = 0;
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Chaps
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs | Types.PaymentScheme.FasterPayments,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.Live
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Chaps,
                Balance = creditorAccountBalance,
                Status = AccountStatus.Live
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        [Category("DebtorAccount")]
        public void MakePayment_DebtorAccountIncorrectStatus_PaymentNotOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            int creditorAccountBalance = 0;
            var request = new MakePaymentRequest
            {
                Amount = 10,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.InboundPaymentsOnly
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = creditorAccountBalance,
                Status = AccountStatus.InboundPaymentsOnly
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        [Category("DebtorAccount")]
        public void MakePayment_DebtorAccountInsufficientFunds_PaymentNotOK()
        {
            //Arrange
            int DebtorAccountBalance = 10;
            int creditorAccountBalance = 0;
            var request = new MakePaymentRequest
            {
                Amount = 11,
                DebtorAccountNumber = debtorAccountNumber,
                CreditorAccountNumber = creditorAccountNumber,
                PaymentDate = DateTime.Today,
                PaymentScheme = Types.PaymentScheme.Bacs
            };
            debtorAccount = new Account
            {
                AccountNumber = debtorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = DebtorAccountBalance,
                Status = AccountStatus.Live
            };
            creditorAccount = new Account
            {
                AccountNumber = creditorAccountNumber,
                AllowedPaymentSchemes = Types.PaymentScheme.Bacs,
                Balance = creditorAccountBalance,
                Status = AccountStatus.Live
            };
            accountDataStoreMock
                .Setup(o => o.GetAccount(debtorAccountNumber))
                .Returns(debtorAccount);
            accountDataStoreMock
                .Setup(o => o.GetAccount(creditorAccountNumber))
                .Returns(creditorAccount);

            //Act
            var paymentService = new PaymentService(accountRepositoryMock.Object);
            var result = paymentService.MakePayment(request);

            //Assert
            Assert.IsFalse(result.Success);
        }
        #endregion
    }
}
