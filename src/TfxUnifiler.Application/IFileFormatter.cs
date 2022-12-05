using Xyz.TForce.Unifiler.Application.Models;

namespace Xyz.TForce.Unifiler.Application
{

  public interface IFileFormatter
  {

    string CreateMd5File(CreateMd5FileArguments args);

    string CreateSfvFile(CreateSfvFileArguments args);

    string CreateShaFile(CreateShaFileArguments args);
  }
}
