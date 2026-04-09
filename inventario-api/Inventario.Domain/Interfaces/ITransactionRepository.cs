using Inventario.Domain.DTOs;
using Inventario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Inventario.Domain.Models.ResponseModel;

namespace Inventario.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        IResponseModel<bool> Add(Transaction transaction);
        IResponseModel<List<TransactionDTO>> Get(
                                                   string start,
                                                   string end,
                                                   long? transaction = null,
                                                   string? product = null,
                                                   int pageNumber = 2,
                                                   int pageSize = 10);

        IResponseModel<List<TransactionProductDTO>> GetGroupByProduct(
                                                   string start,
                                                   string end,
                                                   long? transaction = null,
                                                   int pageNumber = 2,
                                                   int pageSize = 10);

        IResponseModel<bool> Update(Transaction transaction);
        IResponseModel<bool> Delete(long id);
    }
}
