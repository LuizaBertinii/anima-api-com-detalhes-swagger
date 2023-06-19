using System.Text.Json.Serialization;

namespace Exercicio.ModelViews;

public struct Home
{
    [JsonPropertyName("Mensagem")]
    public string Message { get; set;}
}