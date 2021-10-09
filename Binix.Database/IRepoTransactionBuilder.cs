using System;

namespace Binix.Database
{
    public interface IRepoTransactionBuilder
    {
        IRepoTransaction Build();
    }
}