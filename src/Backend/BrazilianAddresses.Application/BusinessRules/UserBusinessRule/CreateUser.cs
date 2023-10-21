using AutoMapper;
using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;
using BrazilianAddresses.Application.Services.Cryptography;
using BrazilianAddresses.Application.Validators.UserValidator;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation.Results;

namespace BrazilianAddresses.Application.BusinessRules.UserBusinessRule
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly IMapper _mapper;

        private readonly IWorkUnit _workUnit;

        private readonly PasswordEncryption _passwordEncryption;

        public CreateUser(IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper, IWorkUnit workUnit, PasswordEncryption passwordEncryption)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
            _passwordEncryption = passwordEncryption;
        }

        public async Task<BaseResponseJson> Execute(UserRequestJson userRequestJson)
        {
            await ValidateUser(userRequestJson);

            User user = _mapper.Map<User>(userRequestJson);

            user.Salt = Guid.NewGuid().ToString().Replace("-", "");

            user.Password = _passwordEncryption.Encrypt(userRequestJson.Password, user.Salt);

            await _userWriteOnlyRepository.Add(user);

            await _workUnit.Commit();

            return new BaseResponseJson
            {
                Success = true,
                Message = APIMSG.USER_CREATED
            };
        }

        private async Task ValidateUser(UserRequestJson userRequestJson)
        {
            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            if (!validationResult.IsValid)
            {
                List<string> errorMessage = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessage);
            }

            bool userExists = await _userReadOnlyRepository.ExistsUserWithEmail(userRequestJson.Email);

            if (userExists)
                throw new ValidationException(string.Format(APIMSG.EXISTING_EMAIL, userRequestJson.Email));
        }
    }
}
