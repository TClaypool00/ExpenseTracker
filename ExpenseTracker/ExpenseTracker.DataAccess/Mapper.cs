using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.DataAccess.DataModels;

namespace ExpenseTracker.DataAccess
{
    public class Mapper
    {
        /*  ************************
         *  *                      *
         *  *       Bills          *
         *  *                      *
         *  ************************
         */ 

        public static Bills MapBills(CoreBills bills)
        {
            return new Bills
            {
                BillId = bills.BillId,
                BillLate = bills.BillLate,
                BillName = bills.BillName,
                BillPrice = bills.BillPrice,
                DueDate = bills.DueDate,

                UserId = bills.User.UserId
            };
        }

        public static CoreBills MapBills(Bills bills)
        {
            return new CoreBills
            {
                BillId = bills.BillId,
                BillLate = bills.BillLate,
                BillName = bills.BillName,
                BillPrice = bills.BillPrice,
                DueDate = bills.DueDate,

                User = MapUsers(bills.User)
            };
        }

        /*  ************************
         *  *                      *
         *  *       Budgets        *
         *  *                      *
         *  ************************
         */

        public static Budget MapBudget(CoreBudget budget)
        {
            return new Budget
            {
                BudgetId = budget.BudgetId,
                TotalAmtBills = budget.TotalAmtBills,
                
                UserId = budget.User.UserId
            };
        }

        public static CoreBudget MapBudget(Budget budget)
        {
            return new CoreBudget
            {
                BudgetId = budget.BudgetId,
                TotalAmtBills = budget.TotalAmtBills,

                User = MapUsers(budget.User)
            };
        }

        /*  ************************
         *  *                      *
         *  *    Credit Union      *
         *  *                      *
         *  ************************
         */

        public static CreditUnion MapUnion(CoreCreditUnion union)
        {
            return new CreditUnion
            {
                UnionId = union.UnionId,
                CreditUnionName = union.CreditUnionName,
                Street = union.Street,
                State = union.State,
                City = union.City,
                Zip = union.Zip
            };
        }

        public static CoreCreditUnion MapUnion(CreditUnion union)
        {
            return new CoreCreditUnion
            {
                UnionId = union.UnionId,
                CreditUnionName = union.CreditUnionName,
                Street = union.Street,
                State = union.State,
                City = union.City,
                Zip = union.Zip
            };
        }

        /*  ************************
         *  *                      *
         *  *       Loans          *
         *  *                      *
         *  ************************
         */

        public static Loan MapLoan(CoreLoan loan)
        {
            return new Loan
            {
                LoanId = loan.LoanId,
                Deposit = loan.Deposit,
                MonthlyAmountDue = loan.MonthlyAmountDue,
                PaymentDueDate = loan.PaymentDueDate,
                TotalAmountDue = loan.TotalAmountDue,
                
                UserId = loan.User.UserId,

                UnionId = loan.Union.UnionId
            };
        }

        public static CoreLoan MapLoan(Loan loan)
        {
            return new CoreLoan
            {
                LoanId = loan.LoanId,
                Deposit = loan.Deposit,
                MonthlyAmountDue = loan.MonthlyAmountDue,
                PaymentDueDate = loan.PaymentDueDate,
                TotalAmountDue = loan.TotalAmountDue,

                User = MapUsers(loan.User),

                Union = MapUnion(loan.Union)
            };
        }

        /*  ************************
         *  *                      *
         *  *       Non-Bills      *
         *  *                      *
         *  ************************
         */

        public static NonBills MapNonBills(CoreNonBills bills)
        {
            return new NonBills
            {
                NonBillId = bills.NonBillId,
                StoreName = bills.StoreName,
                Price = bills.Price,

                UserId = bills.User.UserId
            };
        }

        public static CoreNonBills MapNonBills(NonBills bills)
        {
            return new CoreNonBills
            {
                NonBillId = bills.NonBillId,
                StoreName = bills.StoreName,
                Price = bills.Price,

                User = MapUsers(bills.User)
            };
        }

        /*  ************************
         *  *                      *
         *  *     Subscriptions    *
         *  *                      *
         *  ************************
         */

        public static Subscriptions MapSub(CoreSubscriptions sub)
        {
            return new Subscriptions
            {
                SubId = sub.SubId,
                AmountDue = sub.AmountDue,
                DueDate = sub.DueDate,
                CompanyName = sub.CompanyName,

                UserId = sub.User.UserId
            };
        }

        public static CoreSubscriptions MapSub(Subscriptions sub)
        {
            return new CoreSubscriptions
            {
                SubId = sub.SubId,
                AmountDue = sub.AmountDue,
                DueDate = sub.DueDate,
                CompanyName = sub.CompanyName,

                User = MapUsers(sub.User)
            };
        }

        /*  ************************
        *  *                      *
        *  *       Users          *
        *  *                      *
        *  ************************
        */

        public static Users MapUsers(CoreUsers users)
        {
            return new Users
            {
                UserId = users.UserId,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                Password = users.Password,
                IsAdmin = users.IsAdmin,
                City = users.City,
                Street = users.Street,
                State = users.State,
                Zip = users.Zip,
                PhoneNumber = users.PhoneNumber
            };
        }

        public static CoreUsers MapUsers(Users users)
        {
            return new CoreUsers
            {
                UserId = users.UserId,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                Password = users.Password,
                IsAdmin = users.IsAdmin,
                City = users.City,
                Street = users.Street,
                State = users.State,
                Zip = users.Zip,
                PhoneNumber = users.PhoneNumber
            };
        }
    }
}
