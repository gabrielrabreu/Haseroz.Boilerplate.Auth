using Ardalis.Result;
using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.DevKit;

namespace Haseroz.Boilerplate.Auth.UseCases.SignUp;

public record SignUpCommand(string Username, string Email, string Password) : ICommand<Result<TokenDto>>;
