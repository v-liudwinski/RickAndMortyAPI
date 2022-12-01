namespace RickAndMorty.BLL.Models;

public class Episode : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? AirDate { get; set; }
    public string EpisodeCode { get; set; }
    public IEnumerable<Uri> Characters { get; set; }
}