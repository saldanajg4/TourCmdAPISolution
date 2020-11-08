using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Services;

namespace TourCmdAPI.Repos
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private readonly OrderContext _context;
        public PaymentDetailsRepository(OrderContext context)
        {
            _context = context;
        }

        public void DeletePaymentDetail(PaymentDetail payment)
        {
              _context.PaymentDetails.Remove(payment);      
        }

        public async Task<bool> ExistsPaymentDetail(int id)
        {
            return await _context.PaymentDetails.AnyAsync(payment => payment.Id == id );
        }

        public async Task<PaymentDetail> GetPaymentDetailById(int id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }

        public async Task<IEnumerable<PaymentDetail>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        public async Task PostPaymentDetail(PaymentDetail payment)
        {
            await _context.PaymentDetails.AddAsync(payment);
        }

        public void PutPaymentDetails(int id, PaymentDetail payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
           
        }
         public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}