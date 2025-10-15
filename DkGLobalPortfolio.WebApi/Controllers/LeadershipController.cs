using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Leader.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/leadership")]
    [ApiController]
    public class LeadershipController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public LeadershipController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllLeadership(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.Leaderships.GetAllAsync(new GenericServiceRequest<LeaderShip>
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
        public async Task<ApiResponse> GetLeadership(int LeaderId, CancellationToken cancellationToken)
        {
            try
            {
                if (LeaderId <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id not found.";
                    return response;
                }
                var data = await _serviceManager.Leaderships.GetAsync(new GenericServiceRequest<LeaderShip>
                {
                    Expression = x => x.Id == LeaderId,
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
        public async Task<ApiResponse> CreateLeadership(CreateLeadershipDto dto, CancellationToken cancellationToken)
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
                
                var toCreate = new LeaderShip
                {
                    Name = dto.Name,
                    Designation = dto.Designation,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    ImageUrl = imageUrl,
                    IsActive = true
                };
                await _serviceManager.Leaderships.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateLeadership(UpdateLeadershipDto dto, CancellationToken cancellationToken)
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
                var leaderData = await _serviceManager.Leaderships.GetAsync(new GenericServiceRequest<LeaderShip>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var imageUrl = "";
                if(dto.ImageUrl != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(leaderData.ImageUrl))
                    {
                        _serviceManager.File.DeleteFile(leaderData.ImageUrl);
                    }

                    imageUrl = await _serviceManager.File.FileUpload(dto.ImageUrl, "images");
                }
                

                leaderData.Name = dto.Name ?? leaderData.Name;
                leaderData.Designation = dto.Designation ?? leaderData.Designation;
                leaderData.Email = dto.Email ?? leaderData.Email;
                leaderData.PhoneNumber = dto.PhoneNumber ?? leaderData.PhoneNumber;
                leaderData.ImageUrl = imageUrl != "" ? imageUrl : leaderData.ImageUrl;
                
                _serviceManager.Leaderships.Update(leaderData);
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
        public async Task<ApiResponse> DeleteLeadership(int LeaderShipId, CancellationToken cancellationToken)
        {
            if(LeaderShipId <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var leaderData = await _serviceManager.Leaderships.GetAsync(new GenericServiceRequest<LeaderShip>
            {
                Expression = x=>x.Id == LeaderShipId,
                NoTracking = true,
                CancellationToken = cancellationToken
            });
            if(leaderData == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "No data found to delete";
                return response;
            }

            //delete old images
            if (!string.IsNullOrEmpty(leaderData.ImageUrl))
            {
                _serviceManager.File.DeleteFile(leaderData.ImageUrl);
            }

            _serviceManager.Leaderships.Remove(leaderData);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
