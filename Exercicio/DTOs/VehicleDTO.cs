using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Exercicio.DTOs;

[DisplayName("VehicleDTO")]
public record VehicleDTO
{
    [JsonPropertyName("Marca")]
    [Required(ErrorMessage = "A marca não pode ser vazio")]
    public string Mark {get;set;} = default!;


    [JsonPropertyName("Modelo")]
    [Required(ErrorMessage = "O modelo não pode ser vazio")]
    public string Model {get;set;} = default!;

    [JsonPropertyName("Ano")]
    public int Year { get; set; } = default!;
}