using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Blog.Dto;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/blogs/tag")]
    [ApiController]
    public class BlogTagController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ApiResponse response;
        public BlogTagController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            response = new ApiResponse();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllBlogTag(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _serviceManager.Tags.GetAllAsync(new GenericServiceRequest<Tag>
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
        public async Task<ApiResponse> GetBlogTag(int TagId, CancellationToken cancellationToken)
        {
            try
            {
                if (TagId <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id not found.";
                    return response;
                }
                var data = await _serviceManager.Tags.GetAsync(new GenericServiceRequest<Tag>
                {
                    Expression = x => x.Id == TagId,
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
        public async Task<ApiResponse> CreateBlogTag(CreateTagDto dto, CancellationToken cancellationToken)
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
                if(dto.Slug == null)
                {
                    // Generate slug from title
                    slug = Slug.Generate(dto.Name);

                    // check if slug already exists
                    var existingPost = await _serviceManager.Tags.GetAsync(new GenericServiceRequest<Tag>
                    {
                        Expression = b => b.Slug == slug,
                        NoTracking = true,
                        CancellationToken = cancellationToken
                    });
                    if (existingPost != null)
                    {
                        // Append a number to make it unique
                        int counter = 1;
                        while (await _serviceManager.Tags.AnyAsync(new GenericServiceRequest<Tag>
                        {
                            Expression = b => b.Slug == $"{slug}-{counter}",
                            NoTracking = true,
                            CancellationToken = cancellationToken
                        }))
                        {
                            counter++;
                        }
                        slug = $"{slug}-{counter}";
                    }
                }
                if (dto.Slug != null) 
                {
                 slug = dto.Slug;
                }
                
                var toCreate = new Tag
                {
                    Name = dto.Name,
                    Slug = slug,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                await _serviceManager.Tags.AddAsync(toCreate);
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
        public async Task<ApiResponse> UpdateBlogTag(UpdateTagDto dto, CancellationToken cancellationToken)
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
                var slug = String.Empty;
                var toUpdate = await _serviceManager.Tags.GetAsync(new GenericServiceRequest<Tag>
                {
                    Expression = b => b.Id == dto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (dto.Name != null)
                {
                    if(dto.Slug != null)
                    {
                        slug = dto.Slug;
                    }
                    if(dto.Slug == null)
                    {
                        // Generate slug from title
                        slug = Slug.Generate(dto.Name);

                        // check if slug already exists
                        var existingPost = await _serviceManager.Tags.GetAsync(new GenericServiceRequest<Tag>
                        {
                            Expression = b => b.Slug == slug,
                            NoTracking = true,
                            CancellationToken = cancellationToken
                        });
                        if (existingPost != null)
                        {
                            // Append a number to make it unique
                            int counter = 1;
                            while (await _serviceManager.Tags.AnyAsync(new GenericServiceRequest<Tag>
                            {
                                Expression = b => b.Slug == $"{slug}-{counter}",
                                NoTracking = true,
                                CancellationToken = cancellationToken
                            }))
                            {
                                counter++;
                            }
                            slug = $"{slug}-{counter}";
                        }
                    }
                }

                toUpdate.Name = dto.Name ?? toUpdate.Name;
                toUpdate.Slug = slug;
                
                _serviceManager.Tags.Update(toUpdate);
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
        public async Task<ApiResponse> DeleteBlogTag(int TagId, CancellationToken cancellationToken)
        {
            if(TagId <= 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Id required.";
                return response;
            }

            var toDelete = await _serviceManager.Tags.GetAsync(new GenericServiceRequest<Tag>
            {
                Expression = x=>x.Id == TagId,
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

            _serviceManager.Tags.Remove(toDelete);
            await _serviceManager.Save();

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Deleted Successfully";
            return response;
        }



    }
}   
