using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Blog.Dto;
using DkGLobalPortfolio.WebApi.Models.Report;
using DkGLobalPortfolio.WebApi.Models.Report.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/report-category")]
    [ApiController]
    public class ReportCategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public ReportCategoryController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllReportCategory(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.ReportCategories.GetAllAsync(new GenericServiceRequest<ReportCategory>
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
        public async Task<ApiResponse> GetReportCategory(int CatId, CancellationToken cancellationToken)
        {
            try
            {
                if (CatId <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id not found.";
                    return response;
                }
                var data = await _serviceManager.ReportCategories.GetAsync(new GenericServiceRequest<ReportCategory>
                {
                    Expression = x => x.Id == CatId,
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
        public async Task<ApiResponse> CreateReportCategory(CreateReportCategoryDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var slug = string.Empty;
                if (dto == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Empty request data";
                    return response;
                }
                
                var toCreate = new ReportCategory
                {
                    Name = dto.Name,
                    ShortDescription = dto.ShortDescription,
                    IsActive = true
                };
                await _serviceManager.ReportCategories.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateReportCategory(UpdateReportCategoryDto dto, CancellationToken cancellationToken)
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
                var toUpdate = await _serviceManager.ReportCategories.GetAsync(new GenericServiceRequest<ReportCategory>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });

                toUpdate.Name = dto.Name ?? toUpdate.Name;
                toUpdate.ShortDescription = dto.ShortDescription ?? toUpdate.ShortDescription;
                
                _serviceManager.ReportCategories.Update(toUpdate);
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
        public async Task<ApiResponse> DeleteReportCategory(int CatId, CancellationToken cancellationToken)
        {
            if(CatId <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDelete = await _serviceManager.ReportCategories.GetAsync(new GenericServiceRequest<ReportCategory>
            {
                Expression = x=>x.Id == CatId,
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

            _serviceManager.ReportCategories.Remove(toDelete);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
