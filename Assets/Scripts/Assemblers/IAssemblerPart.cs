using System.Threading.Tasks;

namespace App.Assemblers
{
    public interface IAssemblerPart
    {
        Task Launch();
    }
}