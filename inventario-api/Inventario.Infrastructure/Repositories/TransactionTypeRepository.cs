using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Inventario.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Inventario.Domain.Models.ResponseModel;

namespace Inventario.Infrastructure.Repositories
{
    public class TransactionTypeRepository(InventoryDBContext dbContext) : ITransactionTypeRepository
    {
        public IResponseModel<List<TransactionType>> GetAll()
        {
            IResponseModel<List<TransactionType>> response = new IResponseModel<List<TransactionType>>();
            try
            {
                var categories = dbContext.TransactionType.ToList();

                response.Data = categories;
                response.Status = true;
                response.Message = "Éxito";

            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error, {ex.Message}";
            }

            return response;
        }
    }
}
