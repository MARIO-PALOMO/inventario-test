using Inventario.Domain.DTOs;
using Inventario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Inventario.Domain.Models.ResponseModel;

namespace Inventario.Domain.Interfaces
{
    public interface IProductRepository
    {
        IResponseModel<List<ProductDTO>> Get(
            string? name = null,
            long? category = null,
            int pageNumber = 2,
            int pageSize = 10
            );
        IResponseModel<List<ProductDTO>> GetAll();
        IResponseModel<ProductDTO> GetById(Guid id);
        IResponseModel<bool> Add(Product product);
        IResponseModel<bool> Update(Product product);
        IResponseModel<bool> Delete(Guid id);
        IResponseModel<bool> UpdateStock(Transaction transaction);
    }
}
