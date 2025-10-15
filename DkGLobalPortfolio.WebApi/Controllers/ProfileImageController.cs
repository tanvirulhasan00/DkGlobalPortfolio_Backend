
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Models.Profile.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/profile-images")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public ProfileImageController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllProfileImage(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.ProfileImages.GetAllAsync(new GenericServiceRequest<ProfileImage>
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
        public async Task<ApiResponse> GetProfileImage(int Id, CancellationToken cancellationToken)
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
                var data = await _serviceManager.ProfileImages.GetAsync(new GenericServiceRequest<ProfileImage>
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
        public async Task<ApiResponse> CreateProfileImage(CreateProfileImageUrlDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var profileImageUrl = "";
                if (dto == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Empty request data";
                    return response;
                } 
                if(dto.ImageUrl != null)
                {
                    profileImageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "profile-images");
                }

                var toCreate = new ProfileImage
                {
                    Title = dto.Title,
                    ImageUrl = profileImageUrl,
                    CompanyInfoId = dto.OwnerId,
                    IsActive = true
                };
                await _serviceManager.ProfileImages.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateProfileImage(UpdateProfileImageUrlDto dto, CancellationToken cancellationToken)
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
                var toUpdate = await _serviceManager.ProfileImages.GetAsync(new GenericServiceRequest<ProfileImage>
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

                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "profile-images");
                }
                
                toUpdate.Title = dto.Title ?? toUpdate.Title;
                toUpdate.ImageUrl = imageUrl != "" ? imageUrl : toUpdate.ImageUrl;
                toUpdate.CompanyInfoId = dto.OwnerId <= 0 ? toUpdate.CompanyInfoId : dto.OwnerId;
                
                _serviceManager.ProfileImages.Update(toUpdate);
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
        public async Task<ApiResponse> DeleteProfileImage(int Id, CancellationToken cancellationToken)
        {
            if(Id <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDelete = await _serviceManager.ProfileImages.GetAsync(new GenericServiceRequest<ProfileImage>
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
            _serviceManager.ProfileImages.Remove(toDelete);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
