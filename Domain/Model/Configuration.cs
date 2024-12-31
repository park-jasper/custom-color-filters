namespace Domain.Model;

public record Configuration(string SelectedFilter, bool ApplyDefaultFilterOnStart, IReadOnlyList<Filter> CustomFilters);
