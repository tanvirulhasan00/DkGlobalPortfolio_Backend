
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Models.Profile.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/company-info")]
    [ApiController]
    public class CompanyInfoController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public CompanyInfoController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllCompanyInfo(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.CompanyInfos.GetAllAsync(new GenericServiceRequest<CompanyInfo>
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
        public async Task<ApiResponse> GetCompanyInfo(int Id, CancellationToken cancellationToken)
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
                var data = await _serviceManager.CompanyInfos.GetAsync(new GenericServiceRequest<CompanyInfo>
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
        public async Task<ApiResponse> CreateCompanyInfo(CreateCompanyInfoDto dto, CancellationToken cancellationToken)
        {
            try
            {
               
                if (dto == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Empty request data";
                    return response;
                }
                
                var toCreate = new CompanyInfo
                {
                    Name = dto.Name,
                    Quote = dto.Quote,
                    ShortTitle = dto.ShortTitle,
                    Description = dto.Description,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Location = dto.Location,
                    MapLink = dto.MapLink,
                    SecondMapLink = dto.SecondMapLink,
                    FacebookLink = dto.FacebookLink,
                    YoutubeLink = dto.YoutubeLink,
                    LinkedInLink = dto.LinkedInLink,
                    InstagramLink = dto.InstagramLink,
                    TwitterLink = dto.TwitterLink,
                    Mission = dto.Mission,
                    Vision = dto.Vision,
                    AnnualTurnover = dto.AnnualTurnover,
                    NumberOfEmployees = dto.NumberOfEmployees,
                    NumberOfSewingPlants = dto.NumberOfSewingPlants,
                    ProductionCapacity = dto.ProductionCapacity,
                    PrimaryMarkets = dto.PrimaryMarkets,
                    
                    
                };
                await _serviceManager.CompanyInfos.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateCompanyInfo(UpdateCompanyInfoDto dto, CancellationToken cancellationToken)
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
                var toUpdate = await _serviceManager.CompanyInfos.GetAsync(new GenericServiceRequest<CompanyInfo>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });

                toUpdate.Name = dto.Name ?? toUpdate.Name;
                toUpdate.Quote = dto.Quote ?? toUpdate.Quote;
                toUpdate.ShortTitle = dto.ShortTitle ?? toUpdate.ShortTitle;
                toUpdate.Description = dto.Description ?? toUpdate.Description;
                toUpdate.Email = dto.Email ?? toUpdate.Email;
                toUpdate.PhoneNumber = dto.PhoneNumber ?? toUpdate.PhoneNumber;
                toUpdate.Location = dto.Location ?? toUpdate.Location;
                toUpdate.MapLink = dto.MapLink ?? toUpdate.MapLink;
                toUpdate.SecondMapLink = dto.SecondMapLink ?? toUpdate.SecondMapLink;
                toUpdate.FacebookLink = dto.FacebookLink ?? toUpdate.FacebookLink;
                toUpdate.YoutubeLink = dto.YoutubeLink ?? toUpdate.YoutubeLink;
                toUpdate.LinkedInLink = dto.LinkedInLink ?? toUpdate.LinkedInLink;
                toUpdate.InstagramLink = dto.InstagramLink ?? toUpdate.InstagramLink;
                toUpdate.TwitterLink = dto.TwitterLink ?? toUpdate.TwitterLink;
                toUpdate.Mission = dto.Mission ?? toUpdate.Mission;
                toUpdate.Vision = dto.Vision ?? toUpdate.Vision;
                toUpdate.AnnualTurnover = dto.AnnualTurnover <= 0 ? toUpdate.AnnualTurnover : dto.AnnualTurnover;
                toUpdate.NumberOfEmployees = dto.NumberOfEmployees <= 0 ? toUpdate.NumberOfEmployees : dto.NumberOfEmployees;
                toUpdate.NumberOfSewingPlants = dto.NumberOfSewingPlants <= 0 ? toUpdate.NumberOfSewingPlants : dto.NumberOfSewingPlants;
                toUpdate.ProductionCapacity = dto.ProductionCapacity <= 0 ? toUpdate.ProductionCapacity : dto.ProductionCapacity;
                toUpdate.PrimaryMarkets = dto.PrimaryMarkets ?? toUpdate.PrimaryMarkets;
               
                
                _serviceManager.CompanyInfos.Update(toUpdate);
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
        public async Task<ApiResponse> DeleteCompanyInfo(int Id, CancellationToken cancellationToken)
        {
            if(Id <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDelete = await _serviceManager.CompanyInfos.GetAsync(new GenericServiceRequest<CompanyInfo>
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

            _serviceManager.CompanyInfos.Remove(toDelete);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
