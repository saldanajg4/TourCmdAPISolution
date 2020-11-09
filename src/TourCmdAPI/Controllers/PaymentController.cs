using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Dtos;
using System;
using TourCmdAPI.Helpers;
using TourCmdAPI.Services;
using TourCmdAPI.Filter;
using Microsoft.Extensions.Caching.Memory;

namespace TourCmdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentDetailsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUriServices _uriService;
        private readonly IMemoryCache _memoryCache;
        public PaymentController(IPaymentDetailsRepository repo, IMapper mapper, IUriServices uriService, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _uriService = uriService;
            _repo = repo;
            _mapper = mapper;
        }

        // [HttpGet]
        // [RequestHeaderMatchesMediaType("Accept",
        //     new[] { "application/vnd.jose.paymentdetails+json" })]
        // public async Task<IActionResult> GetPaymentDetails()
        // {
        //     var pdCached = "paymentDetailsList";
        //     if(!_memoryCache.TryGetValue(pdCached, out IEnumerable<Dtos.PaymentDetail> paymentDetailDto)){
        //         IEnumerable<Entities.PaymentDetail> paymentEntity = await this._repo.GetPaymentDetails();
        //         paymentDetailDto = this._mapper.Map<IEnumerable<Dtos.PaymentDetail>>(paymentEntity);
        //         var cacheExpiryOptions = new MemoryCacheEntryOptions
        //     {
        //         AbsoluteExpiration = DateTime.Now.AddMinutes(3),
        //         Priority = CacheItemPriority.High,
        //         SlidingExpiration = TimeSpan.FromMilliseconds(8)
        //     };
        //     _memoryCache.Set(pdCached, paymentDetailDto, cacheExpiryOptions);
        //     }
            
        //     return Ok(paymentDetailDto);
        // }

        [HttpGet]
        [RequestHeaderMatchesMediaType("Accept",
            new[] { "application/vnd.jose.paymentdetails+json" })]
        public async Task<IActionResult> GetPaymentDetails() {
            IEnumerable<Entities.PaymentDetail> paymentEntity = await this._repo.GetPaymentDetails();
            var paymentDetailDto = this._mapper.Map<IEnumerable<Dtos.PaymentDetail>>(paymentEntity);
            
            // var route = Request.Path.Value;//this will get the /api/payment
            // var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            // IEnumerable<Entities.PaymentDetail> paymentEntity = await this._repo.GetPaymentDetails();
            // var paymentDetailDto = this._mapper.Map<IEnumerable<Dtos.PaymentDetail>>(paymentEntity);
            // var totalRecords = await this._repo.GetTotalOfPaymentDetails();
            // var pagedResponse = PaginationHelper.CreatePagedReponse<IEnumerable<Dtos.PaymentDetail>>
            //     (paymentDetailDto, validFilter, totalRecords, _uriService, route);
            return Ok(paymentDetailDto);
        }


        [HttpGet("{Id}", Name = "GetPaymentDetailsById")]
        public async Task<IActionResult> GetPaymentDetailsById(int Id)
        {
            var paymentEntity = await _repo.GetPaymentDetailById(Id);
            if (paymentEntity == null)
                return NotFound();
            var paymentDto = _mapper.Map<Dtos.PaymentDetail>(paymentEntity);

            return Ok(paymentDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var payment = await _repo.GetPaymentDetailById(id);
            if (payment == null)
                return NotFound();

            _repo.DeletePaymentDetail(payment);
            if (!await _repo.SaveAsync())
                throw new Exception("Error deleting payment details.");

            return Ok(payment);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, Dtos.PaymentDetail payment)
        {
            if (id != payment.Id)
                return BadRequest();
            var paymentEntity = _mapper.Map<Entities.PaymentDetail>(payment);
            this._repo.PutPaymentDetails(id, paymentEntity);
            if (!await _repo.SaveAsync())
                return BadRequest();
            return Ok();
        }
        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] { "application/vnd.jose.paymentdetailsforcreation+json" })]
        public async Task<IActionResult> PostPaymentDetail(PaymentDetailForCreation payment)
        {
            var paymentEntity = _mapper.Map<Entities.PaymentDetail>(payment);
            if (paymentEntity == null)
                return BadRequest();
            await _repo.PostPaymentDetail(paymentEntity);
            var paymentDto = _mapper.Map<Dtos.PaymentDetail>(paymentEntity);
            if (!await _repo.SaveAsync())
                throw new Exception("Error registering payment");


            return CreatedAtRoute("GetPaymentDetailsById",
                new { Id = paymentDto.Id }, paymentDto);
        }
    }
}