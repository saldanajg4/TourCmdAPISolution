using System.Collections.Generic;
using System.Threading.Tasks;
using TourCmdAPI.Entities;

namespace TourCmdAPI.IRepos
{
    public interface IPaymentDetailsRepository
    {
        Task<IEnumerable<PaymentDetail>> GetPaymentDetails();
        Task<PaymentDetail> GetPaymentDetailById(int id);
        void PutPaymentDetails(int id, PaymentDetail payment);
        Task<bool> SaveAsync();
        Task PostPaymentDetail(Entities.PaymentDetail payment);
        void DeletePaymentDetail(PaymentDetail payment);
        Task<bool> ExistsPaymentDetail(int id);
        Task<int> GetTotalOfPaymentDetails();
    }
}