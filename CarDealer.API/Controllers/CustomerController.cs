using CarDealer.Controllers.DTO;
using CarDealer.Data.Entities;
using CarDealer.Services;
using CarDealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        #region Setup
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }
        #endregion


        #region Get By Customer ID
        [HttpGet("[Controller]/Get_Customer_By_ID")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByID(Guid guid)
        {
            Customer customer = await _customerService.GetByID(guid);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
        #endregion


        #region Get Sale By TIN
        [HttpGet("[Controller]/Get_By_TIN")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByTIN(string tin)
        {
            Customer customer = await _customerService.GetByTIN(tin);

            if (customer == null)
            {
                _logger.LogWarning($"No Customer found with TIN: {tin}");
                return NotFound();
            }

            return Ok(customer);
        }
        #endregion


        #region Get By FirstName
        [HttpGet("[controller]/Get_By_First_Name")]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByFirstName(string name)
        {
            List<Customer> customersList = await _customerService.GetByFirstName(name);

            if (customersList == null || !customersList.Any())
            {
                _logger.LogWarning($"No Customer named: {name}");
                return NotFound();
            }

            return Ok(customersList);
        }
        #endregion


        #region Get By Middle Name
        [HttpGet("[controller]/Get_By_Middle_Name")]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByMiddleName(string name)
        {
            List<Customer> customersList = await _customerService.GetByMiddleName(name);

            if (customersList == null || !customersList.Any())
            {
                _logger.LogWarning($"No Customer named: {name}");
                return NotFound();
            }

            return Ok(customersList);
        }
        #endregion


        #region Get By Last Name
        [HttpGet("[controller]/Get_By_Last_Name")]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByLastName(string name)
        {
            List<Customer> customersList = await _customerService.GetByLastName(name);

            if (customersList == null || !customersList.Any())
            {
                _logger.LogWarning($"No Customer named: {name}");
                return NotFound();
            }

            return Ok(customersList);
        }
        #endregion


        #region Add Customer
        [HttpPost("[Controller]/Add_Customer")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerData customerData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            if (_customerService.ExistsTIN(customerData.TIN))
            {
                return Conflict();
            }
            else
            {
                Customer customer = await _customerService.AddCustomer(customerData);
                return Ok(customer);
            }
        }
        #endregion


        #region Edit By ID
        [HttpPut("[Controller]/Edit_By_ID")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> EditByID(Guid guid, [FromBody] CustomerData customerData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            Customer customer = await _customerService.EditByID(guid, customerData);

            return Ok(customer);
        }
        #endregion


        #region Delete By ID
        [HttpDelete("[Controller]/Delete_By_Customer_ID")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByID([FromQuery] Guid guid)
        {
            if (!_customerService.ExistsByID(guid))
                return NotFound();

            await _customerService.DeleteByID(guid);

            return Ok();
        }
        #endregion




    }
}
