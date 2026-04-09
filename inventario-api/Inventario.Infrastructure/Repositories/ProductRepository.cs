using Inventario.Domain.DTOs;
using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Inventario.Domain.Models;
using Inventario.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static Inventario.Domain.Models.ResponseModel;

namespace Inventario.Infrastructure.Repositories
{
    public class ProductRepository(InventoryDBContext dbContext) : IProductRepository
    {

        public IResponseModel<bool> UpdateStock(Transaction transaction)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {
                var product = dbContext.Product.Where(p => p.Id == transaction.ProductId).FirstOrDefault();
                var currentStock = product.Stock;
                var saveStock = transaction.Amount;

                if (transaction.TransactionTypeId == Constants.Venta && currentStock < saveStock)
                {
                    response.Status = false;
                    response.Message = "Stock insuficiente";
                }
                else
                {
                    var updatedStock = 0;

                    if (transaction.TransactionTypeId == Constants.Venta)
                    {
                        updatedStock = currentStock - saveStock;
                    }
                    else if (transaction.TransactionTypeId == Constants.Compra)
                    {
                        updatedStock = currentStock + saveStock;
                    }

                    product.Stock = updatedStock;
                    product.ModificationDate = DateTime.Now;

                    dbContext.Product.Attach(product);
                    dbContext.Entry(product).Property(u => u.Stock).IsModified = true;
                    dbContext.Entry(product).Property(u => u.ModificationDate).IsModified = true;

                    dbContext.SaveChanges();

                    response.Status = true;
                    response.Message = "Stock actualizado exitosamente";
                }




            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al actualizar stock, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al actualizar stock, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<List<ProductDTO>> Get(
            string? name = null,
            long? category = null,
            int pageNumber = 2,
            int pageSize = 10
            )
        {
            IResponseModel<List<ProductDTO>> response = new IResponseModel<List<ProductDTO>>();
            try
            {
                var products = (from p in dbContext.Product
                                join c in dbContext.Category on p.CategoryId equals c.Id
                                where
                                (name == null ? true : p.Name.ToLower().Contains(name.ToLower())) &&
                                (category == null ? true : p.CategoryId == category)
                                orderby p.CreationDate descending
                                select new ProductDTO
                                {
                                    Category = c,
                                    CategoryId = c.Id,
                                    ProductId = p.Id,
                                    Name = p.Name,
                                    Description = p.Description,
                                    Image = p.Image,
                                    Price = p.Price,
                                    Stock = p.Stock,
                                    StatusStock = p.Stock > 0 ? true : false,
                                    CreationDate = (DateTime)p.CreationDate,
                                    ModificationDate = (DateTime)p.ModificationDate


                                })
                            .Skip((int)((pageNumber - 1) * pageSize))
                            .Take((int)pageSize).ToList();

                var total = dbContext.Product.Where(p => (name == null ? true : p.Name.ToLower().Contains(name.ToLower())) && (category == null ? true : p.CategoryId == category)).Count();

                response.Total = total;
                response.Data = products;
                response.Status = true;
                response.Message = "Éxito";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar productos, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar productos, {ex.Message}";
            }

            return response;
        }


        public IResponseModel<List<ProductDTO>> GetAll()
        {
            IResponseModel<List<ProductDTO>> response = new IResponseModel<List<ProductDTO>>();
            try
            {
                var products = (from p in dbContext.Product
                                join c in dbContext.Category on p.CategoryId equals c.Id
                                orderby p.CreationDate descending
                                select new ProductDTO
                                {
                                    Category = c,
                                    CategoryId = c.Id,
                                    ProductId = p.Id,
                                    Name = p.Name,
                                    Description = p.Description,
                                    Image = p.Image,
                                    Price = p.Price,
                                    Stock = p.Stock,
                                    StatusStock = p.Stock > 0 ? true : false,
                                    CreationDate = (DateTime)p.CreationDate,
                                    ModificationDate = (DateTime)p.ModificationDate


                                }).ToList();


                response.Data = products;
                response.Status = true;
                response.Message = "Éxito";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar productos, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar productos, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<ProductDTO> GetById(Guid id)
        {
            IResponseModel<ProductDTO> response = new IResponseModel<ProductDTO>();
            try
            {
                var products = (from p in dbContext.Product
                                join c in dbContext.Category on p.CategoryId equals c.Id
                                where p.Id == id
                                select new ProductDTO
                                {
                                    Category = c,
                                    CategoryId = c.Id,
                                    ProductId = p.Id,
                                    Name = p.Name,
                                    Description = p.Description,
                                    Image = p.Image,
                                    Price = p.Price,
                                    Stock = p.Stock,
                                    StatusStock = p.Stock > 0 ? true : false,
                                    CreationDate = (DateTime)p.CreationDate,
                                    ModificationDate = (DateTime)p.ModificationDate


                                }).FirstOrDefault();


                response.Data = products;
                response.Status = true;
                response.Message = "Éxito";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar productos, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al buscar productos, {ex.Message}";
            }

            return response;
        }
        public IResponseModel<bool> Add(Product product)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {

                dbContext.Set<Product>().Add(product);
                dbContext.SaveChanges();


                response.Status = true;
                response.Message = "Producto Guardado Exitosamente";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al guardar producto, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al guardar producto, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<bool> Update(Product product)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {
                product.ModificationDate = DateTime.Now;
                dbContext.Product.Attach(product);
                dbContext.Entry(product).Property(u => u.Name).IsModified = true;
                dbContext.Entry(product).Property(u => u.Description).IsModified = true;
                dbContext.Entry(product).Property(u => u.CategoryId).IsModified = true;
                dbContext.Entry(product).Property(u => u.Image).IsModified = true;
                dbContext.Entry(product).Property(u => u.Price).IsModified = true;
                dbContext.Entry(product).Property(u => u.Stock).IsModified = true;
                dbContext.Entry(product).Property(u => u.ModificationDate).IsModified = true;
                dbContext.SaveChanges();

                response.Status = true;
                response.Message = "Producto Modificado Exitosamente";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al editar producto, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al editar producto, {ex.Message}";
            }

            return response;
        }

        public IResponseModel<bool> Delete(Guid id)
        {
            IResponseModel<bool> response = new IResponseModel<bool>();
            try
            {
                var product = dbContext.Product.Where(u => u.Id == id).FirstOrDefault();
                dbContext.Set<Product>().Remove(product);
                dbContext.SaveChanges();

                response.Status = true;
                response.Message = "Producto Eliminado Exitosamente";
            }
            catch (SqlException ex)
            {
                response.Status = false;
                response.Message = $"Error al eliminar producto, {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error al eliminar producto, {ex.Message}";
            }

            return response;
        }
    }
}
