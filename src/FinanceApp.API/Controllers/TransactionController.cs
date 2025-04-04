using System.Security.Claims;
using FinanceApp.Application.DTOs.Transaction;
using FinanceApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        
        private Guid GetUserId() 
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found");

            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() 
        {
            var userId = GetUserId();
            var transactions = await _transactionService.GetAllAsync(userId);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            var userId = GetUserId();
            var transactions = await _transactionService.GetByIdAsync(id, userId);
            if(transactions == null) 
                return NotFound();

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto transactionDto)
        {
            var userId = GetUserId();
            var transaction = await _transactionService.CreateAsync(transactionDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = transaction.Id}, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTransactionDto updateDto)
        {
            var userId = GetUserId();
            
            var updated = await _transactionService.UpdateAsync(updateDto, userId);

            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _transactionService.DeleteAsync(id, userId);
            return deleted ? NoContent() : NotFound();
        }

    }
}
