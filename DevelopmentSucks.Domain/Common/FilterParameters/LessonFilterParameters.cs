namespace DevelopmentSucks.Domain.Common.FilterParameters;

public class LessonFilterParameters : PaginingParameters
{
    public int? MinOrder { get; set; }
    public int? MaxOrder { get; set; }
}
