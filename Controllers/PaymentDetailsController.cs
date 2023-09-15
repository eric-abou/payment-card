using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentCard.API.Data;
using PaymentCard.API.Models;

namespace PaymentCard.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {

        private readonly PaymentDbContext _context;

        public PaymentDetailsController(PaymentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
        {
            if (_context.PaymentDetails == null)
            {
                return NotFound();
            }

            return await _context.PaymentDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetails(int id)
        {
            if (_context.PaymentDetails == null)
                return NotFound();

            //var paymentDetail = await _context.PaymentDetails.FirstOrDefaultAsync(x => x.Id == id);
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
            //return Ok(paymentDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetails paymentDetail)
        {
            if (id != paymentDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetail(PaymentDetails paymentDetail)
        {
            if (_context.PaymentDetails == null)
            {
                return Problem("Entity set 'PaymentDbContext.PaymentDetails'  is null.");
            }
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(await _context.PaymentDetails.ToListAsync());
        }

        // DELETE: api/PaymentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            if (_context.PaymentDetails == null)
            {
                return NotFound();
            }
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailExists(int id)
        {
            return (_context.PaymentDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
