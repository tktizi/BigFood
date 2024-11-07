using BigFood_Reviews.Entities;
using BigFood_Reviews.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BigFood_Reviews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return Ok(customers);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(Add), new { id = customer.Id }, customer);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (customer == null || id != customer.Id)
            {
                return BadRequest();
            }

            var existingCustomer = await _unitOfWork.Customers.GetAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            await _unitOfWork.Customers.ReplaceAsync(existingCustomer);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _unitOfWork.Customers.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }
    }

}
