

namespace gymappyt.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(string cardNumber, string exp, string cvv, string name, decimal amount);
    }

    public class FakePaymentService : IPaymentService
    {
        public async Task<bool> ProcessPaymentAsync(string cardNumber, string exp, string cvv, string name, decimal amount)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}