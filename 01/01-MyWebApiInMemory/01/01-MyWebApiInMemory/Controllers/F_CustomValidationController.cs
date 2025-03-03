using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _01_MyWebApiInMemory.Controllers
{

    public class CustomValidatedOrder
    {
        public int Id { get; set; }
        [Required]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Make sure initialized

        public decimal TotalPrice { get; set; } // Calculated, should be validated
    }

    public class OrderItem
    {
        [Required]
        public string Product { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; } // Could derive this from Product in a real app
    }


    [Route("api/[controller]")]
    [ApiController]
    public class F_CustomValidationController : ControllerBase
    {
        private static readonly List<CustomValidatedOrder> _orders = new List<CustomValidatedOrder>();
        private static int _nextOrderId = 1;


        [HttpPost("orders")]
        public ActionResult<CustomValidatedOrder> PostOrder([FromBody] CustomValidatedOrder order)
        {
            // Custom Validation: Calculate TotalPrice and compare with client's TotalPrice
            decimal calculatedTotalPrice = 0;
            foreach (var item in order.OrderItems)
            {
                calculatedTotalPrice += item.Quantity * item.PricePerItem;
            }

            if (calculatedTotalPrice != order.TotalPrice)
            {
                ModelState.AddModelError("TotalPrice", "Total price is incorrect. Please recalculate.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            order.Id = _nextOrderId++;
            _orders.Add(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet("orders/{id}")]
        public ActionResult<CustomValidatedOrder> GetOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
}
