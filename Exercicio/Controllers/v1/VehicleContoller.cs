using Microsoft.AspNetCore.Mvc;
using Exercicio.ModelViews;
using Exercicio.Db;
using Microsoft.EntityFrameworkCore;
using Exercicio.Entities;
using Exercicio.DTOs;
using Exercicio.Services;

namespace Exercicio.Controllers.v1;

// [ApiController] // para habilitar a validação padrão
[Route("/vehicle")]
public class VehiclesController : ControllerBase
{
    public VehiclesController(DbAppContext context)
    {
        this._context = context;
    }

    private DbAppContext _context;

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var clients = await _context.Vehicles.ToListAsync();
        return StatusCode(200, clients);
    }

    [HttpPost]
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
