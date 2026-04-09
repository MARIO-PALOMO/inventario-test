using Inventario.Domain.DTOs;
using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Inventario.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static Inventario.Domain.Models.ResponseModel;

namespace Inventario.Infrastructure.Repositories
{
    partial class TransactionRepository(InventoryDBContext dbContext, IProductRepository productRepository) : ITransactionRepository
    {
        public IResponseModel<bool> Add(Transaction transaction)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {

                var stockValidation = productRepository.UpdateStock(transaction);

                if (stockValidation.Status)
                {
                    transaction.Date = DateTime.Now;
                    dbContext.Set<Transaction>().Add(transaction);
                    dbContext.SaveChanges();

                    response.Status = true;
                    response.Message = "Transacción Guardada Exitosamente";
                }
                else
                {
                    response.Status = false;
                    response.Message = stockValidation.Message;
                }


            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al guardar transacción, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al guardar transacción, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<bool> Update(Transaction transaction)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {
                transaction.ModificationDate = DateTime.Now;
                transaction.Date = DateTime.Now;
                dbContext.Transaction.Attach(transaction);
                dbContext.Entry(transaction).Property(u => u.TransactionTypeId).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.ProductId).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.Amount).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.UnitPrice).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.TotalPrice).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.Detail).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.ModificationDate).IsModified = true;
                dbContext.Entry(transaction).Property(u => u.Date).IsModified = true;
                dbContext.SaveChanges();

                response.Status = true;
                response.Message = "Transacción Modificada Exitosamente";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al editar transacción, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al editar transacción, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<bool> Delete(long id)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {
                var transaction = dbContext.Transaction.Where(u => u.Id == id).FirstOrDefault();
                dbContext.Set<Transaction>().Remove(transaction);
                dbContext.SaveChanges();

                response.Status = true;
                response.Message = "Transacción Eliminada Exitosamente";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al eliminar transacción, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al eliminar transacción, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<List<TransactionDTO>> Get(
                                                   string start,
                                                   string end,
                                                   long? transaction = null,
                                                   string? product= null,
                                                   int pageNumber = 2,
                                                   int pageSize = 10)
        {
            IResponseModel<List<TransactionDTO>> response = new IResponseModel<List<TransactionDTO>>();
            try
            {
                DateTime startDate = DateTime.ParseExact(start, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(end, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                var transactions = (from p in dbContext.Product
                                    join c in dbContext.Category on p.CategoryId equals c.Id
                                    join t in dbContext.Transaction on p.Id equals t.ProductId
                                    join y in dbContext.TransactionType on t.TransactionTypeId equals y.Id
                                    where
                                    (transaction == null ? true : t.TransactionTypeId == transaction) &&
                                    (product == null ? true : t.Product.Name.ToLower().Contains(product.ToLower())) &&
                                    (t.Date >= startDate && t.Date <= endDate)
                                    orderby t.Date, p.Id descending
                                    select new TransactionDTO
                                    {
                                        TransactionId = t.Id,
                                        Code = t.Code,
                                        Date = t.Date,
                                        TransactionTypeId = t.TransactionTypeId,
                                        TransactionType = y,
                                        ProductId = t.ProductId,
                                        Product = p,
                                        Amount = t.Amount,
                                        UnitPrice = t.UnitPrice,
                                        TotalPrice = t.TotalPrice,
                                        Detail = t.Detail,
                                        CreationDate = (DateTime)t.CreationDate,
                                        ModificationDate = (DateTime)t.ModificationDate


                                    })
                            .Skip((int)((pageNumber - 1) * pageSize))
                            .Take((int)pageSize).ToList();

                var total = dbContext.Transaction.Where(t => (transaction == null ? true : t.TransactionTypeId == transaction) && (t.Date >= startDate && t.Date <= endDate)).Count();

                response.Total = total;
                response.Data = transactions;
                response.Status = true;
                response.Message = "Éxito";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar transacciones, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar transacciones, {ex.Message}";
            }

            return response;
        }


        public IResponseModel<List<TransactionProductDTO>> GetGroupByProduct(
                                                   string start,
                                                   string end,
                                                   long? transaction = null,
                                                   int pageNumber = 2,
                                                   int pageSize = 10)
        {
            IResponseModel<List<TransactionProductDTO>> response = new IResponseModel<List<TransactionProductDTO>>();
            try
            {
                DateTime startDate = DateTime.ParseExact(start, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(end, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                var query = from t in dbContext.Transaction
                                   join p in dbContext.Product on t.ProductId equals p.Id
                                   join y in dbContext.TransactionType on t.TransactionTypeId equals y.Id
                                   where
                                   (transaction == null ? true : t.TransactionTypeId == transaction) &&
                                   (t.Date >= startDate && t.Date <= endDate)
                                   group t by new { ProductId = p.Id, ProductName = p.Name, p.Stock, TransactionTypeName = y.Name } into g
                                   select new TransactionProductDTO
                                   {
                                       Name = g.Key.ProductName,
                                       CurrentStock = g.Key.Stock,
                                       TransactionType = g.Key.TransactionTypeName,
                                       TotalTransactionType = g.Count(),
                                       TotalPurchasesAmount = g.Sum(x => x.TransactionTypeId == 2 ? x.Amount : 0),
                                       TotalSalesAmount = g.Sum(x => x.TransactionTypeId == 1 ? x.Amount : 0),
                                       TotalPurchasesPrice = g.Sum(x => x.TransactionTypeId == 2 ? x.TotalPrice : 0),
                                       TotalSalesPrice = g.Sum(x => x.TransactionTypeId == 1 ? x.TotalPrice : 0)
                                   };
                            

                var transactions = query.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToList();
                var total = query.Count();

                response.Total = total;
                response.Data = transactions;
                response.Status = true;
                response.Message = "Éxito";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar transacciones, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar transacciones, {ex.Message}";
            }

            return response;
        }
    }
}
