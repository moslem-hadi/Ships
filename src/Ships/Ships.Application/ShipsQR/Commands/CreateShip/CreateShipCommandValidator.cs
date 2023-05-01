﻿using Ships.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Ships.Application.ShipsQR.Commands;

public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand> 
{
    private readonly IApplicationDbContext _context;

    public CreateShipCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(v => v.ShipCode)
            .NotEmpty().WithMessage("ShipCode is required.")
            .MaximumLength(12).WithMessage("ShipCode must not exceed 12 characters.")
            .MustAsync(
                async (model, shipId, cancellation) =>
                {
                    return await IsUniqueShipCodeAsync(model.ShipCode, model.Id, cancellation);
                }
             ).WithMessage("{PropertyName} must be unique.")
            .Must(CodeIsValid).WithMessage("The ship code is not valid.");
    }


    private async Task<bool> IsUniqueShipCodeAsync(string shipCode, int? shipId, CancellationToken cancellationToken)
    {
        return await _context.Ships
            .AllAsync(l => l.ShipCode.Code != shipCode && (shipId == null || l.Id != shipId), cancellationToken);
    }
    public bool CodeIsValid(string shipCode)
    {
        var regex = @"^[a-zA-Z]{4}[-]{1}\d{4}[-]{1}[a-zA-Z]{1}\d{1}$";
        var match = Regex.Match(shipCode, regex, RegexOptions.IgnoreCase);
        return match.Success;
    }
}
