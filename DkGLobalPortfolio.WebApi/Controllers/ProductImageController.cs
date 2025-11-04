
using DkGLobalPortfolio.WebApi.Models.Product;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Models.Profile.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/product-images")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public ProductImageController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllProductImage(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.ProductImages.GetAllAsync(new GenericServiceRequest<ProductImage>
                {
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (!data.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found.";
                    return response;
                }

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = data;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<ApiResponse> GetProductImage(int Id, CancellationToken cancellationToken)
        {
            try
            {
                if (Id <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id not found.";
                    return response;
                }
                var data = await _serviceManager.ProductImages.GetAsync(new GenericServiceRequest<ProductImage>
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found.";
                    return response;
                }

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = data;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ApiResponse> CreateProductImage(CreateProfileImageUrlDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var productImageUrl = "";
                if (dto == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Empty request data";
                    return response;
                } 
                if(dto.ImageUrl != null)
                {
                    productImageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "product-images");
                }

                var toCreate = new ProductImage
                {
                    Title = dto.Title,
                    SearchText = dto.SearchText,
                    ImageUrl = productImageUrl,
                    ProductId = dto.OwnerId,
                    IsActive = true
                };
                await _serviceManager.ProductImages.AddAsync(toCreate);
                await _serviceManager.Save();

                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Created Successfully";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ApiResponse> UpdateProductImage(UpdateProfileImageUrlDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto.Id <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id required.";
                    return response;
                }
                var toUpdate = await _serviceManager.ProductImages.GetAsync(new GenericServiceRequest<ProductImage>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var imageUrl = "";
                if(dto.ImageUrl != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(toUpdate.ImageUrl))
                    {
                        _serviceManager.File.DeleteFile(toUpdate.ImageUrl);
                    }

                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "product-images");
                }
                
                toUpdate.Title = dto.Title ?? toUpdate.Title;
                toUpdate.SearchText = dto.SearchText ?? toUpdate.SearchText;
                toUpdate.ImageUrl = imageUrl != "" ? imageUrl : toUpdate.ImageUrl;
                toUpdate.ProductId = dto.OwnerId <= 0 ? toUpdate.ProductId : dto.OwnerId;
                
                _serviceManager.ProductImages.Update(toUpdate);
                await _serviceManager.Save();

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Updated Successfully";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpPut]
        [Route("update-by-productid")]
        public async Task<ApiResponse> UpdateProductImageByProductId(UpdateProfileImageUrlDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto.OwnerId <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id required.";
                    return response;
                }
                var toUpdate = await _serviceManager.ProductImages.GetAsync(new GenericServiceRequest<ProductImage>
                {
                    Expression = b => b.ProductId == dto.OwnerId,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var imageUrl = "";
                if (dto.ImageUrl != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(toUpdate.ImageUrl))
                    {
                        _serviceManager.File.DeleteFile(toUpdate.ImageUrl);
                    }

                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "product-images");
                }

                toUpdate.Title = dto.Title ?? toUpdate.Title;
                toUpdate.ImageUrl = imageUrl != "" ? imageUrl : toUpdate.ImageUrl;

                _serviceManager.ProductImages.Update(toUpdate);
                await _serviceManager.Save();

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Updated Successfully";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ApiResponse> DeleteProductImage(int Id, CancellationToken cancellationToken)
        {
            if(Id <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDelete = await _serviceManager.ProductImages.GetAsync(new GenericServiceRequest<ProductImage>
            {
                Expression = x=>x.Id == Id,
                NoTracking = true,
                CancellationToken = cancellationToken
            });
            if(toDelete == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "No data found to delete";
                return response;
            }
            //delete old images
            if (!string.IsNullOrEmpty(toDelete.ImageUrl))
            {
                _serviceManager.File.DeleteFile(toDelete.ImageUrl);
            }
            _serviceManager.ProductImages.Remove(toDelete);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
