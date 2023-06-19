using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Exercicio.Entities;

[DisplayName("Vehicle")]
[Table("vehicle")]
public record Vehicle
{
    [Key]
    [Column("id")]
    public int Id {get;set;}

    [JsonPropertyName("Marca")]
    [Column("mark")]
    [MaxLength(100)]
    [Required(ErrorMessage = "A marca não pode ser vazio")]
    public string Mark {get;set;} = default!;


    [JsonPropertyName("Modelo")]
    [Column("model")]
    [MaxLength(100)]
    [Required(ErrorMessage = "O modelo não pode ser vazio")]
    public string Model {get;set;} = default!;

    [JsonPropertyName("Ano")]
    [Column("year")]
    public int Year { get; set; } = default!;
}