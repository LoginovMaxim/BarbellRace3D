using System.Threading.Tasks;

namespace Assemblers
{
    public interface IAssemblerPart
    {
        Task Launch();
    }
}