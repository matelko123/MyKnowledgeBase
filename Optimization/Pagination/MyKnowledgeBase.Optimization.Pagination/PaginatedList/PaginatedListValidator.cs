using FluentValidation;

namespace MyKnowledgeBase.Optimization.Pagination.PaginatedList;

public class PaginatedListValidator : AbstractValidator<PaginatedListRequest>
{
    public PaginatedListValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25).WithMessage("Page size can only be between 1 and 25 per page");
    }
}