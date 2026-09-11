namespace MovieCatalog.Api.DTOs;

public class MovieQueryParams
{
    public string? Genre { get; set; }
    public int? Year { get; set; }
    public string? SortBy { get; set; } = "title";
    public bool Descending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}