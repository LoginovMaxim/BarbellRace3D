using System;

namespace App.Data
{
    public interface IData
    {
        event Action DataChanged;
    }
}