using System.Collections.Generic;

namespace Assemblers
{
    public sealed class GameAssembler : Assembler, IGameAssembler
    {
        public GameAssembler(List<IAssemblerPart> assemblerParts) : base(assemblerParts)
        {
        }
    }
}