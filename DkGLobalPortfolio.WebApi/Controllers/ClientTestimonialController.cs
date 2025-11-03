using DkGLobalPortfolio.WebApi.Models.ClientTestimonial;
using DkGLobalPortfolio.WebApi.Models.ClientTestimonialDto.Dto;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Leader.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/client-testimonial")]
    [ApiController]
    public class ClientTestimonialController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ApiResponse response;
        public ClientTestimonialController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllClientTestimonial(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.ClientTestimonials.GetAllAsync(new GenericServiceRequest<ClientTestimonial>
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
        public async Task<ApiResponse> GetClientTestimonial(int Id, CancellationToken cancellationToken)
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
                var data = await _serviceManager.ClientTestimonials.GetAsync(new GenericServiceRequest<ClientTestimonial>
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
        public async Task<ApiResponse> CreateClientTestimonial(CreateClientTestimonialDto dto, CancellationToken cancellationToken)
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
                
                var toCreate = new ClientTestimonial
                {
                    Name = dto.Name,
                    CompanyName = dto.CompanyName,
                    Message = dto.Message,
                    ImageUrl = imageUrl,
                    ReviewStars = dto.ReviewStars,
                    IsActive = true
                };
                await _serviceManager.ClientTestimonials.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateClientTestimonial(UpdateClientTestimonialDto dto, CancellationToken cancellationToken)
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
                var data = await _serviceManager.ClientTestimonials.GetAsync(new GenericServiceRequest<ClientTestimonial>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var imageUrl = "";
                if(dto.ImageUrl != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(data.ImageUrl))
                    {
                        _serviceManager.File.DeleteFile(data.ImageUrl);
                    }

                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "images");
                }
                

                data.Name = dto.Name ?? data.Name;
                data.CompanyName = dto.CompanyName ?? data.CompanyName;
                data.Message = dto.Message ?? data.Message;
                data.ReviewStars = dto.ReviewStars <= 0 ? data.ReviewStars : dto.ReviewStars;
                data.ImageUrl = imageUrl != "" ? imageUrl : data.ImageUrl;
                
                _serviceManager.ClientTestimonials.Update(data);
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
        public async Task<ApiResponse> DeleteClientTestimonial(int Id, CancellationToken cancellationToken)
        {
            if(Id <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var data = await _serviceManager.ClientTestimonials.GetAsync(new GenericServiceRequest<ClientTestimonial>
            {
                Expression = x=>x.Id == Id,
                NoTracking = true,
                CancellationToken = cancellationToken
            });
            if(data == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "No data found to delete";
                return response;
            }

            //delete old images
            if (!string.IsNullOrEmpty(data.ImageUrl))
            {
                _serviceManager.File.DeleteFile(data.ImageUrl);
            }

            _serviceManager.ClientTestimonials.Remove(data);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
