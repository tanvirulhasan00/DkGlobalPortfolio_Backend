using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Blog.Dto;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Leader.Dto;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Partner.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/partners")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public PartnerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllPartner(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.Partners.GetAllAsync(new GenericServiceRequest<Partner>
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
        public async Task<ApiResponse> GetPartner(int PartnerId, CancellationToken cancellationToken)
        {
            try
            {
                if (PartnerId <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id not found.";
                    return response;
                }
                var data = await _serviceManager.Partners.GetAsync(new GenericServiceRequest<Partner>
                {
                    Expression = x => x.Id == PartnerId,
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
        public async Task<ApiResponse> CreatePartner(CreatePartnerDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var imageUrl = "";
                if (dto == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Empty request data";
                    return response;
                } 
                if(dto.ImageUrl != null)
                {
                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "images");
                }
                
                var toCreate = new Partner
                {
                    Title = dto.Title,
                    ImageUrl = imageUrl,
                    IsActive = true
                };
                await _serviceManager.Partners.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdatePartner(UpdatePartnerDto dto, CancellationToken cancellationToken)
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
                var toUpdateData = await _serviceManager.Partners.GetAsync(new GenericServiceRequest<Partner>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var imageUrl = "";
                if(dto.ImageUrl != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(toUpdateData.ImageUrl))
                    {
                        _serviceManager.File.DeleteFile(toUpdateData.ImageUrl);
                    }

                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "images");
                }


                toUpdateData.Title = dto.Title ?? toUpdateData.Title;
                toUpdateData.ImageUrl = imageUrl != "" ? imageUrl : toUpdateData.ImageUrl;
                
                _serviceManager.Partners.Update(toUpdateData);
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
        public async Task<ApiResponse> DeletePartner(int PartnerId, CancellationToken cancellationToken)
        {
            if(PartnerId <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDeleteData = await _serviceManager.Partners.GetAsync(new GenericServiceRequest<Partner>
            {
                Expression = x=>x.Id == PartnerId,
                NoTracking = true,
                CancellationToken = cancellationToken
            });
            if(toDeleteData == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "No data found to delete";
                return response;
            }

            _serviceManager.Partners.Remove(toDeleteData);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
