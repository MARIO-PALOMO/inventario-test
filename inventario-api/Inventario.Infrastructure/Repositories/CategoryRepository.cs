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
    public class CategoryRepository(InventoryDBContext dbContext) : ICategoryRepository
    {

        public IResponseModel<List<Category>> GetAll()
        {
            IResponseModel<List<Category>> response = new IResponseModel<List<Category>>();
            try
            {
                var categories = dbContext.Category.ToList();

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
