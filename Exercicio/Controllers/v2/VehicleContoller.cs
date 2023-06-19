using Microsoft.AspNetCore.Mvc;
using Exercicio.ModelViews;
using Exercicio.Db;
using Microsoft.EntityFrameworkCore;
using Exercicio.Entities;
using Exercicio.DTOs;
using Exercicio.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Exercicio.Filters;

namespace Exercicio.Controllers.v2;

// [ApiController] // para habilitar a validação padrão
[Route("/v2/vehicles")]
public class VehiclesController : ControllerBase
{
    public VehiclesController(DbAppContext context)
    {
        this._context = context;
    }

    private DbAppContext _context;

    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Lista de clientes", Description = "Retorna uma lista de veículos paginado por 10")]
    [ProducesResponseType(200, Type = typeof(List<Vehicle>))]
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var vehicle = await _context.Vehicles.Take(10).ToListAsync();
        return StatusCode(200, vehicle);
    }

    [HttpPost]
    [CustomValidationExceptionFilter] // validação individual
    public async Task<ActionResult> Create([FromBody] VehicleDTO vehicleDTO)
    {
        var vehicle = new BuilderDtoToEntity().Build<Vehicle, VehicleDTO>(vehicleDTO);

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return StatusCode(201, vehicle);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Show([FromRoute] int id)
    {
        if(id < 1) return StatusCode(404, new ApiError { Message = "O id não pode ser menor do que 1", StatusCode = 404 });
        
        var vehicle = await _context.Vehicles.FindAsync(id);
        if(vehicle is null) return StatusCode(404, new ApiError { Message = "O registro não foi encontrado", StatusCode = 404 });

        return StatusCode(200, vehicle);
    }

    /// <summary>
    /// Atualizar clientes
    /// </summary>
    /// <param name="id"></param>
    /// <param name="vehicleDTO"></param>
    /// <returns></returns>
    [ApiExplorerSettings(GroupName="Clientes")]
    [SwaggerOperation(Tags = new[] { "put", "update" }, Summary = "Atualização do veículo", Description = "Faz atualização de todos os campos da entidade de veículo")]
    [ProducesResponseType(200, Type = typeof(Vehicle))]
    [ProducesResponseType(404, Type = typeof(ApiError))]
    [ProducesResponseType(400, Type = typeof(ApiError))]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] VehicleDTO vehicleDTO)
    {
        if(id < 1) return StatusCode(404, new ApiError { Message = "O id não pode ser menor do que 1", StatusCode = 404 });
        
        var vehicle = await _context.Vehicles.FindAsync(id);
        if(vehicle is null) return StatusCode(404, new ApiError { Message = "O registro não foi encontrado", StatusCode = 404 });

        vehicle = new BuilderDtoToEntity().Build<Vehicle, VehicleDTO>(vehicle, vehicleDTO);
        
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();

        return StatusCode(200, vehicle);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateName([FromRoute] int id, [FromBody] VehicleDTO vehicleDTO)
    {
        if(id < 1) return StatusCode(404, new ApiError { Message = "O id não pode ser menor do que 1", StatusCode = 404 });
        
        var vehicle = await _context.Vehicles.FindAsync(id);
        if(vehicle is null) return StatusCode(404, new ApiError { Message = "O registro não foi encontrado", StatusCode = 404 });

        vehicle.Mark = vehicleDTO.Mark;

        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();

        return StatusCode(200, vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Destroy([FromRoute] int id)
    {
        if(id < 1) return StatusCode(404, new ApiError { Message = "O id não pode ser menor do que 1", StatusCode = 404 });
        
        var vehicle = await _context.Vehicles.FindAsync(id);
        if(vehicle is null) return StatusCode(404, new ApiError { Message = "O registro não foi encontrado", StatusCode = 404 });

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return StatusCode(204);
    }
}
