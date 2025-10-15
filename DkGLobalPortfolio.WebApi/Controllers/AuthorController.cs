using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Blog.Dto;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Leader.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/blogs/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public AuthorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllAuthor(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.Authors.GetAllAsync(new GenericServiceRequest<Author>
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
        public async Task<ApiResponse> GetAuthor(int AuthorId, CancellationToken cancellationToken)
        {
            try
            {
                if (AuthorId <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id not found.";
                    return response;
                }
                var data = await _serviceManager.Authors.GetAsync(new GenericServiceRequest<Author>
                {
                    Expression = x => x.Id == AuthorId,
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
        public async Task<ApiResponse> CreateAuthor(CreateAuthorDto dto, CancellationToken cancellationToken)
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
                if(dto.Avatar != null)
                {
                    imageUrl = await _serviceManager.File.FileUpload(dto.Avatar, "blogs/author-images");
                }
                
                var toCreate = new Author
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Bio = dto.Bio,
                    Avatar = imageUrl,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                await _serviceManager.Authors.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateAuthor(UpdateAuthorDto dto, CancellationToken cancellationToken)
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
                var toUpdate = await _serviceManager.Authors.GetAsync(new GenericServiceRequest<Author>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var imageUrl = "";
                if(dto.Avatar != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(toUpdate.Avatar))
                    {
                        _serviceManager.File.DeleteFile(toUpdate.Avatar);
                    }

                    imageUrl = await _serviceManager.File.FileUpload(dto.Avatar, "blogs/author-images");
                }


                toUpdate.FirstName = dto.FirstName ?? toUpdate.FirstName;
                toUpdate.LastName = dto.LastName ?? toUpdate.LastName;
                toUpdate.Email = dto.Email ?? toUpdate.Email;
                toUpdate.Bio = dto.Bio ?? toUpdate.Bio;
                toUpdate.Avatar = imageUrl != "" ? imageUrl : toUpdate.Avatar;
                toUpdate.UpdatedAt = DateTime.Now;
                
                _serviceManager.Authors.Update(toUpdate);
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
        public async Task<ApiResponse> DeleteAuthor(int AuthorId, CancellationToken cancellationToken)
        {
            if(AuthorId <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDelete = await _serviceManager.Authors.GetAsync(new GenericServiceRequest<Author>
            {
                Expression = x=>x.Id == AuthorId,
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
            if (!string.IsNullOrEmpty(toDelete.Avatar))
            {
                _serviceManager.File.DeleteFile(toDelete.Avatar);
            }

            _serviceManager.Authors.Remove(toDelete);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
