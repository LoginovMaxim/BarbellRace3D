using System;

namespace Data
{
    public interface IData
    {
        event Action DataChanged;
    }
}