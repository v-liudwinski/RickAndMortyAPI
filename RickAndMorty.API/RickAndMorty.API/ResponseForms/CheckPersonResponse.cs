namespace RickAndMorty.API.ResponseForms;

public class CheckPersonResponse
{
    public string PersonName { get; set; }
    public string EpisodeName { get; set; }
    public bool IsPersonInEpisode { get; set; }
}