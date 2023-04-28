using Ships.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ships.Application.ShipsQR.Commands.CreateTodoList;

public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateShipCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("The ship name title already exists.")
            .MustAsync(CodeIsOK).WithMessage("The ship code is not valid.");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Ships
            .AllAsync(l => l.Name != name, cancellationToken);
    }
    public async Task<bool> CodeIsOK(string name, CancellationToken cancellationToken)
    {
        return true;
    }
}
