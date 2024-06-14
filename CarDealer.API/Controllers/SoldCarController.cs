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
    public class SoldCarController : ControllerBase
    {
        #region Setup
        private readonly ISoldCarService _soldCarService;
        private readonly ILogger<AvailableCarController> _logger;

        public SoldCarController(ISoldCarService soldCarService, ILogger<AvailableCarController> logger)
        {
            _soldCarService = soldCarService;
            _logger = logger;
        }
        #endregion


        #region Get By Sale ID
        [HttpGet("[Controller]/Get_Sale_By_ID")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBySaleID(Guid guid)
        {
            SoldCar car = await _soldCarService.GetBySaleID(guid);

            if (car == null)
                return NotFound();

            return Ok(car);
        }
        #endregion


        #region Get Sale By Vehicle ID
        [HttpGet("[Controller]/Get_Sale_By_Vehicle_ID")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByCarID(Guid guid)
        {
            SoldCar car = await _soldCarService.GetByCarID(guid);

            if (car == null)
            {
                _logger.LogWarning($"No sold car {guid} found");
                return NotFound();
            }

            return Ok(car);
        }
        #endregion


        #region Get Sale By License Plate
        [HttpGet("[Controller]/Get_Sale_By_License_Plate")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSaleByPlate(string plate)
        {
            SoldCar car = await _soldCarService.GetByCarPlate(plate);

            if (car == null)
            {
                _logger.LogWarning($"No sold car found with plate: {plate}");
                return NotFound();
            }

            return Ok(car);
        }
        #endregion


        #region Get Sale By Sales Manager
        [HttpGet("[controller]/Get_Sales_By_SalesManager")]
        [ProducesResponseType(typeof(List<SoldCar>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBySalesManager(Guid guid)
        {
            List<SoldCar> carsList = await _soldCarService.GetBySalesManagerID(guid);

            if (carsList == null || !carsList.Any())
            {
                _logger.LogWarning($"No car sold by Sales Manager: {guid}");
                return NotFound();
            }

            return Ok(carsList);
        }
        #endregion


        #region Get Shops By Customer
        [HttpGet("[controller]/Get_Shops_By_Customer")]
        [ProducesResponseType(typeof(List<SoldCar>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByCustomer(Guid guid)
        {
            List<SoldCar> carsList = await _soldCarService.GetByCustomerID(guid);

            if (carsList == null || !carsList.Any())
            {
                _logger.LogWarning($"No shops made by Customer: {guid}");
                return NotFound();
            }

            return Ok(carsList);
        }
        #endregion


        #region Add Sold Car
        [HttpPost("[Controller]/Add_New_Sold_Vehicle")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> AddSoldCar([FromBody] SoldCarData soldCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            if (_soldCarService.ExistsCarID(soldCarData.Car_ID))
            {
                return Conflict();
            }
            else
            {
                SoldCar car = await _soldCarService.AddSoldCar(soldCarData);
                return Ok(car);
            }
        }
        #endregion


        #region Edit By Sale ID
        [HttpPut("[Controller]/Edit_Vehicle_By_Sale_ID")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> EditByID(Guid guid, [FromBody] SoldCarData soldCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            SoldCar car = await _soldCarService.EditBySaleID(guid, soldCarData);

            return Ok(car);
        }
        #endregion


        #region Delete By ID
        [HttpDelete("[Controller]/Delete_By_Sale_ID")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByID([FromQuery] Guid guid)
        {
            if (!_soldCarService.ExistsSaleID(guid))
                return NotFound();

            await _soldCarService.DeleteBySaleID(guid);

            return Ok();
        }
        #endregion


        #region Delete By Plate
        [HttpDelete("[Controller]/Delete_By_Plate")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByPlate([FromQuery] string plate)
        {
            if (!_soldCarService.ExistsPlate(plate))
                return NotFound();

            await _soldCarService.DeleteByPlate(plate);

            return Ok();
        }
        #endregion
    }
}
