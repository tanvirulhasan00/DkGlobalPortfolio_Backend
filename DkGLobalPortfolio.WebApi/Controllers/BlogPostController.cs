using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/blogs/posts")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BlogPostController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllBlogPost(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Post>
                {
                    Expression = null,
                    IncludeProperties = "Author,Category,BlogPostTags",
                    OrderType = OrderTypeClass.OrderType.Descending,
                    OrderExpression = b=>b.PublishedAt,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await _serviceManager.Posts.GetAllAsync(genericReq);
                if (!data.Any())
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data not found";
                    return response;
                }

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = data;
                return response;
            }catch(TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<ApiResponse> GetBlogPostById(int BlogId,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if(BlogId <= 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Blog id required";
                    return response;
                }
                var genericReq = new GenericServiceRequest<Post>
                {
                    Expression = x=>x.Id == BlogId,
                    IncludeProperties = "Author,Category,BlogPostTags",
                    OrderType = null,
                    OrderExpression = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await _serviceManager.Posts.GetAsync(genericReq);
                if (data == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data not found";
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
        [Route("get-by-slug")]
        public async Task<ApiResponse> GetBlogPostBySlug(string slug, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (string.IsNullOrEmpty(slug))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Blog slug string required";
                    return response;
                }
                var genericReq = new GenericServiceRequest<Post>
                {
                    Expression = x => x.Slug == slug,
                    IncludeProperties = "Author,Category,BlogPostTags",
                    OrderType = null,
                    OrderExpression = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await _serviceManager.Posts.GetAsync(genericReq);
                if (data == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data not found";
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
        [Route("get-by-author")]
        public async Task<ApiResponse> GetBlogPostByAuthor(int AuthorId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (AuthorId <= 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Blog author id required";
                    return response;
                }
                var genericReq = new GenericServiceRequest<Post>
                {
                    Expression = x => x.AuthorId == AuthorId,
                    IncludeProperties = "Author,Category,BlogPostTags",
                    OrderType = null,
                    OrderExpression = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await _serviceManager.Posts.GetAsync(genericReq);
                if (data == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data not found";
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
        [Route("get-by-category")]
        public async Task<ApiResponse> GetBlogPostByCategory(int CategoryId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (CategoryId <= 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Blog category id required";
                    return response;
                }
                var genericReq = new GenericServiceRequest<Post>
                {
                    Expression = x => x.CategoryId == CategoryId,
                    IncludeProperties = "Author,Category,BlogPostTags",
                    OrderType = null,
                    OrderExpression = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await _serviceManager.Posts.GetAsync(genericReq);
                if (data == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data not found";
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
        [Route("get-by-tag")]
        public async Task<ApiResponse> GetBlogPostByTag(int TagId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (TagId <= 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Blog tag id required";
                    return response;
                }
                var genericReq = new GenericServiceRequest<Post>
                {
                    Expression = x => x.BlogPostTags.Any(t=>t.TagId == TagId),
                    IncludeProperties = "Author,Category,BlogPostTags",
                    OrderType = OrderTypeClass.OrderType.Descending,
                    OrderExpression = b=>b.PublishedAt,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await _serviceManager.Posts.GetAsync(genericReq);
                if (data == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data not found";
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
        public async Task<ApiResponse> CreateBlogPost(CreatePostDto createPostDto,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {

                // Generate slug from title
                var slug = GenerateSlug(createPostDto.Title);

                // check if slug already exists
                var existingPost = await _serviceManager.Posts.GetAsync(new GenericServiceRequest<Post>
                {
                    Expression = b => b.Slug == slug,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (existingPost != null)
                {
                    // Append a number to make it unique
                    int counter = 1;
                    while (await _serviceManager.Posts.AnyAsync(new GenericServiceRequest<Post>
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
                var blogPost = new Post
                {
                    Title = createPostDto.Title,
                    Slug = slug,
                    Content = createPostDto.Content,
                    Excerpt = createPostDto.Excerpt,
                    FeaturedImage = createPostDto.FeaturedImage,
                    AuthorId = createPostDto.AuthorId,
                    CategoryId = createPostDto.CategoryId,
                    Status = createPostDto.Status,
                    ReadingTime = createPostDto.ReadingTime,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = createPostDto.IsActive
                };
                // Set published date if status is published
                if (createPostDto.Status == "published")
                {
                    blogPost.PublishedAt = DateTime.UtcNow;
                }

                await _serviceManager.Posts.AddAsync(blogPost);
                await _serviceManager.Save();

                //now handle tags
                if (createPostDto.TagIds != null && createPostDto.TagIds.Any()) 
                {
                    foreach (var tagId in createPostDto.TagIds)
                    {
                        var blogPostTag = new BlogPostTag
                        {
                            PostId = blogPost.Id,  // newly created post Id
                            TagId = tagId,
                            AddedAt = DateTime.UtcNow,
                            AddedBy = createPostDto.AuthorId
                        };

                        await _serviceManager.BlogPostTags.AddAsync(blogPostTag);
                    }

                    await _serviceManager.Save();
                }

                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Created Successfully";
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


        private static string GenerateSlug(string phrase)
        {
            string str = phrase.ToLowerInvariant();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // remove invalid chars
            str = Regex.Replace(str, @"\s+", "-").Trim('-'); // convert spaces to hyphens
            str = Regex.Replace(str, @"-+", "-"); // collapse multiple hyphens
            return str;
        }
    }

}
