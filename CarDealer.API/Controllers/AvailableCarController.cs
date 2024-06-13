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
        private readonly ILogger<AvailableCarController> _logger;

        public AvailableCarController(IAvailableCarService availableCarService, ILogger<AvailableCarController> logger)
        {
            _availableCarService = availableCarService;
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


        #region Get By License Plate
        [HttpGet("[Controller]/Get_Car_By_Engine")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByEngine(string engine)
        {
            List<AvailableCar> cars = await _availableCarService.GetByEngine(engine);

            if (cars == null)
            {
                _logger.LogWarning($"No car found with engine: {engine}");
                return NotFound();
            }

            return Ok(cars);
        }
        #endregion


        #region Add Available Car
        [HttpPost("[Controller]/Add_New_Available_Car")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public IActionResult AddAvailableCar([FromBody] AvailableCarData availableCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            if (_availableCarService.Exists(availableCarData.License_Plate))
            {
                return Conflict();
            }
            else
            {
                var car = new AvailableCar
                {
                    License_Plate = availableCarData.License_Plate,
                    Segment = availableCarData.Segment,
                    Model = availableCarData.Model,
                    Year = availableCarData.Year,
                    Engine = availableCarData.Engine,
                    Transmission = availableCarData.Transmission,
                    Mileage = availableCarData.Mileage,
                    Fuel_Economy = availableCarData.Fuel_Economy,
                    Key_Features = availableCarData.Key_Features,
                    Paint_Color = availableCarData.Paint_Color,
                    Interior_Color = availableCarData.Interior_Color,
                    Accessories = availableCarData.Accessories,
                    Cost = availableCarData.Cost,
                    Arrival_Date = availableCarData.Arrival_Date,
                    Car_Image = availableCarData.Car_Image,
                    Intern_Image = availableCarData.Intern_Image
                };

                _availableCarService.Add(car);
                return Ok(car);
            }
        }
        #endregion


        #region Edit
        [HttpPut("[Controller]/Edit_Car_By_ID")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> EditByID(Guid guid, [FromBody] AvailableCarData availableCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            var car = await _availableCarService.GetByID(guid);
            if (car == null)
                return NotFound();

            var editedCar = await _availableCarService.EditByID(guid, availableCarData);
            return Ok(editedCar);
        }
            #endregion

            #region Edit
            [HttpPut("[Controller]/Edit_Car_By_Plate")]
        [ProducesResponseType(typeof(AvailableCar), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Microsoft.IdentityModel.Tokens.ValidationFailure>), 400)]
        public async Task<IActionResult> EditByPlate(string plate, [FromBody] AvailableCarData availableCarData)
        {
            /*var validationResult = _bookDataValidator.Validate(bookData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/

            var car = await _availableCarService.GetByPlate(plate);

            if (car == null)
                return NotFound();

            var editedCar = await _availableCarService.EditByPlate(plate, availableCarData);
            return Ok(editedCar);
        }
        #endregion
    }
}
