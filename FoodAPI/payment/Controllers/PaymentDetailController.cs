using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using payment.Models;

namespace payment.Controllers
{
    [Route("api/PaymentDetail")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        PaymentDetailContext _context = new PaymentDetailContext();

        // GET: PaymentDetailController
        public IEnumerable<PaymentDetail>Get()
        {
            return _context.PaymentDetails.ToList();
        }

        // GET: PaymentDetailController/Details/5
        public PaymentDetail Get(int id)
        {
            return _context.PaymentDetails.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] PaymentDetail value)
        {
            _context.PaymentDetails.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PaymentDetail value)
        {
            PaymentDetail s = _context.PaymentDetails.Find(id);
            s.CardOwnerName = value.CardOwnerName;
            _context.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PaymentDetail s = _context.PaymentDetails.Find(id);
            _context.PaymentDetails.Remove(s);
            _context.SaveChanges();
        }
    }
}
