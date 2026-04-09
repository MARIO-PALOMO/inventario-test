using Inventario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Inventario.Domain.Models.ResponseModel;

namespace Inventario.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        IResponseModel<List<Category>> GetAll();
    }
}
