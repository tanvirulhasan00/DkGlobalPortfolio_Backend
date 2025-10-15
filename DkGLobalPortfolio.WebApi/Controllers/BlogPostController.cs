using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Blog.Dto;
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
                var slug = Slug.Generate(createPostDto.Title);

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
                var featureImage = "";
                if (createPostDto.FeaturedImage != null)
                {
                    featureImage = await _serviceManager.File.FileUpload(createPostDto.FeaturedImage, "blogs/post-images");
                }
                var blogPost = new Post
                {
                    Title = createPostDto.Title,
                    Slug = slug,
                    Content = createPostDto.Content,
                    Excerpt = createPostDto.Excerpt,
                    FeaturedImage = featureImage,
                    AuthorId = createPostDto.AuthorId,
                    CategoryId = createPostDto.CategoryId,
                    Status = createPostDto.Status,
                    ReadingTime = createPostDto.ReadingTime,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive = createPostDto.IsActive
                };
                // Set published date if status is published
                if (createPostDto.Status == "published")
                {
                    blogPost.PublishedAt = DateTime.Now;
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
                            AddedAt = DateTime.Now,
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

        [HttpPost]
        [Route("update")]
        public async Task<ApiResponse> UpdateBlogPost(UpdatePostDto updatePostDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if(updatePostDto.Id <= 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Id required";
                    return response;
                }
                var existingPost = await _serviceManager.Posts.GetAsync(new GenericServiceRequest<Post>
                {
                    Expression = b => b.Id == updatePostDto.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                var slug = "";
                if(existingPost.Title != updatePostDto.Title)
                {
                    // Generate slug from title
                     slug = Slug.Generate(updatePostDto.Title);

                    // check if slug already exists
                    var existingPostTo = await _serviceManager.Posts.GetAsync(new GenericServiceRequest<Post>
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
                }

                var featuredImage = "";
                if (updatePostDto.FeaturedImage != null)
                {
                    //delete old images
                    if (!string.IsNullOrEmpty(existingPost.FeaturedImage))
                    {
                        _serviceManager.File.DeleteFile(existingPost.FeaturedImage);
                    }

                    featuredImage = await _serviceManager.File.FileUpload(updatePostDto.FeaturedImage, "blogs/post-images");
                }

                existingPost.Title = updatePostDto.Title ?? existingPost.Title;
                existingPost.Slug = slug ?? existingPost.Slug;
                existingPost.Content = updatePostDto.Content ?? existingPost.Content;
                existingPost.Excerpt = updatePostDto.Excerpt ?? existingPost.Excerpt;
                existingPost.FeaturedImage = featuredImage ?? existingPost.FeaturedImage;
                existingPost.AuthorId = updatePostDto.AuthorId <= 0 ? existingPost.AuthorId : updatePostDto.AuthorId;
                existingPost.CategoryId = updatePostDto.CategoryId <= 0 ? existingPost.CategoryId : updatePostDto.CategoryId;
                existingPost.Status = updatePostDto.Status ?? existingPost.Status;
                existingPost.ReadingTime = updatePostDto.ReadingTime <= 0 ? existingPost.ReadingTime : updatePostDto.ReadingTime;
                existingPost.UpdatedAt = DateTime.Now;   

                _serviceManager.Posts.Update(existingPost);
                await _serviceManager.Save();

                // now handle tags
                if (updatePostDto.TagIds != null)
                {
                    // Remove existing tags for this post
                    var existingTags = await _serviceManager.BlogPostTags.GetAllAsync(new GenericServiceRequest<BlogPostTag>
                    {
                        Expression = t => t.PostId == updatePostDto.Id,
                        NoTracking = false, // we want to track so we can delete
                        CancellationToken = cancellationToken
                    });

                    if (existingTags.Any())
                    {
                        foreach (var oldTag in existingTags)
                        {
                            _serviceManager.BlogPostTags.Remove(oldTag);
                        }
                    }

                    // Add new tags
                    foreach (var tagId in updatePostDto.TagIds)
                    {
                        var blogPostTag = new BlogPostTag
                        {
                            PostId = updatePostDto.Id,
                            TagId = tagId,
                            AddedAt = DateTime.Now,
                            AddedBy = updatePostDto.AuthorId
                        };

                        await _serviceManager.BlogPostTags.AddAsync(blogPostTag);
                    }

                    await _serviceManager.Save();
                }


                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Updated Successfully";
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

        //Delete API (Soft Delete)
        [HttpPost]
        [Route("soft-delete")]
        public async Task<ApiResponse> SoftDeleteBlogPost(DeletePostDto deletePostDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (deletePostDto == null || deletePostDto.Ids == null || !deletePostDto.Ids.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "At least one Id is required";
                    return response;
                }

                foreach (var id in deletePostDto.Ids)
                {
                    var post = await _serviceManager.Posts.GetAsync(new GenericServiceRequest<Post>
                    {
                        Expression = b => b.Id == id && !b.IsDeleted,
                        NoTracking = false,
                        CancellationToken = cancellationToken
                    });

                    if (post != null)
                    {
                        post.IsDeleted = true;
                        post.DeletedAt = DateTime.Now;
                        post.DeletedBy = deletePostDto.DeletedBy;

                        _serviceManager.Posts.Update(post);
                    }
                }

                await _serviceManager.Save();

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Post(s) soft deleted successfully";
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

        //Delete API (Hard Delete)
        [HttpPost]
        [Route("delete")]
        public async Task<ApiResponse> DeleteBlogPost(DeletePostDto deletePostDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (deletePostDto == null || deletePostDto.Ids == null || !deletePostDto.Ids.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "At least one Id is required";
                    return response;
                }

                foreach (var id in deletePostDto.Ids)
                {
                    var post = await _serviceManager.Posts.GetAsync(new GenericServiceRequest<Post>
                    {
                        Expression = b => b.Id == id,
                        NoTracking = false,
                        CancellationToken = cancellationToken
                    });

                    if (post != null)
                    {
                        // Delete related tags first
                        var existingTags = await _serviceManager.BlogPostTags.GetAllAsync(new GenericServiceRequest<BlogPostTag>
                        {
                            Expression = t => t.PostId == id,
                            NoTracking = false,
                            CancellationToken = cancellationToken
                        });

                        if (existingTags.Any())
                        {
                            foreach (var tag in existingTags)
                            {
                                _serviceManager.BlogPostTags.Remove(tag);
                            }
                        }

                        //delete old images
                        if (!string.IsNullOrEmpty(post.FeaturedImage))
                        {
                            _serviceManager.File.DeleteFile(post.FeaturedImage);
                        }

                        // Delete the post itself
                        _serviceManager.Posts.Remove(post);
                    }
                }

                await _serviceManager.Save();

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Post(s) deleted successfully";
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

        //Restore API
        [HttpPost]
        [Route("restore")]
        public async Task<ApiResponse> RestoreBlogPost(DeletePostDto restorePostDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (restorePostDto == null || restorePostDto.Ids == null || !restorePostDto.Ids.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "At least one Id is required";
                    return response;
                }

                foreach (var id in restorePostDto.Ids)
                {
                    var post = await _serviceManager.Posts.GetAsync(new GenericServiceRequest<Post>
                    {
                        Expression = b => b.Id == id && b.IsDeleted, // only restore deleted ones
                        NoTracking = false,
                        CancellationToken = cancellationToken
                    });

                    if (post != null)
                    {
                        post.IsDeleted = false;
                        post.DeletedAt = null;
                        post.DeletedBy = null;
                        post.UpdatedAt = DateTime.Now;

                        _serviceManager.Posts.Update(post);
                    }
                }

                await _serviceManager.Save();

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Post(s) restored successfully";
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

    }

}
