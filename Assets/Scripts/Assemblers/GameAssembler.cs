using System.Collections.Generic;

namespace App.Assemblers
{
    public sealed class GameAssembler : Assembler, IGameAssembler
    {
        public GameAssembler(List<IAssemblerPart> assemblerParts) : base(assemblerParts)
        {
        }
    }
}