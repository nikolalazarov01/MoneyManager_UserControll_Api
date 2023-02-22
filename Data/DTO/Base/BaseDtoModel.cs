using System.Text.Json.Serialization;
using Data.Models.DTO.Hateoas;

namespace Data.Models.DTO.Base;

public abstract class BaseDtoModel
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<HateoasLink> Links { get; set; }
}