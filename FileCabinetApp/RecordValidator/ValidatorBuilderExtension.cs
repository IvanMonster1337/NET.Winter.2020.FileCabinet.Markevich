﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>Class-builder for validator creating.</summary>
    public static class ValidatorBuilderExtension
    {
        /// <summary>Creates the default validator.</summary>
        /// <param name="builder">The builder.</param>
        /// <returns>Returns the default validator.</returns>
        public static IRecordValidator CreateDefault(this ValidatorBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("validation-rules.json").Build();

            FirstNameValidator firstNameValidator =
                config.GetSection("default:firstName").Get<FirstNameValidator>();
            LastNameValidator lastNameValidator =
                config.GetSection("default:lastName").Get<LastNameValidator>();
            DateOfBirthValidator dateOfBirthValidator =
                config.GetSection("default:dateOfBirth").Get<DateOfBirthValidator>();
            CodeValidator codeValidator =
                config.GetSection("default:code").Get<CodeValidator>();
            LetterValidator letterValidator =
                config.GetSection("default:letter").Get<LetterValidator>();
            BalanceValidator balanceValidator =
                config.GetSection("default:balance").Get<BalanceValidator>();

            return new CompositeValidator(new IRecordValidator[]
                {
                    firstNameValidator, lastNameValidator,
                    dateOfBirthValidator, codeValidator,
                    letterValidator, balanceValidator,
                });
        }

        /// <summary>Creates the custom validator.</summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The validator.</returns>
        public static IRecordValidator CreateCustom(this ValidatorBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("validation-rules.json").Build();

            FirstNameValidator firstNameValidator =
                config.GetSection("custom:firstName").Get<FirstNameValidator>();
            LastNameValidator lastNameValidator =
                config.GetSection("custom:lastName").Get<LastNameValidator>();
            DateOfBirthValidator dateOfBirthValidator =
                config.GetSection("custom:dateOfBirth").Get<DateOfBirthValidator>();
            CodeValidator codeValidator =
                config.GetSection("custom:code").Get<CodeValidator>();
            LetterValidator letterValidator =
                config.GetSection("custom:letter").Get<LetterValidator>();
            BalanceValidator balanceValidator =
                config.GetSection("custom:balance").Get<BalanceValidator>();

            return new CompositeValidator(new IRecordValidator[]
            {
                firstNameValidator, lastNameValidator,
                dateOfBirthValidator, codeValidator,
                letterValidator, balanceValidator,
            });
        }
    }
}
