﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Create;

namespace PhotoContest.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsApiController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewsApiController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        /// <summary>
        /// Create a review.
        /// </summary>
        /// <param name="newReviewDTO">Details of new review.</param>
        /// <returns>Returns created review.</returns>
        [Authorize(Roles = "Organizer, User")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(NewReviewDTO newReviewDTO)
        {
            try
            {
                var result = await this.reviewService.CreateAsync(newReviewDTO);
                return Created("post",result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get review for photo by Id.
        /// </summary>
        /// <param name="id">Id of photo to search for.</param>
        /// <returns>Returns review for photo.</returns>
        [Authorize(Roles ="Organizer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetForPhotoAsync(Guid id)
        {
            try
            {
                var result = await this.reviewService.GetForPhotoAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get review for user.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>Returns reviews for user.</returns>
        [Authorize]
        [HttpGet("username")]
        public async Task<IActionResult> GetForUserAsync(string username)
        {
            try
            {
                var result = await this.reviewService.GetForUserAsync(username);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}