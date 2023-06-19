using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WebApi.Entities;

[JsonObject(Title = "Itens Pedidos")]
[Table("orderItems")]
public record OrderItem
{
    [Key]
    [Column("id")]
    public int Id {get;set;}

    [JsonPropertyName("order_id")]
    [Column("order_id")]
    [Required(ErrorMessage = "O order_id é obrigatório")]
    public int OrderId {get;set;}
    [Newtonsoft.Json.JsonIgnore]
    public Order Order {get;set;} = default!;

    [JsonPropertyName("product_id")]
    [Column("product_id")]
    [Required(ErrorMessage = "O product_id é obrigatório")]
    public int ProductId {get;set;}

    [Newtonsoft.Json.JsonIgnore]
    public Product Product {get;set;} = default!;

    [JsonPropertyName("length")]
    [Column("length")]
    public int Length {get;set;}
    
    [JsonPropertyName("Valor")]
    [Column("price")]
    public double Price {get;set;}
}