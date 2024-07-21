using Ardalis.Result;
using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.DevKit;

namespace Haseroz.Boilerplate.Auth.UseCases.SignIn;

public record SignInCommand(string Username, string Password) : ICommand<Result<TokenDto>>;
