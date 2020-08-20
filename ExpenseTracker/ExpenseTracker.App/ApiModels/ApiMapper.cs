using ExpenseTracker.Core.CoreModels;

namespace ExpenseTracker.App.ApiModels
{
    public class ApiMapper
    {
        /*  ************************
         *  *                      *
         *  *       Bills          *
         *  *                      *
         *  ************************
         */
        public static ApiBills MapBills(CoreBills bills)
        {
            return new ApiBills
            {
                BillId = bills.BillId,
                BillLate = bills.BillLate,
                BillName = bills.BillName,
                BillPrice = bills.BillPrice,
                DueDate = bills.DueDate,

                UserId = bills.User.UserId,
                UserFirstName = bills.User.FirstName,
                UserLastName = bills.User.LastName
            };
        }

        /*  ************************
         *  *                      *
         *  *       Budgets        *
         *  *                      *
         *  ************************
         */

        public static ApiBudget MapBudgets(CoreBudget budget)
        {
            return new ApiBudget
            {
                BudgetId = budget.BudgetId,
                TotalAmtBills = budget.TotalAmtBills,

                UserId = budget.User.UserId,
                UserFirstName = budget.User.FirstName,
                UserLastName = budget.User.LastName
            };
        }

        /*  ************************
         *  *                      *
         *  *    Credit Union      *
         *  *                      *
         *  ************************
         */

        public static ApiCreditUnion MapUnion(CoreCreditUnion union)
        {
            return new ApiCreditUnion
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

        public static ApiLoan MapLoan(CoreLoan loan)
        {
            return new ApiLoan
            {
                LoanId = loan.LoanId,
                Deposit = loan.Deposit,
                MonthlyAmountDue = loan.MonthlyAmountDue,
                PaymentDueDate = loan.PaymentDueDate,
                TotalAmountDue = loan.TotalAmountDue,

                UserId = loan.User.UserId,
                UserFirstName = loan.User.FirstName,
                UserLastName = loan.User.LastName,

                UnionId = loan.Union.UnionId,
                CreditUnionName = loan.Union.CreditUnionName
            };
        }

        /*  ************************
         *  *                      *
         *  *       Non-Bills      *
         *  *                      *
         *  ************************
         */

        public static ApiNonBills MapNonBills(CoreNonBills bills)
        {
            return new ApiNonBills
            {
                NonBillId = bills.NonBillId,
                StoreName = bills.StoreName,
                Price = bills.Price,

                UserId = bills.User.UserId,
                UserFirstName = bills.User.FirstName,
                UserLastName = bills.User.LastName
            };
        }

        /*  ************************
         *  *                      *
         *  *     Subscriptions    *
         *  *                      *
         *  ************************
         */

        public static ApiSubscriptions MapSub(CoreSubscriptions sub)
        {
            return new ApiSubscriptions
            {
                SubId = sub.SubId,
                AmountDue = sub.AmountDue,
                DueDate = sub.DueDate,
                CompanyName = sub.CompanyName,

                UserId = sub.User.UserId,
                UserFirstName = sub.User.FirstName,
                UserLastName = sub.User.LastName
            };
        }

        /*  ************************
        *  *                      *
        *  *       Users          *
        *  *                      *
        *  ************************
        */

        public static ApiUsers MapUsers(CoreUsers users)
        {
            return new ApiUsers
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