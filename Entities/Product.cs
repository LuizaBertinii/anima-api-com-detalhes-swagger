using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WebApi.Entities;

[JsonObject(Title = "Produtos")]
[Table("product")]
public record Product
{
    [Key]
    [Column("id")]
    public int Id {get;set;}

    [JsonPropertyName("Nome")]
    [Column("name")]
    public string Name {get;set;} = default!;

    [JsonPropertyName("Descrição")]
    [Column("description")]
    public string Description {get;set;} = default!;

    [JsonPropertyName("Preço")]
    [Column("price")]
    public double Price {get;set;} = default!;
}