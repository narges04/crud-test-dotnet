﻿using FluentValidation;
using Mc2.CrudTest.Presentation.Contracts.Customers;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Presentation.Application.Customers;

public class CustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a first name");
        RuleFor(x => x.Email).Must(BeAValidEmail).WithMessage("Email format is not valid");
        RuleFor(x => x.PhoneNumber).Must(BeAValidPhoneNumber).WithMessage("Phone number format is not valid");
        RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.Now).WithMessage("Date of birth should not greater than today date");
    }

    private bool BeAValidEmail(string email)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
            return true;
        else
            return false;
    }
    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
        try
        {
            PhoneNumbers.PhoneNumber number = phoneNumberUtil.Parse(phoneNumber, default);
            return phoneNumberUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }
}
