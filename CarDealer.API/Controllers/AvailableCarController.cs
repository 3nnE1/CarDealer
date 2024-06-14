using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
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
    public class AvailableCarController : ControllerBase
    {
        #region Setup
        private readonly IAvailableCarService _availableCarService;
        private readonly ISoldCarService _soldCarService;
        private readonly ILogger<AvailableCarController> _logger;

        public AvailableCarController(IAvailableCarService availableCarService, ISoldCarService soldCarService, ILogger<AvailableCarController> logger)
        {
            _availableCarService = availableCarService;
            _soldCarService = soldCarService;
            _logger = logger;
        }
        #endregion


        #region Get By GUID
        [HttpGet("[Controller]/Get_Car_By_ID")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByID(Guid guid)
        {
            AvailableCar car = await _availableCarService.GetByID(guid);
           
            if (car == null)
                return NotFound();

            return Ok(car);
        }
        #endregion


        #region Get By License Plate
        [HttpGet("[Controller]/Get_Car_By_License_Plate")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByPlate(string plate)
        {
            AvailableCar car = await _availableCarService.GetByPlate(plate);

            if (car == null)
            {
                _logger.LogWarning($"No car found with plate: {plate}");
                return NotFound();
            }

            return Ok(car);
        }
        #endregion


        #region Get By Engine
        [HttpGet("[controller]/Get_Car_By_Engine")]
        [ProducesResponseType(typeof(List<AvailableCar>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByEngine(string engine)
        {
           List<AvailableCar> carsList = await _availableCarService.GetByEngine(engine);

            if (carsList == null || !carsList.Any())
            {
                _logger.LogWarning($"No car found with engine: {engine}");
                return NotFound();
            }

            return Ok(carsList);
        }
        #endregion


        #region Add Available Car
        [HttpPost("[Controller]/Add_New_Available_Car")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> AddAvailableCar([FromBody] AvailableCarData availableCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            if (_availableCarService.ExistsPlate(availableCarData.License_Plate))
            {
                return Conflict();
            }
            else
            {
                AvailableCar car = await _availableCarService.AddAvailableCar(availableCarData);
                return Ok(car);
            }
        }
        #endregion


        #region Edit By ID
        [HttpPut("[Controller]/Edit_Car_By_ID")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> EditByID(Guid guid, [FromBody] AvailableCarData availableCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            AvailableCar car = await _availableCarService.EditByID(guid, availableCarData);
        
            return Ok(car);
        }
        #endregion


        #region Edit By Plate
        [HttpPut("[Controller]/Edit_Car_By_Plate")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> EditByPlate(string plate, [FromBody] AvailableCarData availableCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            AvailableCar car = await _availableCarService.EditByPlate(plate, availableCarData);

            return Ok(car);
        }
        #endregion


        #region Sell
        [HttpPost("[Controller]/Sell")]
        [ProducesResponseType(typeof(SoldCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> Sell(Guid guid, [FromBody] SoldCarData soldCarData)
        {
            SoldCar car = await _availableCarService.Sell(guid, soldCarData);
            return Ok(car);
        }

        #endregion


        #region Delete By ID
        [HttpDelete("[Controller]/Delete_By_ID")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByID([FromQuery] Guid guid)
        {
            if (!_availableCarService.ExistsID(guid))
                return NotFound();

            await _availableCarService.DeleteByID(guid);

            return Ok();
        }
        #endregion


        #region Delete By Plate
        [HttpDelete("[Controller]/Delete_By_Plate")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByPlate([FromQuery] string plate)
        {
            if (!_availableCarService.ExistsPlate(plate))
                return NotFound();

            await _availableCarService.DeleteByPlate(plate);

            return Ok();
        }
        #endregion
    }
}
