public class PagedMovieResult
{
    public int movieid { get; set; }
    public string title { get; set; } = string.Empty;
    public int duration { get; set; }
    public int releaseyear { get; set; }
    public float rating { get; set; }
    public string? posterurl { get; set; }
    public string? status { get; set; }
    public string? genres { get; set; }
    public long totalcount { get; set; }
}